using Parts_locator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Interface
{
    public interface IMoldImpeller
    {
        Task<List<MoldImpeller>> GetMoldImpellerMasterlist();
        Task<List<MoldImpeller>> GetHistoryTransaction();

        // MASTERLIST FUNCTIONS
        Task<bool> AddMasterlist(MoldImpeller masterlist);
        Task<bool> EditMasterlist(MoldImpeller masterlist);
        Task<bool> DeleteMasterlist(int ID);
        Task<bool> ImportMasterlist(MoldImpeller masterlist);
        Task<bool> InsertTransaction(MoldImpeller masterlist);
    }
}
