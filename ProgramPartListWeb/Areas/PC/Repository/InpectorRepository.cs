using Newtonsoft.Json;
using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Repository
{
    public class InpectorRepository : IInspector
    {
        // -------------  Employees Or Users Data -------------------
        public Task<List<Employee>> GetEmployee() => SqlDataAccess.GetDataAsync<Employee>("EmployeeDataList", null, System.Data.CommandType.StoredProcedure, "Employee");
        
        public async Task<int> GetEmployeeByDepartment(string employee)
        {
            var data = await GetEmployee();
            var employeedata = data.SingleOrDefault(e => e.EmployeeID == employee);
            return (employeedata != null) ? employeedata.Department_ID : 0;
        }

        public Task<List<UsersModel>> GetUsersInfoSetting()
        {
            string strsql = $@"SELECT ua.User_ID, u.Employee_ID, u.Fullname, u.Signature
                            FROM UserAccounts ua
                            INNER JOIN Users u ON u.User_ID = ua.User_ID
                            INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                            WHERE ua.IsActive = 1 AND (p.Project_ID IN (1, 9))";
            return SqlDataAccess.GetDataAsync<UsersModel>(strsql);
        }


        // -------------  DashBoard Schedule Inpectors --------------
        public Task<List<PatrolSchedule>> GetScheduleDate() => SqlDataAccess.GetDataAsync<PatrolSchedule>("GetScheduleDate", null);

        // -------------  Inspector Management ----------------------
        public Task<List<InspectorModel>> GetInpectorsData() => SqlDataAccess.GetDataAsync<InspectorModel>("Getinpectors", null, System.Data.CommandType.StoredProcedure, "Inspectors");
        public Task<bool> AddEditInpectors(object paramaters, int mode)
        {
            string strsql = (mode == 0) 
                       ?"INSERT INTO Patrol_Inspectors(Employee_ID, DateQualified, OJTRegistration, Remarks) " +
                        "VALUES(@Employee_ID, @DateQualified, @OJTRegistration, @Remarks)" 
                       :"UPDATE Patrol_Inspectors SET Employee_ID =@Employee_ID,  DateQualified = @DateQualified, OJTRegistration =@OJTRegistration, Remarks =@Remarks " +
                         "WHERE InspectID =@InspectID";
           
            return  SqlDataAccess.ExecuteAsync(strsql, paramaters, System.Data.CommandType.Text,"Inspectors");
        }
        public Task<bool> ApproveAndDisapproveInpectors(int inspectID, int status)
        {
            string strsql = "UPDATE Patrol_Inspectors SET Approval =@Approval " +
                            "WHERE InspectID =@InspectID";
            return  SqlDataAccess.ExecuteAsync(strsql, new { InspectID = inspectID, Approval = status }, System.Data.CommandType.Text, "Inspectors");
        }

        // -------------  Registration Management ----------------------
        public  Task<List<PatrolRegistionModel>> GetRegistrationData() => SqlDataAccess.GetDataAsync<PatrolRegistionModel>("GetPatrolRegistration", null, System.Data.CommandType.StoredProcedure, "Registration");
        public  Task<List<FindingModel>> GetPatrolFindings(string reg) => SqlDataAccess.GetDataAsync<FindingModel>("GetFindings", new { Regno = reg }, System.Data.CommandType.StoredProcedure);
        public async Task<bool> AddRegistration(RegistrationModel reg, string json)
        {
            //INSERT MAIN REGISTRATION PROCESS
            bool result = await SqlDataAccess.ExecuteAsync("InsertRegistration",
                          new
                          {
                              RegNo = reg.RegNo,
                              DateConduct = reg.DateConduct,
                              Employee_ID = "24050006",
                              PIC = reg.PIC,
                              PIC_Comments = reg.PIC_Comments,
                              Manager = reg.Manager,
                              Manager_Comments = reg.Manager_Comments,
                              IsSigned = reg.IsSigned
                          }, System.Data.CommandType.StoredProcedure);

            // INSERT FILES TO THE OTHER TABLES
            await SqlDataAccess.ExecuteAsync("InserFiles", 
                new { RegNo = reg.RegNo, FilePath = reg.FilePath, PatrolPath = reg.PatrolPath }, System.Data.CommandType.StoredProcedure);

            // INSERT FINDING AND COUNTERMEASURE PROCESS
            // Make a Json format 
            var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);

            foreach (var f in findings)
            {
                var findparams = new
                {
                    RegNo = "P1SA-" + f.RegNo,
                    FindID = f.FindID,
                    FindDescription = f.FindDescription,
                    Countermeasure = f.Countermeasure
                };
                await SqlDataAccess.ExecuteAsync("InsertFindings", findparams, System.Data.CommandType.StoredProcedure, "Registration");
            }

            return result;
        }
        public async Task<bool> EditRegistration(RegistrationModel reg, string json)
        {
            // INSERT MAIN REGISTRATION PROCESS
            var regMain =  SqlDataAccess.ExecuteAsync("UpdateRegistration", new
            {
                RegNo = reg.RegNo,
                DateConduct = reg.DateConduct,
                Employee_ID = reg.Employee_ID,
                PIC = reg.PIC,
                PIC_Comments = reg.PIC_Comments,
                Manager = reg.Manager,
                Manager_Comments = reg.Manager_Comments
            }, System.Data.CommandType.StoredProcedure);

            var regFiles =  SqlDataAccess.ExecuteAsync("EditPatrolFiles", new
            {
                FilePath = reg.FilePath,
                PatrolPath = reg.PatrolPath,
                RegNo = reg.RegNo
            }, System.Data.CommandType.Text);

       
            bool[] results = await Task.WhenAll(regMain, regFiles);



            // INSERT FINDING AND COUNTERMEASURE PROCESS
            // Deserialize findings
            var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);

            foreach (var f in findings)
            {
                var findparams = new
                {
                    RegNo = f.RegNo,
                    FindID = f.FindID,
                    FindDescription = f.FindDescription,
                    Countermeasure = f.Countermeasure
                };
                await SqlDataAccess.ExecuteAsync("UpdateFindings", findparams, System.Data.CommandType.StoredProcedure, "Registration");
            }

            return results.All(r => r);
        }
        public async Task<bool> DeleteRegistration(string RegNo)
        {
            string strsql = "DELETE FROM Patrol_Findngs WHERE RegNo = @RegNo";
            string strsql2 = "DELETE FROM Patrol_Registration WHERE RegNo = @RegNo";
            var parameter = new { RegNo = RegNo };

            var task1 = SqlDataAccess.ExecuteAsync(strsql, parameter, System.Data.CommandType.Text, "Registration");
            var task2 = SqlDataAccess.ExecuteAsync(strsql2, parameter);

            await Task.WhenAll(task1, task2);

            return true; // Success
        }



        // -------------  Set Schedule Inpectors ------------------------
        public  Task<List<CalendarSched>> GetCalendarData(string Employee_ID)
        {
            string strsql = $@"SELECT s.ScheduleID, s.Employee_ID, e.FullName,
                                    p.ProcessID, p.ProcessName, s.ScheduleDate, s.IsActive  
                            FROM Patrol_Schedule s 
                            INNER JOIN Patrol_Process p ON s.ProcessID = p.ProcessID
                            INNER JOIN Employee_tbl e ON e.Employee_ID = s.Employee_ID
                            WHERE (s.Employee_ID IS NULL OR s.Employee_ID = @Employee_ID) AND s.IsActive = 1";
            return  SqlDataAccess.GetDataAsync<CalendarSched>(strsql, new { Employee_ID = Employee_ID });
        }
        public Task<List<CalendarSched>> GetScheduleDateByMonth()
        {
            string strsql = $@"SELECT s.ScheduleID, s.Employee_ID, e.FullName,
                                     p.ProcessID, p.ProcessName, s.ScheduleDate, s.IsActive,
                                     e.Department_ID
                            FROM Patrol_Schedule s 
                            INNER JOIN Patrol_Process p ON s.ProcessID = p.ProcessID  
                            INNER JOIN Employee_tbl e ON e.Employee_ID = s.Employee_ID
                            WHERE  ScheduleDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                            AND ScheduleDate < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) 
                            AND s.IsActive = 1";
            return SqlDataAccess.GetDataAsync<CalendarSched>(strsql, null);
        }
        public Task<List<ProccessModel>> GetProcessData(int depid)
        {
            string sql = "SELECT ProcessID, ProcessName, DepartmentID FROM Patrol_Process WHERE DepartmentID =@DepartmentID";
            return  SqlDataAccess.GetDataAsync<ProccessModel>(sql, new { DepartmentID = depid });
        }

        public Task<bool> SetScheduleCalendar(object paramaters, int mode)
        {
            string strsql = (mode == 0) 
                         ? "INSERT INTO Patrol_Schedule(Employee_ID, ProcessID, ScheduleDate, TrainerID) " +
                               "VALUES(@Employee_ID, @ProcessID, @ScheduleDate, @TrainerID)"
                         : "UPDATE Patrol_Schedule SET   ProcessID =@ProcessID" +
                         "WHERE ScheduleID =@ScheduleID";
            return SqlDataAccess.ExecuteAsync(strsql, paramaters);   
        }
        public  Task<bool> RemoveScheduleCalendar(int ID)
        {
           string strsql = "UPDATE Patrol_Schedule SET   IsActive =@IsActive " +
                         "WHERE ScheduleID =@ScheduleID";
           return SqlDataAccess.ExecuteAsync(strsql, new { IsActive = 0, ScheduleID = ID });
        }

        public Task<List<EmailRecepients>> GetEmailsList()
        {
            return SqlDataAccess.GetDataAsync<EmailRecepients>($@"SELECT 
                        FirstName, LastName, Email, Local 
                        FROM P1SA_Emails 
                        ORDER BY LastName DESC");
        }
    }
}