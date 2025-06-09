using Parts_locator.Interface;
using Parts_locator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Repository
{
    public class MoldImpellerRepository : IMoldImpeller
    {
        public Task<bool> AddMasterlist(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMasterlist(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMasterlist(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<List<MoldImpeller>> GetHistoryTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<List<MoldImpeller>> GetMoldImpellerMasterlist()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ImportMasterlist(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertTransaction(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }
    }
}
