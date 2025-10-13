using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    internal interface INCR
    {
        Task<IEnumerable<NCRModels>> GetNCRData(int type);
        Task<bool> InsertNCRData(NCRModels ncr);
        Task<bool> UpdateNCRData(NCRModels ncr);
    }
}
