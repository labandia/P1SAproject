using ProgramPartListWeb.Areas.PC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface IRegistration
    {
        Task<List<PatrolRegistrationViewModel>> GetRegistrationData();
        Task<List<FindingModel>> GetRegisterFindings(string regNo);
        Task<List<RegistrationFiles>> GetRegisterFiles(string regNo);


        Task<List<EmailModelV2>> PatrolEmailData();

        Task<bool> AddRegistration(AddFormRegistrationModel model, string json);
        Task<bool> EditReg_ProcessOwner(ProcessOwnerForms model, string json);
        Task<bool> ApproveByInspector(string RegNo, string Datecon, string newfilepath, string ManagerID);
        Task<bool> ApproveByManager(string RegNo, string comments, string newfilepath, string DepManager);

        Task<bool> ApproveByDivManager(string RegNo, string newfilepath, string DivManagerID);
        Task<bool> ReturnEmailMessage(string RegNo, string comments, int ReturnByID);
    }
}
