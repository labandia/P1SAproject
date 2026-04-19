using FanTraceableSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Interface
{
    public interface ISummary
    {
        Task<List<SummaryraceableShopOrderModel>> TraceableShopOrderSummary(
              string search,
              DateTime? startDate,
              DateTime? endDate,
              int isEdit, 
              int section,
             int pageNumber,
             int pageSize);

        // FOR PAGINATION PURPOSES
        Task<int> GetSummaryCount(string search,
             DateTime? startDate,
             DateTime? endDate,
             int isEdit,
             int section);
    }
}
