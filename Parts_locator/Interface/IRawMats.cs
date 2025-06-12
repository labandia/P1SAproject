using Parts_locator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parts_locator.Interface
{
    internal interface IRawMats
    {
        Task<List<RawMatModel>> GetRawMatsMasterList(int bush);
        Task<List<RawMatModel>> GetRawMatsSummaryData(int act);

        // MASTERLIST FUNCTIONS
        Task<bool> AddMasterlist(MoldImpeller masterlist);
        Task<bool> EditMasterlist(MoldImpeller masterlist);
        Task<bool> DeleteMasterlist(int ID);
        Task<bool> ImportMasterlist(MoldImpeller masterlist);
        Task<bool> InsertTransaction(MoldImpeller masterlist);
    }
}
