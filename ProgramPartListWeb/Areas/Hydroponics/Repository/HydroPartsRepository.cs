using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class HydroPartsRepository : IHyrdoParts
    {
        public async Task<bool> AddInventory(AddInventoryModel model)
        {
            bool result = false;
            // Step 1: Insert into Hydro_InventoryParts table
            string insertPartQuery = $@"INSERT INTO Hydro_InventoryParts 
                                        (PartNo, PartName, CategoryID, Supplier, Unit, ImageParts, Unit_Price) 
                                        VALUES 
                                        (@PartNo, @PartName, @CategoryID, @Supplier, @Unit, @ImageParts, @Unit_Price)";
            var partParaers = new
            {
                model.PartNo,
                model.PartName,
                model.CategoryID,
                model.Supplier,
                model.Unit,
                model.ImageParts,
                model.Unit_Price
            };



            // Step 2: Insert into Hydro_Stocks table
            string insertStockQuery = $@"INSERT INTO Hydro_Stocks 
                                         (PartNo, CurrentQty, ReorderLevel, WarningLevel, Unit) 
                                         VALUES 
                                         (@PartNo, @CurrentQty, @ReorderLevel, @WarningLevel, @Unit)";
            var stockPramers = new
            {
                model.PartNo,
                model.CurrentQty,
                model.ReorderLevel,
                model.WarningLevel,
                model.Unit
            };

            bool parstResult = await SqlDataAccess.UpdateInsertQuery(insertPartQuery, partParaers);

            if (!parstResult) return false;
            
            result = await SqlDataAccess.UpdateInsertQuery(insertStockQuery, stockPramers);

            return result;
        }

        public async Task<bool> AddStockItem(List<AddStocksItem> stocks)
        {
            bool result = false;    
   
            foreach (var item in stocks)
            {
                // Step 1:  Update Hydro_Stocks
                string strsql = $@"UPDATE Hydro_Stocks 
                              SET CurrentQty = CurrentQty + @CurrentQty 
                               WHERE  PartNo =@PartNo";

                await SqlDataAccess.UpdateInsertQuery(strsql, new
                {
                    PartNo = item.PartNo,
                    CurrentQty = item.quantity
                });


                //// 2️⃣ Auto-allocate to incomplete order details
                //string incompleteOrdersQuery = @"
                //        SELECT od.OrderID, od.PartNo, od.RequiredQty, od.QtyUsed, s.CurrentQty
                //        FROM Hydro_OrderDetails od
                //        JOIN Hydro_Stocks s ON od.PartNo = s.PartNo
                //        WHERE od.PartNo = @PartNo
                //          AND od.QtyUsed < od.RequiredQty
                //        ORDER BY od.OrderID ASC";

                //var incompleteOrders = await SqlDataAccess.GetData<IncompleteOrderDetail>(incompleteOrdersQuery, new { PartNo = item.PartNo });


                //foreach(var order in incompleteOrders)
                //{
                //    double requiredQty = order.RequiredQty;
                //    double qtyUsed = order.QtyUsed;
                //    double currentStock = order.QtyUsed;
                //    double remainingQty = requiredQty - qtyUsed;


                //    // Determine allocation amount
                //    double qtyToAllocate = Math.Min(remainingQty, currentStock);

                //    if (qtyToAllocate <= 0)
                //        continue; // nothing to allocate


                //    // If A 

                //}



                result = true;
            }

            return result;
        }

        

        public async Task<bool> EditInventory(AddInventoryModel model, string partno)
        {
            bool result = false;    
            // Step 1: Update Hydro_InventoryParts table
            var updateQuery = $@"UPDATE Hydro_InventoryParts SET
                                 PartNo =@PartNo, PartName =@PartName, CategoryID =@CategoryID, 
                                 Supplier =@Supplier, Unit =@Unit, ImageParts =@ImageParts,
                                 Unit_Price =@Unit_Price 
                                 WHERE PartNo =@TempPart;";

            var parameters = new
            {
                model.PartNo,
                model.PartName,
                model.CategoryID,
                model.Supplier,
                model.Unit,
                model.ImageParts,
                model.Unit_Price,
                TempPart = partno
            };

            // Step 2: Update Hydro_Stocks table
            var updateStockQuery = $@"UPDATE Hydro_Stocks SET
                                 PartNo =@PartNo, ReorderLevel =@ReorderLevel, WarningLevel =@WarningLevel, 
                                 Unit =@Unit
                                 WHERE PartNo =@TempPart;";

            var stocksparams = new
            {
                model.PartNo,
                model.ReorderLevel,
                model.WarningLevel,
                model.Unit,
                TempPart = partno
            };

            bool parstResult = await SqlDataAccess.UpdateInsertQuery(updateQuery, parameters);

            if (!parstResult) return false;

            result = await SqlDataAccess.UpdateInsertQuery(updateStockQuery, stocksparams);

            return result;
        }

        public async Task<IEnumerable<StockAddDetailsModel>> GetAddStocksDetails(int ID)
        {
            string strquery = $@"SELECT
	                            s.PartNo, 
	                            i.PartName,
	                            s.QuantityRequested,
	                            i.Unit
                            FROM Hydro_StockRequestItems s 
                            INNER JOIN Hydro_InventoryParts i ON i.PartNo = s.PartNo
                            WHERE s.RequestID = @RequestID";

            return await SqlDataAccess.GetData<StockAddDetailsModel>(strquery, new { RequestID  = ID });
        }

        public async Task<IEnumerable<StockAddModel>> GetAddStocksList()
        {
            string strquery = $@"SELECT
	                            s.RequestID,
	                            s.RequestNo,
	                            s.RequestedBy,
                                s.Purpose,
	                            FORMAT(s.RequestDate, 'MM/dd/yyyy') as RequestDate,
	                            (SELECT COUNT(*) FROM Hydro_StockRequestItems WHERE RequestID = s.RequestID) as NoItems
                            FROM Hydro_StockRequests s
                            ORDER BY RequestID DESC";

            return await SqlDataAccess.GetData<StockAddModel>(strquery, null);
        }

        public Task<List<StockPartsModel>> GetInventoryList()
        {
            string strquery = $@"SELECT
                                    s.StockID,
	                                p.PartNo, 
	                                p.PartName, 
	                                c.CategoryID,
	                                p.Unit_Price,
	                                c.CategoryName,
	                                p.Supplier,
	                                p.Unit,
	                                s.CurrentQty,
	                                s.ReorderLevel,
	                                s.WarningLevel,
	                                s.Status,
	                                s.LastUpdated,
                                    p.ImageParts
                                FROM Hydro_InventoryParts p
                                LEFT JOIN Hydro_CategoryParts c ON c.CategoryID = p.CategoryID
                                LEFT JOIN Hydro_Stocks s ON s.PartNo = p.PartNo
                                ORDER BY s.StockID ASC";

            return SqlDataAccess.GetData<StockPartsModel>(strquery, null);
        }

        public Task<bool> IncrementAndDecreaseStocks(int StockID, int CurrentQty, int Required)
        {
            return SqlDataAccess.UpdateInsertQuery("UPDATE Hydro_Stocks SET CurrentQty =@CurrentQty WHERE StockID =@StockID",
                new
                {
                    CurrentQty = CurrentQty,
                    StockID = StockID,
                });
        }

        public Task<bool> UpdateStocks(int ID, int Quan)
        {
            string strsql = $@"UPDATE Hydro_Stocks SET CurrentQty =@CurrentQty 
                               WHERE  PartID =@PartID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new { 
                                                    PartID = ID, 
                                                    CurrentQty = Quan 
                                                });
        }


        public Task<bool> UpdateWarning(int StockID, double WarningLevel)
        {
            string strsql = $@"UPDATE Hydro_Stocks 
                               SET WarningLevel =@WarningLevel 
                               WHERE  StockID =@StockID";

            return SqlDataAccess.UpdateInsertQuery(strsql,
                                                new
                                                {
                                                    StockID = StockID,
                                                    WarningLevel = WarningLevel
                                                });
        }
    }
}