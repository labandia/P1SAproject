using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using MSDMonitoring.Data;

namespace FanTraceableSystem.Services
{
    public class TraceableService : ITraceable
    {
        public async Task<bool> AddTraceTransactions(TraceableShopOrderModel trac, List<TracePCBModel> pcb)
        {
             if(pcb.Count != 0)
            {
                foreach(var item in pcb)
                {
                    await SqlDataAccess.UpdateInsertQuery($@"INSERT INTO", new
                        {
                            trac.FinalShopOrder,
                            PCBShopOrder  = item.PCBShopOrder,
                            trac.Revision,
                            trac.PCBA,
                            trac.DatePrepared,
                            trac.TimeInput,
                            trac.PreparedQuantity,
                            trac.PreparedBy,
                            trac.Shift,
                            trac.Customer,
                            trac.InspectorName,
                            trac.CardCaseNo,
                            trac.Remarks,
                            trac.PCBIncharge,
                            trac.PCBIssuer,
                            Quantity = item.Quantity
                        }
                    );
                }
            }

             return true;
        }

        public Task<List<TraceableShopOrderModel>> TraceableShopOrder(string search,
                DateTime? startDate,
                DateTime? endDate)
        {
            var sql = @"
        SELECT *
        FROM FanTraceability
        WHERE 1=1
    ";

            var parameters = new DynamicParameters();

            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (FinalShopOrder LIKE @Search OR PCBShopOrder LIKE @Search)";
                parameters.Add("@Search", $"%{search}%");
            }

            // 📅 Start Date filter
            if (startDate.HasValue)
            {
                sql += " AND DatePrepared >= @StartDate";
                parameters.Add("@StartDate", startDate.Value.Date);
            }

            // 📅 End Date filter (inclusive)
            if (endDate.HasValue)
            {
                sql += " AND DatePrepared < DATEADD(DAY, 1, @EndDate)";
                parameters.Add("@EndDate", endDate.Value.Date);
            }


            throw new NotImplementedException();
        }
    }
}
