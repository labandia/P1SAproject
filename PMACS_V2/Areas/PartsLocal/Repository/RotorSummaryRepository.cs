
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Repository
{
    public class RotorSummaryRepository : IShopOrderIn
    {
       
        public async Task<bool> AddTransactionIN(ShopOrderInModel shop)
        {
            string updatestorage = $@"UPDATE PartsLocatorRotor_Location SET Quantity = Quantity + @Quantity
                                     WHERE Partnumber =@Partnumber AND Area =@Area";

            bool storageResult = await SqlDataAccess.UpdateInsertQuery(updatestorage, new { 
                    Quantity = shop.Quantity, 
                    Partnumber = shop.Partnumber, 
                    Area = shop.Area 
             });

            // if the Update storage is Success proceed to Summary insert
            if (storageResult)
            {
                string strsql = $@"INSERT INTO PartsLocatorRotor_Transaction(TransactionType, Partnumber, RotorOrder, Area, Quantity, PreviousQuantity,  Remarks) 
                              VALUES(0, @Partnumber, @RotorOrder, @Area, @Quantity, @PreviousQuantity,  @Remarks)";

                await SqlDataAccess.UpdateInsertQuery(strsql, shop);
            }

            return storageResult;
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

        public async Task<IEnumerable<ShopOrderInModel>> GetShopOderInlist()
        {
            string strsql = $@"SELECT t.TransactionID, 
                              FORMAT(t.TransactionDate, 'MM/dd/yy') as TransactionDate,
	                          FORMAT(t.TransactionDate, 'hh:mm') as TransactionTime
                              ,t.RotorOrder
                              ,t.Partnumber
	                          ,m.ModelName
	                          ,t.Area
                              ,t.Quantity
                              ,t.PreviousQuantity
                              ,t.Remarks
                          FROM PartsLocatorRotor_Transaction t
                          INNER JOIN PartsLocatorRotor_Masterlist m 
                          ON t.Partnumber = m.Partnumber
                          WHERE  t.TransactionType = 0
                          ORDER BY t.TransactionID DESC";

            return await SqlDataAccess.GetData<ShopOrderInModel>(strsql, null);
        }

      
    }
}