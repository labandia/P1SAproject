using ProductConfirm.Data;
using ProductConfirm.Global;
using ProductConfirm.Modules;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProductConfirm.Modals
{
    public partial class MagnetHeight : Form
    {
        private UIShoporder ui;
        private Dataconnect db;

        public int ID { get; set; }
        public int itemID { get; set; }
        public int measureID { get; set; }
        public string partnum { get; set; }
        public string shopordersec { get; set; }
        public int RotorID { get; set; }



        public int mode = 0;
        public double min;
        public double max;


        public MagnetHeight(UIShoporder sh)
        {
            InitializeComponent();
            db  = new Dataconnect();
            this.ui = sh;
        }


        public bool ValidationForm()
        {
            bool check = true;
            string shop = SupplierText.Text;
            string lot = ShaftText.Text;

            if (string.IsNullOrEmpty(shop))
            {
                supply_error.Visible = true;
                check = false;
            }
            else
            {
                supply_error.Visible = false;
            }

            if (string.IsNullOrEmpty(lot))
            {
                Shaft_error.Visible = true;
                check = false;
            }
            else
            {
                Shaft_error.Visible = false;
            }
            return check;
        }



        public async void Displyalreadyinput()
        {
            // MessageBox.Show("adsad" + measureID);
            string strsql = "SELECT s.Shoporder, sp.MH_supply, sp.MH_lot, " +
                            "a.MH_first_min, a.MH_second_min, a.MH_third_min, a.MH_fourth_min, a.MH_fifth_min, " +
                            "a.MH_first_max, a.MH_second_max, a.MH_third_max, a.MH_fourth_max, a.MH_fifth_max " +
                            "FROM ProdCon_ShopOrder_tbl s " +
                            "INNER JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = s.ShopOrderID AND s.ShopOrderID = " + measureID + " " +
                           "INNER JOIN ProdCon_ShopOrder_ActualData a ON a.ShopOrderID = s.ShopOrderID AND s.ShopOrderID = " + measureID + "  ";

            DataTable bp = new DataTable();
            bp = await db.GetData(strsql);


            if (bp.Rows.Count > 0)
            {
                DataRow row = bp.Rows[0];

                SupplierText.Text = row["MH_supply"] != DBNull.Value ? (string)row["MH_supply"] : "";
                ShaftText.Text = row["MH_lot"] != DBNull.Value ? (string)row["MH_lot"] : "";
                firstmin.Text = row["MH_first_min"] != DBNull.Value ? Convert.ToDecimal(row["MH_first_min"]).ToString() : "";
                secondmin.Text = row["MH_second_min"] != DBNull.Value ? Convert.ToDecimal(row["MH_second_min"]).ToString() : "";
                thirdmin.Text = row["MH_third_min"] != DBNull.Value ? Convert.ToDecimal(row["MH_third_min"]).ToString() : "";
                fourthmin.Text = row["MH_fourth_min"] != DBNull.Value ? Convert.ToDecimal(row["MH_fourth_min"]).ToString() : "";
                fifthmin.Text = row["MH_fifth_min"] != DBNull.Value ? Convert.ToDecimal(row["MH_fifth_min"]).ToString() : "";
                firstmax.Text = row["MH_first_max"] != DBNull.Value ? Convert.ToDecimal(row["MH_first_max"]).ToString() : "";
                secondmax.Text = row["MH_second_max"] != DBNull.Value ? Convert.ToDecimal(row["MH_second_max"]).ToString() : "";
                thirdmax.Text = row["MH_third_max"] != DBNull.Value ? Convert.ToDecimal(row["MH_third_max"]).ToString() : "";
                fourthmax.Text = row["MH_fourth_max"] != DBNull.Value ? Convert.ToDecimal(row["MH_fourth_max"]).ToString() : "";
                fifthmax.Text = row["MH_fifth_max"] != DBNull.Value ? Convert.ToDecimal(row["MH_fifth_max"]).ToString() : "";
            }

        }
        public async void GetMinandMax()
        {
            DataTable bp = await Products.GetMinandMax(RotorID);
            if (bp.Rows.Count > 0)
            {
                DataRow row = bp.Rows[0];
                min = row["MagnetHeightMin"] != DBNull.Value ? Convert.ToDouble(row["MagnetHeightMin"]) : 0.0;
                max = row["MagnetHeightMax"] != DBNull.Value ? Convert.ToDouble(row["MagnetHeightMax"]) : 0.0;
                modelText.Text = "Model name: " + row["ProductType"].ToString();
            }

            rangetext.Text = "Standard Value: " + min + " - " + max;
            changetextbackground();
        }



        private async void updatebtn_Click(object sender, EventArgs e)
        {
            if (ValidationForm())
            {
                int stats = 1;
                string updatesmeasure = "UPDATE ProdCon_ShopOrderData_tbl SET Status =@Status " +
                                        "WHERE Item_ID =@Item_ID AND ShopOrderID =@ShopOrderID";
                SqlParameter[] shopParameters =
                {
                    new SqlParameter("@Status", stats),
                    new SqlParameter("@Item_ID", itemID),
                    new SqlParameter("@ShopOrderID", ID)
                };

                await db.ExecuteCommandUpdate(updatesmeasure, shopParameters);



                //CHECK IF THE ProdCon_ShopOrder_supplier IS ALREADY INSERTED 
                string strsupply = "SELECT p.Shoporder FROM ProdCon_ShopOrder_supplier p " +
                                "WHERE p.ShopOrderID = " + ID + "";
                if (await db.CheckifExist(strsupply))
                {
                    string updatesql = " UPDATE ProdCon_ShopOrder_supplier SET " +
                         " MH_supply =@MH_supply, MH_lot =@MH_lot " +
                         " WHERE Shoporder = @Shoporder AND ShopOrderID =@ShopOrderID";
                    SqlParameter[] updateparameter =
                    {
                         new SqlParameter("@Shoporder", shopordersec),
                         new SqlParameter("@MH_supply", SupplierText.Text),
                         new SqlParameter("@MH_lot", ShaftText.Text),
                         new SqlParameter("@ShopOrderID", ID)
                    };

                    await db.ExecuteCommandUpdate(updatesql, updateparameter);
                }
                else
                {
                    // INSERT THE THE SHOPORDER SUPPLIER
                    string measurequery = "INSERT INTO ProdCon_ShopOrder_supplier(Shoporder, MH_supply, MH_lot, ShopOrderID) " +
                     "VALUES (@Shoporder, @MH_supply, @MH_lot, @ShopOrderID)";

                    SqlParameter[] measurepararmeter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@MH_supply", SupplierText.Text),
                     new SqlParameter("@MH_lot", ShaftText.Text),
                     new SqlParameter("@ShopOrderID", ID)
                };

                    await db.ExecuteCommandUpdate(measurequery, measurepararmeter);
                }


                //CHECK IF THE ProdCon_ShopOrder_ActualData IS ALREADY INSERTED 
                string stractualData = "SELECT p.Shoporder FROM ProdCon_ShopOrder_ActualData p " +
                             "WHERE p.ShopOrderID = " + ID + "";
                if (await db.CheckifExist(stractualData))
                {
                    string updatesqldata = " UPDATE ProdCon_ShopOrder_ActualData SET " +
                                           " MH_first_min =@MH_first_min, MH_second_min =@MH_second_min, MH_third_min =@MH_third_min, MH_fourth_min =@MH_fourth_min, MH_fifth_min =@MH_fifth_min, " +
                                           " MH_first_max =@MH_first_max, MH_second_max =@MH_second_max, MH_third_max =@MH_third_max, MH_fourth_max =@MH_fourth_max, MH_fifth_max =@MH_fifth_max " +
                                           " WHERE Shoporder =@Shoporder AND ShopOrderID =@ShopOrderID";
                    SqlParameter[] updatedataparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@MH_first_min", string.IsNullOrWhiteSpace(firstmin.Text) ? 0 : Convert.ToDouble(firstmin.Text)),
                     new SqlParameter("@MH_second_min", string.IsNullOrWhiteSpace(secondmin.Text) ? 0 : Convert.ToDouble(secondmin.Text)),
                     new SqlParameter("@MH_third_min", string.IsNullOrWhiteSpace(thirdmin.Text) ? 0 : Convert.ToDouble(thirdmin.Text)),
                     new SqlParameter("@MH_fourth_min", string.IsNullOrWhiteSpace(fourthmin.Text) ? 0 : Convert.ToDouble(fourthmin.Text)),
                     new SqlParameter("@MH_fifth_min", string.IsNullOrWhiteSpace(fifthmin.Text) ? 0 : Convert.ToDouble(fifthmin.Text)),
                     new SqlParameter("@MH_first_max", string.IsNullOrWhiteSpace(firstmax.Text) ? 0 : Convert.ToDouble(firstmax.Text)),
                     new SqlParameter("@MH_second_max", string.IsNullOrWhiteSpace(secondmax.Text) ? 0 : Convert.ToDouble(secondmax.Text)),
                     new SqlParameter("@MH_third_max", string.IsNullOrWhiteSpace(thirdmax.Text) ? 0 : Convert.ToDouble(thirdmax.Text)),
                     new SqlParameter("@MH_fourth_max", string.IsNullOrWhiteSpace(fourthmax.Text) ? 0 : Convert.ToDouble(fourthmax.Text)),
                     new SqlParameter("@MH_fifth_max", string.IsNullOrWhiteSpace(fifthmax.Text) ? 0 : Convert.ToDouble(fifthmax.Text)),
                     new SqlParameter("@ShopOrderID", ID)
                 };
                    await db.ExecuteCommandUpdate(updatesqldata, updatedataparameter);
                }
                else
                {
                    // INSERT THE THE SHOPORDER ACTUAL DATA
                    string dataquery = "INSERT INTO ProdCon_ShopOrder_ActualData(Shoporder, MH_first_min, MH_second_min, MH_third_min, MH_fourth_min, MH_fifth_min, MH_first_max, MH_second_max, MH_third_max, MH_fourth_max, MH_fifth_max, ShopOrderID) " +
                     "VALUES (@Shoporder, @MH_first_min, @MH_second_min, @MH_third_min, @MH_fourth_min, @MH_fifth_min, @MH_first_max, @MH_second_max, @MH_third_max, @MH_fourth_max,  @MH_fifth_max, @ShopOrderID)";

                    SqlParameter[] dataparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@MH_first_min", string.IsNullOrWhiteSpace(firstmin.Text) ? 0 : Convert.ToDouble(firstmin.Text)),
                     new SqlParameter("@MH_second_min", string.IsNullOrWhiteSpace(secondmin.Text) ? 0 : Convert.ToDouble(secondmin.Text)),
                     new SqlParameter("@MH_third_min", string.IsNullOrWhiteSpace(thirdmin.Text) ? 0 : Convert.ToDouble(thirdmin.Text)),
                     new SqlParameter("@MH_fourth_min", string.IsNullOrWhiteSpace(fourthmin.Text) ? 0 : Convert.ToDouble(fourthmin.Text)),
                     new SqlParameter("@MH_fifth_min", string.IsNullOrWhiteSpace(fifthmin.Text) ? 0 : Convert.ToDouble(fifthmin.Text)),
                     new SqlParameter("@MH_first_max", string.IsNullOrWhiteSpace(firstmax.Text) ? 0 : Convert.ToDouble(firstmax.Text)),
                     new SqlParameter("@MH_second_max", string.IsNullOrWhiteSpace(secondmax.Text) ? 0 : Convert.ToDouble(secondmax.Text)),
                     new SqlParameter("@MH_third_max", string.IsNullOrWhiteSpace(thirdmax.Text) ? 0 : Convert.ToDouble(thirdmax.Text)),
                     new SqlParameter("@MH_fourth_max", string.IsNullOrWhiteSpace(fourthmax.Text) ? 0 : Convert.ToDouble(fourthmax.Text)),
                     new SqlParameter("@MH_fifth_max", string.IsNullOrWhiteSpace(fifthmax.Text) ? 0 : Convert.ToDouble(fifthmax.Text)),
                     new SqlParameter("@ShopOrderID", ID)
                  };
                    await db.ExecuteCommandUpdate(dataquery, dataparameter);
                }

               
                ui.displaymeasurementTable(shopordersec, ID);
                Visible = false;
            }
        }

        private void MagnetHeight_Load(object sender, EventArgs e)
        {
            Displyalreadyinput();
            GetMinandMax();
            CheckStatusInput();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }

        private void firstmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !firstmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void secondmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !secondmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void thirdmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !thirdmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void fourthmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !fourthmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void fifthmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !fifthmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void firstmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !firstmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void secondmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !secondmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void thirdmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !thirdmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void fourthmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !fourthmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void fifthmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !fifthmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void firstmin_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(firstmin.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    firstmin.BackColor = Color.White;
                    firstmin.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    firstmin.BackColor = Color.FromArgb(250, 214, 210);
                    firstmin.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                firstmin.BackColor = Color.White;
                firstmin.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void secondmin_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(secondmin.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    secondmin.BackColor = Color.White;
                    secondmin.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    secondmin.BackColor = Color.FromArgb(250, 214, 210);
                    secondmin.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                secondmin.BackColor = Color.White;
                secondmin.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void thirdmin_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(thirdmin.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    thirdmin.BackColor = Color.White;
                    thirdmin.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    thirdmin.BackColor = Color.FromArgb(250, 214, 210);
                    thirdmin.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                thirdmin.BackColor = Color.White;
                thirdmin.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void fourthmin_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(fourthmin.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    fourthmin.BackColor = Color.White;
                    fourthmin.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    fourthmin.BackColor = Color.FromArgb(250, 214, 210);
                    fourthmin.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                fourthmin.BackColor = Color.White;
                fourthmin.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void fifthmin_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(fifthmin.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    fifthmin.BackColor = Color.White;
                    fifthmin.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    fifthmin.BackColor = Color.FromArgb(250, 214, 210);
                    fifthmin.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                fifthmin.BackColor = Color.White;
                fifthmin.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void firstmax_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(firstmax.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    firstmax.BackColor = Color.White;
                    firstmax.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    firstmax.BackColor = Color.FromArgb(250, 214, 210);
                    firstmax.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                firstmax.BackColor = Color.White;
                firstmax.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void secondmax_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(secondmax.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    secondmax.BackColor = Color.White;
                    secondmax.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    secondmax.BackColor = Color.FromArgb(250, 214, 210);
                    secondmax.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                secondmax.BackColor = Color.White;
                secondmax.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void thirdmax_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(thirdmax.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    thirdmax.BackColor = Color.White;
                    thirdmax.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    thirdmax.BackColor = Color.FromArgb(250, 214, 210);
                    thirdmax.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                thirdmax.BackColor = Color.White;
                thirdmax.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void fourthmax_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(fourthmax.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    fourthmax.BackColor = Color.White;
                    fourthmax.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    fourthmax.BackColor = Color.FromArgb(250, 214, 210);
                    fourthmax.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                fourthmax.BackColor = Color.White;
                fourthmax.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void fifthmax_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(fifthmax.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    fifthmax.BackColor = Color.White;
                    fifthmax.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    fifthmax.BackColor = Color.FromArgb(250, 214, 210);
                    fifthmax.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                fifthmax.BackColor = Color.White;
                fifthmax.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }


        public void changetextbackground()
        {
            // First Textbox
            if (double.TryParse(firstmin.Text, out double value))
            {
                bool a = value >= min && value <= max;
                firstmin.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                firstmin.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                firstmin.BackColor = Color.White;
            }

            // Second Textbox
            if (double.TryParse(secondmin.Text, out double value1))
            {
                bool a = value1 >= min && value1 <= max;
                secondmin.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                secondmin.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                secondmin.BackColor = Color.White;
            }


            // Third Textbox
            if (double.TryParse(thirdmin.Text, out double value2))
            {
                bool a = value2 >= min && value2 <= max;
                thirdmin.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                thirdmin.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                thirdmin.BackColor = Color.White;
            }

            // Fourth Textbox
            if (double.TryParse(fourthmin.Text, out double value3))
            {
                bool a = value3 >= min && value3 <= max;
                fourthmin.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                fourthmin.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                fourthmin.BackColor = Color.White;
            }

            // Fifth Textbox
            if (double.TryParse(fifthmin.Text, out double value4))
            {
                bool a = value4 >= min && value4 <= max;
                fifthmin.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                fifthmin.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                fifthmin.BackColor = Color.White;
            }


            // Six Textbox
            if (double.TryParse(firstmax.Text, out double value5))
            {
                bool a = value5 >= min && value5 <= max;
                firstmax.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                firstmax.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                firstmax.BackColor = Color.White;
            }


            // Fifth Textbox
            if (double.TryParse(secondmax.Text, out double value6))
            {
                bool a = value6 >= min && value6 <= max;
                secondmax.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                secondmax.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                secondmax.BackColor = Color.White;
            }


            // Fifth Textbox
            if (double.TryParse(thirdmax.Text, out double value7))
            {
                bool a = value7 >= min && value7 <= max;
                thirdmax.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                thirdmax.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                thirdmax.BackColor = Color.White;
            }


            // Fifth Textbox
            if (double.TryParse(fourthmax.Text, out double value8))
            {
                bool a = value8 >= min && value8 <= max;
                fourthmax.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                fourthmax.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                fourthmax.BackColor = Color.White;
            }

            // Fifth Textbox
            if (double.TryParse(fifthmax.Text, out double value9))
            {
                bool a = value9 >= min && value9 <= max;
                fifthmax.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                fifthmax.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                fifthmax.BackColor = Color.White;
            }

        }

        private void firstmin_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(firstmin.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = firstmin.Text.Trim();
                firstmin.Text = scannedCode;
                secondmin.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void secondmin_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(secondmin.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = secondmin.Text.Trim();
                secondmin.Text = scannedCode;
                thirdmin.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void thirdmin_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(thirdmin.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = thirdmin.Text.Trim();
                thirdmin.Text = scannedCode;
                fourthmin.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void fourthmin_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(fourthmin.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = fourthmin.Text.Trim();
                fourthmin.Text = scannedCode;
                fifthmin.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void fifthmin_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(fifthmin.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = fifthmin.Text.Trim();
                fifthmin.Text = scannedCode;
                firstmax.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void firstmax_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(firstmax.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = firstmax.Text.Trim();
                firstmax.Text = scannedCode;
                secondmax.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void secondmax_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(secondmax.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = secondmax.Text.Trim();
                secondmax.Text = scannedCode;
                thirdmax.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void thirdmax_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(thirdmax.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = thirdmax.Text.Trim();
                thirdmax.Text = scannedCode;
                fourthmax.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void fourthmax_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(fourthmax.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = fourthmax.Text.Trim();
                fourthmax.Text = scannedCode;
                fifthmax.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void fifthmax_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(fifthmax.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = fifthmax.Text.Trim();
                fifthmax.Text = scannedCode;
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }


        public void CheckStatusInput()
        {
            int stats = Int32.Parse(StatusID.Text);

            if (stats == 1)
            {

                SupplierText.Enabled = false;
                ShaftText.Enabled = false;
                firstmin.Enabled = false;
                firstmax.Enabled = false;
                secondmin.Enabled = false;
                secondmax.Enabled = false;
                thirdmin.Enabled = false;
                thirdmax.Enabled = false;
                fourthmin.Enabled = false;
                fourthmax.Enabled = false;
                fifthmin.Enabled = false;
                fifthmax.Enabled = false;
            }
        }
    }
}
