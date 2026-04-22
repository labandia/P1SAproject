using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanTraceableSystem.Data;

namespace FanTraceableSystem.Interface
{
    public interface ISubassy
    {
        Task<List<TraceableSubAssyModel>> GetSubAssyDatalist(string finalShopOrder);
        Task<bool> CheckShopOrder(string finalShopOrder);   
        Task<bool> AddSubAssy(TraceableSubAssyModel subassy);
        Task<bool> UpdateSubAssy(TraceableSubAssyModel subassy);
        Task<bool> RemoveSubAssy(int id);
    }
}
