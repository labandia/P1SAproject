using ProgramPartListWeb.Areas.PC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface IRegistration
    {
        Task<List<PatrolRegistionModel>> GetRegistrationData();
        Task<bool> AddRegistration(AddFormRegistrationModel model, string json);
    }
}
