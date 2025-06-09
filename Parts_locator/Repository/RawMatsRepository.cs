using Parts_locator.Interface;
using Parts_locator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parts_locator.Repository
{
    internal class RawMatsRepository : IRawMats
    {
        public Task<List<RawMatModel>> GetRawMatsMasterList(int bush)
        {
            throw new NotImplementedException();
        }

        public Task<List<RawMatModel>> GetRawMatsSummaryData(int act)
        {
            throw new NotImplementedException();
        }
    }
}
