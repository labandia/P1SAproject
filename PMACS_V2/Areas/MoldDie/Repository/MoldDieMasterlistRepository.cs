using Dapper;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public class MoldDieMasterlistRepository : IDieMasterList
    {
        public Task<List<MoldieMasterModel>> GetModelDieMasterList(string searchText)
        {
            string strsql = $@"SELECT PartNo
                          ,PartDescription
                          ,Dimension_Quality
                          ,DieSerial
                          ,DieNumber
                          ,Cavity
                          ,PreviousCount
                          ,ProcessID
                          ,ShotCountprevious
                      FROM DieMold_MoldingMainParts 
                      WHERE 1 = 1 ";

            var parameters = new DynamicParameters();
      

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                strsql += @" AND (
                        PartNo LIKE @SearchPrefix)";

                parameters.Add("@SearchPrefix", $"{searchText}%");
            }

            return SqlDataAccess.QueryAsync<MoldieMasterModel>(strsql, parameters);
        }


        public async Task<bool> AddMoldieMasterList(MoldieMasterModel model)
        {
            bool IsExist = await SqlDataAccess.ExistsAsync($@"SELECT COUNT(*) 
                FROM DieMold_MoldingMainParts WHERE PartNo =@PartNo AND DieSerial =@DieSerial", new
            {
                model.PartNo, model.DieSerial
            });

            if (IsExist) return false;

            int rows = await SqlDataAccess.ExecuteAsync($@"INSERT INTO DieMold_MoldingMainParts(PartNo, PartDescription, DieSerial, DieNumber, Cavity, ProcessID) 
                  VALUES(@PartNo, @PartDescription, @DieSerial, @DieNumber, @Cavity, @ProcessID)", model);

            return rows > 0;
        }

        public async Task<bool> EditMoldieMasterList(MoldieMasterModel model)
        {
            int rows = await SqlDataAccess.ExecuteAsync($@"UPDATE DieMold_MoldingMainParts SET 
                PartDescription =@PartDescription, Dimension_Quality =@Dimension_Quality, DieSerial =@DieSerial, 
                DieNumber =@DieNumber, Cavity =@Cavity, ProcessID =@ProcessID WHERE PartNo =@PartNo", model);

            return rows > 0;
        }

        public Task<bool> DeleteMoldieMaster(string partno)
        {
            throw new NotImplementedException();
        }

        

    }
}