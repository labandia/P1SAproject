using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZebraPrinterLabel
{
    internal class MasterlistRepository : IMasterlist
    {
        public Task<List<AmbassadorModel>> GetAmbassadordata(string partnum)
        {
            throw new NotImplementedException();
        }
    }
}
