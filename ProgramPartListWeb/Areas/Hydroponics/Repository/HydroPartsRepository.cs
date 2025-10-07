using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
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
                                 PartNo =@PartNo, CurrentQty =@CurrentQty, ReorderLevel =@ReorderLevel, WarningLevel =@WarningLevel, 
                                 Unit =@Unit
                                 WHERE PartNo =@TempPart;";

            var stocksparams = new
            {
                model.PartNo,
                model.CurrentQty,
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