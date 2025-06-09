using ProductConfirm.Data;
using ProductConfirm.Global;
using ProductConfirm.Modules;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;


namespace ProductConfirm.Modals
{
    public partial class CaulkDent : Form
    {
        private UIShoporder ui;


        public int ID { get; set; }
        public int RotorID { get; set; }
        public int itemID { get; set; }
        public int measureID { get; set; }
        public string partnum { get; set; }
        public string shopordersec { get; set; }
       
       
        public int mode = 0;
        public double min;
        public double max;
       

        public CaulkDent(UIShoporder sh)
        {
            InitializeComponent();
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



        private  async void button2_Click(object sender, EventArgs e)
        {
            Dataconnect db = new Dataconnect();
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
                          " CD_supply =@CD_supply, CD_lot =@CD_lot " +
                          " WHERE Shoporder =@Shoporder AND ShopOrderID =@ShopOrderID";
                    SqlParameter[] updateparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@CD_supply", SupplierText.Text),
                     new SqlParameter("@CD_lot", ShaftText.Text),
                     new SqlParameter("@ShopOrderID", ID)
                };

                    await db.ExecuteCommandUpdate(updatesql, updateparameter);
                }
                else
                {
                    // INSERT THE THE SHOPORDER SUPPLIER
                    string measurequery = "INSERT INTO ProdCon_ShopOrder_supplier(Shoporder, CD_supply, CD_lot, ShopOrderID) " +
                     "VALUES (@Shoporder, @CD_supply, @CD_lot, @ShopOrderID)";

                    SqlParameter[] measurepararmeter =
                    {
                   new SqlParameter("@Shoporder", shopordersec),
                   new SqlParameter("@CD_supply", SupplierText.Text),
                   new SqlParameter("@CD_lot", ShaftText.Text),
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
                                       " CD_first =@CD_first, CD_second =@CD_second, CD_third =@CD_third, CD_fourth =@CD_fourth, CD_fifth =@CD_fifth, CD_six =@CD_six, CD_seven =@CD_seven, CD_eight =@CD_eight  " +
                                       " WHERE Shoporder = @Shoporder AND ShopOrderID =@ShopOrderID";
                    SqlParameter[] updatedataparameter =
                    {
                     new SqlParameter("@Shoporder", shopordersec),
                     new SqlParameter("@CD_first", string.IsNullOrWhiteSpace(firstText.Text) ? 0 : Convert.ToDouble(firstText.Text)),
                     new SqlParameter("@CD_second", string.IsNullOrWhiteSpace(SecondText.Text) ? 0 : Convert.ToDouble(SecondText.Text)),
                     new SqlParameter("@CD_third", string.IsNullOrWhiteSpace(thirdText.Text) ? 0 : Convert.ToDouble(thirdText.Text)),
                     new SqlParameter("@CD_fourth", string.IsNullOrWhiteSpace(fourthText.Text) ? 0 : Convert.ToDouble(fourthText.Text)),
                     new SqlParameter("@CD_fifth", string.IsNullOrWhiteSpace(FifthText.Text) ? 0 : Convert.ToDouble(FifthText.Text)),
                     new SqlParameter("@CD_six", string.IsNullOrWhiteSpace(sixText.Text) ? 0 : Convert.ToDouble(sixText.Text)),
                     new SqlParameter("@CD_seven", string.IsNullOrWhiteSpace(seventhText.Text) ? 0 : Convert.ToDouble(seventhText.Text)),
                     new SqlParameter("@CD_eight", string.IsNullOrWhiteSpace(eightText.Text) ? 0 : Convert.ToDouble(eightText.Text)),
                     new SqlParameter("@ShopOrderID", ID)
                 };
                    await db.ExecuteCommandUpdate(updatesqldata, updatedataparameter);
                }
                else
                {
                    // INSERT THE THE SHOPORDER ACTUAL DATA
                    string dataquery = "INSERT INTO ProdCon_ShopOrder_ActualData(Shoporder, CD_first, CD_second, CD_third, CD_fourth, CD_fifth, CD_six, CD_seven, CD_eight, ShopOrderID) " +
                     "VALUES (@Shoporder, @CD_first, @CD_second, @CD_third, @CD_fourth, @CD_fifth, @CD_six, @CD_seven, @CD_eight, @ShopOrderID)";

                    SqlParameter[] dataparameter =
                    {
                         new SqlParameter("@Shoporder", shopordersec),
                         new SqlParameter("@CD_first", string.IsNullOrWhiteSpace(firstText.Text) ? 0 : Convert.ToDouble(firstText.Text)),
                         new SqlParameter("@CD_second", string.IsNullOrWhiteSpace(SecondText.Text) ? 0 : Convert.ToDouble(SecondText.Text)),
                         new SqlParameter("@CD_third", string.IsNullOrWhiteSpace(thirdText.Text) ? 0 : Convert.ToDouble(thirdText.Text)),
                         new SqlParameter("@CD_fourth", string.IsNullOrWhiteSpace(fourthText.Text) ? 0 : Convert.ToDouble(fourthText.Text)),
                         new SqlParameter("@CD_fifth", string.IsNullOrWhiteSpace(FifthText.Text) ? 0 : Convert.ToDouble(FifthText.Text)),
                         new SqlParameter("@CD_six", string.IsNullOrWhiteSpace(sixText.Text) ? 0 : Convert.ToDouble(sixText.Text)),
                         new SqlParameter("@CD_seven", string.IsNullOrWhiteSpace(seventhText.Text) ? 0 : Convert.ToDouble(seventhText.Text)),
                         new SqlParameter("@CD_eight", string.IsNullOrWhiteSpace(eightText.Text) ? 0 : Convert.ToDouble(eightText.Text)),
                         new SqlParameter("@ShopOrderID", ID)
                };
                    await db.ExecuteCommandUpdate(dataquery, dataparameter);
                }

               
                ui.displaymeasurementTable(shopordersec, ID);
                Visible = false;
            }
        }



        public async void Displyalreadyinput()
        {  
            Dataconnect db = new Dataconnect();
            // int itemID = 6;
            string strsql = "SELECT s.Shoporder, sp.CD_supply, sp.CD_lot, a.CD_first, a.CD_second, a.CD_third,a.CD_fourth, " +
	                        "a.CD_fifth, a.CD_six,a.CD_seven, a.CD_eight " +
                            "FROM ProdCon_ShopOrder_tbl s " +
                            "INNER JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = s.ShopOrderID AND s.ShopOrderID = " + measureID + " " +
                            "INNER JOIN ProdCon_ShopOrder_ActualData a ON a.ShopOrderID = s.ShopOrderID AND s.ShopOrderID = " + measureID + "  ";

            DataTable bp = new DataTable();
            bp = await db.GetData(strsql);
         

            if (bp.Rows.Count > 0)
            {
                DataRow row = bp.Rows[0];
                //string fullname = (string)row["FullName"];
                SupplierText.Text = row["CD_supply"] != DBNull.Value ? (string)row["CD_supply"] : "";
                ShaftText.Text = row["CD_lot"] != DBNull.Value ? (string)row["CD_lot"] : "";

                firstText.Text = row["CD_first"] != DBNull.Value ? Convert.ToDecimal(row["CD_first"]).ToString() : "";
                SecondText.Text = row["CD_second"] != DBNull.Value ? Convert.ToDecimal(row["CD_second"]).ToString() : "";
                thirdText.Text = row["CD_third"] != DBNull.Value ? Convert.ToDecimal(row["CD_third"]).ToString() : "";
                fourthText.Text = row["CD_fourth"] != DBNull.Value ? Convert.ToDecimal(row["CD_fourth"]).ToString() : "";
                FifthText.Text = row["CD_fifth"] != DBNull.Value ? Convert.ToDecimal(row["CD_fifth"]).ToString() : "";
                sixText.Text = row["CD_six"] != DBNull.Value ? Convert.ToDecimal(row["CD_six"]).ToString() : "";
                seventhText.Text = row["CD_seven"] != DBNull.Value ? Convert.ToDecimal(row["CD_seven"]).ToString() : "";
                eightText.Text = row["CD_eight"] != DBNull.Value ? Convert.ToDecimal(row["CD_eight"]).ToString() : "";
            }

        }

        public async void GetMinandMax()
        {
           
            DataTable bp = await Products.GetMinandMax(RotorID);
            if (bp.Rows.Count > 0)
            {
                DataRow row = bp.Rows[0];
                min = row["CaulkingDentMin"] != DBNull.Value ? Convert.ToDouble(row["CaulkingDentMin"]) : 0.0;
                max = row["CaulkingDentMax"] != DBNull.Value ? Convert.ToDouble(row["CaulkingDentMax"]) : 0.0;
                modeltext.Text = "Model name: " + row["ProductType"].ToString();
            }

            rangetext.Text = "Standard Value: " + min + "-" + max;
            changetextbackground();
        }


        private void CaulkDent_Load(object sender, EventArgs e)
        {
            Displyalreadyinput();
            GetMinandMax();
            CheckStatusInput();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
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
                    string scannedCode = firstText.Text.Trim();
                    firstText.Text = scannedCode;
                    this.SelectNextControl((Control)sender, true, true, true, true);
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

        private void sixText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !sixText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void seventhText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !seventhText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void eightText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !eightText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
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

        private void sixText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(sixText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    sixText.BackColor = Color.White;
                    sixText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    sixText.BackColor = Color.FromArgb(250, 214, 210);
                    sixText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                sixText.BackColor = Color.White;
                sixText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void seventhText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(seventhText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    seventhText.BackColor = Color.White;
                    seventhText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    seventhText.BackColor = Color.FromArgb(250, 214, 210);
                    seventhText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                seventhText.BackColor = Color.White;
                seventhText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void eightText_TextChanged(object sender, EventArgs e)
        {
            // Check if the value in the TextBox is a valid number
            if (double.TryParse(eightText.Text, out double value))
            {

                // If the value is greater than 0.2, change the background color to red
                if (value >= min && value <= max)
                {
                    eightText.BackColor = Color.White;
                    eightText.ForeColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    // Otherwise, reset the background color to default (e.g., white)
                    eightText.BackColor = Color.FromArgb(250, 214, 210);
                    eightText.ForeColor = Color.FromArgb(100, 9, 9);
                }
            }
            else
            {
                // If the value is not a valid number, reset the background color
                eightText.BackColor = Color.White;
                eightText.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void firstText_BackColorChanged(object sender, EventArgs e)
        {
           
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
            }


            // Third Textbox
            if (double.TryParse(thirdText.Text, out double value2))
            {
                bool b = value2 >= min && value2 <= max;
                thirdText.BackColor = (b) ? Color.White : Color.FromArgb(250, 214, 210);
                thirdText.ForeColor = (b) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                thirdText.BackColor = Color.White;
            }

            // Fourth Textbox
            if (double.TryParse(fourthText.Text, out double value3))
            {
                bool c = value3 >= min && value3 <= max;
                fourthText.BackColor = (c) ? Color.White : Color.FromArgb(250, 214, 210);
                fourthText.ForeColor = (c) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                fourthText.BackColor = Color.White;
            }

            // Fifth Textbox
            if (double.TryParse(FifthText.Text, out double value4))
            {
                bool d = value4 >= min && value4 <= max;
                FifthText.BackColor = (d) ? Color.White : Color.FromArgb(250, 214, 210);
                FifthText.ForeColor = (d) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                FifthText.BackColor = Color.White;
            }


            // Six Textbox
            if (double.TryParse(sixText.Text, out double value5))
            {
                bool e = value5 >= min && value5 <= max;
                sixText.BackColor = (e) ? Color.White : Color.FromArgb(250, 214, 210);
                sixText.ForeColor = (e) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                sixText.BackColor = Color.White;
            }


            // Fifth Textbox
            if (double.TryParse(seventhText.Text, out double value6))
            {
                bool f = value6 >= min && value6 <= max;
                seventhText.BackColor = (f) ? Color.White : Color.FromArgb(250, 214, 210);
                seventhText.ForeColor = (f) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                seventhText.BackColor = Color.White;
            }


            // Fifth Textbox
            if (double.TryParse(eightText.Text, out double value7))
            {
                bool g = value7 >= min && value7 <= max;
                eightText.BackColor = (g) ? Color.White : Color.FromArgb(250, 214, 210);
                eightText.ForeColor = (g) ? Color.Black : Color.FromArgb(100, 9, 9);
            }
            else
            {
                eightText.BackColor = Color.White;
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
                sixText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void sixText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(sixText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = sixText.Text.Trim();
                sixText.Text = scannedCode;
                seventhText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void seventhText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(seventhText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = seventhText.Text.Trim();
                seventhText.Text = scannedCode;
                eightText.Focus();
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }

        private void eightText_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {


                // Check if the value in the TextBox is a valid number
                if (double.TryParse(eightText.Text, out double value))
                {
                    if (value < min || value > max)
                    {
                        MessageBox.Show("Out of specs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string scannedCode = eightText.Text.Trim();
                eightText.Text = scannedCode;
                e.SuppressKeyPress = true; // Prevent the newline from being added
            }
        }


        public void CheckStatusInput()
        {
            int stats = Int32.Parse(StatusID.Text);

            if(stats == 1)
            {

                SupplierText.Enabled = false;
                ShaftText.Enabled = false;
                firstText.Enabled = false;
                SecondText.Enabled = false;
                thirdText.Enabled = false;
                fourthText.Enabled = false;
                FifthText.Enabled = false;
                sixText.Enabled = false;
                seventhText.Enabled = false;
                eightText.Enabled = false;
            }
        }
    }
}
