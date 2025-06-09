using ProductConfirm.Data;
using ProductConfirm.Global;
using ProductConfirm.Modules;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm.Modals
{
    public partial class Edit_shoporder : Form
    {
        private readonly UIShoporder _shop;
        private readonly Dataconnect db;
        public int ID { get; set; }  

        public Edit_shoporder(UIShoporder shop)
        {
            InitializeComponent();
            db = new Dataconnect();
            _shop = shop;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            int shop = await checkpartnumberExist();

            if (shop > 0)
            {
                Dataconnect db = new Dataconnect();
                string updateQuery = "UPDATE ProdCon_ShopOrder_tbl SET  RotorProductID =@RotorProductID, Shoporder =@Shoporder, Line =@Line, Inputby =@Inputby" +
                                     " WHERE ShoporderID =@ShoporderID";


                SqlParameter[] parameters =
                {
                        new SqlParameter("@ShoporderID", ID),
                        new SqlParameter("@RotorProductID", shop),
                        new SqlParameter("@Shoporder", Shoptext.Text),
                        new SqlParameter("@Line", Line_text.Text),
                        new SqlParameter("@Inputby", EnterText.Text)
                };

                bool success = await db.ExecuteCommandUpdate(updateQuery, parameters);

                if (success)
                {
                    MessageBox.Show("Update successfully");
                    _shop.displayshopordertable();
                   // _emp.Displayemployee(depid);
                   // _emp.comboBox1.SelectedIndex = comboBox1.SelectedIndex;
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Update Error please contact the Developer of the system");
                }
            }
            else
            {
                MessageBox.Show($"Part number {PartText.Text} is not exist in the database");
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }


        public async Task<int> checkpartnumberExist()
        {
            int result = 0;
            Products p = new Products();

            DataTable dt = await p.getOneproduct(PartText.Text, Productcombo.Text);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    result = dr.Field<int>("RotorProductID");
                }
            }
            else
            {
                result = 0; 
            }
            return result;

        }

        private void Edit_shoporder_Load(object sender, EventArgs e)
        {
            Displaypartnum();
        }

        public async void Displaypartnum()
        {
            Dataconnect db = new Dataconnect();

            string scannedCode = PartText.Text.Trim();
            PartText.Text = scannedCode;

            var strsql = "SELECT  ProductType " +
                         "FROM ProdCon_RotorProduct " +
                         "WHERE RotorAssy = '" + PartText.Text + "'";
            DataTable table = await db.GetData(strsql);
            Productcombo.Items.Clear();

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Productcombo.Items.Add(row[0].ToString());
                }
                Productcombo.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Wrong input Rotor assy partnumber");
            }
        }

        private async void PartText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string scannedCode = PartText.Text.Trim();
                PartText.Text = scannedCode;

                var strsql = "SELECT  ProductType " +
                             "FROM ProdCon_RotorProduct " +
                             "WHERE RotorAssy = '" + PartText.Text + "'";
                DataTable table = await db.GetData(strsql);
                Productcombo.Items.Clear();

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Productcombo.Items.Add(row[0].ToString());
                    }
                    Productcombo.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Wrong input Rotor assy partnumber");
                }

            }
        }

        private void PartText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13 || e.KeyChar == (char)10)
            {
                e.Handled = true;

                string scannedCode = PartText.Text.Trim();
                PartText.Text = scannedCode;
            }
        }
    }
}
