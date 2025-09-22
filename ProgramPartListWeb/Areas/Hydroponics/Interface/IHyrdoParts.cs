using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IHyrdoParts
    {
        Task<List<StockPartsModel>> GetInventoryList();
        Task<List<ChamberTypePartsModel>> GetChamberTypePartsList(int chamberID);
        Task<List<ChamberTypeList>> GetChambersType();
        Task<bool>AddInventory(StockPartsModel model);
        Task<bool> UpdateStocks(int ID, int Quan);
        Task<bool> UpdateWarning(int StockID, double WarningLevel);
    }

   
}
