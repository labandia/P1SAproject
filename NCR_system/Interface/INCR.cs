using NCR_system.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface INCR
    {
        Task<List<NCRDatamodel>> GetSummaryNCR(int type);
        Task<List<NCRModels>> GetNCRData(string search,
            string Category,
            int section,
            int Stats,
            int type);
        Task<bool> InsertNCRData(NCRModels ncr);
        Task<bool> UpdateNCRData(NCRModels ncr);
    }
}
