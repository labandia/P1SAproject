using Parts_locator.Modals;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class MoldShoporder_IN_dialog : Form
    {
        public string part;
        public int currentquan;
        public int newquan;
        public int rack;
        public int action;

        public MoldShoporder_IN_dialog(string part, int currentquan, int newquan, int rack, int act)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.rack = rack;
            this.action = act;
            this.newquan = newquan;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            GlobalDb db = new GlobalDb();
            int total = 0;

            if (action == 0)
            {
                total = currentquan + newquan;
            }
            else
            {
                total =  currentquan - newquan;
            }

            //ProductDetails.instanceform.partnumtext.Text = "CHANGE SUCCESS";

            //UPDATES THE STORAGE QUANTITY BY PALLETE
            string updatestorage = " UPDATE Part_ProductBushLocation SET " +
                                   " Quantity =@Quantity  " +
                                   " WHERE PartNumber = @PartNumber";

            SqlParameter[] updateparamaters =
            {
                   new SqlParameter("@Quantity", total),
                   new SqlParameter("@PartNumber", part)
            };

            //INSERT A NEW  SHOPORDER DATA
            string shopinserquery = "INSERT INTO Part_transaction_BushMold_shoporder(PartNumber, Quantity, Inputby, Action) " +
                                   "VALUES (@PartNumber, @Quantity, @Inputby, @Action)";

            SqlParameter[] parameters =
            {
                   new SqlParameter("@PartNumber", part),
                   new SqlParameter("@Quantity",  newquan),
                   new SqlParameter("@Inputby", inputText.Text),
                   new SqlParameter("@Action", action)
                };

            bool updatesuccess = db.ExecuteCommandUpdate(updatestorage, updateparamaters);
            bool transactsuccess = db.ExecuteCommandUpdate(shopinserquery, parameters);

            if (updatesuccess && transactsuccess)
            {
                //MessageBox.Show("New shoporder Entered ");
                BushPartDetails.instanceform.RawQuantity.Text = Convert.ToString(total);
                Visible = false;
                this.Hide();
            }
        }

        private void MoldShoporder_IN_dialog_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
