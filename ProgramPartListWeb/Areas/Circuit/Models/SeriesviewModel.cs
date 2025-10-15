using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class SeriesviewModel
    {
        public int Series_ID  { get; set; }
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
    }
    public class SeriesviewModelAdd
    {
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
    }
    public class MachineSelection
    {
        public string MachineSerial { get; set; }
    }

    public class SummaryHistory
    {
        public string Series_no { get; set; }
        public string ProductName { get; set; }
        public string AbassadorPartnum { get; set; }
        public string ItemCode { get; set; }
        public int NeedQuan { get; set; }
        public int CompIN { get; set; }
        public int CompOut { get; set; }
        public int Totalprod { get; set; }
        public int Diff { get; set; }
    }
    public class ReturnModel
    {
        public string ComponentsName { get; set; }
        public int Quantity { get; set; }
        public string ItemCode { get; set; }
        public string Ambassador { get; set; }
        public string Reel_ID { get; set; }
    }

    public class SuppliersModel {
        public int SupID { get; set; } = 0;
        public string AbassadorPartnum { get; set; }
        public string Partname { get; set; }
        public string Supplier { get; set; } 
        public string Code { get; set; }    
    }
    public class ExportRequest
    {
        public List<SummaryHistory> Data { get; set; }
    }
}