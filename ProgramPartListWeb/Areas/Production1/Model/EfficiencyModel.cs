using System;
using System.Collections.Generic;
using System.Linq;
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
        public decimal? EasedCycleTimeSec { get; set; }     // DECIMAL(9,2) NULL, > 0 (eased-up)
        public decimal TargetDivisor { get; set; } = 1m;    // DECIMAL(4,2), default 1

        // Optional join column (not in table)
        public string ProcessName { get; set; }
    }

    /// <summary>
    /// dbo.EfficiencyEmployeeCode - maps efficiency code to employee code
    /// </summary>
    public class EfficiencyEmployeeCode
    {
        public string EffyCode { get; set; } = string.Empty;      // NVARCHAR(20), PK (e.g. S2CR1040)
        public string EmployeeCode { get; set; } = string.Empty;  // NVARCHAR(20), FK -> P1SA_Employees_Temp
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
        public byte ProcessId { get; set; }                 // FK -> EfficiencyProcess
        public string EffyCode { get; set; } = string.Empty;      // FK -> EfficiencyEmployeeCode
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
        public string ProcessName { get; set; }
        public string EmployeeCode { get; set; }

        // Helpers (not mapped)
        public string ShiftCode => Shift ? "N" : "D";
    }
}