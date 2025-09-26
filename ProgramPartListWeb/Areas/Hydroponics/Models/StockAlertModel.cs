using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Hydroponics.Models
{
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
        public string Shift { get; set; }
        public DateTime SentAt { get; set; }
        public int Sequence { get; set; }

    }
}