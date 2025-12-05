using Newtonsoft.Json;
using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Repository
{
    public class RegistrationRepository : IRegistration
    {
     
        public Task<List<PatrolRegistrationViewModel>> GetRegistrationData(string prefix)
        {
            string strsql = $@"SELECT 
                                r.RegNo,
                                d.SectionName, 
                                FORMAT(r.DateConduct, 'MM/dd/yyyy') as DateConduct, 
                                r.ReportStatus,
	                            f.Filepath, 
                                f.CounterPath,

	                             a.Inspect_ID, 
                                 Emp.FullName AS InspectName,
	                             a.Inspect_Comments, 
	                             a.Inspect_IsAproved, 
	                             a.Inspect_IsSent,

	                            a.PIC_ID, 
								PIC.FullName AS PICName, 
								a.PIC_Comments, 
								a.PIC_IsSent, 

	                            a.Manager_ID, 
								man.FullName as ManagerName,  
								a.Manager_Comments, 
								a.Manager_Isedit, 
								a.Manager_IsSent, 

	                            a.DepManager_ID, 
								dep.FullName as DepartName,
								a.DepManager_IsAproved, 
								a.DepManager_IsSent, 

	                            a.DivManager_ID,  
								div.FullName as DivName,
								a.DivManager_IsAproved, 
								a.DivManager_IsSent

                            FROM Patrol_Registration r
                            INNER JOIN P1SADepartment_tbl d ON d.DepartmentID = r.Department_ID
                            INNER JOIN Patrol_Registration_Files f ON f.RegNo = r.RegNo
                            INNER JOIN Patrol_Registration_Approvelist a ON a.RegNo = r.RegNo
                            LEFT JOIN Patrol_UserEmail Emp 
								ON r.Employee_ID = Emp.Employee_ID 
							LEFT JOIN Patrol_UserEmail PIC 
								ON r.PIC_ID = PIC.Employee_ID
							LEFT JOIN Patrol_UserEmail man 
								ON r.Manager_ID = man.Employee_ID 
							LEFT JOIN Patrol_UserEmail dep 
								ON r.DepManager_ID = dep.Employee_ID 
							LEFT JOIN Patrol_UserEmail div 
								ON r.DivManager_ID = div.Employee_ID 
                            WHERE r.RegNo LIKE '%{prefix}%'      
                            ORDER BY DateCreated DESC";
            return SqlDataAccess.GetData<PatrolRegistrationViewModel>(strsql);
        }
        public Task<List<EmailModelV2>> PatrolEmailData()
        {
            string strsql = $@"SELECT Employee_ID, FullName, Email, Position, Department_ID, DepPrefix, Signature
                               FROM Patrol_UserEmail";
            return SqlDataAccess.GetData<EmailModelV2>(strsql);
        }


        public Task<EmailModelV2> GetEmployeeEmailDetails(string emp, int pos, string pre)
        {
            string strsql = $@"SELECT FullName, Email, Position, Department_ID, Signature
                               FROM Patrol_UserEmail 
                               WHERE Employee_ID =@Employee_ID AND Position =@Position AND DepPrefix =@DepPrefix";
            return SqlDataAccess.GetObjectOnly<EmailModelV2>(strsql, 
                new { 
                    Employee_ID  = emp,
                    Position = pos,
                    DepPrefix = pre
                });
        }


        public Task<List<FindingModel>> GetRegisterFindings(string regNo) => SqlDataAccess.GetData<FindingModel>("GetFindings", new { Regno = regNo });

        public Task<List<RegistrationFiles>> GetRegisterFiles(string regNo)
        {
             return SqlDataAccess.GetData<RegistrationFiles>($@"SELECT RegNo, FilePath, CounterPath FROM Patrol_Registration_Files WHERE RegNo =@RegNo ", new { RegNo = regNo });
        }

        public async Task<bool> AddRegistration(AddFormRegistrationModel reg, string json)
        {
            //INSERT MAIN REGISTRATION PROCESS
            bool result = await SqlDataAccess.UpdateInsertQuery("InsertRegistration", new
            {
                RegNo = reg.RegNo,
                Department_ID = reg.Department_ID,
                PIC_ID = reg.PIC_ID,
                Employee_ID = reg.Employee_ID
            });

            // If main insert failed → stop the whole process
            if (!result) return false;

            // ========== 2. INSERT FILES ==========
            await SqlDataAccess.UpdateInsertQuery("InserFiles", new
            {
                RegNo = reg.RegNo,
                FilePath = reg.FilePath
            });

            // ========== 3. INSERT APPROVAL LIST ==========
            await SqlDataAccess.UpdateInsertQuery(@"
                INSERT INTO Patrol_Registration_Approvelist
                (RegNo, PIC_ID, Inspect_ID, Inspect_IsAproved, Inspect_IsSent) 
                VALUES(@RegNo, @PIC_ID, @Inspect_ID, 1, 1)",
            new
            {
                RegNo = reg.RegNo,
                PIC_ID = reg.PIC_ID,
                Inspect_ID = reg.Employee_ID
            });

            // ========== 4. INSERT FINDINGS ==========
            var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);

            if (findings != null && findings.Count > 0)
            {
                foreach (var f in findings)
                {
                    await SqlDataAccess.UpdateInsertQuery("InsertFindings", new
                    {
                        RegNo = "P1SA-" + f.RegNo,
                        FindID = f.FindID,
                        FindDescription = f.FindDescription
                    }, "Registration");
                }
            }


            return true;
        }

        public async Task<bool> EditReg_ProcessOwner(ProcessOwnerForms reg, string json)
        {
            // UPDATE THE MAIN REGISTRATION TABLE
            string mainsql = $@"UPDATE Patrol_Registration
                                SET ReportStatus = 3, Manager_ID =@Manager_ID,  DepManager_ID =@DepManager_ID
                                WHERE RegNo = @RegNo";

            var regMain = SqlDataAccess.UpdateInsertQuery(mainsql, new 
            { 
                RegNo = reg.RegNo,
                Manager_ID = reg.DepManager_ID,
                DepManager_ID = reg.DepManager_ID
            });


            string appsql = $@"UPDATE Patrol_Registration_Approvelist
                                SET PIC_Comments = @PIC_Comments, Manager_ID =@Manager_ID,  DepManager_ID =@DepManager_ID, 
                                PIC_IsSent = 1
                                WHERE RegNo = @RegNo";

            var regApp = SqlDataAccess.UpdateInsertQuery(appsql, new
            {
                RegNo = reg.RegNo,
                PIC_Comments = reg.PIC_Comments,
                Manager_ID = reg.DepManager_ID,
                DepManager_ID = reg.DepManager_ID
            });

            // UPDATE THE UPLOADED FILES
            var regFiles = SqlDataAccess.UpdateInsertQuery("EditPatrolFilesRegister", new
            {
                FilePath = reg.Filepath,
                CounterPath = reg.CounterPath,
                RegNo = reg.RegNo
            });

            bool[] results = await Task.WhenAll(regMain, regApp, regFiles);

            // INSERT FINDING AND COUNTERMEASURE PROCESS
            // Deserialize findings
            var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);

            foreach (var f in findings)
            {
                var findparams = new
                {
                    RegNo = f.RegNo,
                    FindID = f.FindID,
                    Countermeasure = f.Countermeasure
                };
                await SqlDataAccess.UpdateInsertQuery(@" UPDATE Patrol_Findngs SET  
                            Countermeasure =@Countermeasure
                            WHERE RegNo =@RegNo AND FindID =@FindID", findparams);
            }

            return results.All(r => r);
        }

        public async Task<bool> ApproveByInspector(string reg, string datecon, string newfilepath, string ManagerID)
        {
            var regsql = SqlDataAccess.UpdateInsertQuery(@" UPDATE Patrol_Registration SET  
                            ReportStatus = 3, DateConduct = @DateConduct, Manager_ID = @Manager_ID, DepManager_ID =@DepManager_ID
                            WHERE RegNo =@RegNo", new { RegNo = reg, DateConduct = datecon, Manager_ID = ManagerID, DepManager_ID = ManagerID });

            var revsql = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Approvelist SET  
                            Inspect_IsAproved =  1, Manager_ID = @Manager_ID, DepManager_ID =@DepManager_ID
                            WHERE RegNo =@RegNo", new { RegNo = reg, Manager_ID = ManagerID, DepManager_ID = ManagerID });

            var regFiles = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Files SET 
                            FilePath =@FilePath
                            WHERE RegNo =@RegNo", new { FilePath = newfilepath,  RegNo = reg });

            bool[] results = await Task.WhenAll(regsql, revsql, regFiles);

            return results.All(r => r);
        }

        public async Task<bool> ApproveByManager(string reg, string comments, string newfilepath, string DepManager)
        {
            var regsql = SqlDataAccess.UpdateInsertQuery(@" UPDATE Patrol_Registration SET  
                            ReportStatus = 5, DepManager_ID =@DepManager_ID, 
                            WHERE RegNo =@RegNo", new { RegNo = reg, DepManager_ID = DepManager });

            var revsql = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Approvelist SET  
                            Inspect_IsAproved =  1, DepManager_IsAproved = 1,  Manager_Comments =@Manager_Comments, DepManager_ID =@DepManager_ID
                            WHERE RegNo =@RegNo", new { RegNo = reg, Manager_Comments = comments, DepManager_ID = DepManager });

            var regFiles = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Files SET FilePath =@FilePath
                            WHERE RegNo =@RegNo", new
            {
                FilePath = newfilepath,
                RegNo = reg
            });

            bool[] results = await Task.WhenAll(regsql, revsql, regFiles);

            return results.All(r => r);
        }

        public async Task<bool> ApproveByDepartment(string reg, string comments, string newfilepath, string DepManager)
        {
            var regsql = SqlDataAccess.UpdateInsertQuery(@" UPDATE Patrol_Registration SET  
                            ReportStatus = 5, DivManager_ID =@DivManager_ID
                            WHERE RegNo =@RegNo", new { RegNo = reg, DivManager_ID = DepManager });

            var revsql = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Approvelist SET  
                            DepManager_IsAproved =  1,  Manager_Comments =@Manager_Comments, DivManager_ID =@DivManager_ID
                            WHERE RegNo =@RegNo", new { RegNo = reg, Manager_Comments = comments, DivManager_ID = DepManager });

            var regFiles = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Files SET FilePath =@FilePath
                            WHERE RegNo =@RegNo", new
            {
                FilePath = newfilepath,
                RegNo = reg
            });

            bool[] results = await Task.WhenAll(regsql, revsql, regFiles);

            return results.All(r => r);
        }


        public async Task<bool> ApproveByDivManager(string reg, string newfilepath, string DivManagerID)
        {
            var regsql = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration SET  
                            ReportStatus = 6, IsApproved = 1, ApprovalDate = GETDATE()
                            WHERE RegNo =@RegNo", new { RegNo = reg });

            var revsql = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Approvelist SET  
                            DivManager_IsAproved =  1
                            WHERE RegNo =@RegNo", new { RegNo = reg });

            var regFiles = SqlDataAccess.UpdateInsertQuery(@"UPDATE Patrol_Registration_Files SET FilePath =@FilePath
                            WHERE RegNo =@RegNo", new
            {
                FilePath = newfilepath,
                RegNo = reg
            });

            bool[] results = await Task.WhenAll(regsql, revsql, regFiles);

            return results.All(r => r);
        }


        public Task<bool> ReturnEmailMessage(string RegNo, string comments, int reportStats)
        {
            if(reportStats == 2) { 
                // -- Updates status of the PIC
            }else if(reportStats == 3)
            {
                // -- Updates status of the Inspector
            }
            else if(reportStats == 4)
            {
                // -- Updates status of the Manager
            }



            throw new NotImplementedException();
        }

        public Task<bool> SaveSignatureData(int userID, string fileName)
        {
            string strsql = "UPDATE Patrol_UserEmail Set Signature =@Signature WHERE Employee_ID =@Employee_ID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new { Signature = fileName, Employee_ID = userID });
        }
    }
}