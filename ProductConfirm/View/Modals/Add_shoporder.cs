using ProductConfirm.Global;
using ProductConfirm.Helper;
using ProductConfirm.Modules;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ProductConfirm.Modals
{
    public partial class Add_shoporder : Form
    {
        private readonly UIShoporder mn;
        private readonly Dataconnect db;

        public int RotorID;

        public Add_shoporder(UIShoporder emp)
        {
            InitializeComponent();
            db = new Dataconnect();
            mn = emp;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            Globalfunction g = new Globalfunction();    
            // CHECK VALIDATION FORM INPUTS
            if (ValidationForm())
            {
                // CHECK IF THE PARTNUMBER EXIST
                if (await checkpartnumberExist())
                {

                    if (await CheckShoporders(PartText.Text))
                    {
                        MessageBox.Show("Shoporder is not yet Done Checking", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO ProdCon_ShopOrder_tbl(Shoporder, Shift, Line, Inputby, RotorProductID) " +
                        "VALUES (@Shoporder, @Shift, @Line, @Inputby, @RotorProductID)";
                        SqlParameter[] parameters =
                        {
                             new SqlParameter("@Partnum", PartText.Text),
                             new SqlParameter("@Shoporder", Shoptext.Text),
                             new SqlParameter("@Shift", g.GetTheShiftSchedule()),
                             new SqlParameter("@Line", Line_text.Text),
                             new SqlParameter("@Inputby", EnterText.Text),
                             new SqlParameter("@RotorProductID", RotorID)

                        };
                        bool success = await db.ExecuteCommandUpdate(insertQuery, parameters);

                        if (success)
                        {
                            MessageBox.Show("Add Data successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            saveRequireMeasurements(PartText.Text, Shoptext.Text, Productcombo.SelectedItem?.ToString());
                            mn.displayshopordertable();
                            Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("ERROR CHECK THE CODE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {               
                    MessageBox.Show("Part number : " + PartText.Text + " Doesn't Exist in the database or the input is incorrect", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }       
       
        }



        public async Task<bool> CheckShoporders(string shop)
        {
            string strsql = "SELECT ShoporderID FROM ProdCon_ShopOrder_tbl " +
                            "WHERE Shoporder = '" + shop + "' AND Stats = 0 ";

            DataTable dt = await db.GetData(strsql);
            return dt.Rows.Count > 0;
        }



        public async void saveRequireMeasurements(string part, string shop, string prod)
        {
            Dataconnect db = new Dataconnect();
            DataTable dt = await db.GetData("SELECT  *  " +
                                               "FROM ProdCon_RotorProduct p " +
                                               "INNER JOIN ProdCon_RotorProductInfo r ON p.RotorProductID = r.RotorProductID " +
                                               "WHERE RotorAssy = '"+ part +"' AND ProductType = '" + prod + "'");
            int item = 0;
            int shopID = await db.GetLastID("ProdCon_ShopOrder_tbl", "ShoporderID");


            DataRow row = dt.Rows[0];

            // Access your columns by name or index
            var caulkmin = row["CaulkingDentMin"] != DBNull.Value ? row["CaulkingDentMin"].ToString() : string.Empty;
            var caulkmax = row["CaulkingDentMax"] != DBNull.Value ? row["CaulkingDentMax"].ToString() : string.Empty;
            var shaftmin = row["ShaftLengthMin"] != DBNull.Value ? row["ShaftLengthMin"].ToString() : string.Empty;
            var shaftmax = row["ShaftLengthMax"] != DBNull.Value ? row["ShaftLengthMax"].ToString() : string.Empty;
            var SEAmin = row["SEA_Min"] != DBNull.Value ? row["SEA_Min"].ToString() : string.Empty;
            var SEAmax = row["SEA_Max"] != DBNull.Value ? row["SEA_Max"].ToString() : string.Empty;
            var ShaftPull = row["ShaftPullingForce"] != DBNull.Value ? row["ShaftPullingForce"].ToString() : string.Empty;
            var BushPull = row["BushPullingForce"] != DBNull.Value ? row["BushPullingForce"].ToString() : string.Empty;
            var MagnetMin = row["MagnetHeightMin"] != DBNull.Value ? row["MagnetHeightMin"].ToString() : string.Empty;
            var MagnetMax = row["MagnetHeightMax"] != DBNull.Value ? row["MagnetHeightMax"].ToString() : string.Empty;

            if (caulkmin != "" && caulkmax != "")
            {
                string insertQuery = "INSERT INTO ProdCon_ShopOrderData_tbl(Item_ID, Shoporder, ShopOrderID) " +
                   "VALUES (@Item_ID, @Shoporder, @ShopOrderID)";

                item = 1;

                SqlParameter[] parameters =
                {
                         new SqlParameter("@Item_ID", item),
                         new SqlParameter("@Shoporder", shop),
                         new SqlParameter("@ShopOrderID", shopID)
                };
                await db.ExecuteCommandUpdate(insertQuery, parameters);


                Debug.WriteLine($"Caulking Dent inserted");
            }
            if (shaftmin != "" && shaftmax != "")
            {
                string insertQuery = "INSERT INTO ProdCon_ShopOrderData_tbl(Item_ID, Shoporder, ShopOrderID) " +
                  "VALUES (@Item_ID, @Shoporder, @ShopOrderID)";

                item = 2;

                SqlParameter[] parameters =
                {
                         new SqlParameter("@Item_ID", item),
                         new SqlParameter("@Shoporder", shop),
                         new SqlParameter("@ShopOrderID", shopID)
                    };
                await db.ExecuteCommandUpdate(insertQuery, parameters);
                Debug.WriteLine($"Shaft Length  inserted");
            }

            if (SEAmin != "" && SEAmax != "")
            {
                string insertQuery = "INSERT INTO ProdCon_ShopOrderData_tbl(Item_ID, Shoporder, ShopOrderID) " +
                  "VALUES (@Item_ID, @Shoporder, @ShopOrderID)";

                item = 3;


                SqlParameter[] parameters =
                {
                         new SqlParameter("@Item_ID", item),
                         new SqlParameter("@Shoporder", shop),
                         new SqlParameter("@ShopOrderID", shopID)
                    };
                await db.ExecuteCommandUpdate(insertQuery, parameters);

                Debug.WriteLine($"Surface edge   inserted");
            }

            if (ShaftPull != "")
            {
                string insertQuery = "INSERT INTO ProdCon_ShopOrderData_tbl(Item_ID, Shoporder, ShopOrderID) " +
                  "VALUES (@Item_ID, @Shoporder, @ShopOrderID)";

                item = 4;

                SqlParameter[] parameters =
                {
                         new SqlParameter("@Item_ID", item),
                         new SqlParameter("@Shoporder", shop),
                         new SqlParameter("@ShopOrderID", shopID)
                    };
                await db.ExecuteCommandUpdate(insertQuery, parameters);
                Debug.WriteLine($"ShaftPullingForce   inserted");
            }

            if (BushPull != "")
            {
                string insertQuery = "INSERT INTO ProdCon_ShopOrderData_tbl(Item_ID, Shoporder, ShopOrderID) " +
                  "VALUES (@Item_ID, @Shoporder, @ShopOrderID)";

                item = 5;

                SqlParameter[] parameters =
                {
                         new SqlParameter("@Item_ID", item),
                         new SqlParameter("@Shoporder", shop),
                         new SqlParameter("@ShopOrderID", shopID)
                    };
                await db.ExecuteCommandUpdate(insertQuery, parameters);
                Debug.WriteLine($"BushPullingForce   inserted");
            }

            if (MagnetMin != "" && MagnetMax != "")
            {
                string insertQuery = "INSERT INTO ProdCon_ShopOrderData_tbl(Item_ID, Shoporder, ShopOrderID) " +
                  "VALUES (@Item_ID, @Shoporder, @ShopOrderID)";

                item = 6;


                SqlParameter[] parameters =
                {
                         new SqlParameter("@Item_ID", item),
                         new SqlParameter("@Shoporder", shop),
                         new SqlParameter("@ShopOrderID", shopID)
                    };
                await db.ExecuteCommandUpdate(insertQuery, parameters);
                Debug.WriteLine($"MagnetHeightMax   inserted");
            }

        }





        // TO CANCEL THE FORMS SUBMIT
        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }


        public async Task<bool> checkpartnumberExist()
        {
            bool check = false;
            string selectedItem = String.Empty;
            // Your code when there is a selected item
            if (Productcombo.SelectedItem != null)
            {      
                selectedItem = Productcombo.SelectedItem.ToString();
            }
            string strsql = "SELECT RotorProductID FROM ProdCon_RotorProduct " +
                            "WHERE RotorAssy = '" + PartText.Text + "' AND ProductType = '" + selectedItem + "'";
            
            if (string.IsNullOrEmpty(PartText.Text))
            {
                MessageBox.Show("Part number is Required");
                return false;
            }
            else
            {
                DataTable dt = await db.GetData(strsql);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    RotorID = Convert.ToInt32(row["RotorProductID"]);
                    check = true;
                }
                else
                {
                    check = false;
                }
           
            }

            return check;
        }


        public bool ValidationForm()
        {
            bool check = true;
            string shop = Shoptext.Text;
            string part = PartText.Text;
            string line = Line_text.Text;
            string enter = EnterText.Text;

            if (string.IsNullOrEmpty(shop))
            {
                shop_error.Visible = true;
                check = false;
            }
            else
            {
                shop_error.Visible = false;
            }

            if (string.IsNullOrEmpty(part))
            {
                part_error.Visible = true;
                check = false;
            }
            else
            {
                part_error.Visible = false;
            }

            if (string.IsNullOrEmpty(line))
            {
                Line_error.Visible = true;
                check = false;
            }
            else
            {
                Line_error.Visible = false;
            }

            

            

            if (string.IsNullOrEmpty(enter))
            {
                Input_error.Visible = true;
                check = false;
            }
            else
            {
                Input_error.Visible = false;
            }

            return check;
        }

        private void Add_shoporder_Load(object sender, EventArgs e)
        {

        }

        private void Productcombo_SelectedIndexChanged(object sender, EventArgs e)
        {

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
