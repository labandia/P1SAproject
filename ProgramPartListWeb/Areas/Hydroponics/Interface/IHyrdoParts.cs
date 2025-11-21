using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IHyrdoParts
    {
        Task<List<StockPartsModel>> GetInventoryList();

        Task<bool> AddInventory(AddInventoryModel model);

        Task<bool> EditInventory(AddInventoryModel model, string partno);

        Task<bool> UpdateStocks(int ID, int Quan);
        Task<bool> UpdateWarning(int StockID, double WarningLevel);



        Task<bool> IncrementAndDecreaseStocks(int StockID, int CurrentQty, int Required);



        Task<IEnumerable<StockAddModel>> GetAddStocksList();
        Task<IEnumerable<StockAddDetailsModel>> GetAddStocksDetails(int ID);
        Task<bool> AddStockItem(List<AddStocksItem> stocks);
    }
}
