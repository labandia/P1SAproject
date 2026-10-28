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
        public byte ProcessId { get; set; }                 // FK -> EfficiencyProcess
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
        public string EffyCode { get; set; } = string.Empty;      // NVARCHAR(20), PK (e.g. S2CR1040)
        public string EmployeeCode { get; set; } = string.Empty;  // NVARCHAR(20), FK -> P1SA_Employees_Temp
        public string FullName { get; set; } = string.Empty;
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
        public string ShiftCode => Shift ? "N" : "D";
        public decimal CycleTimeSec { get; private set; }
        public decimal CurrentTarget { get; private set; }
        public string FullName { get; set; }
        public decimal? EasedCycleTimeSec { get; set; }
        public int? TargetEased { get; set; }
        public string ProcessName
        {
            get
            {
                return ProcessId == 1 ? "COVER DRILLING " :
                       ProcessId == 2 ? "IMPELLER" :
                       ProcessId == 3 ? "EPIFORM" : "";
            }
        }

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








}