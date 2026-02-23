using POS_System.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    }
}
