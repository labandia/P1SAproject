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
	                                p.PartID, 
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
                                LEFT JOIN Hydro_Stocks s ON s.PartID = p.PartID
                                ORDER BY p.PartID";

            return SqlDataAccess.GetData<StockPartsModel>(strquery, null);
        }
        public Task<List<ChamberTypePartsModel>> GetChamberTypePartsList(int chamberID)
        {
            string strquery = $@"SELECT 
	                                i.PartID,
	                                i.PartNo,
	                                i.PartName,
	                                i.Supplier,
	                                i.Unit,
	                                c.QuantityPerChamber as RequireQty
                                FROM Hydro_ChamberParts c
                                INNER JOIN Hydro_InventoryParts i ON c.PartID = i.PartID 
                                WHERE c.ChamberID = @ChamberID";

            return SqlDataAccess.GetData<ChamberTypePartsModel>(strquery, new { ChamberID = chamberID });
        }

        public Task<bool> AddInventory(StockPartsModel model)
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

        public Task<List<ChamberTypeList>> GetChambersType() => SqlDataAccess.GetData<ChamberTypeList>("SELECT ChamberID, ChamberName FROM Hydro_ChamberMasterlist");

        
    }
}