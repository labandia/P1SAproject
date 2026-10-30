using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Helper;
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

        public Task<EfficiencyDailyMonitor> GetByIdAsync(int efficiencyId)
        {
            throw new NotImplementedException();
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
                  FROM EfficiencyCycleTime");
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

        public Task<List<EfficiencyShopOrderStaging>> GetEfficiencyShopOrder()
        {
            throw new NotImplementedException();
        }

        public Task<List<EfficiencyShopOrderStaging>> GetEfficientShopOrders(string ShopOrder)
        {
            return SqlDataAcess_Test.QueryAsync<EfficiencyShopOrderStaging>(@"
                SELECT TOP 20 StagingId, ShopOrder, Partnumber, Model
                FROM EfficiencyShopOrder_Staging
                WHERE ShopOrder LIKE @ShopOrder + '%'
                ORDER BY ShopOrder",
               new { ShopOrder });
        }

        public Task<List<EfficiencyEmployeeCode>> GetEmployeeName(string searchText)
        {
            string query = $@"
                   SELECT f.EffyCode, e.EmployeeCode, e.FullName, e.Process, e.AgencyName
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


            return  SqlDataAcess_Test
                    .QueryAsync<EfficiencyEmployeeCode>(query);
        }

        public Task<List<string>> GetProcessFilter()
        {
            return SqlDataAcess_Test.StringListAsync($@"SELECT ProcessName
                FROM EfficiencyDailyMonitor
                WHERE ProcessName IS NOT NULL
                GROUP BY ProcessName
                ");
        }

        public Task<bool> UpdateEfficiencyRecord(EfficiencyDailyMonitor row)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadOrAddCycleTime(EfficiencyCycleTime shop)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadShopOrderCSV(EfficiencyShopOrderStaging shop)
        {
            throw new NotImplementedException();
        }
    }
}