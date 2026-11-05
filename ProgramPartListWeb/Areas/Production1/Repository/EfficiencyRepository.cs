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
            const string sql = @"
            SELECT
                        m.EfficiencyID,
                        m.WorkDate,
                        m.Shift,
                        m.MachineNo,
                        m.ShopOrder,
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

                    LEFT JOIN P1SA_Employees_Temp t
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

            return SqlDataAcess_Test.QueryAsync<EfficiencyDailyMonitor>(sql, new { start, end });
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
        public Task<bool> UploadShopOrderCSV(EfficiencyShopOrderStaging shop)
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
                   INNER JOIN P1SA_Employees_Temp e ON e.EmployeeCode = f.EmployeeCode
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
                @"INSERT INTO EfficiencyCycleTime (Partnumber, CycleTimeSec)
                  VALUES (@Partnumber, @CycleTimeSec)",
                new
                {
                    cycleTime.Partnumber,
                    cycleTime.CycleTimeSec
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

        public Task<bool> UploadOrAddCycleTime(EfficiencyCycleTime shop)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCycleTime(int cycleId)
        {
            return await SqlDataAcess_Test.ExecuteAsync(
              @"DELETE FROM EfficiencyCycleTime
                  WHERE CycleID = @CycleID",
              new  {  CycleID = cycleId }) > 0;
        }

        
    }
}