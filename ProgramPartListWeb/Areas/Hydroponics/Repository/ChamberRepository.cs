using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class ChamberRepository : IChambers
    {
        public Task<List<ChamberModel>> GetChambersData(int chamber)
        {
            string strsql = $@"SELECT
	                            c.PartID, 
	                            i.PartNo,
	                            i.PartName,
	                            cp.CategoryName,
	                            i.Supplier,
	                            CONCAT(c.QuantityPerChamber, ' ', i.Unit) as RequireQty,
	                            i.UnitCost_PHP,
	                            c.QuantityPerChamber * i.UnitCost_PHP as TotalPHPCost
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartID = i.PartID
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            WHERE c.ChamberID = @ChamberID";
            return SqlDataAccess.GetData<ChamberModel>(strsql, new { ChamberID = chamber });
        }

        public Task<bool> AdditionalChambers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Deletechambers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditChambers()
        {
            throw new NotImplementedException();
        }

        
    }
}