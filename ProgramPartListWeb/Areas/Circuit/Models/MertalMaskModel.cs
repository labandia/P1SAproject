using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Circuit.Models
{
    public class MetalMaskModel
    {
        public int RecordID { get; set; }
        public string Partnumber { get; set; }
        public int AREA { get; set; }
        public string Alternate { get; set; }
        public string Side { get; set; }
        public decimal Thickness { get; set; }
        public int Blocks { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public int? ModelType { get; set; }
        public string PWB_Blocks { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateManufacture { get; set; }
    }

    public class MetalMaskTransaction
    {
        public int RecordID { get; private set; }

        public DateTime DateInput { get; set; }
        public bool? Shift { get; set; }        // false = Day, true = Night
        public int SMTLine { get; set; } = 0;

        public string Partnumber { get; set; } = string.Empty;
        public int AREA { get; set; } = 0;

        // SMT Line Timing
        public DateTime SMT_start { get; set; }
        public DateTime SMT_end { get; set; }

        // Computed column (read-only)
        public int TotalTime { get; private set; }

        public int TotalPrintBoard { get; set; }
        public string SMT_Operator { get; set; }

        public DateTime CleanDate { get; set; }
        public string Pattern { get; set; }
        public string Frame { get; set; }
        public string RevisionNo { get; set; }

        public int ReadOne { get; set; }
        public int ReadTwo { get; set; }
        public int ReadThree { get; set; }
        public int ReadFour { get; set; }

        public string Result { get; set; }
        public string Remarks { get; set; }
        public string PIC { get; set; }

        /// <summary>
        /// 0 = Washing, 1 = Tension, 2 = Brushing, 3 = Completed
        /// </summary>
        public int Status { get; set; }

        public bool IsDelete { get; set; }

    }


}