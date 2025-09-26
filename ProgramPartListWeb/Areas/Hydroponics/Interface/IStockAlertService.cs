using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IStockAlertService
    {
        Task<List<StockPartsModel>> GetAllStockAlertsAsync();
        Task<List<StockSendLogs>> GetStockSendEmailLogs();

        Task CheckStockLevelsAsync();
        Task<List<StockAlert>> GetActiveAlertsAsync();
  
        Task MarkAlertAsReadAsync(int alertId);
        Task SendNotificationsAsync(StockAlert alert);
        Task<bool> SendEmailNotificationStocks();
    }
}
