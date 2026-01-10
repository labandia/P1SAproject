
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Helper;

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
            string strsql = $@"UPDATE PartsLocatorRotor_Transaction SET IsDelete = 1 WHERE TransactionID =@TransactionID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                TransactionID = shop.TransactionID
            }); 
        }


        public Task<bool> EditTransaction(ShopOrderInModel shop)
        {
            string strsql = $@"UPDATE PartsLocatorRotor_Transaction 
                              SET RotorOrder =@RotorOrder, Quantity =@Quantity, 
                                Remarks =@Remarks
                              WHERE TransactionID =@TransactionID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                RotorOrder = shop.RotorOrder,
                Quantity = shop.Quantity,
                Remarks = shop.Remarks,
                TransactionID = shop.TransactionID
            });
        }


        public async Task<IEnumerable<ShopOrderInModel>> GetShopOderInlist(DateTime startDate,
            DateTime endDate,
            string search,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

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
                          WHERE  t.TransactionType = 0 AND t.IsDelete = 0
                          AND t.TransactionDate >= @StartDate
                          AND t.TransactionDate < DATEADD(DAY, 1, @EndDate)
                          AND (
                                @Search IS NULL
                                OR t.Partnumber LIKE '%' + @Search + '%'
                                OR m.ModelName LIKE '%' + @Search + '%'
                                OR CAST(t.RotorOrder AS varchar(50)) LIKE '%' + @Search + '%'
                              )
                          ORDER BY t.TransactionID DESC
                          OFFSET @Offset ROWS
                          FETCH NEXT @PageSize ROWS ONLY";

            return await SqlDataAccess.GetData<ShopOrderInModel>(
                   strsql,
                   new
                   {
                       StartDate = startDate.Date,
                       EndDate = endDate.Date,
                       Search = string.IsNullOrWhiteSpace(search) ? null : search,
                       Offset = offset,
                       PageSize = pageSize
                   });
        }

      
    }
}