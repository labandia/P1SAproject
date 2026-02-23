using POS_System.Modals;
using POS_System.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace POS_System
{
    public partial class ProductsPageForm : Form
    {
        private ProductService productService = new ProductService();
        private List<Product> allProducts = new List<Product>();
        private List<string> Categorylist = new List<string>();

        public ProductsPageForm()
        {
            InitializeComponent();
        }

        public async void LoadProductsAsync()
        {
            allProducts = await productService.LoadProductsAsync();
      


            var categories = allProducts
               .Select(p => p.Category)
               .Where(c => !string.IsNullOrWhiteSpace(c) && c.All(ch => char.IsLetter(ch) || char.IsWhiteSpace(ch))) // only letters & spaces
               .Distinct()
            .ToList();

            categories.Insert(0, "All");
            Categorylist = categories;  
            Categorycbn.DataSource = Categorylist;
            productsTable.AutoGenerateColumns = true;
            productsTable.DataSource = allProducts;

            AddActionButton();
        }

        private void DisplayFilteredProducts(List<Product> products)
        {
            productsTable.AutoGenerateColumns = true;
            productsTable.DataSource = products;
        }

        private void ApplyFilter()
        {
            string searchTextfilter = SearchText.Text.ToLower();
            string selectedCategory = Categorycbn.SelectedItem?.ToString();

            var filtered = allProducts.Where(p =>
                (string.IsNullOrEmpty(searchTextfilter) || p.ItemName.ToLower().Contains(searchTextfilter))
                && (selectedCategory == "All" || p.Category == selectedCategory)
            );

            DisplayFilteredProducts(filtered.ToList());
        }

        private void ProductsPageForm_Load(object sender, EventArgs e)
        {
            LoadProductsAsync();
        }


        private void AddActionButton()
        {
            if (productsTable.Columns["btnAction"] != null)
                return; // prevent duplicate button

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn
            {
                Name = "btnAction",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 80
            };

            DataGridViewButtonColumn deletbtn = new DataGridViewButtonColumn
            {
                Name = "DeleteAction",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80
            };

            productsTable.Columns.Add(btn);
            productsTable.Columns.Add(deletbtn);    
        }

        private async void productsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (productsTable.Columns[e.ColumnIndex].Name == "btnAction")
            {
                Product product =
                    (Product)productsTable.Rows[e.RowIndex].DataBoundItem;
               

                EdiProducts ediProducts = new EdiProducts(product, this);
                ediProducts.ShowDialog();

            }

            if (productsTable.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                Product product =
                    (Product)productsTable.Rows[e.RowIndex].DataBoundItem;

                if (MessageBox.Show("Delete this Product?", "Deleted",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                await productService.DeleteProductAsync(product.ItemNo);

                LoadProductsAsync();
            }
        }

        private void Categorycbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void FinalPaymentbtn_Click(object sender, EventArgs e)
        {
            using (var addproduct = new AddProductsForm(this, Categorylist))
            {
                if (addproduct.ShowDialog() == DialogResult.OK)
                {
                    // Payment completed successfully
                    MessageBox.Show("Update successful.");
                    LoadProductsAsync(); // Refresh products to update stocks   
                }
            }
        }
    }
}
