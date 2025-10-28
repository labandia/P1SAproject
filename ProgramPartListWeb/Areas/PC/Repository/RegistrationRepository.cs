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
     
        public Task<List<PatrolRegistionModel>> GetRegistrationData()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> AddRegistration(AddFormRegistrationModel reg, string json)
        {
            //INSERT MAIN REGISTRATION PROCESS
            bool result = await SqlDataAccess.UpdateInsertQuery("InsertRegistration",new
                       {
                           RegNo = "P1SA-" + reg.RegNo,
                           Department_ID = reg.Department_ID
                       });

            // INSERT FILES TO THE OTHER TABLES
            await SqlDataAccess.UpdateInsertQuery("InserFiles", new { RegNo = "P1SA-" + reg.RegNo, FilePath = reg.FilePath});
            await SqlDataAccess.UpdateInsertQuery("INSERT INTO Patrol_Registration_Approvelist(RegNo) VALUES(@RegNo)", new { RegNo = "P1SA-" + reg.RegNo });

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
    }
}