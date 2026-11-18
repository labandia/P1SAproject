using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Production1.Model
{
    /// <summary>
    /// dbo.EfficiencyProcess - lookup table (EPIFORM, ...)
    /// </summary>
    public class EfficiencyProcess
    {
        public byte ProcessId { get; set; }                 // TINYINT IDENTITY, PK
        public string ProcessName { get; set; } = string.Empty;   // NVARCHAR(50), UNIQUE
    }

    /// <summary>
    /// dbo.EfficiencyCycleTime - cycle time per process + part number
    /// </summary>
    public class EfficiencyCycleTime
    {
        public int CycleID { get; set; }                    // INT IDENTITY, PK
        public byte ProcessId { get; set; } = 1;              // FK -> EfficiencyProcess
        public string Partnumber { get; set; } = string.Empty;    // NVARCHAR(50)
        public string Model { get; set; }                   // NVARCHAR(100) NULL
        public decimal CycleTimeSec { get; set; }           // DECIMAL(9,2), > 0 (current)

        // Optional join column (not in table)

        public string ProcessName
        {
            get
            {
                return ProcessId == 1 ? "COVER DRILLING " :
                       ProcessId == 2 ? "IMPELLER" :
                       ProcessId == 3 ? "EPIFORM" : "";
            }
        }
    }

    /// <summary>
    /// dbo.EfficiencyEmployeeCode - maps efficiency code to employee code
    /// </summary>
    public class EfficiencyEmployeeCode
    {
        public int EmployID { get; set; }                 // INT IDENTITY, PK
        public string EffyCode { get; set; } = string.Empty;      // NVARCHAR(20), PK (e.g. S2CR1040)
        public string EmployeeCode { get; set; } = string.Empty;  // NVARCHAR(20), FK -> P1SA_Employees_Temp
        public string FullName { get; set; } = string.Empty;
        public string Process { get; set; } = string.Empty;
        public string AgencyName { get; set; } = string.Empty;
    }

    /// <summary>
    /// dbo.EfficiencyDailyMonitor - daily efficiency record
    /// </summary>
    public class EfficiencyDailyMonitor
    {
        public int EfficiencyID { get; set; }               // INT IDENTITY, PK
        public DateTime WorkDate { get; set; } = DateTime.Today;  // DATE
        public bool Shift { get; set; }                     // BIT: false = D, true = N
        public string MachineNo { get; set; } = string.Empty;     // NVARCHAR(20), e.g. D-122
        public string ShopOrder { get; set; } = string.Empty;     // NVARCHAR(20)
        public string Partnumber { get; set; }              // NVARCHAR(50) NULL
        public int  ProcessId { get; set; }                 // FK -> EfficiencyProcess
        public string ProcessName { get; set; } = string.Empty;
        public string EffyCode { get; set; } = string.Empty;      // FK -> EfficiencyEmployeeCode
        public string getModel { get; set; }
        public int Output { get; set; }                     // >= 0
        public int DowntimeMin { get; set; }                // >= 0
        public TimeSpan StartOpe { get; set; }              // TIME(0)
        public TimeSpan EndOpe { get; set; }                // TIME(0)

        // Computed (PERSISTED) columns - read-only, do NOT include in INSERT/UPDATE
        public int DurationMin { get; private set; }        // minutes, handles overnight spans
        public decimal WorkHours { get; private set; }      // DECIMAL(5,2)

        public DateTime CreatedAt { get; set; }             // DATETIME2(0), DB default
        public string CreatedBy { get; set; }               // NVARCHAR(100) NULL

        // Optional join columns (not in table)
        public string EmployeeCode { get; set; }
        public int Target { get; set; }
        public decimal Efficiency { get; set; }
        // Helpers (not mapped)
        public string ShiftCode => Shift ? "NS" : "DS";
        public decimal CycleTimeSec { get; private set; }
        public decimal CurrentTarget { get; private set; }
        public string FullName { get; set; }
        public decimal? EasedCycleTimeSec { get; set; }
        public int? TargetEased { get; set; }
        //public string ProcessName
        //{
        //    get
        //    {
        //        return ProcessId == 1 ? "COVER DRILLING " :
        //               ProcessId == 2 ? "IMPELLER" :
        //               ProcessId == 3 ? "EPIFORM" : "";
        //    }
        //}

        public string WorkDateText => WorkDate.ToString("yyyy-MM-dd");
        public string StartOpeText => StartOpe.ToString(@"hh\:mm");
        public string EndOpeText => EndOpe.ToString(@"hh\:mm");
    }


    public class EfficiencyShopOrderStaging
    {
        public int StagingId { get; set; }
        public string ShopOrder { get; set; } = string.Empty;     // NVARCHAR(20)
        public string Partnumber { get; set; }
        public string Model { get; set; }
    }


    public class EfficiencyDailySummary
    {
        public int TotalRecords { get; set; }

        public long TotalOutput { get; set; }

        public long TotalTarget { get; set; }

        public long TotalTargetLessDP { get; set; }

        public decimal EfficiencyRate { get; set; }
    }

    public class EfficiencyManpowerSummary
    {
        public int ProcessId { get; set; }

        public string ProcessName { get; set; } = string.Empty;  
        public int Manpower { get; set; }
        public int Remaining { get; set; }
        public int Improved { get; set; }

        public int RatePct { get; set; }
    }



    public class ImportResult
    {
        public int Inserted { get; set; }
        public int Updated { get; set; }
        public List<string> Missing { get; set; } = new List<string>();
    }


    public class EfficiencyProcessReport
    {
        public int ProcessId { get; set; }

        public string ProcessName { get; set; }

        public decimal RemainingPct { get; set; }

        public decimal Target { get; set; }

        // Dynamic monthly and weekly values.
        // Example: Jan, Feb, Mar, Apr, W1, W2 ... W18.
        public Dictionary<string, decimal> PeriodValues { get; set; }
            = new Dictionary<string, decimal>();
    }

    public class EfficiencyProcessReportResponse
    {
        public List<string> PeriodColumns { get; set; }
            = new List<string>();

        public List<EfficiencyProcessReport> Data { get; set; }
            = new List<EfficiencyProcessReport>();
    }

    public class EfficiencyPeriodResult
    {
        public int ProcessId { get; set; }

        public string ProcessName { get; set; }

        public decimal RemainingPct { get; set; }

        public decimal Target { get; set; }

        public string PeriodKey { get; set; }

        public int SortOrder { get; set; }

        public decimal PeriodValue { get; set; }
    }

    public enum EfficiencyDateFilter
    {
        Today,
        ThisWeek,
        ThisMonth,
        Custom
    }
}