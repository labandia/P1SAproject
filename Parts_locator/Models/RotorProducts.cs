using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.Models
{
    internal class RotorProducts 
    {
        //// ROTOR PROPERTIES
        //public string Pallet { get; set; }
        //public int PalletID { get; set; }


        //// ###################### ROTOR METHODS USE ################################## //
        //public  DataTable SearchProductLocation()
        //{
        //    GlobalDb db = new GlobalDb();

        //    var searchsql = "SELECT l.PartNumber, pa.PalletName, l.PalletID " +
        //                           "FROM Part_ProductPalateLocation l " +
        //                           "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
        //                           "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
        //                           "WHERE pr.PartNumber = '" + Partnum + "'";
        //    return db.GetData(searchsql);
        //}




        ////DISPLAY THE MASTERLIST 
        //public  DataTable getProductList()
        //{

        //    GlobalDb db = new GlobalDb();

        //    var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
        //                    "FROM Part_ProductPalateLocation l " +
        //                    "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
        //                    "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
        //                    "ORDER BY pa.PalletName ASC";
        //    return db.GetData(strsql);
        //}
        //public  DataTable getStorage()
        //{
        //    GlobalDb db = new GlobalDb();

        //    var strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
        //                    "FROM Part_ProductPalateLocation l " +
        //                    "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
        //                    "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
        //                    "WHERE l.PalletID = " + PalletID + " " +
        //                    "ORDER BY pa.ModelName ASC";
        //    return db.GetData(strsql);
        //}
        //public  DataTable getProductDetails()
        //{
        //    GlobalDb db = new GlobalDb();

        //    var strsql = "SELECT TOP 1 l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity " +
        //                    "FROM Part_ProductPalateLocation l " +
        //                    "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
        //                    "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
        //                    "WHERE l.PalletID = " + PalletID + " AND pr.PartNumber = '"+ Partnum +"' ";

        //    return db.GetData(strsql);
        //}


        ////GET THE TOTAL NUMBER OF STORAGE
        //public  int getTotalStorageAmount()
        //{
        //    GlobalDb db = new GlobalDb();
        //    int total = 0;
        //    var strsql = "SELECT SUM(Quantity) as total FROM Part_ProductPalateLocation";

        //    DataTable dt = new DataTable();
        //    dt = db.GetData(strsql);

        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow row = dt.Rows[0];
        //        total = Convert.ToInt32(row["total"].ToString());
        //    }
        //    return total;
        //}

        //// ###################### END OF THE METHODS FOR ROTOR ################################## /
    }

    internal class Transaction
    {
        GlobalDb con;
        public string ShopOrder { get; set; }
        public string finalShopOrder { get; set; }
        public string PartNumber { get; set; }
        public string ModelBase { get; set; }
        public int CurrentQuantity { get; set; }
        public int newQuantity { get; set; }
        public int PLanquan { get; set; }
        public string PlanStart { get; set; }
        public string Inputby { get; set; }
        public string Stats { get; set; }
        public string Bush { get; set; }
        public int Action { get; set; }
        public int PalletID { get; set; }

        public Transaction()
        {

            this.ShopOrder = ShopOrder;
            this.finalShopOrder = finalShopOrder;
            this.CurrentQuantity = CurrentQuantity;
            this.newQuantity = newQuantity;
            this.Inputby = Inputby;
            this.PartNumber = PartNumber;
            this.PalletID = PalletID;
            this.ModelBase = ModelBase;
            this.PLanquan = PLanquan;
            this.PlanStart = PlanStart;
            this.Stats = Stats;
            this.Bush = Bush;
            this.Action = Action;
        }

        // GET THE SUMMARY MONITORING IN 
        public DataTable GetMonitoringIN(string dstart, string dend)
        {
            con = new GlobalDb();

            DataTable dt = new DataTable();
            string strsql = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput,  FORMAT(DateInput, 'hh:mm:ss tt') as Timein, ShopOrder, PartNumber, Quantity, Inputby " +
                     "FROM Part_transaction_shoporder " +
                     "WHERE Action = 0  AND  (CAST(t.DateInput AS DATE) between '" + dstart + "' AND '" + dend + "') " +
                     "ORDER BY TransactionID DESC";
            dt = con.GetData(strsql);
            return dt;
        }

        //GET THE SUMMARY MONIORING OUT
        public DataTable GetMonitoringOUT(string dstart, string dend)
        {
            con = new GlobalDb();
            string strsql = "SELECT FORMAT(t.DateInput, 'MM/dd/yyyy') as DateInput, " +
                            "FORMAT(t.DateInput, 'hh:mm:ss tt') as Timein, " +
                            "t.ShopOrder, t.Plan_qty, t.Plan_date, t.ModelBase, " +
                            "t.PartNumber, p.ModelName, t.Quantity, t.Status, t.BushType, t.Inputby " +
                            "FROM Part_transaction_shoporder t " +
                            "INNER JOIN Part_Products p ON p.PartNumber = t.PartNumber " +
                            "WHERE Action = 1 AND  (CAST(t.DateInput AS DATE) between '" + dstart + "' AND '" + dend + "') " +
                            "ORDER BY TransactionID DESC";
            DataTable dt = con.GetData(strsql);
            return dt;
        }



        // INSERT DATA FOR THE SHOP ORDER MONITORING IN
        public bool Shoporder_IN(int currentlocal)
        {
            con = new GlobalDb();
            bool checks = false;
            // CHECK IF THE LOCATION IS THE SAME
            if (currentlocal == 0)
            {
                int total = CurrentQuantity + newQuantity;

                //ProductDetails.instanceform.partnumtext.Text = "CHANGE SUCCESS";

                //UPDATES THE STORAGE QUANTITY BY PALLETE
                string updatestorage = " UPDATE Part_ProductPalateLocation SET " +
                                       " Quantity =@Quantity  " +
                                       " WHERE PartNumber = @PartNumber AND PalletID =@PalletID";

                SqlParameter[] updateparamaters =
                {
                   new SqlParameter("@Quantity", total),
                   new SqlParameter("@PartNumber", PartNumber),
                   new SqlParameter("@PalletID", PalletID)
                };

                //INSERT A NEW  SHOPORDER DATA
                string shopinserquery = "INSERT INTO Part_transaction_shoporder(ShopOrder, PartNumber, Quantity, Inputby, Action) " +
                                       "VALUES (@ShopOrder, @PartNumber, @Quantity, @Inputby, @Action)";

                SqlParameter[] parameters =
                {
                   new SqlParameter("@ShopOrder", ShopOrder),
                   new SqlParameter("@PartNumber", PartNumber),
                   new SqlParameter("@Quantity",  newQuantity),
                   new SqlParameter("@Inputby", Inputby),
                   new SqlParameter("@Action", Action)
                };

                bool updatesuccess = con.ExecuteCommandUpdate(updatestorage, updateparamaters);
                bool transactsuccess = con.ExecuteCommandUpdate(shopinserquery, parameters);

                if (updatesuccess && transactsuccess)
                {
                    MessageBox.Show("UPDATES THE CURRENT SOTRAGE ");
                    checks = true;
                }
            }
            else
            {
                //INSERT A NEW  lOCATION PALETE
                string insertnewlocationquery = "INSERT INTO Part_ProductPalateLocation(PartNumber, PalletID, Quantity) " +
                                       "VALUES (@PartNumber, @PalletID, @Quantity)";

                SqlParameter[] localparameter =
                {
                   new SqlParameter("@PartNumber", PartNumber),
                   new SqlParameter("@PalletID", currentlocal),
                   new SqlParameter("@Quantity",  Convert.ToInt32(newQuantity))
                };

                //INSERT A NEW  SHOPORDER DATA
                string shopinserquery = "INSERT INTO Part_transaction_shoporder(ShopOrder, PartNumber, Quantity, Inputby, Action) " +
                                       "VALUES (@ShopOrder, @PartNumber, @Quantity, @Inputby, @Action)";

                SqlParameter[] parameters =
                {
                   new SqlParameter("@ShopOrder", ShopOrder),
                   new SqlParameter("@PartNumber", PartNumber),
                   new SqlParameter("@Quantity", newQuantity),
                   new SqlParameter("@Inputby", Inputby),
                   new SqlParameter("@Action", Action)
                };


                bool transactsuccess = con.ExecuteCommandUpdate(shopinserquery, parameters);
                bool updatesuccess = con.ExecuteCommandUpdate(insertnewlocationquery, localparameter);

                if (updatesuccess &&  transactsuccess)
                {
                    MessageBox.Show("INSERT NEW LOCATION SUCCESSFULLY");
                    checks = true;
                }

            }

            return checks;


        }


        // INSERT DATA FOR THE SHOP ORDER MONITORING OUT
        public bool Shoporder_OUT()
        {
            con = new GlobalDb();

            int total = CurrentQuantity - newQuantity;
            bool result = false;

            //UPDATES THE STORAGE QUANTITY BY PALLETE
            string updatestorage = " UPDATE Part_ProductPalateLocation SET " +
                                   " Quantity =@Quantity  " +
                                   " WHERE PartNumber = @PartNumber AND PalletID =@PalletID";

            SqlParameter[] updateparamaters =
            {
                   new SqlParameter("@Quantity", total),
                   new SqlParameter("@PartNumber", PartNumber),
                   new SqlParameter("@PalletID", PalletID)
            };

            string shopinserquery = "INSERT INTO Part_transaction_shoporder(ShopOrder, Plan_qty, Plan_date, ModelBase, PartNumber, Quantity, Status, BushType,  Inputby, Action) " +
                                  "VALUES (@Shoporder, @Plan_qty, @Plan_date, @ModelBase, @PartNumber, @Quantity, @Status, @BushType,  @Inputby, @Action)";

            SqlParameter[] parameters =
            {
                 new SqlParameter("@ShopOrder", ShopOrder),
                 new SqlParameter("@Plan_qty", PLanquan),
                 new SqlParameter("@Plan_date", PlanStart),
                 new SqlParameter("@ModelBase", ModelBase),
                 new SqlParameter("@PartNumber", PartNumber),
                 new SqlParameter("@Quantity", newQuantity),
                 new SqlParameter("@Status", Stats),
                 new SqlParameter("@BushType", Bush),
                 new SqlParameter("@Inputby", Inputby),
                 new SqlParameter("@Action", Action)
            };

            bool updatesuccess = con.ExecuteCommandUpdate(updatestorage, updateparamaters);
            bool transacsuccess = con.ExecuteCommandUpdate(shopinserquery, parameters);

            if (updatesuccess && transacsuccess)
            {
                MessageBox.Show("UPDATES THE CURRENT SOTRAGE  ");
                MessageBox.Show("UPDATES SHOPORDER DATA ");
                result = true;
            }



            return result;
        }

    }


    internal class Transaction_Rotor : RotorProducts
    {
        //GlobalDb con;
        //private string _Shoporder;
        //private string _Fshoporder;
        //private string _Modelbase;
        //private string _PlanStart;
        //private string _Input;
        //private string _Status;
        //private string _Bush;
        //private int _Action;
        //private int _CurrentQuan;
        //private int _NewQuan;
        //private int _PlanQuan;

        //public string ShopOrder { get { return _Shoporder; } set { _Shoporder = value; } }
        //public string finalShopOrder { get { return _Fshoporder; } set { _Fshoporder = value; } }
        //public string ModelBase { get { return _Modelbase; } set { _Modelbase = value; } }
        //public int CurrentQuantity { get { return _CurrentQuan; } set { _CurrentQuan = value; } }
        //public int newQuantity { get { return _NewQuan; } set { _NewQuan = value; } }
        //public int PLanquan { get { return _PlanQuan; } set { _PlanQuan = value; } }
        //public string PlanStart { get { return _PlanStart; } set { _PlanStart = value; } }
        //public string Inputby { get { return _Input; } set { _Input = value; } }
        //public string Stats { get { return _Status; } set { _Status = value; } }
        //public string Bush { get { return _Bush; } set { _Bush = value; } }
        //public int Action { get { return _Action; } set { _Action = value; } }



        //// GET THE SUMMARY MONITORING IN 
        //public DataTable GetMonitoringIN(string dstart, string dend)
        //{
        //    con = new GlobalDb();

        //    DataTable dt = new DataTable();
        //    string strsql = "SELECT FORMAT(DateInput, 'dd/MM/yyyy') as DateInput,  FORMAT(DateInput, 'hh:mm:ss tt') as Timein, ShopOrder, PartNumber, Quantity, Inputby " +
        //             "FROM Part_transaction_shoporder_IN " +
        //             "WHERE  CAST(DateInput AS DATE) between '" + dstart + "' AND '" + dend + "' " +
        //             "ORDER BY TransactionID DESC";
        //    dt = con.GetData(strsql);
        //    return dt;
        //}

        ////GET THE SUMMARY MONIORING OUT
        //public DataTable GetMonitoringOUT(string dstart, string dend)
        //{
        //    con = new GlobalDb();
        //    string strsql = "SELECT FORMAT(t.DateInput, 'MM/dd/yyyy') as DateInput, " +
        //                    "FORMAT(t.DateInput, 'hh:mm:ss tt') as Timein, " +
        //                    "t.ShopOrder, t.Plan_qty, t.Plan_date, t.ModelBase, " +
        //                    "t.PartNumber, p.ModelName, t.Quantity, t.Status, t.BushType, t.Inputby " +
        //                    "FROM Part_transaction_shoporder t " +
        //                    "INNER JOIN Part_Products p ON p.PartNumber = t.PartNumber " +
        //                    "WHERE Action = 1 AND  (CAST(t.DateInput AS DATE) between '" + dstart + "' AND '" + dend + "') " +
        //                    "ORDER BY TransactionID DESC";
        //    DataTable dt = con.GetData(strsql);
        //    return dt;
        //}



        //// INSERT DATA FOR THE SHOP ORDER MONITORING IN
        //public bool Shoporder_IN(int currentlocal)
        //{
        //    con = new GlobalDb();
        //    bool checks = false;
        //    // CHECK IF THE LOCATION IS THE SAME
        //    if (currentlocal == 0)
        //    {
        //        int total = CurrentQuantity + newQuantity;

        //        //ProductDetails.instanceform.partnumtext.Text = "CHANGE SUCCESS";

        //        //UPDATES THE STORAGE QUANTITY BY PALLETE
        //        string updatestorage = " UPDATE Part_ProductPalateLocation SET " +
        //                               " Quantity =@Quantity  " +
        //                               " WHERE PartNumber = @PartNumber AND PalletID =@PalletID";

        //        SqlParameter[] updateparamaters =
        //        {
        //           new SqlParameter("@Quantity", total),
        //           new SqlParameter("@PartNumber", Partnum),
        //           new SqlParameter("@PalletID", PalletID)
        //        };

        //        //INSERT A NEW  SHOPORDER DATA
        //        string shopinserquery = "INSERT INTO Part_transaction_shoporder_IN(ShopOrder, PartNumber, Quantity, Inputby) " +
        //                               "VALUES (@ShopOrder, @PartNumber, @Quantity, @Inputby)";

        //        SqlParameter[] parameters =
        //        {
        //           new SqlParameter("@ShopOrder", ShopOrder),
        //           new SqlParameter("@PartNumber", Partnum),
        //           new SqlParameter("@Quantity",  newQuantity),
        //           new SqlParameter("@Inputby", Inputby)
        //        };

        //        bool updatesuccess = con.ExecuteCommandUpdate(updatestorage, updateparamaters);
        //        bool transactsuccess = con.ExecuteCommandUpdate(shopinserquery, parameters);

        //        if (updatesuccess && transactsuccess)
        //        {
        //            MessageBox.Show("UPDATES THE CURRENT SOTRAGE ");
        //            checks = true;
        //        }
        //    }
        //    else
        //    {
        //        //INSERT A NEW  lOCATION PALETE
        //        string insertnewlocationquery = "INSERT INTO Part_ProductPalateLocation(PartNumber, PalletID, Quantity) " +
        //                               "VALUES (@PartNumber, @PalletID, @Quantity)";

        //        SqlParameter[] localparameter =
        //        {
        //           new SqlParameter("@PartNumber", Partnum),
        //           new SqlParameter("@PalletID", currentlocal),
        //           new SqlParameter("@Quantity",  Convert.ToInt32(newQuantity))
        //        };

        //        //INSERT A NEW  SHOPORDER DATA
        //        string shopinserquery = "INSERT INTO Part_transaction_shoporder_IN(ShopOrder, PartNumber, Quantity, Inputby,) " +
        //                               "VALUES (@ShopOrder, @PartNumber, @Quantity, @Inputby)";

        //        SqlParameter[] parameters =
        //        {
        //           new SqlParameter("@ShopOrder", ShopOrder),
        //           new SqlParameter("@PartNumber", Partnum),
        //           new SqlParameter("@Quantity", newQuantity),
        //           new SqlParameter("@Inputby", Inputby)
        //        };


        //        bool transactsuccess = con.ExecuteCommandUpdate(shopinserquery, parameters);
        //        bool updatesuccess = con.ExecuteCommandUpdate(insertnewlocationquery, localparameter);

        //        if (updatesuccess &&  transactsuccess)
        //        {
        //            MessageBox.Show("INSERT NEW LOCATION SUCCESSFULLY");
        //            checks = true;
        //        }

        //    }

        //    return checks;


        //}


        //// INSERT DATA FOR THE SHOP ORDER MONITORING OUT
        //public bool Shoporder_OUT()
        //{
        //    con = new GlobalDb();

        //    int total = CurrentQuantity - newQuantity;
        //    bool result = false;

        //    //UPDATES THE STORAGE QUANTITY BY PALLETE
        //    string updatestorage = " UPDATE Part_ProductPalateLocation SET " +
        //                           " Quantity =@Quantity  " +
        //                           " WHERE PartNumber = @PartNumber AND PalletID =@PalletID";

        //    SqlParameter[] updateparamaters =
        //    {
        //           new SqlParameter("@Quantity", total),
        //           new SqlParameter("@PartNumber", Partnum),
        //           new SqlParameter("@PalletID", PalletID)
        //    };

        //    string shopinserquery = "INSERT INTO Part_transaction_shoporder(ShopOrder, Plan_qty, Plan_date, ModelBase, PartNumber, Quantity, Status, BushType,  Inputby, Action) " +
        //                          "VALUES (@Shoporder, @Plan_qty, @Plan_date, @ModelBase, @PartNumber, @Quantity, @Status, @BushType,  @Inputby, @Action)";

        //    SqlParameter[] parameters =
        //    {
        //         new SqlParameter("@ShopOrder", ShopOrder),
        //         new SqlParameter("@Plan_qty", PLanquan),
        //         new SqlParameter("@Plan_date", PlanStart),
        //         new SqlParameter("@ModelBase", ModelBase),
        //         new SqlParameter("@PartNumber", Partnum),
        //         new SqlParameter("@Quantity", newQuantity),
        //         new SqlParameter("@Status", Stats),
        //         new SqlParameter("@BushType", Bush),
        //         new SqlParameter("@Inputby", Inputby),
        //         new SqlParameter("@Action", Action)
        //    };

        //    bool updatesuccess = con.ExecuteCommandUpdate(updatestorage, updateparamaters);
        //    bool transacsuccess = con.ExecuteCommandUpdate(shopinserquery, parameters);

        //    if (updatesuccess && transacsuccess)
        //    {
        //        MessageBox.Show("UPDATES THE CURRENT SOTRAGE  ");
        //        MessageBox.Show("UPDATES SHOPORDER DATA ");
        //        result = true;
        //    }



        //    return result;
        //}
    }
}
