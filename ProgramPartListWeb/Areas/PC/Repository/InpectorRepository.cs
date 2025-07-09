using Newtonsoft.Json;
using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Repository
{
    public class InpectorRepository : IInspector
    {
        // -------------  Employees Or Users Data -------------------
        public Task<List<Employee>> GetEmployee() => SqlDataAccess.GetData<Employee>("EmployeeDataList", null, "Employee");
        
        public async Task<int> GetEmployeeByDepartment(string employee)
        {
            var data = await GetEmployee();
            var employeedata = data.SingleOrDefault(e => e.EmployeeID == employee);
            return (employeedata != null) ? employeedata.Department_ID : 0;
        }

        // -------------  DashBoard Schedule Inpectors --------------
        public Task<List<PatrolSchedule>> GetScheduleDate() => SqlDataAccess.GetData<PatrolSchedule>("GetScheduleDate", null);

        // -------------  Inspector Management ----------------------
        public Task<List<InspectorModel>> GetInpectorsData() => SqlDataAccess.GetData<InspectorModel>("Getinpectors", null, "Inspectors");
        public Task<bool> AddEditInpectors(object paramaters, int mode)
        {
            string strsql = (mode == 0) 
                       ?"INSERT INTO Patrol_Inspectors(Employee_ID, DateQualified, OJTRegistration, Remarks) " +
                        "VALUES(@Employee_ID, @DateQualified, @OJTRegistration, @Remarks)" 
                       :"UPDATE Patrol_Inspectors SET Employee_ID =@Employee_ID,  DateQualified = @DateQualified, OJTRegistration =@OJTRegistration, Remarks =@Remarks " +
                         "WHERE InspectID =@InspectID";
           
            return  SqlDataAccess.UpdateInsertQuery(strsql, paramaters, "Inspectors");
        }
        public Task<bool> ApproveAndDisapproveInpectors(int inspectID, int status)
        {
            string strsql = "UPDATE Patrol_Inspectors SET Approval =@Approval " +
                            "WHERE InspectID =@InspectID";
            return  SqlDataAccess.UpdateInsertQuery(strsql, new { InspectID = inspectID, Approval = status }, "Inspectors");
        }

        // -------------  Registration Management ----------------------
        public  Task<List<PatrolRegistionModel>> GetRegistrationData() => SqlDataAccess.GetData<PatrolRegistionModel>("GetPatrolRegistration", null, "Registration");
        public  Task<List<FindingModel>> GetPatrolFindings(string reg) => SqlDataAccess.GetData<FindingModel>("GetFindings", new { Regno = reg });
        public async Task<bool> AddRegistration(object paramaters, string json)
        {
            //INSERT MAIN REGISTRATION PROCESS
            bool result = await SqlDataAccess.UpdateInsertQuery("InsertRegistration", paramaters);

            // INSERT FINDING AND COUNTERMEASURE PROCESS
            // Deserialize findings
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
                await SqlDataAccess.UpdateInsertQuery("InsertFindings", findparams, "Registration");
            }

            return result;
        }
        public async Task<bool> EditRegistration(object paramaters, string json)
        {
            // INSERT MAIN REGISTRATION PROCESS
            bool result = await SqlDataAccess.UpdateInsertQuery("UpdateRegistration", paramaters);

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
                await SqlDataAccess.UpdateInsertQuery("UpdateFindings", findparams);
            }

            return result;
        }
        public async Task<bool> DeleteRegistration(string RegNo)
        {
            string strsql = "DELETE FROM Patrol_Findngs WHERE RegNo = @RegNo";
            string strsql2 = "DELETE FROM Patrol_Registration WHERE RegNo = @RegNo";
            var parameter = new { RegNo = RegNo };

            var task1 = SqlDataAccess.UpdateInsertQuery(strsql, parameter, "Registration");
            var task2 = SqlDataAccess.UpdateInsertQuery(strsql2, parameter);

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
            return  SqlDataAccess.GetData<CalendarSched>(strsql, new { Employee_ID = Employee_ID });
        }
        public Task<List<CalendarSched>> GetScheduleDateByMonth()
        {
            string strsql = $@"SELECT s.ScheduleID, s.Employee_ID, e.FullName,
                                     p.ProcessID, p.ProcessName, s.ScheduleDate, s.IsActive 
                            FROM Patrol_Schedule s 
                            INNER JOIN Patrol_Process p ON s.ProcessID = p.ProcessID  
                            INNER JOIN Employee_tbl e ON e.Employee_ID = s.Employee_ID
                            WHERE  ScheduleDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                            AND ScheduleDate < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) 
                            AND s.IsActive = 1";
            return SqlDataAccess.GetData<CalendarSched>(strsql, null);
        }
        public Task<List<ProccessModel>> GetProcessData(int depid)
        {
            string sql = "SELECT ProcessID, ProcessName, DepartmentID FROM Patrol_Process WHERE DepartmentID =@DepartmentID";
            return  SqlDataAccess.GetData<ProccessModel>(sql, new { DepartmentID = depid });
        }

        public Task<bool> SetScheduleCalendar(object paramaters, int mode)
        {
            string strsql = (mode == 0) 
                         ? "INSERT INTO Patrol_Schedule(Employee_ID, ProcessID, ScheduleDate, TrainerID) " +
                               "VALUES(@Employee_ID, @ProcessID, @ScheduleDate, @TrainerID)"
                         : "UPDATE Patrol_Schedule SET   ProcessID =@ProcessID" +
                         "WHERE ScheduleID =@ScheduleID";
            return SqlDataAccess.UpdateInsertQuery(strsql, paramaters);   
        }
        public  Task<bool> RemoveScheduleCalendar(int ID)
        {
           string strsql = "UPDATE Patrol_Schedule SET   IsActive =@IsActive " +
                         "WHERE ScheduleID =@ScheduleID";
           return SqlDataAccess.UpdateInsertQuery(strsql, new { IsActive = 0, ScheduleID = ID });
        }

        
    }
}