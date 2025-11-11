using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMACS_V2.Areas.PartsLocal.Model
{
    public class RotoFormsModel
    {
        private int _RecordID;
        private string _Partnumber;
        private string _ModelName;
        private int _Area;
        private int _Quantity;
        private string _FrontImage;
        private string _BackImage;
    }

    public class Transactionforms
    {
        public int TransactionType { get; set; }
        public string Area { get; set; }
        public string Partnumber { get; set; }
        public string RotorOrder { get; set; }
        public int Quantity { get; set; } // --- Input Quantity
        public string Remarks { get; set; }
        public string ShopOrder { get; set; }
        public int PlanQuantity { get; set; }
        public string PlanDate { get; set; }
        public string ModelBase { get; set; }
        public string Status { get; set; }
        public int BushType { get; set; }

    }

    public class ShopOrderOutForm
    {
        public string ShopOrder { get; set; }
        public int TransactionType { get; set; }
        public string Partnumber { get; set; }
        public int Quantity { get; set; }
        public string Area { get; set; }
        public string Remarks { get; set; }

    }
}