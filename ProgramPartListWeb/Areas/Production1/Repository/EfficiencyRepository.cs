using Aspose.Cells;
using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Production1.Repository
{
    public class EfficiencyRepository : IEfficiencyProcessRepository
    {
        public static int IsSwitch = 1;

        string monitortbl = IsSwitch == 0 ? "EfficiencyDailyMonitor_V2" : "EfficiencyDailyMonitor";
        string Cycletimetbl = IsSwitch == 0 ? "EfficiencyCycleTime_V2": "EfficiencyCycleTime";
        string employeetbl = IsSwitch == 0 ?  "EfficiencyEmployeeCode_V2" : "EfficiencyEmployeeCode";
        string stagingShoptbl = IsSwitch == 0 ? "EfficiencyShopOrder_Staging_V2" : "EfficiencyShopOrder_Staging";


        public Task<List<string>> GetProcessFilter()
        {
            return SqlDataAcess_Test.StringListAsync($@"SELECT ProcessName
                FROM {monitortbl}
                WHERE ProcessName IS NOT NULL
                GROUP BY ProcessName
                ");
        }

        // ============================================================
        // ===================== EFFICIENCY RECORDS ==========================
        //=============================================================
        public async Task<int> AddEfficiencyRecord(EfficiencyDailyMonitor row)
        {
            return await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO {monitortbl}
                (WorkDate, ProcessId, Shift, MachineNo,  ShopOrder, Model, EffyCode, Output, DowntimeMin, StartOpe, EndOpe, Partnumber, ProcessName) 
                VALUES(@WorkDate, @ProcessId, @Shift, @MachineNo, @ShopOrder, @Model, @EffyCode, @Output, @DowntimeMin, @StartOpe, @EndOpe, @Partnumber, @ProcessName)", new
            {
                row.WorkDate,
                row.Shift,
                row.ProcessId,
                row.MachineNo,
                row.ShopOrder,
                row.EffyCode,
                row.Output,
                row.DowntimeMin,
                row.StartOpe, 
                row.EndOpe,
                Model = row.getModel, 
                row.Partnumber, 
                row.ProcessName
            });
        }
        public Task<List<EfficiencyCycleTime>> GetEfficiencyCycleTime()
        {
            return SqlDataAcess_Test
                   .QueryAsync<EfficiencyCycleTime>($@"
                 SELECT CycleID
                      ,ProcessId
                      ,Partnumber
                      ,Model
                      ,CycleTimeSec
                      ,TargetDivisor
                  FROM {Cycletimetbl} ORDER BY Model ASC");
        }
        public Task<List<EfficiencyDailyMonitor>> GetEfficiencyDailyMonitors(DateTime? start, DateTime? end)
        {
            Debug.WriteLine($@"Date Start : {start.ToString()} - Date End : {end.ToString()}");

             string sql = $@"
                SELECT
                        m.EfficiencyID,
                        m.WorkDate,
                        m.Shift,
                        m.MachineNo,
                        m.ShopOrder,
                        m.ProcessId,
                        m.Partnumber,
                        m.Model AS getModel,
                        m.ProcessName,
                        m.EffyCode,
                        e.EmployeeCode,
                        t.FullName,
                        m.Output,
                        m.DowntimeMin,
                        m.StartOpe,
                        m.EndOpe,
                        m.DurationMin,

                        -- Working Hours (same as calcForm)
                        x.WorkHours,

                        c.CycleTimeSec,
                        c.EasedCycleTimeSec,

                        -- Target
                        x.Target,
                        x.TargetEased,

                        -- Downtime in PCS
                        x.DTPics,

                        -- Target Less Downtime
                        x.TargetLessDP,

                        -- Final Efficiency
                        CAST(
                            x.Output * 100.0 / NULLIF(x.TargetLessDP, 0)
                            AS decimal(10,2)
                        ) AS Efficiency

                    FROM {monitortbl} m

                    LEFT JOIN {employeetbl} e
                        ON e.EffyCode = m.EffyCode

                    LEFT JOIN P1SA_Employees t
                        ON t.EmployeeCode = e.EmployeeCode

                      LEFT JOIN {Cycletimetbl} c
                    ON c.Partnumber = m.Partnumber

                    CROSS APPLY (
                        SELECT
                            CASE
                                WHEN DATEDIFF(MINUTE,
                                    CAST(m.StartOpe AS time),
                                    CAST(m.EndOpe AS time)) < 0
                                THEN DATEDIFF(MINUTE,
                                    CAST(m.StartOpe AS time),
                                    CAST(m.EndOpe AS time)) + 1440
                                ELSE DATEDIFF(MINUTE,
                                    CAST(m.StartOpe AS time),
                                    CAST(m.EndOpe AS time))
                            END AS WorkMinutes
                    ) w

                    CROSS APPLY (
                        SELECT
                            CAST(w.WorkMinutes / 60.0 AS decimal(10,2)) AS WorkHours,

                            CAST(
                                ROUND(
                                    (w.WorkMinutes * 60.0) /
                                    NULLIF(c.CycleTimeSec, 0), 0
                                ) AS int
                            ) AS Target,

                            CAST(
                                FLOOR(
                                    (w.WorkMinutes * 60.0) /
                                    NULLIF(c.EasedCycleTimeSec, 0)
                                ) AS int
                            ) AS TargetEased,

                            CAST(
                                ROUND(
                                    (m.DowntimeMin * 60.0) /
                                    NULLIF(c.CycleTimeSec, 0), 0
                                ) AS int
                            ) AS DTPics,

                            m.Output
                    ) a

                    CROSS APPLY (
                        SELECT
                            a.WorkHours,
                            a.Target,
                            a.TargetEased,
                            a.DTPics,
                            a.Output,
                            a.Target - a.DTPics AS TargetLessDP
                    ) x

                    WHERE (@start IS NULL OR m.WorkDate >= @start)
                      AND (@end IS NULL OR m.WorkDate < DATEADD(DAY, 1, @end))

                    ORDER BY m.WorkDate DESC, m.EfficiencyID DESC;";

            //var p = new DynamicParameters();
            //p.Add("@start", start?.Date, DbType.Date);
            //p.Add("@end", end?.Date, DbType.Date);


            return SqlDataAcess_Test.QueryAsync<EfficiencyDailyMonitor>(sql, new
            {
                start = start?.ToString("yyyy-MM-dd"),
                end = end?.ToString("yyyy-MM-dd")
            });
        }

        // ============================================================
        // ===================== SHOP ORDERS DATA ==========================
        //=============================================================
        public Task<List<EfficiencyShopOrderStaging>> GetEfficientShopOrders(string ShopOrder)
        {
            bool hasShopOrder = !string.IsNullOrWhiteSpace(ShopOrder);

            string limit = hasShopOrder ? "TOP 100" : "TOP 20";

            string query = $@"
                    SELECT {limit}
                        StagingId,
                        ShopOrder,
                        Partnumber,
                        Model
                    FROM {stagingShoptbl}
                    WHERE 1 = 1 ";

            var parameters = new DynamicParameters();

            // Filter only when ShopOrder has a value
            if (hasShopOrder)
            {
                query += " AND ShopOrder LIKE @ShopOrder + '%' ";

                parameters.Add("@ShopOrder", ShopOrder.Trim());
            }

            query += " ORDER BY ShopOrder ASC";

            return SqlDataAcess_Test.QueryAsync<EfficiencyShopOrderStaging>(
                query,
                parameters
            );
        }
        public Task<bool> UpdateEfficiencyRecord(EfficiencyDailyMonitor row)
        {
            throw new NotImplementedException();
        }
       

        // ============================================================
        // ===================== EMPLOYEE CODE ==========================
        //=============================================================
        public Task<List<EfficiencyEmployeeCode>> GetEmployeeName(string searchText)
        {
            string query = $@"
                   SELECT f.EmployID, f.EffyCode, e.EmployeeCode, e.FullName, e.Process, e.AgencyName
                   FROM {employeetbl} f
                   INNER JOIN P1SA_Employees e ON e.EmployeeCode = f.EmployeeCode
                   WHERE 1 = 1 ";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query += @" AND (@Search IS NULL OR @Search = ''
                            OR e.FullName LIKE '%' + @Search + '%'
                            OR e.EmployeeCode LIKE '%' + @Search + '%')";

                parameters.Add("@SearchPrefix", $"{searchText}%");
            }


            return SqlDataAcess_Test
                    .QueryAsync<EfficiencyEmployeeCode>(query);
        }
        public async Task<bool> AdditionalEmployeeCode(EfficiencyEmployeeCode employeeCode)
        {
            // 1. Check if the EffyCode already exists in the database
            bool IsExist = await SqlDataAcess_Test.ExistsAsync(
                $@"SELECT COUNT(1) FROM {employeetbl} WHERE EffyCode = @EffyCode",
                new { employeeCode.EffyCode });

            // 2. If it exists, return false or throw an exception
            if (IsExist) return false;

            // 3. If it doesn't exist, insert the new record into the database
            return await SqlDataAcess_Test.ExecuteAsync(
                $@"INSERT INTO {employeetbl} (EffyCode, EmployeeCode)
                  VALUES (@EffyCode, @EmployeeCode)",
                new
                {
                    employeeCode.EffyCode,
                    employeeCode.EmployeeCode
                }) > 0;
        }
        public async Task<bool> EditEmployeeCode(EfficiencyEmployeeCode employeeCode)
        {
            // 3. If it doesn't exist, insert the new record into the database
            return await SqlDataAcess_Test.ExecuteAsync(
                $@"UPDATE {employeetbl} SET EffyCode = @EffyCode 
                  WHERE EmployeeCode = @EmployeeCode",
                new
                {
                    employeeCode.EffyCode,
                    employeeCode.EmployeeCode
                }) > 0;
        }
        public async Task<bool> DeleteEmployeeCode(int EmployID)
        {
            return await SqlDataAcess_Test.ExecuteAsync(
             $@"DELETE FROM {employeetbl}
                  WHERE EmployID = @EmployID",
             new { EmployID = EmployID }) > 0;
        }

        // ============================================================
        // ===================== CYCLE TIME ==========================
        //=============================================================
        public async Task<bool> AddCycleTime(EfficiencyCycleTime cycleTime)
        {
            // 1. Check if the EffyCode already exists in the database
            bool IsExist = await SqlDataAcess_Test.ExistsAsync(
                $@"SELECT COUNT(1) FROM {Cycletimetbl} WHERE Partnumber = @Partnumber",
                new { cycleTime.Partnumber });

            // 2. If it exists, return false or throw an exception
            if (IsExist) return false;

            // 3. If it doesn't exist, insert the new record into the database
            return await SqlDataAcess_Test.ExecuteAsync(
                $@"INSERT INTO {Cycletimetbl} (Partnumber, CycleTimeSec, Model)
                  VALUES (@Partnumber, @CycleTimeSec, @Model)",
                new
                {
                    cycleTime.Partnumber,
                    cycleTime.CycleTimeSec,
                    cycleTime.Model 
                }) > 0;
        }

        public async Task<bool> UpdateCycleTime(EfficiencyCycleTime cycleTime)
        {
            // 3. If it doesn't exist, insert the new record into the database
            return await SqlDataAcess_Test.ExecuteAsync(
                $@"UPDATE {Cycletimetbl} SET CycleTimeSec =@CycleTimeSec, 
                  Partnumber =@Partnumber, Model =@Model
                  WHERE CycleID = @CycleID",
                new
                {
                    cycleTime.CycleTimeSec,
                    cycleTime.Partnumber,
                    cycleTime.Model,
                    cycleTime.CycleID
                }) > 0;
        }

      

        public async Task<bool> DeleteCycleTime(int cycleId)
        {
            return await SqlDataAcess_Test.ExecuteAsync(
              $@"DELETE FROM {Cycletimetbl}
                  WHERE CycleID = @CycleID",
              new  {  CycleID = cycleId }) > 0;
        }

        

        public async Task<List<EfficiencyManpowerSummary>> GetEfficiencySummary(
            EfficiencyDateFilter filter,
            DateTime? customStart = null,
            DateTime? customEnd = null)
        {
            DateTime today = DateTime.Today;
            DateTime start;
            DateTime end;


            Debug.WriteLine("SElected Filter : " + filter);
            switch (filter)
            {
                case EfficiencyDateFilter.Today:
                    start = today;
                    end = today;
                    break;

                case EfficiencyDateFilter.ThisWeek:
                    // Monday to Sunday
                    int diff = (7 + (int)today.DayOfWeek - (int)DayOfWeek.Monday) % 7;
                    start = today.AddDays(-diff);
                    end = start.AddDays(6);
                    break;

                case EfficiencyDateFilter.ThisMonth:
                    start = new DateTime(today.Year, today.Month, 1);
                    end = start.AddMonths(1).AddDays(-1);
                    break;

                case EfficiencyDateFilter.Custom:
                    if (!customStart.HasValue || !customEnd.HasValue)
                        throw new ArgumentException("Custom date range is required.");

                    start = customStart.Value.Date;
                    end = customEnd.Value.Date;

                    if (start > end)
                        throw new ArgumentException("Start date cannot be greater than end date.");

                    break;

                default:
                    start = today;
                    end = today;
                    break;
            }

            Debug.WriteLine($@"Date Start : {start} - Date End : {end}");

            const string sql = @"
                ;WITH Processes AS (
                    SELECT *
                    FROM (VALUES
                        (1, '3mm Cover Drilling'),
                        (2, '3mm Impeller Drilling'),
                        (3, '3mm Compound Application'),
                        (4, '5mm Compound Application'),
                        (5, '6mm Compound Application'),
                        (6, '8mm Compound Application')
                    ) AS p (ProcessId, ProcessName)
                ),
                Base AS (
                    SELECT
                        m.ProcessId,
                        m.EffyCode,
                        CASE
                            WHEN x.Output * 100.0 / NULLIF(x.TargetLessDP, 0) >= 100 THEN 1
                            ELSE 0
                        END AS Achieved
                    FROM EfficiencyDailyMonitor_V2 m
                    LEFT JOIN EfficiencyCycleTime c
                        ON c.Partnumber = m.Partnumber
                       AND c.ProcessId  = m.ProcessId
                    CROSS APPLY (
                        SELECT CASE
                            WHEN DATEDIFF(MINUTE, CAST(m.StartOpe AS time), CAST(m.EndOpe AS time)) < 0
                            THEN DATEDIFF(MINUTE, CAST(m.StartOpe AS time), CAST(m.EndOpe AS time)) + 1440
                            ELSE DATEDIFF(MINUTE, CAST(m.StartOpe AS time), CAST(m.EndOpe AS time))
                        END AS WorkMinutes
                    ) w
                    CROSS APPLY (
                        SELECT
                            CAST(ROUND((w.WorkMinutes * 60.0) / NULLIF(c.CycleTimeSec, 0), 0) AS int) AS Target,
                            CAST(ROUND((m.DowntimeMin * 60.0) / NULLIF(c.CycleTimeSec, 0), 0) AS int) AS DTPics
                    ) a
                    CROSS APPLY (
                        SELECT a.Target - a.DTPics AS TargetLessDP,
                               m.Output            AS Output
                    ) x
                    WHERE m.ProcessId BETWEEN 1 AND 6
                      AND m.WorkDate >= @start
                      AND m.WorkDate < DATEADD(DAY, 1, @end)
                ),
                Summary AS (
                    SELECT
                        p.ProcessId,
                        p.ProcessName,
                        COUNT(DISTINCT b.EffyCode)                             AS Manpower,
                        COALESCE(SUM(CASE WHEN b.Achieved = 0 THEN 1 END), 0) AS Remaining,
                        COALESCE(SUM(b.Achieved), 0)                           AS Improved
                    FROM Processes p
                    LEFT JOIN Base b
                        ON b.ProcessId = p.ProcessId
                    GROUP BY p.ProcessId, p.ProcessName
                )
                SELECT
                    ProcessId,
                    ProcessName,
                    Manpower,
                    Remaining,
                    Improved,
                    CAST(100.0 * Improved / NULLIF(Manpower, 0) AS decimal(10,2)) AS RatePct
                FROM Summary
                UNION ALL
                SELECT
                    99,
                    'TOTAL',
                    (SELECT COUNT(DISTINCT EffyCode) FROM Base),
                    SUM(Remaining),
                    SUM(Improved),
                    CAST(100.0 * SUM(Improved) / NULLIF((SELECT COUNT(DISTINCT EffyCode) FROM Base), 0) AS decimal(10,2))
                FROM Summary
                ORDER BY ProcessId;";

            var rows = await SqlDataAcess_Test.QueryAsync<EfficiencyManpowerSummary>(
                sql,
                new { start, end }
            );

            return rows;

            //throw new NotImplementedException();
        }

        public async Task<ImportResult> UploadEmployeeCodes(List<EfficiencyEmployeeCode> rows)
        {
            const string openJson = @"OPENJSON(@json) WITH (
            EffyCode     NVARCHAR(20) '$.EffyCode',
            EmployeeCode NVARCHAR(20) '$.EmployeeCode')";

            // 1) employee IDs that don't exist (the FK would fail the whole chunk)
            const string missingSql = @"
                SELECT DISTINCT s.EmployeeCode
                FROM " + openJson + @" AS s
                WHERE NOT EXISTS (SELECT 1 FROM dbo.P1SA_Employees e 
                WHERE e.EmployeeCode = s.EmployeeCode);";

                    // 2) upsert on EffyCode; EmployeeCode is set on both insert and update
    
             string mergeSql = $@"
                MERGE {employeetbl} AS t
                USING (SELECT s.* FROM " + openJson + @" AS s
                       WHERE EXISTS (SELECT 1 FROM dbo.P1SA_Employees e
                                     WHERE e.EmployeeCode = s.EmployeeCode)) AS s
                   ON t.EffyCode = s.EffyCode
                WHEN MATCHED THEN
                    UPDATE SET t.EmployeeCode = s.EmployeeCode
                WHEN NOT MATCHED THEN
                    INSERT (EffyCode, EmployeeCode)
                    VALUES (s.EffyCode, s.EmployeeCode)
                OUTPUT $action;";

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(rows);
            var result = new ImportResult();

            result.Missing = await SqlDataAcess_Test.QueryAsync<string>(missingSql, new { json });   // still computed, no early return

            var actions = await SqlDataAcess_Test.QueryAsync<string>(mergeSql, new { json });
            result.Inserted = actions.Count(a => a == "INSERT");
            result.Updated = actions.Count(a => a == "UPDATE");
            return result;
        }

        public async Task<ImportResult> UploadShopOrderCSV(List<EfficiencyShopOrderStaging> rows)
        {
             string sql = $@"
                    MERGE {stagingShoptbl} AS t
                    USING (SELECT * FROM OPENJSON(@json) WITH (
                                ShopOrder  NVARCHAR(20) '$.ShopOrder',
                                Partnumber NVARCHAR(50) '$.Partnumber',
                                Model      NVARCHAR(50) '$.Model')) AS s
                       ON t.ShopOrder = s.ShopOrder
                    WHEN MATCHED THEN
                        UPDATE SET t.Partnumber = s.Partnumber, t.Model = s.Model
                    WHEN NOT MATCHED THEN
                        INSERT (ShopOrder, Partnumber, Model)
                        VALUES (s.ShopOrder, s.Partnumber, s.Model)
                    OUTPUT $action;";

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(rows);

            var actions = await SqlDataAcess_Test.QueryAsync<string>(sql, new { json });

            return new ImportResult
            {
                Inserted = actions.Count(a => a == "INSERT"),
                Updated = actions.Count(a => a == "UPDATE")
            };
        }


        public async Task<ImportResult> UploadCycleTimes(
            List<EfficiencyCycleTime> rows)
        {
            try
            {
                if (rows == null || rows.Count == 0)
                    return new ImportResult();

                 string sql = $@"
                    MERGE {Cycletimetbl} AS t
                    USING
                    (
                        SELECT *
                        FROM OPENJSON(@json)
                        WITH
                        (
                            Partnumber   NVARCHAR(50)  '$.Partnumber',
                            Model        NVARCHAR(100) '$.Model',
                            ProcessId    TINYINT       '$.ProcessId',
                            CycleTimeSec DECIMAL(9,2)  '$.CycleTimeSec'
                        )
                    ) AS s

                    ON t.Partnumber = s.Partnumber
                    AND t.ProcessId = s.ProcessId

                    WHEN MATCHED THEN
                        UPDATE SET
                            t.Model = s.Model,
                            t.CycleTimeSec = s.CycleTimeSec

                    WHEN NOT MATCHED THEN
                        INSERT
                        (
                            Partnumber,
                            Model,
                            ProcessId,
                            CycleTimeSec
                        )
                        VALUES
                        (
                            s.Partnumber,
                            s.Model,
                            s.ProcessId,
                            s.CycleTimeSec
                        )

                    OUTPUT $action;
                ";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(rows);

                var actions = await SqlDataAcess_Test.QueryAsync<string>(
                    sql,
                    new { json }
                );

                var result = new ImportResult
                {
                    Inserted = actions.Count(a => a == "INSERT"),
                    Updated = actions.Count(a => a == "UPDATE")
                };

                Debug.WriteLine("========== CYCLE TIME IMPORT ==========");
                Debug.WriteLine("Total Uploaded: " + rows.Count);
                Debug.WriteLine("Inserted: " + result.Inserted);
                Debug.WriteLine("Updated: " + result.Updated);
                Debug.WriteLine("Total Processed: " +
                    (result.Inserted + result.Updated));
                Debug.WriteLine("========================================");

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CYCLE TIME IMPORT ERROR: " + ex);
                throw;
            }
        }

        public async Task<int> EditEfficiencyRecord(EfficiencyDailyMonitor result)
        {
            Debug.WriteLine("========== CYCLE TIME IMPORT ==========");
            Debug.WriteLine("Efficiency : " + result.EfficiencyID);
            Debug.WriteLine("MachineNo: " + result.MachineNo);
            Debug.WriteLine("ShopOrder: " + result.ShopOrder);
            Debug.WriteLine("Process ID: " + result.ProcessId);
            Debug.WriteLine("Total Processed: " +
                (result.Partnumber + result.getModel));
            Debug.WriteLine("========================================");


            return await SqlDataAcess_Test.ExecuteAsync(
               $@"UPDATE {monitortbl} SET WorkDate = @WorkDate, MachineNo = @MachineNo, 
                 ShopOrder = @ShopOrder, Model = @Model, EffyCode = @EffyCode, Output = @Output, 
                 DowntimeMin = @DowntimeMin, StartOpe = @StartOpe, EndOpe = @EndOpe, Partnumber = @Partnumber, 
                 ProcessName = @ProcessName
                 WHERE EfficiencyID = @EfficiencyID", new
               {
                   result.EfficiencyID,
                   result.WorkDate,
                   result.MachineNo,
                   result.ShopOrder,
                   Model = result.getModel,
                   result.EffyCode,
                   result.Output,
                   result.DowntimeMin,
                   result.StartOpe,
                   result.EndOpe,
                   result.Partnumber,
                   result.ProcessName   
               });

        }

        public async Task<bool> DeleteEfficiencyRecord(int ID)
        {
             int result = await SqlDataAcess_Test.ExecuteAsync(
                $@"DELETE FROM {monitortbl} WHERE EfficiencyID = @EfficiencyID",
                new { EfficiencyID = ID });

            return result > 0;  
        }

        public async Task<EfficiencyProcessReportResponse> GetProcessLevelEfficiency(int year, int startMonth, int endMonth)
        {
            try
            {
                // =====================================
                // VALIDATION
                // =====================================
                if (year < 2000 || year > 9999)
                    throw new ArgumentOutOfRangeException(nameof(year));

                if (startMonth < 1 || startMonth > 12)
                    throw new ArgumentOutOfRangeException(nameof(startMonth));

                if (endMonth < 1 || endMonth > 12)
                    throw new ArgumentOutOfRangeException(nameof(endMonth));

                if (startMonth > endMonth)
                    throw new ArgumentException(
                        "Invalid month range.");

                // =====================================
                // SQL QUERY
                // =====================================
                string sql = @"

        DECLARE @TargetPct DECIMAL(10,2) = 85.00;

        DECLARE @ReportStart DATE =
            DATEFROMPARTS(@Year, @StartMonth, 1);

        DECLARE @ReportEndExclusive DATE =
            DATEADD(
                MONTH, 1,
                DATEFROMPARTS(@Year, @EndMonth, 1)
            );

        ;WITH Processes AS
        (
            SELECT *
            FROM (VALUES
                (1, '3mm Cover Drilling'),
                (2, '3mm Impeller Drilling'),
                (3, '3mm Compound Application'),
                (4, '5mm Compound Application'),
                (5, '6mm Compound Application'),
                (6, '8mm Compound Application')
            ) AS p(ProcessId, ProcessName)
        ),

        -- =====================================
        -- MONTH GENERATION
        -- =====================================
        Months AS
        (
            SELECT
                1 AS n,
                @ReportStart AS StartDate

            UNION ALL

            SELECT
                n + 1,
                DATEADD(MONTH, 1, StartDate)
            FROM Months
            WHERE DATEADD(MONTH, 1, StartDate)
                  < @ReportEndExclusive
        ),

        -- =====================================
        -- WEEK GENERATION
        -- =====================================
        Weeks AS
        (
            SELECT
                CAST(1 AS INT) AS n,
                @ReportStart AS StartDate

            UNION ALL

            SELECT
                n + 1,
                DATEADD(DAY, 7, StartDate)
            FROM Weeks
            WHERE DATEADD(DAY, 7, StartDate)
                  < @ReportEndExclusive
        ),

        -- =====================================
        -- MONTHLY + WEEKLY PERIODS
        -- =====================================
        Periods AS
        (
            SELECT
                CASE MONTH(StartDate)
                    WHEN 1 THEN 'Jan'
                    WHEN 2 THEN 'Feb'
                    WHEN 3 THEN 'Mar'
                    WHEN 4 THEN 'Apr'
                    WHEN 5 THEN 'May'
                    WHEN 6 THEN 'Jun'
                    WHEN 7 THEN 'Jul'
                    WHEN 8 THEN 'Aug'
                    WHEN 9 THEN 'Sep'
                    WHEN 10 THEN 'Oct'
                    WHEN 11 THEN 'Nov'
                    WHEN 12 THEN 'Dec'
                END AS PeriodKey,

                n AS SortOrder,
                StartDate,
                DATEADD(MONTH, 1, StartDate) AS EndDate

            FROM Months

            UNION ALL

            SELECT
                'W' + CAST(n AS VARCHAR(3)),
                1000 + n,
                StartDate,

                CASE
                    WHEN DATEADD(DAY, 7, StartDate)
                         > @ReportEndExclusive
                    THEN @ReportEndExclusive

                    ELSE DATEADD(DAY, 7, StartDate)
                END

            FROM Weeks
        ),

        -- =====================================
        -- BASE PRODUCTION DATA
        -- =====================================
        Base AS
        (
            SELECT
                m.ProcessId,
                m.EffyCode,
                m.WorkDate,
                ISNULL(m.Output, 0) AS Output,

                x.TargetLessDP,

                CASE
                    WHEN x.TargetLessDP > 0
                         AND ISNULL(m.Output, 0) * 100.0 /
                             NULLIF(x.TargetLessDP, 0) >= 100
                    THEN 1
                    ELSE 0
                END AS Achieved

            FROM EfficiencyDailyMonitor_V2 m

            LEFT JOIN EfficiencyCycleTime c
                ON c.Partnumber = m.Partnumber
               AND c.ProcessId = m.ProcessId

            CROSS APPLY
            (
                SELECT
                    CASE
                        WHEN DATEDIFF(
                            MINUTE,
                            CAST(m.StartOpe AS TIME),
                            CAST(m.EndOpe AS TIME)
                        ) < 0

                        THEN DATEDIFF(
                            MINUTE,
                            CAST(m.StartOpe AS TIME),
                            CAST(m.EndOpe AS TIME)
                        ) + 1440

                        ELSE DATEDIFF(
                            MINUTE,
                            CAST(m.StartOpe AS TIME),
                            CAST(m.EndOpe AS TIME)
                        )
                    END AS WorkMinutes
            ) w

            CROSS APPLY
            (
                SELECT
                    CAST(
                        ROUND(
                            w.WorkMinutes * 60.0 /
                            NULLIF(c.CycleTimeSec, 0),
                            0
                        ) AS INT
                    ) AS Target,

                    CAST(
                        ROUND(
                            ISNULL(m.DowntimeMin, 0) * 60.0 /
                            NULLIF(c.CycleTimeSec, 0),
                            0
                        ) AS INT
                    ) AS DTPics
            ) a

            CROSS APPLY
            (
                SELECT
                    a.Target - a.DTPics AS TargetLessDP
            ) x

            WHERE m.ProcessId BETWEEN 1 AND 6
              AND m.WorkDate >= @ReportStart
              AND m.WorkDate < @ReportEndExclusive
        ),

        -- =====================================
        -- EMPLOYEE ACHIEVEMENT
        -- =====================================
        PersonProcess AS
        (
            SELECT
                ProcessId,
                EffyCode,
                MAX(Achieved) AS Achieved

            FROM Base

            WHERE EffyCode IS NOT NULL

            GROUP BY
                ProcessId,
                EffyCode
        ),

        -- =====================================
        -- REMAINING MANPOWER
        -- =====================================
        Remain AS
        (
            SELECT
                ProcessId,

                SUM(
                    CASE
                        WHEN Achieved = 0 THEN 1
                        ELSE 0
                    END
                ) AS RemainingCount

            FROM PersonProcess

            GROUP BY ProcessId
        ),

        -- =====================================
        -- PROCESS MONTHLY AND WEEKLY OUTPUT
        -- =====================================
        ProcessPeriod AS
        (
            SELECT
                p.ProcessId,
                p.ProcessName,
                per.PeriodKey,
                per.SortOrder,

                SUM(ISNULL(b.Output, 0)) AS PeriodValue

            FROM Processes p

            CROSS JOIN Periods per

            LEFT JOIN Base b
                ON b.ProcessId = p.ProcessId
               AND b.WorkDate >= per.StartDate
               AND b.WorkDate < per.EndDate

            GROUP BY
                p.ProcessId,
                p.ProcessName,
                per.PeriodKey,
                per.SortOrder
        ),

        -- =====================================
        -- TOTAL OUTPUT
        -- =====================================
        TotalPeriod AS
        (
            SELECT
                99 AS ProcessId,
                'TOTAL' AS ProcessName,
                per.PeriodKey,
                per.SortOrder,

                SUM(ISNULL(b.Output, 0)) AS PeriodValue

            FROM Periods per

            LEFT JOIN Base b
                ON b.WorkDate >= per.StartDate
               AND b.WorkDate < per.EndDate

            GROUP BY
                per.PeriodKey,
                per.SortOrder
        ),

        AllPeriodData AS
        (
            SELECT * FROM ProcessPeriod

            UNION ALL

            SELECT * FROM TotalPeriod
        )

        -- =====================================
        -- FINAL RESULT
        -- =====================================
        SELECT
            p.ProcessId,
            p.ProcessName,

            CAST(
                CASE
                    WHEN p.ProcessId = 99 THEN 100.0

                    ELSE
                        100.0 * ISNULL(r.RemainingCount, 0) /
                        NULLIF(t.TotalRemaining, 0)
                END AS DECIMAL(10,2)
            ) AS RemainingPct,

            @TargetPct AS Target,

            p.PeriodKey,
            p.SortOrder,

            CAST(
                ISNULL(p.PeriodValue, 0)
                AS DECIMAL(18,2)
            ) AS PeriodValue

        FROM AllPeriodData p

        LEFT JOIN Remain r
            ON r.ProcessId = p.ProcessId

        CROSS JOIN
        (
            SELECT SUM(RemainingCount) AS TotalRemaining
            FROM Remain
        ) t

        ORDER BY
            CASE WHEN p.ProcessId = 99 THEN 1 ELSE 0 END,
            p.ProcessId,
            p.SortOrder

        OPTION (MAXRECURSION 100);
        ";

                // =====================================
                // EXECUTE USING YOUR SQL HELPER
                // =====================================
                var parameters = new
                {
                    Year = year,
                    StartMonth = startMonth,
                    EndMonth = endMonth
                };

                List<EfficiencyPeriodResult> result =
                    await SqlDataAcess_Test.QueryAsync<EfficiencyPeriodResult>(
                        sql,
                        parameters
                    );

                // =====================================
                // BUILD DYNAMIC RESPONSE
                // =====================================
                var response = new EfficiencyProcessReportResponse();

                response.PeriodColumns = result
                    .GroupBy(x => new
                    {
                        x.PeriodKey,
                        x.SortOrder
                    })
                    .OrderBy(x => x.Key.SortOrder)
                    .Select(x => x.Key.PeriodKey)
                    .ToList();

                var grouped = result.GroupBy(x => new
                {
                    x.ProcessId,
                    x.ProcessName
                });

                foreach (var group in grouped)
                {
                    var first = group.First();

                    var item = new EfficiencyProcessReport
                    {
                        ProcessId = first.ProcessId,
                        ProcessName = first.ProcessName,
                        RemainingPct = first.RemainingPct,
                        Target = first.Target
                    };

                    foreach (string column in response.PeriodColumns)
                    {
                        item.PeriodValues[column] = 0;
                    }

                    foreach (var period in group)
                    {
                        item.PeriodValues[period.PeriodKey] =
                            period.PeriodValue;
                    }

                    response.Data.Add(item);
                }

                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "GetProcessLevelEfficiency Error: " + ex
                );

                throw;
            }
        }
    }
}