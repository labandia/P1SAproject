using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class StockpartsRepository : IStocksparts
    {
        
        public Task<List<StockPartsModel>> GetStocksTracking()
        {
            string strquery = $@"SELECT
	                                p.PartID, 
	                                p.PartNo, 
	                                p.PartName, 
	                                c.CategoryID,
	                                c.CategoryName,
	                                p.Supplier,
	                                p.Unit,
	                                p.UnitCost_USD,
                                    p.UnitCost_PHP,
	                                s.CurrentQty,
	                                s.ReorderLevel,
	                                s.WarningLevel,
	                                s.Status,
	                                s.LastUpdated
                                FROM Hydro_InventoryParts p
                                LEFT JOIN Hydro_CategoryParts c ON c.CategoryID = p.CategoryID
                                LEFT JOIN Hydro_Stocks s ON s.PartID = p.PartID
                                ORDER BY p.PartID";

            return SqlDataAccess.GetData<StockPartsModel>(strquery);
        }

        public Task<List<StockPartsModel>> GetTransactionStocks()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddStocks(int ID, double Quan)
        {
            var data = await GetStocksTracking() ?? new List<StockPartsModel>();  
            var filterdata = data.FirstOrDefault(x => x.PartID == ID);  

            double latestQty = filterdata.CurrentQty + Quan;

            // 1. Updates the Stocks Quantity 
            return await SqlDataAccess.UpdateInsertQuery($@"UPDATE Hydro_Stocks 
                                                SET CurrentQty =@CurrentQty 
                                                WHERE  PartID =@PartID",
                                             new
                                             {
                                                 PartID = ID,
                                                 CurrentQty = latestQty
                                             });

            // 2. Insert Transaction Table


        }
    }
}