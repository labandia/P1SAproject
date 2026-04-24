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
                int isSearch, 
                int section,
                int pageNumber,
                int pageSize)
        {

            string sql = @"
                      SELECT  
                           f.RecordId,
                           f.FinalShopOrder,
                           s.ShopOrder,
                           p.ProcessName,
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
                    LEFT JOIN FanTraceabilitySub s ON f.RecordId = s.FinalId
                    INNER JOIN FanTraceabilityProcess p ON p.ProcessId = f.ProcessId
                    WHERE f.IsDeletedFinal = 0 AND s.ShopOrder IS NOT NULL ";

            var parameters = new DynamicParameters();

            if (section != 0)
            {
                sql += " AND f.DepartmentID =@DepartmentID";
                parameters.Add("@DepartmentID", section);
            }


            // 🔍 Search by FinalShopOrder filter (only if isSearch is 1)
            if (!string.IsNullOrWhiteSpace(search) && isSearch == 1)
            {
                sql += " AND f.FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", search);
            }

            // 🔍 Search By ShopOrder filter (only if isSearch is 2)
            if (!string.IsNullOrWhiteSpace(search) && isSearch == 2)
            {
                sql += " AND s.ShopOrder = @ShopOrder";
                parameters.Add("@ShopOrder", search);
            }

            if(isSearch == 0)
            {
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
            }

            sql += @"
                ORDER BY f.RecordId DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Offset", (pageNumber - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            return SqlDataAccess.GetData<TraceableOverAllSummaryModel>(sql, parameters); 
        }

        public Task<List<TraceableOverAllSummaryModel>> TraceSearchByShopOrder(string shopOrder, int selectsearch)
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
                    INNER JOIN FanTraceabilityProcess p ON p.ProcessId = f.ProcessId
                    WHERE f.IsDeletedFinal = 0 ";


            var parameters = new DynamicParameters();

            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(shopOrder) && selectsearch == 0)
            {
                sql += " AND f.FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", shopOrder);
            }

            if (!string.IsNullOrWhiteSpace(shopOrder) && selectsearch == 1)
            {
                sql += " AND s.ShopOrder = @ShopOrder";
                parameters.Add("@ShopOrder", shopOrder);
            }

            return SqlDataAccess.GetData<TraceableOverAllSummaryModel>(sql, parameters);
        }

        // FOR SEARCH FINAL ASSY  DETAILS 
        public async Task<FinalTraceabilityModel> TraceAbilityFinalAssy(string final, int depart)
        {
            var data = await SqlDataAccess.GetData<FinalTraceabilityModel>(@"FinalAssyStore",
                    new { FinalShopOrder = final, DepartmentID = depart });

            return data.FirstOrDefault();
        }

        // FOR PAGINATION DISPLAY TOTAL COUNT DATA
        public Task<int> GetTraceableCount(string search, DateTime? startDate, DateTime? endDate, int isEdit, int section)
        {
            string sql = @"SELECT COUNT(*)
                       FROM FanTraceabilityFinal f
	                   LEFT JOIN FanTraceabilitySub s ON s.FinalShopOrder = f.FinalShopOrder
                       AND s.ShopOrder IS NOT NULL
                       INNER JOIN FanTraceabilityProcess p ON p.ProcessId = f.ProcessId
                       WHERE f.IsDeletedFinal = 0  ";

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


        public async Task<bool> AddTraceTransactions(FinalTraceabilityModel trac, BindingList<TraceableSubAssyModel> pcb)
        {
            try
            {
                var finalId = await SqlDataAccess.ExecuteScalarAsync<int>(@"
                INSERT INTO FanTraceabilityFinal
                (FinalShopOrder, PlanQuan, ItemNo, DatePrepared, TimeInput, PreparedBy, Shift, Customer, Modeltype,
                 Incharge, Remarks, FinalIssuedby, DepartmentID, ProcessId)
                OUTPUT INSERTED.RecordId
                VALUES
                (@FinalShopOrder, @PlanQuan, @ItemNo, @DatePrepared, @TimeInput, @PreparedBy, @Shift, @Customer, @Modeltype,
                 @Incharge, @Remarks, @FinalIssuedby, @DepartmentID, @ProcessId)",
                trac);

                // if the input has a Sub Assy Data
                if (pcb.Count != 0)
                {
                    foreach (var items in pcb)
                    {
                        await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceabilitySub
                        (FinalId, FinalShopOrder, ShopOrder, PreparedQuantity, LotNo, Line, SubAssyIssued, DepartmentID, Rev)
                        VALUES(@FinalId, @FinalShopOrder, @ShopOrder, @PreparedQuantity, @LotNo, @Line, @SubAssyIssued, @DepartmentID, @Rev)", new
                        {
                            FinalId = finalId,
                            trac.FinalShopOrder,
                            items.ShopOrder,
                            items.LotNo,
                            items.PreparedQuantity,
                            items.Line,
                            items.SubAssyIssued,
                            trac.DepartmentID, 
                            items.Rev
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
        public async Task<bool> EditTraceTransaction(FinalTraceabilityModel trac, BindingList<TraceableSubAssyModel> pcb, string currentShop)
        {
            await SqlDataAccess.UpdateInsertQuery($@"UPDATE FanTraceabilityFinal SET ProcessId =@ProcessId, 
                ItemNo =@ItemNo, PreparedBy =@PreparedBy, Customer =@Customer, Modeltype =@Modeltype, Remarks =@Remarks,  
                Incharge =@Incharge, FinalIssuedby =@FinalIssuedby WHERE RecordId =@RecordId", new
            {
                trac.RecordId,
                trac.ProcessId,
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
                foreach (var items in pcb)
                {
                    //Debug.WriteLine("Current Shop : " + currentShop + " ISCHANGE : " + items.isChangeShop);
                    //
                    if (items.isAction == 1)
                    {
                        await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceabilitySub
                            (FinalShopOrder, ShopOrder, PreparedQuantity, LotNo, Line, SubAssyIssued, Rev, FinalId, DepartmentID)
                            VALUES(@FinalShopOrder, @ShopOrder, @PreparedQuantity, @LotNo, @Line, @SubAssyIssued, @Rev, @FinalId, @DepartmentID)", new
                        {
                            trac.FinalShopOrder,
                            items.ShopOrder,
                            items.LotNo,
                            items.PreparedQuantity,
                            items.Line,
                            items.Rev,
                            items.SubAssyIssued,
                            trac.DepartmentID,
                            FinalId = trac.RecordId
                        });
                    }
                    else if (items.isAction == 2)
                    {

                        Debug.WriteLine($@"FinalShop : {items.ShopOrder} - 
                                    Lot No : {items.LotNo} - 
                                    PreparedQuantity : {items.PreparedQuantity} - 
                                    Line : {items.Line} - 
                                    Rev : {items.Rev} - 
                                    Action : {items.isAction} - 
                                  ");

             

                        await SqlDataAccess.UpdateInsertQuery($@"
                                UPDATE FanTraceabilitySub
                                SET 
                                    ShopOrder = @ShopOrder, 
                                    Line = @Line,
                                    Rev = @Rev,
                                    LotNo = @LotNo,
                                    SubAssyIssued = @SubAssyIssued,
                                    PreparedQuantity = @PreparedQuantity
                                WHERE SubAssyID = @SubAssyID",
                        new
                        {
                            items.SubAssyID,
                            items.ShopOrder, // new shop order
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
        public Task<List<ProcessModel>> GetProcessesByDepartment(int departmentId)
        {
             return SqlDataAccess.GetData<ProcessModel>(@"SELECT ProcessId, ProcessName, 
                      DepartmentID FROM FanTraceabilityProcess WHERE DepartmentID = @DepartmentID",
                new { DepartmentID = departmentId });   
        }
    }
}
