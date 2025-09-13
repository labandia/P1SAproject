
using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IPartsList
    {
        Task<List<MasterlistPartsModel>> GetPartsMasterlist();
        Task<bool> AddMasterlistParts(MasterlistPartsModel p);
        Task<bool> EditMasterlistParts(MasterlistPartsModel p);
    }
}
