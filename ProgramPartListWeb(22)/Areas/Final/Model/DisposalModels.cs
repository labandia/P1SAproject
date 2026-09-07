using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Final.Model
{
    public class DisposalModels
    {
        public class TotalDisposalMonitor
        {
            public int ControlNumberID { get; set; }
            public string ControlNumber { get; set; }
            public string DateDisposal { get; set; }
            public int TotalQuantity { get; set; }
            public int Category { get; set; }

            public string LeaderCheck { get; set; } = string.Empty;
            public string Approveby { get; set; } = string.Empty;
            public string GuardCheck { get; set; } = string.Empty;

            public int Status { get; set; }
        }

        public class DisposalSummary
        {
            public int RecordID { get; set; }
            public string MaterialName { get; set; } = string.Empty;
            public string Container { get; set; }
            public int? Quantity { get; set; }
            public string Remarks { get; set; }
            public string Units { get; set; }
        }
        public class DisposalEmail
        {
            public string EmailAddress { get; set; }
            public string SentTo { get; set; }
            public int DepartmentID { get; set; }
        }
    }
}