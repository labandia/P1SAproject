using ProductConfirm.Data;
using ProductConfirm.Global;
using ProductConfirm.Modules;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProductConfirm.Modals
{
    public partial class ShaftLength : Form
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


        public ShaftLength(UIShoporder sh)
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
            string strsql = "SELECT s.Shoporder, sp.SL_supply, sp.SL_lot, a.SL_first, a.SL_second, a.SL_third,a.SL_fourth, a.SL_fifth " +
                            "FROM ProdCon_ShopOrder_tbl s " +
                            "INNER JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = s.ShopOrderID AND s.ShopOrderID = " + measureID + " " +
                            "INNER JOIN ProdCon_ShopOrder_ActualData a ON a.ShopOrderID = s.ShopOrderID AND s.ShopOrderID = " + measureID + "  ";

            DataTable bp = new DataTable();
            bp = await db.GetData(strsql);


            if (bp.Rows.Count > 0)
            {
                DataRow row = bp.Rows[0];
 
                SupplierText.Text = row["SL_supply"] != DBNull.Value ? (string)row["SL_supply"] : "";
                ShaftText.Text = row["SL_lot"] != DBNull.Value ? (string)row["SL_lot"] : "";
                firstText.Text = row["SL_first"] != DBNull.Value ? Convert.ToDecimal(row["SL_first"]).ToString() : "";
                SecondText.Text = row["SL_second"] != DBNull.Value ? Convert.ToDecimal(row["SL_second"]).ToString() : "";
                thirdText.Text = row["SL_third"] != DBNull.Value ? Convert.ToDecimal(row["SL_third"]).ToString() : "";
                fourthText.Text = row["SL_fourth"] != DBNull.Value ? Convert.ToDecimal(row["SL_fourth"]).ToString() : "";
                FifthText.Text = row["SL_fifth"] != DBNull.Value ? Convert.ToDecimal(row["SL_fifth"]).ToString() : "";
            }

        }

        public async void GetMinandMax()
        {
            DataTable bp = await Products.GetMinandMax(RotorID);
            if (bp.Rows.Count > 0)
            {
                DataRow row = bp.Rows[0];
                min = row["ShaftLengthMin"] != DBNull.Value ? Convert.ToDouble(row["ShaftLengthMin"]) : 0.0;
                max = row["ShaftLengthMax"] != DBNull.Value ? Convert.ToDouble(row["ShaftLengthMax"]) : 0.0;
                modelText.Text = "Model name: " + row["ProductType"].ToString();
            }

            rangetext.Text = "Standard Value:" + min + " - " + max;
            changetextbackground();
        }

        private void ShaftLength_Load(object sender, EventArgs e)
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

        private async void Updatetbtn_Click(object sender, EventArgs e)
        {
            // Checks the input if is empty or not
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
                         " SL_supply =@SL_supply, SL_lot =@SL_lot " +
                         " WHERE Shoporder = @Shoporder AND ShopOrderID =@ShopOrderID";
                    SqlParameter[] updateparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@SL_supply", SupplierText.Text),
                     new SqlParameter("@SL_lot", ShaftText.Text),
                     new SqlParameter("@ShopOrderID", ID)
                };

                    await db.ExecuteCommandUpdate(updatesql, updateparameter);
                }
                else
                {
                    // INSERT THE THE SHOPORDER SUPPLIER
                    string measurequery = "INSERT INTO ProdCon_ShopOrder_supplier(Shoporder, SL_supply, SL_lot, ShopOrderID) " +
                     "VALUES (@Shoporder, @SL_supply, @SL_lot, @ShopOrderID)";

                    SqlParameter[] measurepararmeter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@SL_supply", SupplierText.Text),
                     new SqlParameter("@SL_lot", ShaftText.Text),
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
                                           " SL_first =@SL_first, SL_second =@SL_second, SL_third =@SL_third, SL_fourth =@SL_fourth, SL_fifth =@SL_fifth " +
                                           " WHERE Shoporder =@Shoporder AND ShopOrderID =@ShopOrderID";
                    SqlParameter[] updatedataparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@SL_first", string.IsNullOrWhiteSpace(firstText.Text) ? 0 :  Convert.ToDouble(firstText.Text)),
                     new SqlParameter("@SL_second", string.IsNullOrWhiteSpace(SecondText.Text) ? 0 :  Convert.ToDouble(SecondText.Text)),
                     new SqlParameter("@SL_third", string.IsNullOrWhiteSpace(thirdText.Text) ? 0 :   Convert.ToDouble(thirdText.Text)),
                     new SqlParameter("@SL_fourth", string.IsNullOrWhiteSpace(fourthText.Text) ? 0 :  Convert.ToDouble(fourthText.Text)),
                     new SqlParameter("@SL_fifth", string.IsNullOrWhiteSpace(FifthText.Text) ? 0 :  Convert.ToDouble(FifthText.Text)),
                     new SqlParameter("@ShopOrderID", ID)
                 };
                    await db.ExecuteCommandUpdate(updatesqldata, updatedataparameter);
                }
                else
                {
                    // INSERT THE THE SHOPORDER ACTUAL DATA
                    string dataquery = "INSERT INTO ProdCon_ShopOrder_ActualData(Shoporder, SL_first, SL_second, SL_third, SL_fourth, SL_fifth, ShopOrderID) " +
                     "VALUES (@Shoporder, @SL_first, @SL_second, @SL_third, @SL_fourth, @SL_fifth, @ShopOrderID)";

                    SqlParameter[] dataparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@SL_first", string.IsNullOrWhiteSpace(firstText.Text) ? 0 : Convert.ToDouble(firstText.Text)),
                     new SqlParameter("@SL_second", string.IsNullOrWhiteSpace(SecondText.Text) ? 0 :  Convert.ToDouble(SecondText.Text)),
                     new SqlParameter("@SL_third", string.IsNullOrWhiteSpace(thirdText.Text) ? 0 :  Convert.ToDouble(thirdText.Text)),
                     new SqlParameter("@SL_fourth", string.IsNullOrWhiteSpace(fourthText.Text) ? 0 :  Convert.ToDouble(fourthText.Text)),
                     new SqlParameter("@SL_fifth", string.IsNullOrWhiteSpace(FifthText.Text) ? 0 :  Convert.ToDouble(FifthText.Text)),
                     new SqlParameter("@ShopOrderID", ID)
                  };
                    await db.ExecuteCommandUpdate(dataquery, dataparameter);
                }

               
                ui.displaymeasurementTable(shopordersec, ID);
                Visible = false;
            }
        }

        private void firstText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(firstText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    firstText.BackColor = Color.White;
                    firstText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    firstText.BackColor = Color.FromArgb(250, 214, 210);
                    firstText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                firstText.BackColor = Color.White;
                firstText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void SecondText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(SecondText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    SecondText.BackColor = Color.White;
                    SecondText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    SecondText.BackColor = Color.FromArgb(250, 214, 210);
                    SecondText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                SecondText.BackColor = Color.White;
                SecondText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void thirdText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(thirdText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    thirdText.BackColor = Color.White;
                    thirdText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    thirdText.BackColor = Color.FromArgb(250, 214, 210);
                    thirdText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                thirdText.BackColor = Color.White;
                thirdText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void fourthText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(fourthText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    fourthText.BackColor = Color.White;
                    fourthText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    fourthText.BackColor = Color.FromArgb(250, 214, 210);
                    fourthText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                fourthText.BackColor = Color.White;
                fourthText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void FifthText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(FifthText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    FifthText.BackColor = Color.White;
                    FifthText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    FifthText.BackColor = Color.FromArgb(250, 214, 210);
                    FifthText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                FifthText.BackColor = Color.White;
                FifthText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void firstText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !firstText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void SecondText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !SecondText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void thirdText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !thirdText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void fourthText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !fourthText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void FifthText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !FifthText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }


        public void changetextbackground()
        {
           
            // First Textbox
            if (double.TryParse(firstText.Text, out double value))
            {
                bool a = value >= min && value <= max;
                firstText.BackColor = (a) ? Color.White : Color.FromArgb(250, 214, 210);
                firstText.ForeColor = (a) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                firstText.BackColor = Color.White;
                firstText.ForeColor = Color.FromArgb(64, 64, 64);
            }

            // Second Textbox
            if (double.TryParse(SecondText.Text, out double value1))
            {
                bool b = value1 >= min && value1 <= max;
                SecondText.BackColor = (b) ? Color.White : Color.FromArgb(250, 214, 210);
                SecondText.ForeColor = (b) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                SecondText.BackColor = Color.White;
                SecondText.ForeColor = Color.FromArgb(64, 64, 64);
            }


            // Third Textbox
            if (double.TryParse(thirdText.Text, out double value2))
            {
                bool c = value2 > min && value2 < max;
                thirdText.BackColor = (c) ? Color.White : Color.FromArgb(250, 214, 210);
                thirdText.ForeColor = (c) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                thirdText.BackColor = Color.White;
                thirdText.ForeColor = Color.FromArgb(64, 64, 64);
            }

            // Fourth Textbox
            if (double.TryParse(fourthText.Text, out double value3))
            {
                bool d = value3 > min && value3 < max;
                fourthText.BackColor = (d) ? Color.White : Color.FromArgb(250, 214, 210);
                fourthText.ForeColor = (d) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                fourthText.BackColor = Color.White;
                fourthText.ForeColor = Color.FromArgb(64, 64, 64);
            }

            // Fifth Textbox
            if (double.TryParse(FifthText.Text, out double value4))
            {
                bool e = value4 > min && value4 < max;
                FifthText.BackColor = (e) ? Color.White : Color.FromArgb(250, 214, 210);
                FifthText.ForeColor = (e) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                FifthText.BackColor = Color.White;
                FifthText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void firstText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(firstText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = firstText.Text.Trim();
                firstText.Text = scannedCode;
                SecondText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void SecondText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(SecondText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = SecondText.Text.Trim();
                SecondText.Text = scannedCode;
                thirdText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void thirdText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(thirdText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = thirdText.Text.Trim();
                thirdText.Text = scannedCode;
                FifthText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void FifthText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(FifthText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = FifthText.Text.Trim();
                FifthText.Text = scannedCode;
                fourthText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void fourthText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(fourthText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = fourthText.Text.Trim();
                fourthText.Text = scannedCode;
                e.SuppressKeyPress = true;

            }
        }

        public void CheckStatusInput()
        {
            int stats = Int32.Parse(StatusID.Text);

            if (stats == 1)
            {

                SupplierText.Enabled = false;
                ShaftText.Enabled = false;
                firstText.Enabled = false;
                SecondText.Enabled = false;
                thirdText.Enabled = false;
                fourthText.Enabled = false;
                FifthText.Enabled = false;
        
            }
        }
    }
}
