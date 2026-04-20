using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Data
{
    public class TraceabilityFInalAssyModel
    {
        public int RecordId { get; set; }
        public string FinalShopOrder { get; set; }
        public string Revision { get; set; }
        public string ItemNo { get; set; }
        public int PlanQuan { get; set; }
        public DateTime DatePrepared { get; set; }
        public string TimeInput { get; set; }
        public string PreparedBy { get; set; }
        public int? Shift { get; set; }
        public string Customer { get; set; }
        public string Modeltype { get; set; }
        public string Remarks { get; set; }
        public string Incharge { get; set; }
        public string FinalIssuedby { get; set; }
        public int DepartmentID { get; set; }
    }
 
}
