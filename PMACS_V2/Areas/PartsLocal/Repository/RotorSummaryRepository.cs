
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Repository
{
    public class RotorSummaryRepository : IShopOrderIn, IShopOrderOut
    {
       
        public async Task<bool> AddTransactionIN(ShopOrderInModel shop)
        {
            string updatestorage = $@"UPDATE PartsLocatorRotor_Location SET Quantity = Quantity + @Quantity
                                     WHERE Partnumber =@Partnumber AND Area =@Area";

            bool storageResult = await SqlDataAccess.UpdateInsertQuery(updatestorage, 
                    new { Quantity = shop.Quantity, Partnumber = shop.Partnumber, Area = shop.Area });

            // if the Update storage is Success proceed to Summary insert
            if (storageResult)
            {
                string strsql = $@"INSERT INTO PartsLocatorRotor_Transaction(TransactionType, Partnumber, RotorOrder, Area, Quantity, PreviousQuantity,  Remarks) 
                              VALUES(@TransactionType, @Partnumber, @RotorOrder, @Area, @Quantity, @PreviousQuantity,  @Remarks)";

                await SqlDataAccess.UpdateInsertQuery(strsql, shop);
            }

            return storageResult;
        }

        public Task<bool> AddTransactionOut(ShopOrderOutModel shop)
        {
            string strsql = $@"INSERT INTO PartsLocatorRotor_Transaction(TransactionType, Partnumber, RotorOrder, ShopOrder, PlanQuantity, 
                              PlanDate, ModelBase, Area, Quantity, Remarks, Status, BushType) 
                              VALUE(@TransactionType, @Partnumber, @RotorOrder, @ShopOrder, @Area, @Quantity, @PlanQuantity, 
                              @PlanDate, @Remarks, @ModelBase, @Status, @BushType)";

            return SqlDataAccess.UpdateInsertQuery(strsql, shop);
        }

        public Task<bool> DeleteTransaction(ShopOrderInModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteTransactionOut(ShopOrderOutModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditTransaction(ShopOrderInModel shop)
        {
            string strsql = $@"UPDATE PartsLocatorRotor_Transaction 
                              SET RotorOrder =@RotorOrder, Quantity =@Quantity
                              WHERE TransactionID =@TransactionID";

            return SqlDataAccess.UpdateInsertQuery(strsql, shop);
        }

        public Task<bool> EditTransactionOut(ShopOrderOutModel shop)
        {
            string strsql = $@"UPDATE PartsLocatorRotor_Transaction 
                              SET RotorOrder =@RotorOrder, ShopOrder =@_ShopOrder, Quantity =@Quantity,  PlanQuantity =@PlanQuantity, 
                                  PlanDate =@PlanDate, ModelBase =@ModelBase, Status =@Status, BushType =@BushType        
                              WHERE TransactionID =@TransactionID";

            return SqlDataAccess.UpdateInsertQuery(strsql, shop);
        }

        public Task<IEnumerable<ShopOrderInModel>> GetShopOderInlist()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ShopOrderOutModel>> GetShopOderOutlist()
        {
            throw new System.NotImplementedException();
        }
    }
}