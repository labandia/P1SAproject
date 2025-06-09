using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Parts_locator.Data
{
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
        public string PlanStart {  get; set; }
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
}
