using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IHyrdoParts
    {
        Task<List<StockPartsModel>> GetInventoryList();
        Task<bool> UpdateStocks(int ID, int Quan);
        Task<bool> UpdateWarning(int StockID, double WarningLevel);
    }
}
