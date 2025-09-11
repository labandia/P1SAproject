using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class HydroPartsRepository : IHyrdoParts
    {
        public Task<List<HydropPartsModel>> GetInventoryList()
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

            return SqlDataAccess.GetData<HydropPartsModel>(strquery);
        }

        public Task<bool> AddInventory(HydropPartsModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateStocks(int ID, int Quan)
        {
            return SqlDataAccess.UpdateInsertQuery($@"UPDATE Hydro_Stocks 
                                                SET CurrentQty =@CurrentQty 
                                                WHERE  PartID =@PartID", 
                                                new { 
                                                    PartID = ID, 
                                                    CurrentQty = Quan 
                                                });
        }
    }
}