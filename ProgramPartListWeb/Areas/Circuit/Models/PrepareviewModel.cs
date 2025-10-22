using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class PrepareviewModel
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

    public class SummaryComponentModel {
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
    public class WarehousePreparedModel
    {
        public int SetNo { get; set; }
        public string AbassadorPartnum { get; set; }
        public string Partname { get; set; }
        public string Locations { get; set; }
        public string FeederType { get; set; }
        public int Prepared_Quantity { get; set; }
        public string Code { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public int WarehouseUse { get; set; }
    }
     public class PartlistModel
     {
         public int SetNo { get; set; }
         public string AbassadorPartnum { get; set; }
         public string Partname { get; set; }   
         public string Locations { get; set; }  
         public string FeederType { get; set; }
         public int Prepared_Quantity { get; set; } 
         public int Machno { get; set; }
     }
     public class SupplierModel
     {
        public int SupID { get; set; }
        public string AbassadorPartnum { get; set; }
        public string Partname { get; set; }
        public string Supplier { get; set; }
        public string Code { get; set; }
    }


    public class PartlistPostModel
    {
        public int Series_ID { get; set; }
        public int SetNo { get; set; }
        public string AbassadorPartnum { get; set; }
        public string FeederType { get; set; }
        public int Prepared_Quantity { get; set; }
        public int Machno { get; set; }
    }
}