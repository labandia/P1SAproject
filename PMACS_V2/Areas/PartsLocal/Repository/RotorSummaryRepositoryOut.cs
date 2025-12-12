using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.PartsLocal.Repository
{
    public class RotorSummaryRepositoryOut : IShopOrderOut
    {
        public async Task<bool> AddTransactionOut(ShopOrderOutModel shop)
        {
            string updatestorage = $@"UPDATE PartsLocatorRotor_Location SET Quantity = Quantity - @Quantity
                                     WHERE Partnumber =@Partnumber AND Area =@Area";

            bool storageResult = await SqlDataAccess.UpdateInsertQuery(updatestorage, new
            {
                Quantity = shop.Quantity,
                Partnumber = shop.Partnumber,
                Area = shop.Area
            });


            string strsql = $@"INSERT INTO PartsLocatorRotor_Transaction(TransactionType, Partnumber, RotorOrder, ShopOrder, PlanQuantity, 
                              PlanDate, ModelBase, Area, Quantity, Remarks, Status, BushType, PreviousQuantity) 
                              VALUES(1, @Partnumber, @RotorOrder, @ShopOrder, @PlanQuantity, 
                              @PlanDate, @ModelBase, @Area, @Quantity, @Remarks, @Status, @BushType, @PreviousQuantity)";

            return await SqlDataAccess.UpdateInsertQuery(strsql, shop);
        }

        public Task<bool> DeleteTransactionOut(ShopOrderOutModel shop)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditTransactionOut(ShopOrderOutModel shop)
        {
            string strsql = $@"UPDATE PartsLocatorRotor_Transaction 
                              SET RotorOrder =@RotorOrder, Quantity =@Quantity
                              WHERE TransactionID =@TransactionID";

            return SqlDataAccess.UpdateInsertQuery(strsql, shop);
        }

        public  async Task<IEnumerable<ShopOrderOutModel>> GetShopOderOutlist()
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
	                            ,t.PlanDate
	                            ,t.PlanQuantity
	                            ,t.ModelBase
	                            ,t.Status
	                            ,t.BushType
                            FROM PartsLocatorRotor_Transaction t
                            INNER JOIN PartsLocatorRotor_Masterlist m 
                            ON t.Partnumber = m.Partnumber
                            WHERE  t.TransactionType = 1";

            return await SqlDataAccess.GetData<ShopOrderOutModel>(strsql, null);
        }
    }
}