using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IChambers
    {
        Task<List<ChamberModel>> GetChambersData(int chamber);
        Task<bool> AdditionalChambers();
        Task<bool> EditChambers();
        Task<bool> Deletechambers();
    }
}
