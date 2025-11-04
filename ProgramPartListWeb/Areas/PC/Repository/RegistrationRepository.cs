using Newtonsoft.Json;
using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.PC.Repository
{
    public class RegistrationRepository : IRegistration
    {
     
        public Task<List<PatrolRegistrationViewModel>> GetRegistrationData()
        {
            string strsql = $@"SELECT 
                                r.RegNo,
                                d.SectionName, 
                                FORMAT(r.DateConduct, 'MM/dd/yyyy') as DateConduct, 
                                r.ReportStatus,
	                            f.Filepath, 
                                f.CounterPath,

	                             a.Inspect_ID, 
	                             a.Inspect_Comments, 
	                             a.Inspect_IsAproved, 
	                             a.Inspect_IsSent,

	                            a.PIC_ID, a.PIC_Comments, a.PIC_IsSent, 

	                            a.Manager_ID, a.Manager_Comments, a.Manager_Isedit, a.Manager_IsSent, 

	                            a.DepManager_ID, a.DepManager_IsAproved, a.DepManager_IsSent, 

	                            a.DivManager_ID,  a.DivManager_IsAproved, a.DivManager_IsSent

                            FROM Patrol_Registration r
                            INNER JOIN P1SADepartment_tbl d ON d.DepartmentID = r.Department_ID
                            INNER JOIN Patrol_Registration_Files f ON f.RegNo = r.RegNo
                            INNER JOIN Patrol_Registration_Approvelist a ON a.RegNo = r.RegNo
                            ORDER BY DateCreated DESC";
            return SqlDataAccess.GetData<PatrolRegistrationViewModel>(strsql);
        }
        public Task<List<FindingModel>> GetRegisterFindings(string regNo) => SqlDataAccess.GetData<FindingModel>("GetFindings", new { Regno = regNo });
     


        public async Task<bool> AddRegistration(AddFormRegistrationModel reg, string json)
        {
            //INSERT MAIN REGISTRATION PROCESS
            bool result = await SqlDataAccess.UpdateInsertQuery("InsertRegistration",new
                       {
                           RegNo = reg.RegNo,
                           Department_ID = reg.Department_ID, 
                           PIC_ID = reg.PIC_ID
                       });

            // INSERT FILES TO THE OTHER TABLES
            await SqlDataAccess.UpdateInsertQuery("InserFiles", new { RegNo = reg.RegNo, FilePath = reg.FilePath});
            await SqlDataAccess.UpdateInsertQuery("INSERT INTO Patrol_Registration_Approvelist(RegNo, PIC_ID) VALUES(@RegNo, @PIC_ID)", 
                new { RegNo = reg.RegNo, PIC_ID = reg.PIC_ID });

            // INSERT FINDING AND COUNTERMEASURE PROCESS
            // Make a Json format 
            var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);

            foreach (var f in findings)
            {
                var findparams = new
                {
                    RegNo = "P1SA-" + f.RegNo,
                    FindID = f.FindID,
                    FindDescription = f.FindDescription
                };
                await SqlDataAccess.UpdateInsertQuery("InsertFindings", findparams, "Registration");
            }


            return result;
        }

        public async Task<bool> EditReg_ProcessOwner(ProcessOwnerForms reg, string json)
        {
            // UPDATE THE MAIN REGISTRATION TABLE
            string mainsql = $@"UPDATE Patrol_Registration_Approvelist
                                SET PIC_Comments = @PIC_Comments, Inspect_ID = @Inspect_ID
                                WHERE RegNo = @RegNo";

            var regMain = SqlDataAccess.UpdateInsertQuery(mainsql, new
            {
                RegNo = reg.RegNo,
                PIC_Comments = reg.PIC_Comments,
                Inspect_ID = reg.Employee_ID
            });
            // UPDATE THE UPLOADED FILES
            var regFiles = SqlDataAccess.UpdateInsertQuery("EditPatrolFilesRegister", new
            {
                FilePath = reg.Filepath,
                CounterPath = reg.CounterPath,
                RegNo = reg.RegNo
            });

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
                await SqlDataAccess.UpdateInsertQuery("UpdateCounterMeasure", findparams, "Registration");
            }

            return results.All(r => r);
        }

        public Task<List<EmailModelV2>> PatrolEmailData()
        {
            string strsql = "SELECT * FROM Patrol_UserEmail";
            return SqlDataAccess.GetData<EmailModelV2>(strsql);
        }

       
    }
}