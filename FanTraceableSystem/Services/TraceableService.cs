using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using MSDMonitoring.Data;

namespace FanTraceableSystem.Services
{
    public class TraceableService : ITraceable
    {
        public Task<List<TraceableOverAllSummaryModel>> TraceableShopOrder(
                string search,
                DateTime? startDate,
                DateTime? endDate, 
                int isEdit, 
                int section,
                int pageNumber,
                int pageSize)
        {

            string sql = @"
                     SELECT  
                           f.RecordId,
                           f.FinalShopOrder,
                           s.ShopOrder,
                           f.Revision,
                           f.ItemNo,
                           f.PlanQuan,
                           s.Line,
                           f.DatePrepared,
                           FORMAT(f.TimeInput, 'hh:mm tt') AS TimeInput,
                           s.PreparedQuantity,
                           f.PreparedBy,
                           f.Shift,
                           f.Customer,
                           f.Modeltype,
                           f.Remarks,
                           f.Incharge,
                           f.FinalIssuedby,
                           s.LotNo,
                           s.Rev,
                           f.IsDeletedFinal,
                           f.DepartmentID,
                           s.SubAssyIssued
                    FROM FanTraceabilityFinal f
                    LEFT JOIN FanTraceabilitySub s  
                        ON f.RecordId = s.FinalId
                        AND s.ShopOrder IS NOT NULL   
                    WHERE f.IsDeletedFinal = 0 ";

            var parameters = new DynamicParameters();

            if (section != 0)
            {
                sql += " AND f.DepartmentID =@DepartmentID";
                parameters.Add("@DepartmentID", section);
            }


            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND f.FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", search);
            }

            // 📅 Start Date filter
            if (startDate.HasValue)
            {
                sql += " AND f.DatePrepared >= @StartDate";
                parameters.Add("@StartDate", startDate.Value.Date);
            }

            // 📅 End Date filter (inclusive)
            if (endDate.HasValue)
            {
                sql += " AND f.DatePrepared < DATEADD(DAY, 1, @EndDate)";
                parameters.Add("@EndDate", endDate.Value.Date);
            }

            sql += @"
                ORDER BY f.RecordId DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Offset", (pageNumber - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            return SqlDataAccess.GetData<TraceableOverAllSummaryModel>(sql, parameters); 
        }


        public async Task<FinalTraceabilityModel> TraceAbilityFinalAssy(string final, int depart)
        {
            var data = await SqlDataAccess.GetData<FinalTraceabilityModel>(
              @"SELECT 
                   RecordId, 
                   FinalShopOrder,
                   PlanQuan,
                   Revision,  
                   ItemNo,    
                   DatePrepared,
                   TimeInput,
                   PreparedBy,
                   Shift,
                   Customer,
                   Modeltype,
                   Remarks,
                   Incharge,
                   FinalIssuedby
              FROM FanTraceabilityFinal 
              WHERE FinalShopOrder = @FinalShopOrder 
                    AND DepartmentID =@DepartmentID",
            new { FinalShopOrder = final, DepartmentID = depart });

            return data.FirstOrDefault();
        }

    

        public Task<int> GetTraceableCount(string search, DateTime? startDate, DateTime? endDate, int isEdit, int section)
        {
            string sql = @"SELECT  COUNT(*)
                       FROM FanTraceabilityFinal f
	                   LEFT JOIN FanTraceabilitySub s ON s.FinalShopOrder = f.FinalShopOrder
                       WHERE f.IsDeletedFinal = 0 AND ShopOrder IS NOT NULL ";

            var parameters = new DynamicParameters();

            if (section != 0)
            {
                sql += " AND f.DepartmentID = @DepartmentID";
                parameters.Add("@DepartmentID", section);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND f.FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", search);
            }

            if (startDate.HasValue && isEdit == 1)
            {
                sql += " AND f.DatePrepared >= @StartDate";
                parameters.Add("@StartDate", startDate.Value.Date);
            }

            if (endDate.HasValue && isEdit == 1)
            {
                sql += " AND f.DatePrepared < DATEADD(DAY, 1, @EndDate)";
                parameters.Add("@EndDate", endDate.Value.Date);
            }

            return  SqlDataAccess.GetCountData(sql, parameters);
        }


        public async Task<bool> AddTraceTransactions(FinalTraceabilityModel trac, List<TraceableSubAssyModel> pcb)
        {
            try
            {
                var finalId = await SqlDataAccess.ExecuteScalarAsync<int>(@"
                INSERT INTO FanTraceabilityFinal
                (FinalShopOrder, PlanQuan, Revision, ItemNo, DatePrepared, TimeInput, PreparedBy, Shift, Customer, Modeltype,
                 Incharge, Remarks, FinalIssuedby, DepartmentID)
                OUTPUT INSERTED.RecordId
                VALUES
                (@FinalShopOrder, @PlanQuan, @Revision, @ItemNo, @DatePrepared, @TimeInput, @PreparedBy, @Shift, @Customer, @Modeltype,
                 @Incharge, @Remarks, @FinalIssuedby, @DepartmentID)",
                trac);

                // if the input has a Sub Assy Data
                if (pcb.Count != 0)
                {
                    foreach (var items in pcb)
                    {
                        await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceabilitySub
                        (FinalId, FinalShopOrder, ShopOrder, PreparedQuantity, LotNo, Line, SubAssyIssued, DepartmentID)
                        VALUES(@FinalId, @FinalShopOrder, @ShopOrder, @PreparedQuantity, @LotNo, @Line, @SubAssyIssued, @DepartmentID)", new
                        {
                            FinalId = finalId,
                            trac.FinalShopOrder,
                            items.ShopOrder,
                            items.LotNo,
                            items.PreparedQuantity,
                            items.Line,
                            items.SubAssyIssued,
                            trac.DepartmentID
                        });
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> EditTraceTransaction(FinalTraceabilityModel trac, BindingList<TraceableSubAssyModel> pcb)
        {
            await SqlDataAccess.UpdateInsertQuery($@"UPDATE FanTraceabilityFinal SET Revision =@Revision, 
                ItemNo =@ItemNo, PreparedBy =@PreparedBy, Customer =@Customer, Modeltype =@Modeltype, Remarks =@Remarks,  
                Incharge =@Incharge, FinalIssuedby =@FinalIssuedby WHERE RecordId =@RecordId", new
            {
                trac.RecordId,
                trac.Revision,
                trac.ItemNo,
                trac.PreparedBy,
                trac.Customer,
                trac.Modeltype,
                trac.Remarks,
                trac.Incharge,
                trac.FinalIssuedby
            });




            if (pcb.Count != 0)
            {
                foreach(var items in pcb)
                {
                    if(items.isAction == 0)
                    {
                        await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceabilitySub(FinalShopOrder, ShopOrder, 
                        PreparedQuantity, LotNo, Line, SubAssyIssued, Rev)
                        VALUES(@FinalShopOrder, @ShopOrder, @PreparedQuantity, @LotNo, @Line, @SubAssyIssued, @Rev)", new
                        {
                            trac.FinalShopOrder,
                            items.ShopOrder,
                            items.LotNo,
                            items.PreparedQuantity,
                            items.Line,
                            items.SubAssyIssued
                        });
                    }
                    else
                    {
                        await SqlDataAccess.UpdateInsertQuery($@"UPDATE FanTraceabilitySub
                                                SET ShopOrder =@ShopOrder, 
                                                     Line =@Line,
                                                     Rev = @Rev,
                                                     LotNo = @LotNo,
                                                     SubAssyIssued = @SubAssyIssued,
                                                     PreparedQuantity = @PreparedQuantity
                                                WHERE FinalId = @FinalId", new
                        {
                            FinalId = trac.RecordId,
                            items.ShopOrder,
                            items.Line,
                            items.Rev,
                            items.LotNo,
                            items.SubAssyIssued,
                            items.PreparedQuantity
                        });
                    }
                }
            }
            else
            {
                
            }


            return true;
        }

      

       
    }
}
