using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMACS_V2.Areas.P1SA.Models
{
    public class DieMoldDaily
    {
        public int RecordID { get; set; }
        public string DimensionQuality { get; set; }
        public string PartNo { get; set; }
        public string DateInput { get; set; }
        public int CycleShot { get; set; }
        public int Total { get; set; }
        public int MachineNo { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string Mincharge { get; set; }
        public string ProcessID { get; set; }
        public string DieSerial { get; set; }
    }
}