using Dapper;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using PMACS_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

            string strquery = $@"SELECT t.RecordID, t.RegNo, t.PartNo, p.Dimension_Quality, 
                                    t.Item, t.DetailsModify, t.ShotRelease,
                                    t.DateArrived,
                                    t.DateRepair,
                                    t.Incharge, t.Remarks
                                FROM DieMoldDieTooling t
                                INNER JOIN DieMold_MoldingMainParts p ON t.PartNo = p.PartNo
                                WHERE t.IsDeleted = 0 AND  (
                                    @Search IS NULL
                                    OR t.RegNo LIKE '%' + @Search + '%'
                                    OR t.PartNo LIKE '%' + @Search + '%'
                                ) ";
            var parameter = new DynamicParameters();
            parameter.Add("@Search", search);


            strquery += $@" ORDER BY t.RecordID DESC
                                OFFSET @Offset ROWS
                                FETCH NEXT @PageSize ROWS ONLY";
            parameter.Add("@Offset", (page - 1) * pageSize);
            parameter.Add("@PageSize", pageSize);

            return SqlDataAccess.QueryAsync<DieMoldToolingModel>(strquery, parameter);
        }
    }
}