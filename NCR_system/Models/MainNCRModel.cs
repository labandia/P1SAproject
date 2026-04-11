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
        public int CategoryID { get; set; }
        public string RegNo { get; set; }
        private DateTime _DateIssued;
        private string _IssueGroup;
        private int _SectionID;

        private string _ModelNo;
        private int _Quantity;
        private string _Contents;

        private DateTime _DateRegist;
        public int ReportStatus { get; set; }
        private string _FilePath;
        private string _DateCloseReg;
        private string _CircularStatus;
        private int Owners;

        /// <summary>
        /// ALL REQUIRED FOR RECURRENCE
        /// </summary>
        ///
        public string Details { get; set; }
        public int Status { get; set; } 
    }
}
