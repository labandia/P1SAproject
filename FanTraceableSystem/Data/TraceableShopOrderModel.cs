using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Data
{
    public class TraceableShopOrderModel
    {
        public int RecordId { get; set; }
        public string FinalShopOrder { get; set; }
        public string PCBShopOrder { get; set; }
        public string Revision { get; set; }
        public string PCBA { get; set; }
        public DateTime DatePrepared { get; set; }
        public DateTime TimeInput { get; set; } = DateTime.Now;
        public int PreparedQuantity { get; set; } = 0;
        public string PreparedBy { get; set; }

        public byte? Shift { get; set; }

        public string Customer { get; set; }
        public string InspectorName { get; set; }

        public string CardCaseNo { get; set; }
        public string Remarks { get; set; }
        public string PCBIncharge { get; set; }
        public string PCBIssuer { get; set; }
        public string LotNo { get; set; }
    }

    public class TracePCBModel
    {
        public string PCBShopOrder { get; set; }
        public int Quantity { get; set; }
    }
}
