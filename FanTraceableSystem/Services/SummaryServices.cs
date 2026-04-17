using Dapper;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using MSDMonitoring.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Services
{
    public class SummaryServices : ISummary
    {
        public Task<List<SummaryraceableShopOrderModel>> TraceableShopOrderSummary(string search, 
            DateTime? startDate, DateTime? endDate, int isEdit, int section)
        {

            string sql = @"
                    SELECT FinalShopOrder
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
                          ,IsDeleted,DepartmentID
                      FROM FanTraceability
                      WHERE IsDeleted = 0 
                ";

            var parameters = new DynamicParameters();

            if(section != 0)
            {
                sql += " AND DepartmentID =@DepartmentID";
                parameters.Add("@DepartmentID", section);
            }


            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (FinalShopOrder LIKE @Search OR PCBShopOrder LIKE @Search)";
                parameters.Add("@Search", $"%{search}%");
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

            return SqlDataAccess.GetData<SummaryraceableShopOrderModel>(sql, parameters);
        }
    }
}
