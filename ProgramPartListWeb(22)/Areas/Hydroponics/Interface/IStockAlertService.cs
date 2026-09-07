using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IStockAlertService
    {
        // GEt Stock Alert Data
        Task<List<StockPartsModel>> GetAllStockAlertsAsync();
        Task<int> GenerateStockNotification(List<int> userID, int hoursInterval = 6);


        Task<IEnumerable<StockNotification>> GetLowStockNotificationList(int userId);
        Task<IEnumerable<StockNotificationDetail>> GetNotificationDetails(int Id, int userID);

        Task<List<StockSendLogs>> GetStockSendEmailLogs();

        Task CheckStockLevelsAsync();
        Task<List<StockAlert>> GetActiveAlertsAsync();
  
        Task MarkAlertAsReadAsync(int alertId);
        Task SendNotificationsAsync(StockAlert alert);
        Task<bool> SendEmailNotificationStocks();

        Task<bool> ClickNotificationCount(int alertId);
    }
}
