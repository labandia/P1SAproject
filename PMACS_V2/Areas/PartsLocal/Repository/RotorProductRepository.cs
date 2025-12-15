
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Helper;
using PMACS_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Repository
{
    public class RotorProductRepository : IProducts
    {
        public async Task<PagedResult<RotorProductModel>> GetRotorMasterlistPage(string search, int pageNumber, int pageSize)
        {
            string strsql = $@"SELECT
                               m.Partnumber, m.ModelName,
                               m.FrontImage, m.BackImage
                            FROM PartsLocatorRotor_Masterlist m
                            WHERE (m.Partnumber LIKE '%' + @search + '%')
                            ORDER BY ModelName ASC
                            OFFSET (@page - 1) * @pageSize ROWS
                            FETCH NEXT @pageSize ROWS ONLY";

            var items = await SqlDataAccess.GetData<RotorProductModel>(strsql, new
            {
                search = search,
                page = pageNumber,
                pageSize = pageSize
            });

            int TotalRecords = items.Count;

            return new PagedResult<RotorProductModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };
        }


        public async Task<bool> AddRotorMasterlist(RotorProductModel rotor)
        {
            // CHECK IF EXIST THEN INSERT AFTERWARDS
            string strsql = $@"INSERT INTO PartsLocatorRotor_Masterlist (Partnumber, ModelName)
                            SELECT @Partnumber, @ModelName
                            WHERE NOT EXISTS (
                                SELECT 1 FROM PartsLocatorRotor_Masterlist 
                                WHERE Partnumber = @Partnumber 
                            )";
       
            bool result = await SqlDataAccess.UpdateInsertQuery(strsql, new { Partnumber = rotor.Partnumber, ModelName = rotor.ModelName });

            if (result)
            {
                string strlocalsql = $@"INSERT INTO PartsLocatorRotor_Location (Partnumber, Area, Quantity)
                            SELECT @Partnumber, @Area, @Quantity
                            WHERE NOT EXISTS (
                                SELECT 1 FROM PartsLocatorRotor_Location 
                                WHERE Partnumber = @Partnumber AND Area =@Area
                            )";

                var parameter = new
                {
                    Partnumber = rotor.Partnumber,
                    Area = rotor.Area,
                    Quantity = rotor.Quantity
                };

                await SqlDataAccess.UpdateInsertQuery(strlocalsql, parameter);
            }

            return result;  
        }

        public async Task<IEnumerable<RotorProductModel>> GetRotorMasterlist()
        {
            string strsql = $@"SELECT
                               m.Partnumber, m.ModelName,
                               m.FrontImage, m.BackImage
                            FROM PartsLocatorRotor_Masterlist m";
            return await SqlDataAccess.GetData<RotorProductModel>(strsql, null);
        }

        

        public async Task<List<RotorProductModel>> GetRotorStorage()
        {
            string strsql = $@"SELECT
                                l.RecordID, 
	                            m.Partnumber, m.ModelName, l.Area, l.Quantity,
	                            m.FrontImage, m.BackImage
                            FROM PartsLocatorRotor_Location l
                            INNER JOIN PartsLocatorRotor_Masterlist m
                            ON m.Partnumber = l.Partnumber
                            ORDER BY l.Area ASC";
            return await SqlDataAccess.GetData<RotorProductModel>(strsql, null);
        }

        public Task<RotorProductModel> GetRotorStorageByID(int ID)
        {
            string strsql = $@"SELECT
                                l.RecordID, 
	                            m.Partnumber, m.ModelName, l.Area, l.Quantity,
	                            m.FrontImage, m.BackImage
                            FROM PartsLocatorRotor_Location l
                            INNER JOIN PartsLocatorRotor_Masterlist m
                            ON m.Partnumber = l.Partnumber
                            WHERE l.RecordID = @RecordID    
                            ORDER BY l.RecordID DESC";
            return SqlDataAccess.GetDataByID<RotorProductModel>(strsql, new { RecordID = ID });
        }

        public Task<bool> UpdateRotorMasterlist(RotorProductModel rotor)
        {
            throw new System.NotImplementedException();
        }
    }
}