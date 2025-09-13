using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        public Task<List<ReelIDHistory>> GetPrintHistoryData()
        {
            string strsql = $@"SELECT h.DateInput, h.ReelID, h.Partnum, p.WarehouseLocal, p.Qty
                            FROM  PartList_PrintLabelHistory h
                            INNER JOIN PartList_PrintLabelData p ON p.Partnum = h.Partnum
                            ORDER BY h.RecordID DESC";
            return SqlDataAccess.GetData<ReelIDHistory>(strsql, null);
        }


        public async Task<bool> AddnewHistorylist(string partnum, string ReelID)
        {
            string insertsql = $@"INSERT INTO PartList_PrintLabelHistory(Partnum, ReelID)
                                  VALUES(@Partnum, @ReelID)";
            return await SqlDataAccess.UpdateInsertQuery(insertsql, new { Partnum = partnum, ReelID = ReelID });
        }

        public Task<bool> AddnewMasterlist(MasterlistData final)
        {
            string insertsql = $@"INSERT INTO PartList_PrintLabelData(Partnum, WarehouseLocal, Qty)
                                  VALUES(@Partnum, @WarehouseLocal, @Qty)";
            return SqlDataAccess.UpdateInsertQuery(insertsql, final);
        }

        public Task<bool> EditMasterlist(int qty, string partnum)
        {
            string insertsql = $@"UPDATE PartList_PrintLabelData SET Qty =@Qty
                                  WHERE Partnum =@Partnum";
            return SqlDataAccess.UpdateInsertQuery(insertsql, new { Qty = qty, Partnum  = partnum });
        }
    }
}
