using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Areas.Final.Model;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace ProgramPartListWeb.Areas.Final.Services
{
    public class DownTimeServices : IDownTime
    {
        private  string strsql = @"  SELECT 
	             i.DownTimeID, 
  				 m.Line,
				 i.FinalShopOrder,
				 m.ItemNo,
				 m.Model,
				 m.PlanQty,
	             i.DownTimeCode,
	             t.DownTimeType, 
	             i.TimeStart,
	             i.TimeEnd,
	             i.Downtime, 
	             i.PIC,
	             i.Details,
                 t.GroupName, 
				CAST(
					(i.Downtime * 60.0) / NULLIF(m.PlanQty, 0)
					AS DECIMAL(10,3)
				) AS CycleTime,
				CAST(
                    CASE
                        WHEN
                            (
                                8.0 /
                                NULLIF(
                                    (i.Downtime * 60.0) / NULLIF(m.PlanQty, 0),
                                    0
                                )
                                * 100
                            ) > 100
                        THEN 100
                        ELSE CEILING(
                            8.0 /
                            NULLIF(
                                (i.Downtime * 60.0) / NULLIF(m.PlanQty, 0),
                                0
                            )
                            * 100
                        )
                    END
                    AS DECIMAL(10,0)
                ) AS OperationRate

              FROM FanTraceabilityDownTimeInput i 
              INNER JOIN FanTraceabilityManufacturingOrder m ON i.FinalShopOrder = m.FinalShopOrder
              INNER JOIN FanTraceabilityDownTimeType t ON t.DownTimeCode = i.DownTimeCode ";

        // =====================================================================
        // =============== FOR DAILY REPORT MONITORING =========================
        // =====================================================================
        public Task<List<DownTimeModel>> GetDailyReportMonitor(string search, string Linename)
        {
            string strsql = $@"SELECT 
	                FORMAT(m.DateStart, 'MM/dd/yy') as DateStart,
                    m.Line,
                    m.FinalShopOrder,
                    m.ItemNo,
                    m.Model,
                    m.PlanQty,
                    m.TimeStart,
                    m.TimeEnd,

                    -- Positive duration in minutes
                    CASE
                        WHEN m.TimeEnd >= m.TimeStart
                            THEN DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd)
                        ELSE
                            DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd) + 1440
                    END AS Downtime,

                    -- Cycle Time in seconds per unit
                    CAST(
                        (
                            CASE
                                WHEN m.TimeEnd >= m.TimeStart
                                    THEN DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd)
                                ELSE
                                    DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd) + 1440
                            END * 60.0
                        ) / NULLIF(m.PlanQty, 0)
                        AS DECIMAL(10,3)
                    ) AS CycleTime,

                    -- Operation Rate
                    CAST(
                        CASE
                            WHEN
                                (
                                    8.0 /
                                    NULLIF(
                                        (
                                            CASE
                                                WHEN m.TimeEnd >= m.TimeStart
                                                    THEN DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd)
                                                ELSE
                                                    DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd) + 1440
                                            END * 60.0
                                        ) / NULLIF(m.PlanQty, 0),
                                        0
                                    ) * 100
                                ) > 100
                            THEN 100

                            ELSE CEILING(
                                8.0 /
                                NULLIF(
                                    (
                                        CASE
                                            WHEN m.TimeEnd >= m.TimeStart
                                                THEN DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd)
                                            ELSE
                                                DATEDIFF(MINUTE, m.TimeStart, m.TimeEnd) + 1440
                                        END * 60.0
                                    ) / NULLIF(m.PlanQty, 0),
                                    0
                                ) * 100
                            )
                        END
                        AS DECIMAL(10,0)
                    ) AS OperationRate,

                    (SELECT SUM(Downtime) FROM FanTraceabilityDownTimeInput WHERE FinalShopOrder = m.FinalShopOrder)  as MachineCount 

                FROM FanTraceabilityManufacturingOrder m
                WHERE m.OrderStatus = 3
                  AND m.TimeEnd IS NOT NULL AND m.DateStart IS NOT NULL";

            var parameters = new DynamicParameters();

            // Line filter
            if (!string.IsNullOrWhiteSpace(Linename))
            {
                strsql += " AND m.Line = @Line";
                parameters.Add("@Line", Linename);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                strsql += @" AND (
                        m.FinalShopOrder LIKE @SearchPrefix)";

                parameters.Add("@SearchPrefix", $"{search}%");
            }

            strsql += " ORDER BY m.DateStart DESC";


            return SqlDataAcess_Test.QueryAsync<DownTimeModel>(strsql, parameters);
        }




        public Task<List<DownTimeModel>> GetDowntimeMonitor(string search, string Linename)
        {
            var parameters = new DynamicParameters();

            // Line filter
            if (!string.IsNullOrWhiteSpace(Linename))
            {
                strsql += " AND m.Line = @Line";
                parameters.Add("@Line", Linename);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                strsql += @" AND (
                        m.FinalShopOrder LIKE @SearchPrefix)";

                parameters.Add("@SearchPrefix", $"{search}%");
            }

            strsql += " ORDER BY i.DownTimeID DESC";


            return SqlDataAcess_Test.QueryAsync<DownTimeModel>(strsql, parameters);
        }

       

        public Task<List<DownTimeModel>> GetDowntimeMonitor(string FinalShopOrder)
        {
			var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(FinalShopOrder))
			{
				strsql += "  WHERE i.FinalShopOrder = @FinalShopOrder";
				parameters.Add("@FinalShopOrder", FinalShopOrder);
            }

            strsql += " ORDER BY i.DownTimeID DESC"; 

            return SqlDataAcess_Test.QueryAsync<DownTimeModel>(strsql, parameters);
        }

        public async Task<bool> AddGetTimeMonitor(DownTimeModel downtime)
        {
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"INSERT 
                INTO FanTraceabilityDownTimeInput(FinalShopOrder, DownTimeCode, PIC, Details) 
                VALUES(@FinalShopOrder, @DownTimeCode, @PIC, @Details)", downtime);

            return rows > 0;
        }

        public async Task<bool> EndTimeMonitor(int DownTimeID)
        {
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"UPDATE FanTraceabilityDownTimeInput SET
                TimeEnd = CAST(GETDATE() AS TIME(0)) WHERE DownTimeID =@DownTimeID", new
            {
                DownTimeID
            });

            return rows > 0;
        }

        public Task<List<DownTimeTypeModel>> GetDownTimeType()
        {
            return SqlDataAcess_Test.QueryAsync<DownTimeTypeModel>($@"SELECT DownTimeCode
                  ,DownTimeType
                  ,GroupName
              FROM FanTraceabilityDownTimeType");
        }

       
    }
}