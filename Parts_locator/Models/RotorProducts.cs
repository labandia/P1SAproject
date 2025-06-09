using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Models
{
    internal class RotorProducts : Products
    {
        // ROTOR PROPERTIES
        public string Pallet { get; set; }
        public int PalletID { get; set; }


        // ###################### ROTOR METHODS USE ################################## //
        public  DataTable SearchProductLocation()
        {
            GlobalDb db = new GlobalDb();

            var searchsql = "SELECT l.PartNumber, pa.PalletName, l.PalletID " +
                                   "FROM Part_ProductPalateLocation l " +
                                   "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                                   "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                                   "WHERE pr.PartNumber = '" + Partnum + "'";
            return db.GetData(searchsql);
        }




        //DISPLAY THE MASTERLIST 
        public  DataTable getProductList()
        {

            GlobalDb db = new GlobalDb();

            var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "ORDER BY pa.PalletName ASC";
            return db.GetData(strsql);
        }
        public  DataTable getStorage()
        {
            GlobalDb db = new GlobalDb();

            var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "WHERE l.PalletID = " + PalletID + " " +
                            "ORDER BY pa.ModelName ASC";
            return db.GetData(strsql);
        }
        public  DataTable getProductDetails()
        {
            GlobalDb db = new GlobalDb();

            var strsql = "SELECT TOP 1 l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "WHERE l.PalletID = " + PalletID + " AND pr.PartNumber = '"+ Partnum +"' ";

            return db.GetData(strsql);
        }


        //GET THE TOTAL NUMBER OF STORAGE
        public  int getTotalStorageAmount()
        {
            GlobalDb db = new GlobalDb();
            int total = 0;
            var strsql = "SELECT SUM(Quantity) as total FROM Part_ProductPalateLocation";

            DataTable dt = new DataTable();
            dt = db.GetData(strsql);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                total = Convert.ToInt32(row["total"].ToString());
            }
            return total;
        }

        // ###################### END OF THE METHODS FOR ROTOR ################################## /
    }
}
