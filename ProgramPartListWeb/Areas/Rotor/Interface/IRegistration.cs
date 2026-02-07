using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Rotor.Interface
{
    public interface IRotorRegistration
    {
        Task<PagedResult<RotorRegistrationModel>> GetRegistrationsList(
            string search, 
            int monthfilter,
            int catID, 
            int Department,
            int pageNumber,
            int pageSize);

        Task<bool> AddRegistration(RotorRegistrationModel masterlist);

        Task<bool> EditRegistration(RotorRegistrationModel masterlist);
        Task<bool> DeleteRegistration(int registID);
    }
}
