using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class HydroPartsRepository : IHyrdoParts
    {
        public Task<List<StockPartsModel>> GetInventoryList()
        {
            string strquery = $@"SELECT
                                    s.StockID,
	                                p.PartNo, 
	                                p.PartName, 
	                                c.CategoryID,
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