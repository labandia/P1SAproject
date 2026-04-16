using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Services
{
    public class TraceableService : ITraceable
    {
        public Task<bool> AddTraceTransactions(TraceableShopOrderModel trac)
        {
            throw new NotImplementedException();
        }

        public Task<List<TraceableShopOrderModel>> TraceableShopOrder(string search, 
            DateTime startdate, 
            DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
