using FanTraceableSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Interface
{
    public interface ITraceable
    {
        Task<List<TraceableShopOrderModel>> TraceableShopOrder(
       string search,
       DateTime? startDate,
       DateTime? endDate, 
       int isEdit, 
       int section);

        Task<bool> AddTraceTransactions(TraceableShopOrderModel trac, List<TracePCBModel> pcb);
    }
}
