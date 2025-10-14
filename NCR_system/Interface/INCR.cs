using NCR_system.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface INCR
    {
        Task<IEnumerable<NCRModels>> GetNCRData(int type);
        Task<bool> InsertNCRData(NCRModels ncr);
        Task<bool> UpdateNCRData(NCRModels ncr);
    }
}
