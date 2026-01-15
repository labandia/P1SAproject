using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalMaskMonitoring.Model
{
    public class MetalMaskModel
    {
        public int RecordID { get;  set; }
        public string Partnumber { get;  set; }
        public int AREA { get;  set; }
        public string Alternate { get;  set; }
        public string Side { get;  set; }
        public decimal Thickness { get;  set; }
        public int Blocks { get;  set; }
        public string Condition { get;  set; }
        public string Remarks { get;  set; }
        public int? ModelType { get;  set; }

       

    }
}
