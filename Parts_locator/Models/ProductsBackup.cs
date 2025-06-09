using System;
using System.Data;
using System.Windows.Forms;


namespace Parts_locator.Models
{
    internal class ProductsBackup
    {
        // COMMON PROPERTIES
        public string partnum {  get; set; }
        public string Modelname { get; set; }
        public int Quantity { get; set; } = 0;
        public int SectionID { get; set; }

        // MOLDING PROPERTIES 
        public string RotorBush { get; set; }
        public string ShaftPartnum { get; set; }
        public int Racks { get; set; }

        // ROTOR PROPERTIES
        public string Pallet {  get; set; }
        public int PalletID { get; set; }




        // ###################### ROTOR METHODS USE ################################## //
        public static DataTable SearchProductLocation(string partnum)
        {
            GlobalDb db = new GlobalDb();

            var searchsql = "SELECT l.PartNumber, pa.PalletName, l.PalletID " +
                                   "FROM Part_ProductPalateLocation l " +
                                   "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                                   "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                                   "WHERE pr.PartNumber = '" + partnum + "'";
            return db.GetData(searchsql);
        }
        



        //DISPLAY THE MASTERLIST 
        public static DataTable getProductList()
        {

            GlobalDb db = new GlobalDb();

            var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "ORDER BY pa.PalletName ASC";
            return db.GetData(strsql); 
        }
        public static DataTable getStorage(int pal)
        {
            GlobalDb db = new GlobalDb();

            var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "WHERE l.PalletID = " + pal + " " +
                            "ORDER BY pa.ModelName ASC";
            return db.GetData(strsql); 
        }
        public static DataTable getProductDetails(int pal, string part)
        {
            GlobalDb db = new GlobalDb();

            var strsql = "SELECT TOP 1 l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
                            "FROM Part_ProductPalateLocation l " +
                            "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                            "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                            "WHERE l.PalletID = " + pal + " AND pr.PartNumber = '"+ part +"' ";

            return db.GetData(strsql);
        }


        //GET THE TOTAL NUMBER OF STORAGE
        public static int getTotalStorageAmount()
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

        // ###################### END OF THE METHODS FOR ROTOR ################################## //
    }
}
