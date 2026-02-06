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
        public DateTime DateReceived { get; set; }
    }

    public class MetalMaskTransaction
    {
        public int RecordID { get; set; }

        // Base operation date (DATE only in SQL)
        public DateTime DateInput { get; set; }

        // 0 = Day, 1 = Night
        public bool? Shift { get; set; }

        public int? SMTLine { get; set; }

        public string Partnumber { get; set; } = string.Empty;
        public int AREA { get; set; }
        public int Blocks { get; set; }
        // TIME columns
        public TimeSpan SMT_start { get; set; }
        public TimeSpan SMT_end { get; set; }

        // Duration in minutes (computed by SQL)
        public int TotalTime { get; private set; }

        // Display-only (HH:MM)
        public string TotalTimeHHMM =>
            $"{TotalTime / 60:00}:{TotalTime % 60:00}";

        public int? TotalPrintBoard { get; set; }
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

   


    public class MetalMasKCountTransact
    {
        public int SMTCount { get; set; }
        public int TensionCount { get; set; }
    }

}