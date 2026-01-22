using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace ProgramPartListWeb.Areas.Hydroponics.Models
{
    public class AddInventoryModel
    {
        public string PartNo { get; set; }
        public string PartName { get; set; }    
        public int CategoryID { get; set; }
        public string Supplier { get; set; }    
        public string Unit { get; set; }
        public string ImageParts { get; set; }
        public int CurrentQty { get; set; } 
        public double ReorderLevel { get; set; }
        public double WarningLevel { get; set; }
        public double Unit_Price { get; set; }
    }

    public class AddPartsChamberModel
    {
        public int ChamberID { get; set; }  
        public string PartNo { get; set; }
        public int QuantityPerChamber { get; set; }
        public double UnitCost_PHP { get; set; }
    }


    public class AddStocksItem
    {
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
        public int availableStock { get; set; }
    }
}