using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IStocksparts
    {
        Task<List<StockAlertModel>> GetStocksAlertList();
        Task<List<StockPartsModel>> GetStocksTracking();
        Task<List<StockPartsModel>> GetTransactionStocks();
        Task<bool> AddStocks(int ID, double Quan);
    }
}
