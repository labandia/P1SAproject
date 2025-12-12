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
        public Task<bool> AddTransactionOut(ShopOrderOutModel shop)
        {
            string strsql = $@"INSERT INTO PartsLocatorRotor_Transaction(TransactionType, Partnumber, RotorOrder, ShopOrder, PlanQuantity, 
                              PlanDate, ModelBase, Area, Quantity, Remarks, Status, BushType) 
                              VALUE(1, @Partnumber, @RotorOrder, @ShopOrder, @Area, @Quantity, @PlanQuantity, 
                              @PlanDate, @Remarks, @ModelBase, @Status, @BushType)";

            return SqlDataAccess.UpdateInsertQuery(strsql, shop);
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
                          FROM PartsLocatorRotor_Transaction t
                          INNER JOIN PartsLocatorRotor_Masterlist m 
                          ON t.Partnumber = m.Partnumber
                          WHERE  t.TransactionType = 0";

            return await SqlDataAccess.GetData<ShopOrderOutModel>(strsql, null);
        }
    }
}