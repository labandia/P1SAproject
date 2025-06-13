using ProductConfirm.Data;
using ProductConfirm.Models;
using ProductConfirm.Modules;
using System;
using System.Windows.Forms;

namespace ProductConfirm.View.Modals
{
    public partial class EditProducts : Form
    {
        private readonly Masterlistpage _mas;
        private readonly IProductRepositoryV2 _prod2;

        public EditProducts(Masterlistpage _m, IProductRepositoryV2 prod2)
        {
            InitializeComponent();
            _mas = _m;
            _prod2=prod2;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(TextID.Text.Trim());
            string minValue = string.IsNullOrWhiteSpace(MinText.Text) ? "0" : MinText.Text;
            string maxValue = string.IsNullOrWhiteSpace(MaxText.Text) ? "0" : MaxText.Text;
            string machinepress = minValue + "-" + maxValue;

            var insertobj = new AddProductDetailsModel
            {
                RotorProductID = ID,
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


            bool result = await _prod2.EditProducts(insertobj); 

            if (result)
            {
                MessageBox.Show("Data Modify Successfully");
                await _mas.DisplayMaster();
                Visible = false;
            }
        }


        public bool Validationform()
        {
            double dbcaulkmin = Convert.ToDouble(Caulkmin.Text);
            double dbcaulkmax = Convert.ToDouble(Caulkmax.Text);
            double dbshaftmin = Convert.ToDouble(Shaftmin.Text);
            double dbshaftmax = Convert.ToDouble(Shaftmax.Text);
            double dbseamin = Convert.ToDouble(SEAmin.Text);
            double dbseamax = Convert.ToDouble(SEAmax.Text);
            double dbmagmin = Convert.ToDouble(Magnetmin.Text);
            double dbmagmax = Convert.ToDouble(Magnetmax.Text);

            if (dbcaulkmin > dbcaulkmax) return true;
            if (dbshaftmin > dbshaftmax) return true;
            if (dbseamin > dbseamax) return true;
            if (dbmagmin > dbmagmax) return true;
           
            return true;
        }

        private void Cancel_btn_Click(object sender, EventArgs e) => Visible = false;  
    
        private async void EditProducts_Load(object sender, EventArgs e)
        {
            int ID = Int32.Parse(TextID.Text);
            var data = await _prod2.GetOnlyOneProducts(ID);

            if (data != null)
            {
                foreach(var item in data)
                {
                    Caulkmin.Text = item.CaulkingDentMin.ToString();
                    Caulkmax.Text = item.CaulkingDentMax.ToString();
                    Shaftmin.Text = item.ShaftLengthMin.ToString();
                    Shaftmax.Text = item.ShaftLengthMax.ToString();
                    SEAmin.Text = item.SEA_Min.ToString();
                    SEAmax.Text = item.SEA_Max.ToString();
                    ShaftPullmin.Text = item.ShaftPullingForce.ToString();
                    ShaftPullmax.Text = item.BushPullingForce.ToString();
                    Magnetmin.Text = item.MagnetHeightMin.ToString();
                    Magnetmax.Text = item.MagnetHeightMax.ToString();
                }
            }
        }


        private object CheckDecimalparse(string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && decimal.TryParse(input.Trim(), out decimal result))
            {
                return result; // Return parsed decimal
            }
            return DBNull.Value; // Return DBNull.Value if input is invalid or empty
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
    }
}
