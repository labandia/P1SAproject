using System;
using System.Collections.Generic;
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
                                                                PreparedBy, Shift, Customer, InspectorName, 
                                                                CardCaseNo, Remarks, PCBIncharge, PCBIssuer, LotNo, DepartmentID, Rev)
                                                             VALUES(@FinalShopOrder, @PCBShopOrder, @Revision, @PCBA, @DatePrepared, 
                                                                    @TimeInput, @PreparedQuantity, @PreparedBy, @Shift, @Customer, @InspectorName, 
                                                                    @CardCaseNo, @Remarks, @PCBIncharge, @PCBIssuer, @LotNo, @DepartmentID, @Rev)", new
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
                            trac.InspectorName,
                            trac.CardCaseNo,
                            trac.Remarks,
                            trac.PCBIncharge,
                            trac.PCBIssuer, 
                            trac.LotNo,
                            trac.DepartmentID,
                            trac.Rev
                        }
                    );
                }
            }

             return true;
        }

       

        public Task<List<TraceableShopOrderModel>> TraceableShopOrder(
                string search,
                DateTime? startDate,
                DateTime? endDate, 
                int isEdit, 
                int section)
        {

            string sql = @"
                    SELECT RecordId
                          ,FinalShopOrder
                          ,PCBShopOrder
                          ,Revision
                          ,PCBA
                          ,DatePrepared
                          ,FORMAT(TimeInput, 'hh:mm tt') AS TimeInput
                          ,PreparedQuantity
                          ,PreparedBy
                          ,Shift
                          ,Customer
                          ,InspectorName
                          ,CardCaseNo
                          ,Remarks
                          ,PCBIncharge
                          ,PCBIssuer
                          ,LotNo
                          ,IsDeleted
                      FROM FanTraceability
                      WHERE IsDeleted = 0  ";

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

            return SqlDataAccess.GetData<TraceableShopOrderModel>(sql, parameters); 
        }

        public async Task<List<TraceableShopOrderModel>> GetFinalShopOrderDetails(string Finalorder)
        {
            string sql = @"
                    SELECT  RecordId
                          ,FinalShopOrder
                          ,PCBShopOrder
                          ,Revision
                          ,PCBA
                          ,DatePrepared
                          ,FORMAT(TimeInput, 'hh:mm tt') AS TimeInput
                          ,PreparedQuantity
                          ,PreparedBy
                          ,Shift
                          ,Customer
                          ,InspectorName
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
    }
}
