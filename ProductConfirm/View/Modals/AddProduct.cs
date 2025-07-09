using ProductConfirm.Data;
using ProductConfirm.DataAccess;
using ProductConfirm.Global;
using ProductConfirm.Models;
using ProductConfirm.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm.Modals
{
    public partial class AddProduct : Form
    {
        private readonly IProductRepositoryV2 _prod;

        private Masterlistpage mp;
        private Dataconnect db;

        public AddProduct(Masterlistpage m, IProductRepositoryV2 prod)
        {
            InitializeComponent();
            db = new Dataconnect();
            mp = m;
            _prod=prod; 
        }

        private void MinText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !MinText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void MaxText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !MaxText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            //ProductRepositoryV2 _prod = new ProductRepositoryV2();
            //----------- Insert the first Data Table ------------------- //
            string minValue = string.IsNullOrWhiteSpace(MinText.Text) ? "0" : MinText.Text;
            string maxValue = string.IsNullOrWhiteSpace(MaxText.Text) ? "0" : MaxText.Text;
            string machinepress = minValue + "-" + maxValue;

            //----------- Create a object to pass the value the in one Variable ------------------- //
            var insertDetails = new AddProductDetailsModel
            {
                RotorProductID = 0,
                RotorAssy = string.IsNullOrWhiteSpace(PartText.Text) ? "0" : PartText.Text.Trim(),
                ProductType = string.IsNullOrWhiteSpace(ModelText.Text) ? "0" : ModelText.Text,
                MachinePressureMinMax = string.IsNullOrWhiteSpace(machinepress) ? "0-0" : machinepress,
                RecommendedPressureSetting = string.IsNullOrWhiteSpace(MaxText.Text) ? 0.00m : Convert.ToDecimal(MaxText.Text),
                CaulkingDentMin = string.IsNullOrWhiteSpace(Caulkmin.Text) ? 0.00m : Convert.ToDecimal(Caulkmin.Text),
                CaulkingDentMax = string.IsNullOrWhiteSpace(Caulkmax.Text) ? 0.00m : Convert.ToDecimal(Caulkmax.Text),
                ShaftLengthMin = string.IsNullOrWhiteSpace(Shaftmin.Text) ? 0.00m : Convert.ToDecimal(Shaftmin.Text),
                ShaftLengthMax = string.IsNullOrWhiteSpace(Shaftmax.Text) ? 0.00m : Convert.ToDecimal(Shaftmax.Text),
                SEA_Min = string.IsNullOrWhiteSpace(SEAmin.Text) ? 0.00m : Convert.ToDecimal(SEAmin.Text),
                SEA_Max = string.IsNullOrWhiteSpace(SEAmax.Text) ? 0.00m : Convert.ToDecimal(SEAmax.Text),
                ShaftPullingForce = string.IsNullOrWhiteSpace(ShaftPullmin.Text) ? 0 : Convert.ToInt32(ShaftPullmin.Text),
                BushPullingForce = string.IsNullOrWhiteSpace(ShaftPullmax.Text) ? 0 : Convert.ToInt32(ShaftPullmax.Text),
                MagnetHeightMin = string.IsNullOrWhiteSpace(Magnetmin.Text) ? 0.00m : Convert.ToDecimal(Magnetmin.Text),
                MagnetHeightMax = string.IsNullOrWhiteSpace(Magnetmax.Text) ? 0.00m : Convert.ToDecimal(Magnetmax.Text)
            };


            bool finalresult = await _prod.AddProducts(insertDetails);

            if (finalresult)
            {
                MessageBox.Show("Insert Data Successfully");
                await mp.DisplayMaster();
                this.Visible = false;
            }

        }


        public void UpdateDisplayTable(int selected)
        {
            DataTable table = new DataTable();
           
        }




        public bool Validateform()
        {
            bool check = true;
            string part = PartText.Text;
            string model = ModelText.Text;
            string min = MinText.Text;
            string max = MaxText.Text;  

            if (string.IsNullOrEmpty(part))
            {
                part_error.Visible = true;
                check = false;
            }
            else
            {
                part_error.Visible = false;
            }

            if (string.IsNullOrEmpty(model))
            {
                model_error.Visible = true;
                check = false;
            }
            else
            {
                model_error.Visible = false;
            }

            if (string.IsNullOrEmpty(min))
            {
                min_error.Visible = true;
                check = false;
            }
            else
            {
                min_error.Visible = false;
            }

            if (string.IsNullOrEmpty(max))
            {
                max_error.Visible = true;
                check = false;
            }
            else
            {
                max_error.Visible = false;
            }
  
            return check;
        }


        public async Task<int> RotorlastID()
        {
            int result = 0;
            Products p = new Products();

            DataTable dt = await p.GetProductID();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    result = dr.Field<int>("RotorProductID");
                }
            }
            
            return result;

        }




        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void Caulkmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Caulkmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void Caulkmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Caulkmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void Shaftmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Shaftmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void Shaftmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Shaftmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void SEAmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !SEAmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void SEAmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !SEAmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void Magnetmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Magnetmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void Magnetmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Magnetmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void ShaftPullmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !ShaftPullmin.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void ShaftPullmax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !ShaftPullmax.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }
    }
}
