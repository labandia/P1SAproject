using Parts_locator.Interface;
using Parts_locator.Models;
using Parts_locator.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Parts_locator.Repository
{
    internal class RawMatsRepository : IRawMats
    {
        // =================== GET DATA ========================
        public Task<List<RawMatModel>> GetRawMatProduct()
        {
            string sql = @"SELECT m.Type, b.PartNumber, m.ModelName, m.RotorBush, 
                                m.ShaftPartnum, m.ShaftBushAssyPartnum, 
                                b.Racks, b.Quantity, i.Sample_img 
                            FROM Part_MoldingBushParts m 
                            INNER JOIN Part_ProductBushLocation b 
                            ON m.PartNumber = b.PartNumber 
                            LEFT JOIN Parts_MoldingRawImage i ON i.PartNumber = m.PartNumber ";

            return SqlDataAccess.GetData<RawMatModel>(sql);
        }

        public Task<List<RawMatModel>> GetRawMatProductByID(int act)
        {
            throw new NotImplementedException();
        }
        public async Task<List<RawMatModel>> GetRawMatProductByType(int bushtype)
        {
            try
            {
                var data = await GetRawMatProduct();
                var filterdata = data.Where(res => res.Type == bushtype).ToList();
                return filterdata;
            }
            catch (FormatException)
            {
                return new List<RawMatModel>();
            }
        }
        public Task<List<RawMatSummaryModel>> GetShopOrderlist(int act)
        {
            string strsql = @"SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput, 
                           FORMAT(DateInput, 'HH:mm:ss tt') as TimeInput,PartNumber, 
                           Quantity,Inputby 
                           FROM Part_transaction_BushMold_shoporder 
                           WHERE Action = @Action
                           ORDER BY TransactionID DESC";
            return  SqlDataAccess.GetData<RawMatSummaryModel>(strsql, new { Action = act });
        }


        public Task<bool> AddMasterlist(RawMatModel masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMasterlist(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMasterlist(string partnum, int qty, int type)
        {
            //UPDATES THE STORAGE QUANTITY BY PALLETE
            string updatestorage = $@"UPDATE Part_ProductBushLocation SET 
                                    Quantity =@Quantity 
                                   WHERE PartNumber = @PartNumber AND Racks =@Racks";

            var parameter = new { Quantity = qty, PartNumber = partnum, Racks = type };

            return  SqlDataAccess.UpdateInsertQuery(updatestorage, parameter);   
        }



        public Task<bool> ImportMasterlist(RawMatModel masterlist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertTransaction(RawMatModel masterlist)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRawMatsQuantity(RawMatInputModel raw)
        {
           
            string updatestorage = " UPDATE Part_ProductBushLocation SET " +
                                   " Quantity =@Quantity  " +
                                   " WHERE PartNumber = @PartNumber";
            var updateparam = new { Quantity = raw.Quantity, PartNumber = raw.PartNumber };

            //INSERT A NEW  SHOPORDER DATA
            string shopinserquery = "INSERT INTO Part_transaction_BushMold_shoporder(PartNumber, Quantity, Inputby, Action) " +
                                   "VALUES (@PartNumber, @Quantity, @Inputby, @Action)";
           
            var updateresult =  SqlDataAccess.UpdateInsertQuery(updatestorage, updateparam);
            var insertresult =  SqlDataAccess.UpdateInsertQuery(shopinserquery, raw);

            bool[] results = await Task.WhenAll(updateresult, insertresult);

            return results.All(r => r);
        }
    }
}
