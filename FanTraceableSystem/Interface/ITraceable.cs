using FanTraceableSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Interface
{
    public interface ITraceable
    {
        // V2  ================
        Task<List<FinalTraceabilityModel>> TraceOverallData(
          string search,
          DateTime? startDate,
          DateTime? endDate,
          int isEdit,
          int section,
          int pageNumber,
          int pageSize);

        Task<List<TraceableShopOrderModel>> TraceableShopOrder(
             string search,
             DateTime? startDate,
             DateTime? endDate,
             int isEdit,
             int section,
             int pageNumber,
             int pageSize);

        // FOR PAGINATION PURPOSES
        Task<int> GetTraceableCount(string search,
             DateTime? startDate,
             DateTime? endDate,
             int isEdit,
             int section);

        Task<List<TraceableShopOrderModel>> GetFinalShopOrderDetails(string Finalorder);

        Task<bool> AddTraceTransactions(TraceableShopOrderModel trac, List<TracePCBModel> pcb);

        Task<bool> EditTraceTransaction(TraceableShopOrderModel trac, BindingList<EditTracePCBModel> pcb);
    }
}
