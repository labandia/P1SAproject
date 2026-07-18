using FanTraceableSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanTraceableSystem.Interface
{
    public interface IUpdateRepository
    {
        Task<UpdateVersionModel> GetLatestVersionAsync(string systemName);
    }
}
