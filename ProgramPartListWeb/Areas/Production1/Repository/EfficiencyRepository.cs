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
        public Task<List<string>> GetProcessFilter()
        {
            return SqlDataAcess_Test.StringListAsync($@"SELECT ProcessName
                FROM EfficiencyDailyMonitor
                WHERE ProcessName IS NOT NULL
                GROUP BY ProcessName
                ");
        }

        // ============================================================
        // ===================== EFFICIENCY RECORDS ==========================
        //=============================================================
        public async Task<int> AddEfficiencyRecord(EfficiencyDailyMonitor row)
        {
            return await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO EfficiencyDailyMonitor
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
                  FROM EfficiencyCycleTime ORDER BY Model ASC");
        }
        public Task<List<EfficiencyDailyMonitor>> GetEfficiencyDailyMonitors(DateTime? start, DateTime? end)
        {
            Debug.WriteLine($@"Date Start : {start.ToString()} - Date End : {end.ToString()}");

            const string sql = @"
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

                    FROM EfficiencyDailyMonitor m

                    LEFT JOIN EfficiencyEmployeeCode e
                        ON e.EffyCode = m.EffyCode

                    LEFT JOIN P1SA_Employees t
                        ON t.EmployeeCode = e.EmployeeCode

                      LEFT JOIN EfficiencyCycleTime c
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
                    FROM EfficiencyShopOrder_Staging
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
                   FROM EfficiencyEmployeeCode f
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
                "SELECT COUNT(1) FROM EfficiencyEmployeeCode WHERE EffyCode = @EffyCode",
                new { employeeCode.EffyCode });

            // 2. If it exists, return false or throw an exception
            if (IsExist) return false;

            // 3. If it doesn't exist, insert the new record into the database
            return await SqlDataAcess_Test.ExecuteAsync(
                @"INSERT INTO EfficiencyEmployeeCode (EffyCode, EmployeeCode)
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
                @"UPDATE EfficiencyEmployeeCode SET EffyCode = @EffyCode 
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
             @"DELETE FROM EfficiencyEmployeeCode
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
                "SELECT COUNT(1) FROM EfficiencyCycleTime WHERE Partnumber = @Partnumber",
                new { cycleTime.Partnumber });

            // 2. If it exists, return false or throw an exception
            if (IsExist) return false;

            // 3. If it doesn't exist, insert the new record into the database
            return await SqlDataAcess_Test.ExecuteAsync(
                @"INSERT INTO EfficiencyCycleTime (Partnumber, CycleTimeSec, Model)
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
                @"UPDATE EfficiencyCycleTime SET CycleTimeSec =@CycleTimeSec, 
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
              @"DELETE FROM EfficiencyCycleTime
                  WHERE CycleID = @CycleID",
              new  {  CycleID = cycleId }) > 0;
        }

        

        public Task<EfficiencyDailySummary> GetEfficiencySummary(
            EfficiencyDailySummary filter, 
            DateTime? customStart = null, 
            DateTime? customEnd = null)
        {
            //        DateTime today = DateTime.Today;

            //        DateTime start;
            //        DateTime end;

            //        switch (filter)
            //        {
            //            case EfficiencyDateFilter.Today:
            //                start = today;
            //                end = today;
            //                break;

            //            case EfficiencyDateFilter.ThisWeek:
            //                // Monday to Sunday
            //                int diff = (7 + (int)today.DayOfWeek - (int)DayOfWeek.Monday) % 7;
            //                start = today.AddDays(-diff);
            //                end = start.AddDays(6);
            //                break;

            //            case EfficiencyDateFilter.ThisMonth:
            //                start = new DateTime(today.Year, today.Month, 1);
            //                end = start.AddMonths(1).AddDays(-1);
            //                break;

            //            case EfficiencyDateFilter.Custom:
            //                if (!customStart.HasValue || !customEnd.HasValue)
            //                    throw new ArgumentException("Custom date range is required.");

            //                start = customStart.Value.Date;
            //                end = customEnd.Value.Date;

            //                if (start > end)
            //                    throw new ArgumentException("Start date cannot be greater than end date.");

            //                break;

            //            default:
            //                start = today;
            //                end = today;
            //                break;
            //        }

            //        const string sql = @"
            //    SELECT
            //        COUNT(m.EfficiencyID) AS TotalRecords,

            //        ISNULL(SUM(m.Output), 0) AS TotalOutput,

            //        ISNULL(SUM(x.Target), 0) AS TotalTarget,

            //        ISNULL(SUM(x.TargetLessDP), 0) AS TotalTargetLessDP,

            //        CAST(
            //            CASE
            //                WHEN ISNULL(SUM(x.TargetLessDP), 0) = 0 THEN 0
            //                ELSE SUM(m.Output) * 100.0 /
            //                     SUM(x.TargetLessDP)
            //            END
            //            AS DECIMAL(10,2)
            //        ) AS EfficiencyRate

            //    FROM EfficiencyDailyMonitor m

            //    LEFT JOIN EfficiencyCycleTime c
            //        ON c.Partnumber = m.Partnumber

            //    CROSS APPLY (
            //        SELECT
            //            CASE
            //                WHEN DATEDIFF(MINUTE,
            //                    CAST(m.StartOpe AS time),
            //                    CAST(m.EndOpe AS time)) < 0
            //                THEN DATEDIFF(MINUTE,
            //                    CAST(m.StartOpe AS time),
            //                    CAST(m.EndOpe AS time)) + 1440
            //                ELSE DATEDIFF(MINUTE,
            //                    CAST(m.StartOpe AS time),
            //                    CAST(m.EndOpe AS time))
            //            END AS WorkMinutes
            //    ) w

            //    CROSS APPLY (
            //        SELECT
            //            CAST(
            //                ROUND(
            //                    (w.WorkMinutes * 60.0) /
            //                    NULLIF(c.CycleTimeSec, 0), 0
            //                ) AS INT
            //            ) AS Target,

            //            CAST(
            //                ROUND(
            //                    (m.DowntimeMin * 60.0) /
            //                    NULLIF(c.CycleTimeSec, 0), 0
            //                ) AS INT
            //            ) AS DTPics
            //    ) a

            //    CROSS APPLY (
            //        SELECT
            //            a.Target,
            //            a.Target - a.DTPics AS TargetLessDP
            //    ) x

            //    WHERE m.WorkDate >= @start
            //      AND m.WorkDate < DATEADD(DAY, 1, @end);
            //";

            //        return SqlDataAcess_Test.QuerySingleAsync<EfficiencyDailySummary>(
            //            sql,
            //            new { start, end }
            //        );

            throw new NotImplementedException();
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
    
            const string mergeSql = @"
                MERGE EfficiencyEmployeeCode AS t
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
            const string sql = @"
                    MERGE EfficiencyShopOrder_Staging AS t
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

                const string sql = @"
            MERGE EfficiencyCycleTime AS t
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
               @"UPDATE EfficiencyDailyMonitor SET WorkDate = @WorkDate, MachineNo = @MachineNo, 
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
                @"DELETE FROM EfficiencyDailyMonitor WHERE EfficiencyID = @EfficiencyID",
                new { EfficiencyID = ID });

            return result > 0;  
        }

        public enum EfficiencyDateFilter
        {
            Today = 1,
            ThisWeek = 2,
            ThisMonth = 3,
            Custom = 4
        }


    }
}