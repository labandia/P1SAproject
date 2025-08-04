using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebraPrinterLabel
{
    public interface IMasterlist
    {
        Task<List<AmbassadorModel>> GetAmbassadordata(string partnum);
    }
}
