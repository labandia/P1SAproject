using Parts_locator.Interface;
using Parts_locator.Models;
using Parts_locator.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Parts_locator.Repository
{
    internal class RawMatsRepository : IRawMats
    {
        public Task<bool> AddMasterlist(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMasterlist(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMasterlist(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RawMatModel>> GetRawMatProduct()
        {
            string sql = "SELECT m.Type, b.PartNumber, m.ModelName, m.RotorBush, " +
                                "m.ShaftPartnum, m.ShaftBushAssyPartnum, " +
                                "b.Racks, b.Quantity, i.Sample_img " +
                            "FROM Part_MoldingBushParts m " +
                            "INNER JOIN Part_ProductBushLocation b " +
                            "ON m.PartNumber = b.PartNumber " +
                            "LEFT JOIN Parts_MoldingRawImage i ON i.PartNumber = m.PartNumber ";

            return await SqlDataAccess.GetData<RawMatModel>(sql);
        }

        public Task<List<RawMatModel>> GetRawMatProductByID(int act)
        {
            throw new NotImplementedException();
        }

        public Task<List<RawMatModel>> GetShopOrderlist()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ImportMasterlist(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertTransaction(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRawMatsQuantity(MoldImpeller masterlist)
        {
            throw new NotImplementedException();
        }
    }
}
