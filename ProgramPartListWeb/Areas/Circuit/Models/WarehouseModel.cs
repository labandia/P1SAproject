using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class WarehouseModel
    {
        public string AbassadorPartnum { get; set; }
        public string Item_name { get; set; }
        public int Reel_Qty { get; set; }
        public string Location { get; set; }
        public string ItemCode { get; set; }
        public string Buyer { get; set; }

    }

    public class WarehouseSummaryModel
    {
        public string AbassadorPartnum { get; set; }
        public int Request_Quantity { get; set; }
        public string Requestby { get; set; }
        public string Buyer { get; set; }
        public string Code { get; set; }
        public int Reel { get; set; }
        public string location { get; set; }
        public string partnametext { get; set; }
    }


    public class PartlistrequestModel
    {
        public string DateCreated { get; set; }
        public string AbassadorPartnum { get; set; }
        public string Item_name { get; set; }

        public string Barcode { get; set; }
        public string Location { get; set; }
        public string ItemCode { get; set; }
        public string Request_Quantity { get; set; }

    }
}