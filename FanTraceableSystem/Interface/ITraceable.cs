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
        Task<List<TraceableOverAllSummaryModel>> TraceableShopOrder(
             string search,
             DateTime? startDate,
             DateTime? endDate,
             int isEdit,
             int section,
             int pageNumber,
             int pageSize);

        Task<List<TraceableOverAllSummaryModel>> TraceSearchByShopOrder(string shopOrder, int selectsearch);

        // FOR PAGINATION PURPOSES
        Task<int> GetTraceableCount(string search,
             DateTime? startDate,
             DateTime? endDate,
             int isEdit,
             int section);

       

        Task<FinalTraceabilityModel> TraceAbilityFinalAssy(string final, int depart);
        Task<bool> AddTraceTransactions(FinalTraceabilityModel final, BindingList<TraceableSubAssyModel> sub);

        Task<bool> EditTraceTransaction(FinalTraceabilityModel final, BindingList<TraceableSubAssyModel> sub, string currentShop);
    }
}
