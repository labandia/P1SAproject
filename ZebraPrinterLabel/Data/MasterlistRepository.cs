using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZebraPrinterLabel.Data;

namespace ZebraPrinterLabel
{
    internal class MasterlistRepository : IMasterlist
    {
        public Task<List<AmbassadorModel>> GetAmbassadordata(string partnum)
        {
            string strsql = "SELECT Partnum, WarehouseLocal, Qty, AreaRacks FROM  PartList_PrintLabelData WHERE Partnum =@Partnum";
            return SqlDataAccess.GetData<AmbassadorModel>(strsql, new { Partnum = partnum });
        }
    }
}
