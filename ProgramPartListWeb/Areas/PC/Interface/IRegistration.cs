using ProgramPartListWeb.Areas.PC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface IRegistration
    {
        Task<List<PatrolRegistrationViewModel>> GetRegistrationData();
        Task<List<FindingModel>> GetRegisterFindings(string regNo);
        Task<List<EmailModelV2>> PatrolEmailData();

        Task<bool> AddRegistration(AddFormRegistrationModel model, string json);
        Task<bool> EditReg_ProcessOwner(ProcessOwnerForms model, string json);
    }
}
