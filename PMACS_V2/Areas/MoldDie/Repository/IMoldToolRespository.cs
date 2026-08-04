using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PMACS_V2.Helper;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public class IMoldToolRespository : IMoldTooling
    {
        public async Task<bool> AddEditMoldTooling(DieMoldToolingModel model, bool isadd)
        {
            int resultrows = 0;

            if (isadd)
            {
                string insertquery = @"INSERT INTO DieMoldDieTooling(RegNo, PartNo, Item, DetailsModify, ShotRelease, 
                                     DateArrived, DateRepair, Incharge, Remarks)
                                   VALUES(@RegNo, @PartNo, @Item, @DetailsModify, @ShotRelease, @DateArrived , @DateRepair, 
                                    @Incharge, @Remarks)";

                resultrows = await SqlDataAccess.ExecuteAsync(insertquery, model);

            }
            else
            {
                string insertquery = @"UPDATE DieMoldDieTooling SET RegNo =@RegNo, Item =@Item, DetailsModify =@DetailsModify, ShotRelease =@ShotRelease, 
                                    DateArrived =@DateArrived, DateRepair =@DateRepair, Incharge =@Incharge, Remarks =@Remarks
                                   WHERE RecordID =@RecordID";

                resultrows = await SqlDataAccess.ExecuteAsync(insertquery, model);

            }

            return resultrows > 0;
        }

        public async Task<bool> DeleteMoldTooling(int recordID)
        {
            int rows = await SqlDataAccess.ExecuteAsync("UPDATE DieMoldDieTooling SET IsDeleted = 1 WHERE RecordID =@RecordID", 
                new { RecordID = recordID });

            return rows > 0;    
        }


        public Task<List<DieMoldToolingModel>> GetMoldToolingList(string search, int page = 1, int pageSize = 50)
        {
            throw new NotImplementedException();
        }
    }
}