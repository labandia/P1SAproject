using Parts_locator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.DataAccess
{
    internal class SQLMainlayoutRespository : IProductRepository
    {
        public void AddProduct()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllProducts()
        {
            GlobalDb db = new GlobalDb();

            var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "ORDER BY pa.PalletName ASC";
            return db.GetData(strsql);
        }

        public DataTable SearchProductLocation(string partnum)
        {
            GlobalDb db = new GlobalDb();

            var searchsql = "SELECT l.PartNumber, pa.PalletName, l.PalletID " +
                                   "FROM Part_ProductPalateLocation l " +
                                   "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                                   "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                                   "WHERE pr.PartNumber = '" + partnum + "'";
            return db.GetData(searchsql);
        }

      
    }
}
