using POS_System.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Modals
{
    public partial class AddProductsForm : Form
    {
        private readonly ProductsPageForm _productsPageForm;
        private List<string> _Categorylist;

        public AddProductsForm(ProductsPageForm productsPageForm, List<string> Categorylist)
        {
            InitializeComponent();
            _productsPageForm = productsPageForm;
            _Categorylist = Categorylist;


            Categorycbn.DataSource = _Categorylist;
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            if(!ValidationForm())
            {
                return;
            }


            var producService = new ProductService();

            try
            {
                await producService.AddProductAsync(new Product
                {
                    ItemName = ItemNameText.Text,
                    Category = Categorycbn.Text,
                    Price = decimal.TryParse(PriceText.Text, out var price) ? price : 0,
                    UnitCost = decimal.TryParse(UnitCostText.Text, out var unitprice) ? unitprice : 0,
                });

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment Error");
            }

            var newProduct = 


            DialogResult = DialogResult.OK;


        }

        private void PriceText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !PriceText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void UnitCostText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !UnitCostText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        public bool ValidationForm()
        {
            if (string.IsNullOrEmpty(ItemNameText.Text) || string.IsNullOrEmpty(PriceText.Text) || string.IsNullOrEmpty(UnitCostText.Text))
            {
                MessageBox.Show("Enter All the Required Fields.");
                return false;
            }

            if (string.IsNullOrEmpty(ItemNameText.Text) )
            {
                prod_error.Visible = true;
                return false;
            }
            else
            {
                prod_error.Visible = false;
            }

            if (string.IsNullOrEmpty(PriceText.Text))
            {
                price_error.Visible = true;
                return false;
            }
            else
            {
                prod_error.Visible = false;
            }

            if (string.IsNullOrEmpty(UnitCostText.Text))
            {
                Unit_error.Visible = true;
                return false;
            }
            else
            {
                Unit_error.Visible = false;
            }

            int selectedIndex = Categorycbn.SelectedIndex;

            if (selectedIndex == 0)
            {
                selectCat_error.Visible = true;
                return false;
            }
            else
            {
                selectCat_error.Visible = false;
            }


            prod_error.Visible = true;
            price_error.Visible = true;
            Unit_error.Visible = true;

            return true;
        }

    }
}
