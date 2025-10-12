using Aspose.Cells.Charts;
using Aspose.Cells.Pivot;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Data;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class StockAlertService : IStockAlertService
    {
        public static string strSender => ConfigurationManager.AppSettings["config:SMTPEmail"];
        private readonly UserRespository _user;

        public StockAlertService(UserRespository user)
        {
            _user = user;
        }


        //List<string> recipientslist = new List<string>
        //{
        //    "jaye.labandia@sanyodenki.com",
        //    "person2@example.com",
        //    "person3@example.com"
        //};

        public async Task<List<string>> GetAllRecepients()
        {
            var data = await _user.GetAllusers() ?? new List<UsersModel>();

            var filterbyProj = data
                .Where(res => res.Project_ID == 10 && !string.IsNullOrEmpty(res.Email))
                .Select(sel => sel.Email).ToList();

            return filterbyProj;
        }



        public Task<List<StockPartsModel>> GetAllStockAlertsAsync()
        {
            string strsql = $@"SELECT
                                    s.StockID,
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
                                LEFT JOIN Hydro_Stocks s ON s.PartNo = p.PartNo
								WHERE s.Status IN ('Restock', 'Out of stocks')
                               ORDER BY 
								CASE s.Status
									WHEN 'Restock' THEN 1
									WHEN 'Out of stocks' THEN 2
								END,
								s.CurrentQty ASC;";

            return SqlDataAccess.GetData<StockPartsModel>(strsql, null);
        }

        public async Task<int> GenerateStockNotification(List<int> userIds, int hoursInterval = 6)
        {
            int notifiResult = 0;

      
            try
            {
                // Step 1: Get the last notification
                string lastSql = @"SELECT TOP 1 CreatedDate 
                       FROM Hyrdo_StockNotifications
                       ORDER BY CreatedDate DESC";



                var lastCreatedList = await SqlDataAccess.GetData<DateTime>(lastSql, null);
                DateTime? lastCreated = lastCreatedList.FirstOrDefault();

                if (!lastCreated.HasValue)
                {
                    lastCreated = DateTime.Now.AddHours(-6); // ensures new notification is generated immediately
                }

                // Check if at least 6 hours have passed since the last notification
                if (DateTime.Now >= lastCreated.Value.AddHours(6))
                {
                    // Step 2: get Latest the Data of a Stocks Alerts
                    var StockalertList = await GetAllStockAlertsAsync();

                    // Step 3: if no data List Acquired return 0
                    if (StockalertList == null || !StockalertList.Any())
                        return 0;  // ❌ Nothing to notify


                    // If has A Stock Alert Data 
                    // Step 4: Insert the Main or the Header First
                    string Mainsql = @"INSERT INTO Hyrdo_StockNotifications(Title)
                                VALUES (@Title);
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                    notifiResult = await SqlDataAccess.GetCountData(Mainsql, new { Title = "Low / Critical Stock Alert" });



                    // Step 5: Insert Notification Details
                    foreach (var item in StockalertList)
                    {

                        string insertDetails = @"
                        INSERT INTO Hyrdo_StockNotificationDetails (NotificationId, PartNo, CurrentQty, ReorderLevel, WarningLevel, Status)
                        VALUES(@NotificationId, @PartNo, @CurrentQty, @ReorderLevel, @WarningLevel, @Status)";
                        var parameter = new
                        {
                            NotificationId = notifiResult,
                            PartNo = item.PartNo,
                            CurrentQty = item.CurrentQty,
                            ReorderLevel = item.ReorderLevel,
                            WarningLevel = item.WarningLevel,
                            Status = item.Status
                        };

                        await SqlDataAccess.UpdateInsertQuery(insertDetails, parameter);
                    }


                    // Step 6. Assign Notification to Users
                    string insertUsers = @"INSERT INTO Hyrdo_StockNotificationUsers(NotificationId, User_ID)
                                  VALUES (@NotificationId, @User_ID);";

                    foreach (var userId in userIds)
                    {
                        await SqlDataAccess.UpdateInsertQuery(insertUsers, new { NotificationId = notifiResult, User_ID = userId });
                    }


                }
            }
            catch(Exception e)
            {
                // 🔴 Log the error, but don’t throw
                Console.WriteLine($"[GenerateStockNotification ERROR]: {e.Message}");
                return 0; // return safe default
            }

        
            return notifiResult;
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
           try
            {
                var emailCount = await GetStockSendEmailLogs();
                bool anySent = false;


                // Gets the list of Recepients from the Database
                var recipientslist = await GetAllRecepients();


                // loop all recipients 
                foreach (var recepient in recipientslist)
                {
                    // get the latest log send email
                    var latestLog = emailCount
                         .Where(res =>
                             res.EmailSent == recepient)
                         .OrderByDescending(res => res.StockLogId)
                         .FirstOrDefault();

                    



                    // Gets the last sent date 
                    DateTime? lastSentAt = latestLog?.SentAt;

                    // strict 4-hour rule 
                    // it skips when its not in the 4 hours pass
                    if (lastSentAt != null && DateTime.Now < lastSentAt.Value.AddHours(4))
                    {
                        continue;
                    }

                    //Debug.WriteLine($"[{recepient}] Sending email...");
                    // check active alerts
                    var activeAlerts = await GetAllStockAlertsAsync();
                    if (!activeAlerts.Any())
                    {
                        break;
                    }



                    string htmlBody = CreateEmailBody(activeAlerts);

                    var SendEmail = new SentEmailModel
                    {
                        Subject = "Stock Alert Notification",
                        Sender = strSender,
                        BCC = "",
                        Body = htmlBody,
                        Recipient = recepient
                    };
                    // send email here...
                    // EMAIL SAVE TO THE DATABASE
                    await EmailService.SendEmailViaSqlDatabase(SendEmail);

                    // first log today
                    if (latestLog == null)
                    {
                        await SqlDataAccess.UpdateInsertQuery(@"
                            INSERT INTO Hydro_StockAlertLog(EmailSent, Sequence)
                            VALUES (@EmailSent, @Sequence)",
                            new
                            {
                                EmailSent = recepient,
                                Sequence = 1
                            });
                    }
                    else
                    {
                        // update existing log with incremented sequence
                        await SqlDataAccess.UpdateInsertQuery(@"
                                UPDATE Hydro_StockAlertLog
                                SET SentAt = GETDATE(), Sequence = Sequence + 1
                                WHERE StockLogId = @StockLogId",
                            new
                            {
                                StockLogId = latestLog.StockLogId
                            });
                    }



                    anySent = true;
                }

                return anySent;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }


        public Task<List<StockSendLogs>> GetStockSendEmailLogs()
        {
            string strsql = $@"SELECT StockLogId, EmailSent, SentAt FROM Hydro_StockAlertLog";
            return SqlDataAccess.GetData<StockSendLogs>(strsql, null);

        }



        public  string CreateEmailBody(List<StockPartsModel> items)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine("<title>Stock Alert Notification</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body style='font-family: Arial, sans-serif; color: #333;'>");

            sb.AppendLine("<h2 style='color:#2E86C1;'>Stock Alert Notification</h2>");
            sb.AppendLine("<p>Dear Users,</p>");
            sb.AppendLine("<p>The following items require your attention:</p>");

            // Table header
            sb.AppendLine("<table style='border-collapse: collapse; width: 100%;'>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th style='border: 1px solid #ccc; padding: 8px; background-color:#f2f2f2; text-align: left;'>PartNo/PartName</th>");
            sb.AppendLine("<th style='border: 1px solid #ccc; padding: 8px; background-color:#f2f2f2;'>Current Stock</th>");
            sb.AppendLine("<th style='border: 1px solid #ccc; padding: 8px; background-color:#f2f2f2;'>Minimum Stock</th>");
            sb.AppendLine("<th style='border: 1px solid #ccc; padding: 8px; background-color:#f2f2f2;'>Status</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            // Table rows
            foreach (var item in items)
            {
                string backolor;

                if (item.Status == "Restock")
                {
                    backolor = "background-color:#fff3cd; color:#856404; text-align: center;";
                }
                else
                {
                    backolor = "background-color:#dc3545; color:#ffffff; text-align: center;";
                }


                sb.AppendLine("<tr>");
                sb.AppendLine($@"<td style='border: 1px solid #ccc; padding: 8px;'>
                                  <p style='color: #185d8b; font-weight: 600; margin: 0;'>{item.PartName}</p>
                                  <small>{item.PartNo}</small>
                              </td>");
                sb.AppendLine($"<td style='border: 1px solid #ccc; padding: 8px; text-align: center;'>{item.CurrentQty}</td>");
                sb.AppendLine($"<td style='border: 1px solid #ccc; padding: 8px; text-align: center;'>{item.ReorderLevel}</td>");
                sb.AppendLine($"<td style='{backolor}'>{item.Status}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");


            sb.AppendLine("<p style='margin-top:20px;'>Please take the necessary action.</p>");
            sb.AppendLine("<p style='margin-top:20px;'>This is an auto system-generated email. Do not reply to this email.</p>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        public async Task<IEnumerable<StockNotification>> GetLowStockNotificationList(int userId)
        {
            return await SqlDataAccess.GetData<StockNotification>($@"
                            SELECT 
	                            n.NotificationId, 
	                            n.Title, 
	                            u.IsRead,
	                            FORMAT(n.CreatedDate, 'MM/dd/yyyy - hh:mm tt') as CreatedDate
                            FROM Hyrdo_StockNotifications n
                            JOIN Hyrdo_StockNotificationUsers u ON n.NotificationId = u.NotificationId
                            WHERE u.User_ID = @User_ID
                            ORDER BY n.CreatedDate DESC;", new { User_ID = userId });
        }

        public async Task<IEnumerable<StockNotificationDetail>> GetNotificationDetails(int Id, int userID)
        {
            string sql = $@"UPDATE Hyrdo_StockNotificationUsers 
                            SET IsRead = 1, ReadDate = GETDATE()
                            WHERE NotificationId = @NotificationId AND User_ID =@User_ID;";
            await SqlDataAccess.UpdateInsertQuery(sql, new { NotificationId = Id, User_ID  = userID });

            return await SqlDataAccess.GetData<StockNotificationDetail>($@"SELECT 
	                                                                        i.PartNo,
	                                                                        i.PartName,
	                                                                        d.CurrentQty, 
	                                                                        d.ReorderLevel, 
	                                                                        d.WarningLevel, 
	                                                                        d.Status
                                                                        FROM Hyrdo_StockNotificationDetails d
                                                                        INNER JOIN Hydro_InventoryParts i ON i.PartNo = d.PartNo
                                                                        WHERE d.NotificationId = @NotificationId;", new { NotificationId = Id });
        }
    }
}