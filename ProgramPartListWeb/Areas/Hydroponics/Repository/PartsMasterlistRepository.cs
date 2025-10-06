using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class PartsMasterlistRepository : IPartsList
    {
        public async Task<IEnumerable<MasterlistPartsModel>> GetPartsMasterlist()
        {
            string strsql = $@"SELECT 
	                            i.PartNo,
	                            i.PartName,
                                c.CategoryID,
	                            c.CategoryName,
	                            i.Supplier,
	                            i.ImageParts,
                                i.Unit
                            FROM Hydro_InventoryParts i
                            INNER JOIN Hydro_CategoryParts c ON c.CategoryID = i.CategoryID";
            return await SqlDataAccess.GetData<MasterlistPartsModel>(strsql, null);
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
                ImageParts = p.ImageParts
            };

            return SqlDataAccess.UpdateInsertQuery(partinsertquery, insertparams);
        }

        public Task<bool> EditMasterlistParts(MasterlistPartsModel p)
        {
            // 1. Check and Insert A new Partnumber
            string partinsertquery = $@"UPDATE Hydro_InventoryParts 
                                        SET  PartName =@PartName, CategoryID =@CategoryID, Supplier =@Supplier,
                                            ImageParts =@ImageParts
                                        WHERE PartNo =@PartNo";

            Debug.WriteLine($@"Part-No : {p.PartNo} - Part Name : {p.PartName}");
            var insertparams = new
            {
                PartNo = p.PartNo,
                PartName = p.PartName,
                CategoryID = p.CategoryID,
                Supplier = p.Supplier,
                ImageParts = p.ImageParts
            };

            
            return SqlDataAccess.UpdateInsertQuery(partinsertquery, insertparams);
        }

        
    }
}