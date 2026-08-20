using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Final.Model
{
    public class DownTimeModel
    {
        public int DownTimeID { get; set; }
        public string DateStart { get; set; }

        public string Line { get; set; }
        public string FinalShopOrder { get; set; }
        public string ItemNo { get; set; }
        public string Model { get; set; }
        public int? PlanQty { get; set; }


        public string DownTimeCode { get; set; }
        public string DownTimeType { get; set; }
        public TimeSpan? TimeStart { get; set; }  
        public TimeSpan? TimeEnd { get; set; }     
        public int? Downtime { get; set; }     
        public string PIC { get; set; }
        public string Details { get; set; }
        public string GroupName { get; set; }

        public double CycleTime { get; set; }
        public int OperationRate { get; set; }
        public int MachineCount { get; set; }
    }

    public class DownTimeReportModel
    {
        public string Line { get; set; }
        public string FinalShopOrder { get; set; }
        public string ItemNo { get; set; }
        public string Model { get; set; }
        public int? PlanQty { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }

        public int Downtime { get; set; }
        public double CycleTime { get; set; }
        public int OperationRate { get; set; }

    }

    public class DownTimeTypeModel
    {
        public string DownTimeCode { get; set; }
        public string DownTimeType { get; set; }
        public string GroupName { get; set; }
    }
}