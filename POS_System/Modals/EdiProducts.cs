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

            MessageBox.Show($"EditingV2: {_prod.ItemName}");

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
    }
}
