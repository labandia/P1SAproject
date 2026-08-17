using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Final.Model
{
    public class DownTimeModel
    {
        public int DownTimeID { get; set; }
        public string FinalShopOrder { get; set; }
        public string DownTimeCode { get; set; }
        public string DownTimeType { get; set; }
        public TimeSpan? TimeStart { get; set; }  
        public TimeSpan? TimeEnd { get; set; }     
        public int? Downtime { get; set; }     
        public string PIC { get; set; }
        public string Details { get; set; }
        public string GroupName { get; set; }
    }
}