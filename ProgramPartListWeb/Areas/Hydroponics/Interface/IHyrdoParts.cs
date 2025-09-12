using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IHyrdoParts
    {
        Task<List<HydropPartsModel>> GetInventoryList();
        Task<List<ChamberTypePartsModel>> GetChamberTypePartsList(int chamberID);
        Task<List<ChamberTypeList>> GetChambersType();
        Task<bool>AddInventory(HydropPartsModel model);
        Task<bool> UpdateStocks(int ID, int Quan);
    }

   
}
