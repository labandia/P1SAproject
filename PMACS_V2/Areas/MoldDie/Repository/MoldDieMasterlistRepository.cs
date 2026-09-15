using Dapper;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public class MoldDieMasterlistRepository : IDieMasterList
    {
        public Task<List<MoldieMasterModel>> GetModelDieMasterList(
            string searchText,
            int page = 1, 
            int pageSize = 50)
        {
            try
            {
                string strsql = $@"SELECT 
                        p.MoldID, p.PartNo
                        ,p.PartDescription
                        ,p.Dimension_Quality
                        ,p.DieSerial
                        ,p.DieNumber
                        ,p.Cavity
                        ,p.PreviousCount
                        ,p.ProcessID
                        ,p.ShotCountprevious
                    FROM DieMold_MoldingMainParts p 
                    INNER JOIN DieMold_DieMaster d ON d.DieSerial = p.DieSerial
                      WHERE 1 = 1 ";

                var parameters = new DynamicParameters();


                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    strsql += @" AND (
                        p.PartNo LIKE @SearchPrefix)";

                    parameters.Add("@SearchPrefix", $"{searchText}%");
                }

                return SqlDataAccess.QueryAsync<MoldieMasterModel>(strsql, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                throw;
            }
        }


        public async Task<bool> AddMoldieMasterList(MoldieMasterModel model)
        {
            try
            {
                await EnsureDieMasterExistsAsync(model);

                int rows = await SqlDataAccess.ExecuteAsync($@"INSERT INTO DieMold_MoldingMainParts
            (PartNo, PartDescription, DieSerial, DieNumber, Cavity, ProcessID)
            VALUES(@PartNo, @PartDescription, @DieSerial, @DieNumber, @Cavity, @ProcessID)", model);

                return rows > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                throw;
            }
        }

        public async Task<bool> EditMoldieMasterList(MoldieMasterModel model)
        {
            try
            {
                await EnsureDieMasterExistsAsync(model);

                int rows = await SqlDataAccess.ExecuteAsync($@"UPDATE DieMold_MoldingMainParts SET
            PartDescription =@PartDescription, Dimension_Quality =@Dimension_Quality, DieSerial =@DieSerial,
            DieNumber =@DieNumber, Cavity =@Cavity, ProcessID =@ProcessID WHERE PartNo =@PartNo", model);

                return rows > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                throw;
            }
        }
        private async Task EnsureDieMasterExistsAsync(MoldieMasterModel model)
        {
            bool isExist = await SqlDataAccess.ExistsAsync($@"SELECT COUNT(*)
        FROM DieMold_DieMaster WHERE DieSerial = @DieSerial", new { model.DieSerial });

            if (!isExist)
            {
                await SqlDataAccess.ExecuteAsync($@"INSERT INTO DieMold_DieMaster
            (DieSerial, DieNumber, Cavity)
            VALUES(@DieSerial, @DieNumber, @Cavity)", model);
            }
        }
        public Task<bool> DeleteMoldieMaster(string partno)
        {
            throw new NotImplementedException();
        }

        

    }
}