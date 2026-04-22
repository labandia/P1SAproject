using Dapper;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using MSDMonitoring.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Services
{
    public class SummaryServices : ISummary
    {
        public Task<int> GetSummaryCount(string search, 
            DateTime? startDate, DateTime? endDate, 
            int isEdit, int section)
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

            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (f.FinalShopOrder LIKE @Search OR s.ShopOrder LIKE @Search)";
                parameters.Add("@Search", $"%{search}%");
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

            return SqlDataAccess.GetCountData(sql, parameters);
        }

        public Task<List<SummaryraceableShopOrderModel>> TraceableShopOrderSummary(string search, 
            DateTime? startDate, DateTime? endDate, int isEdit, int section,
             int pageNumber,
             int pageSize)
        {

            string sql = @"
                    SELECT  f.RecordId
                           ,f.FinalShopOrder
                           ,s.ShopOrder
                           ,f.Revision
                           ,f.ItemNo, PlanQuan, Line
                           ,DatePrepared
                           ,FORMAT(TimeInput, 'hh:mm tt') AS TimeInput
                           ,PreparedQuantity
                           ,PreparedBy
                           ,Shift
                           ,Customer
                           ,f.Modeltype
                           ,Remarks
                           ,f.Incharge
                           ,f.FinalIssuedby
                           ,LotNo
		                   ,s.Rev
                           ,f.IsDeletedFinal
		                   ,f.DepartmentID
                       FROM FanTraceabilityFinal f
	                   LEFT JOIN FanTraceabilitySub s ON s.FinalShopOrder = f.FinalShopOrder
                       WHERE f.IsDeletedFinal = 0 AND ShopOrder IS NOT NULL
                ";

            var parameters = new DynamicParameters();

            if(section != 0)
            {
                sql += " AND f.DepartmentID =@DepartmentID";
                parameters.Add("@DepartmentID", section);
            }


            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (f.FinalShopOrder LIKE @Search OR s.ShopOrder LIKE @Search)";
                parameters.Add("@Search", $"%{search}%");
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


            return SqlDataAccess.GetData<SummaryraceableShopOrderModel>(sql, parameters);
        }
    }
}
