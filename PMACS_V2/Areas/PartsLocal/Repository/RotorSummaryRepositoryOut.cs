using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OfficeOpenXml.Packaging.Ionic.Zlib;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Helper;
using PMACS_V2.Models;

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
                              SET RotorOrder =@RotorOrder, ShopOrder =@ShopOrder, PlanQuantity =@PlanQuantity,
                                  ModelBase =@ModelBase, PreviousQuantity =@PreviousQuantity, 
                                  Status =@Status, 
                                  BushType =@BushType
                              WHERE TransactionID =@TransactionID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                RotorOrder = shop.RotorOrder,
                ShopOrder = shop.ShopOrder,
                PreviousQuantity = shop.PreviousQuantity,   
                PlanQuantity = shop.PlanQuantity,
                ModelBase = shop.ModelBase,
                Status = shop.Status,
                BushType = shop.BushType,
                TransactionID = shop.TransactionID
            });
        }

        public async Task<PagedResult<ShopOrderOutModel>> GetShopOderOutlist(
            DateTime startDate,
            DateTime endDate,
            string search,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            string strsql = @"
                        SELECT 
                            t.TransactionID,
                            FORMAT(t.TransactionDate, 'MM/dd/yy') AS TransactionDate,
                            FORMAT(t.TransactionDate, 'hh:mm') AS TransactionTime,
                            t.RotorOrder,
                            t.Partnumber,
                            m.ModelName,
                            t.ShopOrder,
                            t.Area,
                            t.Quantity,
                            t.PreviousQuantity,
                            t.Remarks,
                            FORMAT(t.PlanDate, 'MM/dd/yy') AS PlanDate,
                            t.PlanQuantity,
                            t.ModelBase,
                            t.Status,
                            t.BushType
                        FROM PartsLocatorRotor_Transaction t
                        INNER JOIN PartsLocatorRotor_Masterlist m 
                            ON t.Partnumber = m.Partnumber
                        WHERE t.TransactionType = 1 AND t.IsDelete = 0
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

            var items =  await SqlDataAccess.GetData<ShopOrderOutModel>(
                    strsql,
                    new
                    {
                        StartDate = startDate.Date,
                        EndDate = endDate.Date,
                        Search = string.IsNullOrWhiteSpace(search) ? null : search,
                        Offset = offset,
                        PageSize = pageSize
                    });

            int TotalRecords = items.Count;

            return new PagedResult<ShopOrderOutModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };
        }
    }
}