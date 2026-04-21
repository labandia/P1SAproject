using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Data
{
    public class TraceableSubAssyModel
    {
        public int SubAssyID { get; set; }  
        public string FinalShopOrder { get; set; }
        public string Rev { get; set; }
        public string ShopOrder { get; set; }
        public int PreparedQuantity { get; set; }
        public string LotNo { get; set; }   
        public string Line { get; set; }
        public string SubAssyIssued { get; set; }
        public int isAction { get; set; }
    }
}
