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
                // 1. Get the original DieSerial.
                string oldDieSerial = await SqlDataAccess.QuerySingleOrDefaultAsync<string>(@"
                    SELECT DieSerial
                    FROM DieMold_MoldingMainParts
                    WHERE PartNo = @PartNo;",
                    new { model.PartNo });

                if (string.IsNullOrWhiteSpace(oldDieSerial))
                    return false;

                // 2. Check whether the serial number changed.
                bool dieSerialChanged = !string.Equals(
                    oldDieSerial.Trim(),
                    model.DieSerial?.Trim(),
                    StringComparison.OrdinalIgnoreCase);

                if (dieSerialChanged)
                {
                    // 3. Ensure the NEW serial exists in DieMaster first.
                    bool newSerialExists = await SqlDataAccess.ExistsAsync(@"
                SELECT 1
                FROM [PMACS_LIVE].[dbo].[DieMold_DieMaster]
                WHERE DieSerial = @DieSerial;",
                        new { model.DieSerial });

                    if (!newSerialExists)
                    {
                        await SqlDataAccess.ExecuteAsync(@"
                    INSERT INTO [PMACS_LIVE].[dbo].[DieMold_DieMaster]
                        (DieSerial, DieNumber, Cavity)
                    VALUES
                        (@DieSerial, @DieNumber, @Cavity);",
                            model);
                    }
                }
                else
                {
                    // Serial is unchanged; ensure the master record exists.
                    await EnsureDieMasterExistsAsync(model);
                }

                // 4. Update the main table after the FK target exists.
                int rows = await SqlDataAccess.ExecuteAsync(@"
            UPDATE DieMold_MoldingMainParts
            SET
                PartDescription = @PartDescription,
                Dimension_Quality = @Dimension_Quality,
                DieSerial = @DieSerial,
                DieNumber = @DieNumber,
                Cavity = @Cavity,
                ProcessID = @ProcessID
            WHERE PartNo = @PartNo;",
                    model);

                if (rows <= 0)
                    return false;

                if (dieSerialChanged)
                {
                    var parameters = new
                    {
                        OldDieSerial = oldDieSerial,
                        NewDieSerial = model.DieSerial,
                        model.DieNumber,
                        model.Cavity
                    };

                    // 5. Update daily records if they exist.
                    await SqlDataAccess.ExecuteAsync(@"
                UPDATE [PMACS_LIVE].[dbo].[DieMold_Daily]
                SET DieSerial = @NewDieSerial
                WHERE DieSerial = @OldDieSerial;",
                        parameters);

                    // 6. Update the old master only if the new serial
                    // did not already exist before the edit.
                    //
                    // If the new serial already existed, keep both master
                    // records unchanged to avoid a duplicate primary key.
                    //
                    // The old master is retained if it is still referenced
                    // by other parts.
                }
                else
                {
                    // Serial unchanged; update its master details.
                    await SqlDataAccess.ExecuteAsync(@"
                UPDATE [PMACS_LIVE].[dbo].[DieMold_DieMaster]
                SET
                    DieNumber = @DieNumber,
                    Cavity = @Cavity
                WHERE DieSerial = @DieSerial;",
                        model);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
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