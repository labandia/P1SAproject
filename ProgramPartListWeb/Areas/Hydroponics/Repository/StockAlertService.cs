using Aspose.Cells.Pivot;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class StockAlertService : IStockAlertService
    {
        List<string> recipientslist = new List<string>
        {
            "jaye.labandia@sanyodenki.com",
            "person2@example.com",
            "person3@example.com"
        };



        public Task<List<StockPartsModel>> GetAllStockAlertsAsync()
        {
            string strsql = $@"SELECT
                                    s.StockID,
	                                p.PartID, 
	                                p.PartNo, 
	                                p.PartName, 
	                                c.CategoryID,
	                                c.CategoryName,
	                                p.Supplier,
	                                p.Unit,
	                                s.CurrentQty,
	                                s.ReorderLevel,
	                                s.WarningLevel,
	                                s.Status,
	                                s.LastUpdated,
                                    p.ImageParts
                                FROM Hydro_InventoryParts p
                                LEFT JOIN Hydro_CategoryParts c ON c.CategoryID = p.CategoryID
                                LEFT JOIN Hydro_Stocks s ON s.PartID = p.PartID
								WHERE s.Status IN ('Restock', 'Out of stocks')
                               ORDER BY 
								CASE s.Status
									WHEN 'Restock' THEN 1
									WHEN 'Out of stocks' THEN 2
								END,
								s.CurrentQty ASC;";

            return SqlDataAccess.GetData<StockPartsModel>(strsql, null);
        }

        public async  Task CheckStockLevelsAsync()
        {
            var item = await GetAllStockAlertsAsync();

            foreach (var stocks in item)
            {
                await CheckProductStockAsync(stocks);
            }

        }

        private async Task CheckProductStockAsync(StockPartsModel product)
        {
            if (product.Status == "Restock")
            {
                await CreateOutOfStockAlertAsync(product);
            }
            else if (product.Status == "Out of stocks")
            {
                await CreateLowStockAlertAsync(product);
            }
        }

        private async Task CreateLowStockAlertAsync(StockPartsModel product)
        {
            string sql = @"INSERT INTO StockAlerts (StockID, Message, Status)
                       VALUES (@StockID, @Message, @Status);";

            var param = new
            {
                StockID = product.StockID,
                Message = $"⚠️ Low stock alert: {product.PartNo} has {product.CurrentQty} {product.Unit} left.",
                Status = product.Status
            };

            await SqlDataAccess.UpdateInsertQuery(sql, param);
        }


        private async Task CreateOutOfStockAlertAsync(StockPartsModel product)
        {
            string sql = @"INSERT INTO StockAlerts (StockID, Message, Status)
                       VALUES (@StockID, @Message, @Status);";

            var param = new
            {
                StockID = product.StockID,
                Message = $"❌ Out of stock: {product.PartNo} is currently unavailable!",
                Status = product.Status
            };

            await SqlDataAccess.UpdateInsertQuery(sql, param);
        }


        public Task<List<StockAlert>> GetActiveAlertsAsync()
        {
             string sql = $@"SELECT 
	                        a.AlertId,
	                        a.StockID,
	                        i.PartNo,
	                        i.PartName,
	                        a.Message,
	                        a.Status
                        FROM Hydro_StockAlerts a
                        INNER JOIN Hydro_Stocks s ON s.StockID = a.StockID
                        INNER JOIN Hydro_InventoryParts i ON i.PartID = s.PartID
                        WHERE a.IsRead = 0";

            return SqlDataAccess.GetData<StockAlert>(sql, null);
        }

        public Task MarkAlertAsReadAsync(int alertId)
        {
            string sql = "UPDATE Hydro_StockAlerts SET IsRead = 1 WHERE AlertId = @AlertId;";
            return SqlDataAccess.UpdateInsertQuery(sql, new { AlertId = alertId });
        }

        public Task SendNotificationsAsync(StockAlert alert)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendEmailNotificationStocks()
        {
            string currentShift = GlobalUtilities.GetTheShiftSchedule();
            if (string.IsNullOrEmpty(currentShift)) return false;

            var emailCount = await GetStockSendEmailLogs();


            foreach (var recepient in recipientslist)
            {
                // Count emails sent today for this shift
                int shiftEmailCount = emailCount.FindAll(e =>
                  e.EmailSent == recepient &&
                  e.Shift == currentShift &&
                  e.SentAt.Date == DateTime.Now.Date // compare date part only
              ).Count;

                Debug.WriteLine($"Emails sent today for {currentShift}: {shiftEmailCount}");

                if(shiftEmailCount == 0)
                {
                    await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO Hydro_StockAlertLog 
                                                            (EmailSent, Shift, SentAt, Sequence) 
                                                            VALUES (@EmailSent, @Shift, @Sequence)",
                        new
                        {
                            EmailSent = recepient,
                            Shift = currentShift,
                            Sequence = 1
                        });
                    return true;
                }


                if (shiftEmailCount >= 2)
                    return false; // Limit reached for this shift


                Debug.WriteLine($"Sending Email Proccess ....");
                // Get active alerts
                var activeAlerts = await GetActiveAlertsAsync();

                if (!activeAlerts.Any())
                    return false; // No alerts, do not send email

                // Prepare email body
                // SENDING emails to the recepients


                var updatelogs = new StockSendLogs
                {
                    EmailSent = recepient,
                    Shift = currentShift,
                    SentAt = DateTime.Now,
                    Sequence = shiftEmailCount + 1
                };




            }

            return true;
        }
        public Task<List<StockSendLogs>> GetStockSendEmailLogs()
        {
            string strsql = $@"SELECT StockLogId, EmailSent, Shift, Sequence, SentAt FROM Hydro_StockAlertLog";
            return SqlDataAccess.GetData<StockSendLogs>(strsql, null);

        }
    }
}