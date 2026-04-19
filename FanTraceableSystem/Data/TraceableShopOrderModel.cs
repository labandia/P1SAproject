using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Data
{
    public class PagingState
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public bool HasNextPage { get; set; }
    }

    public class TraceableShopOrderModel
    {
        public int RecordId { get; set; }
        public string FinalShopOrder { get; set; }
        public string PCBShopOrder { get; set; }
        public string Revision { get; set; }
        public string PCBA { get; set; }
        public int PlanQuan { get; set; }
        public DateTime DatePrepared { get; set; }
        public string TimeInput { get; set; }
        public int PreparedQuantity { get; set; } = 0;
        public string PreparedBy { get; set; }
        public string Rev { get; set; }
        public int? Shift { get; set; }

        public string Customer { get; set; }
        public string InspectorName { get; set; }

        public string CardCaseNo { get; set; }
        public string Remarks { get; set; }
        public string PCBIncharge { get; set; }
        public string PCBIssuer { get; set; }
        public string LotNo { get; set; }
        public int DepartmentID { get; set; }
    }




    public class SummaryraceableShopOrderModel
    {
        public string FinalShopOrder { get; set; }
        public string PCBShopOrder { get; set; }
        public string Revision { get; set; }
        public string PCBA { get; set; }
        public DateTime DatePrepared { get; set; }
        public string TimeInput { get; set; }
        public int PreparedQuantity { get; set; } = 0;
        public string PreparedBy { get; set; }

        public int? Shift { get; set; }

        public string Customer { get; set; }
        public string InspectorName { get; set; }

        public string CardCaseNo { get; set; }
        public string Remarks { get; set; }
        public string PCBIncharge { get; set; }
        public string PCBIssuer { get; set; }
        public string LotNo { get; set; }
        public int DepartmentID { get; set; }
    }

    public class TracePCBModel
    {
        public int RecordId { get; set; }
        public string PCBShopOrder { get; set; }
        public string Rev { get; set; }
        public int Quantity { get; set; }
    }


    public class EditTracePCBModel
    {
        public int RecordId { get; set; }
        public string PCBShopOrder { get; set; }
        public string Rev { get; set; }
        public int Quantity { get; set; }
        public int isAction { get; set;  } // 0 - for add - 1  for edit  - 2 for delete 
    }
}
