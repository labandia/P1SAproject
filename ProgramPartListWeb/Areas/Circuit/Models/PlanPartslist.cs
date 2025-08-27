using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Circuit.Models
{
    public class PlanPartslist
    {
        public int Series_ID { get; set; }
        public string Series_no { get; set; }
        public int Line { get; set; }
        public decimal Timetarget { get; set; }
        public string CreatedBy { get; set; }
        public string Shift { get; set; }
        public string Remarks { get; set; }
        public string SetupNavi { get; set; }
        public string VisualManage { get; set; }
        public string Status { get; set; }
        public string MachineSerial { get; set; }
        public string Modelno { get; set; }
        public string SetGroup { get; set; }
        public int Ongoing { get; set; }
        public int TotalCount { get; set; }
        public int Planstatus { get; set; }
        public string DateCreated { get; set; }

        public List<partlistComponents> components { get; set; } = new List<partlistComponents>();
        public List<SummaryComponentModelV2> summary { get; set; } = new List<SummaryComponentModelV2>();
    }

    public class partlistComponents
    {
        [Key]
        public int RecordID { get; set; }
        public string Series_no { get; set; }
        public int SetNo { get; set; }
        public string AbassadorPartnum { get; set; }
        public string Partname { get; set; }
        public string Locations { get; set; }
        public string FeederType { get; set; }
        public int Prepared_Quantity { get; set; }
        public int Machno { get; set; }
    }
    public class SummaryComponentModelV2
    {
        public int RecordID { get; set; }
        public string DateInput { get; set; }
        public string ComponentName { get; set; }
        public int Quantity { get; set; }
        public string AbassadorPartnum { get; set; }
        public string ReelID { get; set; }
        public int QuantityInput { get; set; }
        public string LotID { get; set; }
        public string Code { get; set; }
        public string Machine { get; set; }
        public string LackExcess { get; set; }
        public string Status { get; set; }
        public int SetNo { get; set; }
        public int SupID { get; set; }
        public int gtotal { get; set; }
        public string LotNo { get; set; }
        public string Partname { get; set; }
        public string Series_no { get; set; }
    }
}