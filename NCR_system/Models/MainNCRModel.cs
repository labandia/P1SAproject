using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Models
{
    public class MainNCRModel
    {
        /// <summary>
        /// ALL REQUIRED FOR MAIN NCR
        /// </summary>
        public int Category { get; set; }
        public string RegNo { get; set; }
        public DateTime DateIssued { get; set; }
        public string IssueGroup { get; set; }
        public int SectionID { get; set; }

        public string ModelNo { get; set; }
        public int Quantity { get; set; }
        public string Contents { get; set; }

        public DateTime DateRegist { get; set; }
        public int ReportStatus { get; set; }
        public string FilePath { get; set; }
        public string DateCloseReg { get; set; }
        public string CircularStatus { get; set; }
        public int Reviewer { get; set; }

        /// <summary>
        /// ALL REQUIRED FOR RECURRENCE
        /// </summary>
        ///
        public string Details { get; set; }
        public int Status { get; set; } 

        public string UploadImage { get; set; }
        public int Process { get; set;  }
    }
}
