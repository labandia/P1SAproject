using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Parts_locator.Models
{
    internal class Transaction_Rotor : RotorProducts
    {
        GlobalDb con;
        private string _Shoporder;
        private string _Fshoporder;
        private string _Modelbase;
        private string _PlanStart;
        private string _Input;
        private string _Status;
        private string _Bush;
        private int _Action;
        private int _CurrentQuan;
        private int _NewQuan;
        private int _PlanQuan;

        public string ShopOrder { get { return _Shoporder;  } set { _Shoporder = value; } }
        public string finalShopOrder { get { return _Fshoporder; } set { _Fshoporder = value; } }
        public string ModelBase { get { return _Modelbase; } set { _Modelbase = value; } }
        public int CurrentQuantity { get { return _CurrentQuan; } set { _CurrentQuan = value; } }
        public int newQuantity { get { return _NewQuan; } set { _NewQuan = value; } }
        public int PLanquan { get { return _PlanQuan; } set { _PlanQuan = value; } }
        public string PlanStart { get { return _PlanStart; } set { _PlanStart = value; } }
        public string Inputby { get { return _Input; } set { _Input = value; } }
        public string Stats { get { return _Status; } set { _Status = value; } }
        public string Bush { get { return _Bush; } set { _Bush = value; } }
        public int Action { get { return _Action; } set { _Action = value; } }



        // GET THE SUMMARY MONITORING IN 
        public DataTable GetMonitoringIN(string dstart, string dend)
        {
            con = new GlobalDb();

            DataTable dt = new DataTable();
            string strsql = "SELECT FORMAT(DateInput, 'dd/MM/yyyy') as DateInput,  FORMAT(DateInput, 'hh:mm:ss tt') as Timein, ShopOrder, PartNumber, Quantity, Inputby " +
                     "FROM Part_transaction_shoporder_IN " +
                     "WHERE  CAST(DateInput AS DATE) between '" + dstart + "' AND '" + dend + "' " +
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
                   new SqlParameter("@PartNumber", Partnum),
                   new SqlParameter("@PalletID", PalletID)
                };

                //INSERT A NEW  SHOPORDER DATA
                string shopinserquery = "INSERT INTO Part_transaction_shoporder_IN(ShopOrder, PartNumber, Quantity, Inputby) " +
                                       "VALUES (@ShopOrder, @PartNumber, @Quantity, @Inputby)";

                SqlParameter[] parameters =
                {
                   new SqlParameter("@ShopOrder", ShopOrder),
                   new SqlParameter("@PartNumber", Partnum),
                   new SqlParameter("@Quantity",  newQuantity),
                   new SqlParameter("@Inputby", Inputby)
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
                   new SqlParameter("@PartNumber", Partnum),
                   new SqlParameter("@PalletID", currentlocal),
                   new SqlParameter("@Quantity",  Convert.ToInt32(newQuantity))
                };

                //INSERT A NEW  SHOPORDER DATA
                string shopinserquery = "INSERT INTO Part_transaction_shoporder_IN(ShopOrder, PartNumber, Quantity, Inputby,) " +
                                       "VALUES (@ShopOrder, @PartNumber, @Quantity, @Inputby)";

                SqlParameter[] parameters =
                {
                   new SqlParameter("@ShopOrder", ShopOrder),
                   new SqlParameter("@PartNumber", Partnum),
                   new SqlParameter("@Quantity", newQuantity),
                   new SqlParameter("@Inputby", Inputby)
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
                   new SqlParameter("@PartNumber", Partnum),
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
                 new SqlParameter("@PartNumber", Partnum),
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
}
