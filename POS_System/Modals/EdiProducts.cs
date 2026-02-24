using POS_System.Services;
using System;
using System.Windows.Forms;

namespace POS_System.Modals
{
    public partial class EdiProducts : Form
    {
        private Product _prod;
        private ProductsPageForm _prodform = new ProductsPageForm();
        public int _prodId;

        public EdiProducts(Product prod, ProductsPageForm prodform)
        {
            InitializeComponent();
            _prod = prod;
            _prodform = prodform;


            _prodId = prod.ItemNo;  
            ProdName.Text = _prod.ItemName;
            ProdPrice.Text = _prod.Price.ToString();
            ProdUnit.Text = _prod.UnitCost.ToString();
            prodStocks.Text = _prod.StockQty.ToString();
        }

        private void EdiProducts_Load(object sender, EventArgs e)
        {
         
           
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            var productService = new ProductService();  

            var updatedProduct = new Product
            {
                ItemNo = _prodId,
                ItemName = ProdName.Text,
                Price = decimal.TryParse(ProdPrice.Text, out decimal price) ? price : 0,
                UnitCost = decimal.TryParse(ProdUnit.Text, out decimal unitCost) ? unitCost : 0,
                StockQty = int.TryParse(prodStocks.Text, out int stockQty) ? stockQty : 0
            };

            await productService.UpdateProductAsync(updatedProduct);

            _prodform.LoadProductsAsync(); // Refresh the products list in the main form    
            this.Close(); // Close the edit form    
        }

        private void ProdUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !ProdUnit.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void prodStocks_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !prodStocks.Text.Contains(".")))
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
