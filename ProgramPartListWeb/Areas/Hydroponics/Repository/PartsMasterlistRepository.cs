using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class PartsMasterlistRepository : IPartsList
    {
        public Task<List<MasterlistPartsModel>> GetPartsMasterlist()
        {
            string strsql = $@"SELECT 
	                            i.PartID,
	                            i.PartNo,
	                            i.PartName,
                                c.CategoryID,
	                            c.CategoryName,
	                            i.Supplier,
	                            i.UnitCost_PHP,
	                            i.ImageParts
                            FROM Hydro_InventoryParts i
                            INNER JOIN Hydro_CategoryParts c ON c.CategoryID = i.CategoryID";
            return SqlDataAccess.GetData<MasterlistPartsModel>(strsql);
        }

        public Task<bool> AddMasterlistParts(MasterlistPartsModel p)
        {
            // 1. Check and Insert A new Partnumber
            string partinsertquery = $@"INSERT INTO Hydro_InventoryParts(PartNo, PartName, CategoryID, Supplier, UnitCost_PHP, ImageParts)
                                        SELECT @PartNo, @PartName, @CategoryID, @Supplier, @UnitCost_PHP, @ImageParts
                                        WHERE NOT EXISTS(
	                                        SELECT 1 FROM Hydro_InventoryParts WHERE PartNo =@PartNo
                                        );";
            var insertparams = new
            {
                PartNo = p.PartNo,
                PartName = p.PartName,
                CategoryID = p.CategoryID,
                Supplier = p.Supplier,
                UnitCost_PHP = p.UnitCost_PHP,
                ImageParts = p.ImageParts
            };

            return SqlDataAccess.UpdateInsertQuery(partinsertquery, insertparams);
        }

        public Task<bool> EditMasterlistParts(MasterlistPartsModel p)
        {
            // 1. Check and Insert A new Partnumber
            string partinsertquery = $@"UPDATE Hydro_InventoryParts 
                                        SET PartNo =@PartNo, PartName =@PartName, CategoryID =@CategoryID, Supplier =@Supplier, 
                                            UnitCost_PHP =@UnitCost_PHP, ImageParts =@ImageParts
                                        WHERE PartID =@PartID";
            var insertparams = new
            {
                PartNo = p.PartNo,
                PartName = p.PartName,
                CategoryID = p.CategoryID,
                Supplier = p.Supplier,
                UnitCost_PHP = p.UnitCost_PHP,
                ImageParts = p.ImageParts,
                PartID = p.PartID
            };

            return SqlDataAccess.UpdateInsertQuery(partinsertquery, insertparams);
        }

        
    }
}