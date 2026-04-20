using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using Microsoft.Office.Interop.Excel;
using MSDMonitoring.Data;

namespace FanTraceableSystem.Services
{
    public class TraceableService : ITraceable
    {
        public async Task<bool> AddTraceTransactions(TraceableShopOrderModel trac, List<TracePCBModel> pcb)
        {
         
            if (pcb.Count != 0)
            {
                foreach(var item in pcb)
                {
                    await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceability(FinalShopOrder, PCBShopOrder, 
                                                                Revision, PCBA, DatePrepared, TimeInput, PreparedQuantity, 
                                                                PreparedBy, Shift, Customer, 
                                                                CardCaseNo, Remarks, PCBIncharge, PCBIssuer, LotNo, DepartmentID, Rev, PlanQuan)
                                                             VALUES(@FinalShopOrder, @PCBShopOrder, @Revision, @PCBA, @DatePrepared, 
                                                                    @TimeInput, @PreparedQuantity, @PreparedBy, @Shift, @Customer, 
                                                                    @CardCaseNo, @Remarks, @PCBIncharge, @PCBIssuer, @LotNo, @DepartmentID, @Rev, @PlanQuan)", new
                        {
                            trac.FinalShopOrder,
                            PCBShopOrder  = item.PCBShopOrder,
                            trac.Revision,
                            trac.PCBA,
                            trac.DatePrepared,
                            trac.TimeInput,
                            PreparedQuantity = item.Quantity,
                            trac.PreparedBy,
                            trac.Shift,
                            trac.Customer,
                            trac.CardCaseNo,
                            trac.Remarks,
                            trac.PCBIncharge,
                            trac.DepartmentID,
                            LotNo = item.LotNo,
                            Line = item.Line,
                            PCBIssuer = item.PCBIssuer,
                            trac.PlanQuan, 
                            Rev = item.Rev
                        }
                    );
                }
            }
            else
            {

            }

             return true;
        }

       

        public Task<List<TraceableShopOrderModel>> TraceableShopOrder(
                string search,
                DateTime? startDate,
                DateTime? endDate, 
                int isEdit, 
                int section,
                int pageNumber,
                int pageSize)
        {

            string sql = @"
                    SELECT RecordId
                          ,FinalShopOrder
                          ,PCBShopOrder
                          ,Revision
                          ,PCBA
                          ,PlanQuan
                          ,DatePrepared
                          ,FORMAT(TimeInput, 'hh:mm tt') AS TimeInput
                          ,PreparedQuantity
                          ,PreparedBy
                          ,Shift
                          ,Line
                          ,Customer
                          ,CardCaseNo
                          ,Remarks
                          ,PCBIncharge
                          ,PCBIssuer
                          ,LotNo
                          ,DepartmentID
                          ,IsDeleted
                    FROM FanTraceability
                    WHERE IsDeleted = 0";

            var parameters = new DynamicParameters();

            if (section != 0)
            {
                sql += " AND DepartmentID =@DepartmentID";
                parameters.Add("@DepartmentID", section);
            }


            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", search);
            }

            // 📅 Start Date filter
            if (startDate.HasValue && isEdit == 1)
            {
                sql += " AND DatePrepared >= @StartDate";
                parameters.Add("@StartDate", startDate.Value.Date);
            }

            // 📅 End Date filter (inclusive)
            if (endDate.HasValue && isEdit == 1)
            {
                sql += " AND DatePrepared < DATEADD(DAY, 1, @EndDate)";
                parameters.Add("@EndDate", endDate.Value.Date);
            }

            sql += @"
                ORDER BY RecordId DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Offset", (pageNumber - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            return SqlDataAccess.GetData<TraceableShopOrderModel>(sql, parameters); 
        }

        public async Task<List<TraceableShopOrderModel>> GetFinalShopOrderDetails(string Finalorder)
        {
            string sql = @"
                    SELECT  RecordId
                          ,FinalShopOrder
                          ,PCBShopOrder
                          ,Revision
                          ,PCBA, PlanQuan, Line
                          ,DatePrepared
                          ,FORMAT(TimeInput, 'hh:mm tt') AS TimeInput
                          ,PreparedQuantity
                          ,PreparedBy
                          ,Shift
                          ,Customer
                          ,CardCaseNo
                          ,Remarks
                          ,PCBIncharge
                          ,PCBIssuer
                          ,LotNo
                          ,IsDeleted
                      FROM FanTraceability
                      WHERE IsDeleted = 0   ";

            var parameters = new DynamicParameters();

            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(Finalorder))
            {
                sql += " AND FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", Finalorder);
            }


            return await SqlDataAccess.GetData<TraceableShopOrderModel>(sql, parameters);
        }

        public Task<int> GetTraceableCount(string search, DateTime? startDate, DateTime? endDate, int isEdit, int section)
        {
            string sql = @"SELECT COUNT(*) 
                   FROM FanTraceability
                   WHERE IsDeleted = 0";

            var parameters = new DynamicParameters();

            if (section != 0)
            {
                sql += " AND DepartmentID = @DepartmentID";
                parameters.Add("@DepartmentID", section);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND FinalShopOrder = @FinalShopOrder";
                parameters.Add("@FinalShopOrder", search);
            }

            if (startDate.HasValue && isEdit == 1)
            {
                sql += " AND DatePrepared >= @StartDate";
                parameters.Add("@StartDate", startDate.Value.Date);
            }

            if (endDate.HasValue && isEdit == 1)
            {
                sql += " AND DatePrepared < DATEADD(DAY, 1, @EndDate)";
                parameters.Add("@EndDate", endDate.Value.Date);
            }

            return  SqlDataAccess.GetCountData(sql, parameters);
        }

        public async Task<bool> EditTraceTransaction(TraceableShopOrderModel trac, BindingList<EditTracePCBModel> pcb)
        {
            if (pcb.Count != 0)
            {
                foreach(var item in pcb)
                {
                    if(item.isAction == 0)
                    {

                        await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceability(FinalShopOrder, PCBShopOrder, 
                                                                Revision, PCBA, DatePrepared, TimeInput, PreparedQuantity, 
                                                                PreparedBy, Shift, Customer, 
                                                                CardCaseNo, Remarks, PCBIncharge, PCBIssuer, LotNo, DepartmentID, Rev, PlanQuan)
                                                             VALUES(@FinalShopOrder, @PCBShopOrder, @Revision, @PCBA, @DatePrepared, 
                                                                    @TimeInput, @PreparedQuantity, @PreparedBy, @Shift, @Customer, 
                                                                    @CardCaseNo, @Remarks, @PCBIncharge, @PCBIssuer, @LotNo, @DepartmentID, @Rev, @PlanQuan)", new
                                {
                                    trac.FinalShopOrder,
                                    PCBShopOrder = item.PCBShopOrder,
                                    trac.Revision,
                                    trac.PCBA,
                                    trac.DatePrepared,
                                    trac.TimeInput,
                                    PreparedQuantity = item.Quantity,
                                    trac.PreparedBy,
                                    trac.Shift,
                                    trac.Customer,
                                    trac.CardCaseNo,
                                    trac.Remarks,
                                    trac.PCBIncharge,
                                    trac.DepartmentID,
                                    LotNo = item.LotNo,
                                    Line = item.Line,
                                    PCBIssuer = item.PCBIssuer,
                                    trac.PlanQuan,
                                    Rev = item.Rev
                                }
                        );

                    }
                    else
                    {
                        await SqlDataAccess.UpdateInsertQuery($@"UPDATE FanTraceability
                                                SET PCBShopOrder =@PCBShopOrder, 
                                                     Line =@Line,
                                                     Rev = @Rev,
                                                     LotNo = @LotNo,
                                                     PCBIssuer = @PCBIssuer,
                                                     PreparedQuantity = @PreparedQuantity
                                                WHERE RecordId = @RecordId", new
                        {
                            item.RecordId,
                            PCBShopOrder = item.PCBShopOrder,
                            Line = item.Line,
                            Rev = item.Rev,
                            LotNo = item.LotNo,
                            PCBIssuer = item.PCBIssuer,
                            PreparedQuantity = item.Quantity    
                        });
                    }


                }
            }
            else
            {
                
            }


            return true;
        }

        public Task<List<FinalTraceabilityModel>> TraceOverallData(
            string search, 
            DateTime? startDate,
            DateTime? endDate, 
            int isEdit, 
            int section, 
            int pageNumber, 
            int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
