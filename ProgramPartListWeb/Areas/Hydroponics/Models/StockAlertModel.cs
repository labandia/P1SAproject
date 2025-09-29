using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Hydroponics.Models
{
    // MAIN NOTIFICATION DISPLAY
    public class StockNotification
    {
        public int NotificationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Shift { get; set; }
        public int Sequence { get; set; }
        public bool IsRead { get; set; }
        public List<StockNotificationDetail> Details { get; set; }
    }
    // NOTIFICATION DETAILS DISPLAY
    public class StockNotificationDetail
    {
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public int CurrentQty { get; set; }
        public int ReorderLevel { get; set; }
        public int WarningLevel { get; set; }
        public string Status { get; set; }
    }
    // NOTIFICATION  FOR EACH USERS 
    public class StockNotificationUser
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
    }




    public class StockAlert
    {
        public int AlertId { get; set; }
        public int StockID { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
       
    }

    public class StockSendLogs
    {
        public int StockLogId { get; set; }
        public string EmailSent { get; set; }
        public DateTime SentAt { get; set; }

    }
}