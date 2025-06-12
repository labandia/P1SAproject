using Parts_locator.Interface;
using Parts_locator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parts_locator.Repository
{
    internal class RawMatsRepository : IRawMats
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

        public Task<List<RawMatModel>> GetRawMatsMasterList(int bush)
        {
            throw new NotImplementedException();
        }

        public Task<List<RawMatModel>> GetRawMatsSummaryData(int act)
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
