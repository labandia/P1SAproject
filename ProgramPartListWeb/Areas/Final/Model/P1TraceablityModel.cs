using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Final.Model
{
    public class P1TraceablityModel
    {
        public string FinalShopOrder { get; set; }
        public string ShopOrder { get; set; }
        public string ProcessName { get; set; }
        public string ItemNo { get; set; }
        public DateTime DatePrepared { get; set; }
        public string TimeInput { get; set; }
        public string Rev { get; set; }
        public decimal PreparedQuantity { get; set; } = 0;
        public string PreparedBy { get; set; }
        public int PlanQuan { get; set; }
        public int? Shift { get; set; }

        public string Customer { get; set; }

        public string Modeltype { get; set; }
        public string Remarks { get; set; }
        public string Incharge { get; set; }
        public string SubAssyIssued { get; set; }
        public string LotNo { get; set; }
        public int DepartmentID { get; set; }
    }

    public class CatergoryPartsModel
    {
        public int CartsPartID { get; set; }
        public string PartsName { get; set; }
    }

    public class MEIGpartsModel
    {
        public string FinalShopOrder { get; set; }
        public int CartsPartID { get; set; }
        public string PartsName { get; set; }
        public string CategoryName { get; set; }
        public string Registration { get; set; }
        public string PartNumber { get; set; }
        public string PinList { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Lines { get; set; }
        public string Issuer { get; set; }
        public string Preparation { get; set; }
    }

}