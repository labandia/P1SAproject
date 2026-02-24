using POS_System.Modals;
using POS_System.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class ProductsPageForm : Form
    {
        private ProductService productService = new ProductService();

        private List<Product> allProducts = new List<Product>();
        private readonly BindingSource productBinding = new BindingSource();
        private List<string> categoryList = new List<string>();
        private List<Product> filteredProducts = new List<Product>();


        private System.Windows.Forms.Timer searchTimer;

        private const int PageSize = 100;
        private int currentPage = 1;
        private int totalPages = 1;


        public ProductsPageForm()
        {
            InitializeComponent();

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300; // 🔥 300ms debounce
            searchTimer.Tick += SearchTimer_Tick;

            productsTable.AutoGenerateColumns = true; // set once
            productsTable.DataSource = productBinding;
        }

        public async Task LoadProductsAsync()
        {
            allProducts = await productService.LoadProductsAsync();

            // 🔥 Precompute lowercase cache ONCE
            foreach (var p in allProducts)
            {
                p.SearchCache = (p.ItemName + " " + p.Category)?.ToLower();
            }

            

            productBinding.DataSource = allProducts;

            SetupCategories();
            AddActionButton();
        }

        private void DisplayFilteredProducts(List<Product> products)
        {
            productsTable.AutoGenerateColumns = true;
            productsTable.DataSource = products;
        }

        private void SetupCategories()
        {
            // Prevent SelectedIndexChanged from firing during setup
            Categorycbn.SelectedIndexChanged -= Categorycbn_SelectedIndexChanged;

            // Fast distinct using HashSet (O(n))
            var categorySet = new HashSet<string>(
                allProducts
                    .Where(p => !string.IsNullOrWhiteSpace(p.Category))
                    .Select(p => p.Category)
            );

            var sortedCategories = categorySet
                .OrderBy(c => c)
                .ToList();

            sortedCategories.Insert(0, "All");

            Categorycbn.DataSource = null; // important reset
            Categorycbn.DataSource = sortedCategories;

            Categorycbn.SelectedIndex = 0;

            // Re-attach event
            Categorycbn.SelectedIndexChanged += Categorycbn_SelectedIndexChanged;
        }

        private void ApplyFilter()
        {
            string searchText = SearchText.Text;
            string selectedCategory = Categorycbn.SelectedItem?.ToString();

            IEnumerable<Product> query = allProducts;

            // 🔥 O(1) fast lookup using cached lowercase
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(p => p.SearchCache.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(selectedCategory) &&
                selectedCategory != "All")
            {
                query = query.Where(p => p.Category == selectedCategory);
            }

            //productBinding.DataSource = query.ToList();

            filteredProducts = query.ToList();

            SetupPagination();
            LoadCurrentPage();
        }

        private async void ProductsPageForm_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }


        private void AddActionButton()
        {
            if (productsTable.Columns["btnAction"] != null)
                return;

            productsTable.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "btnAction",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 70
            });

            productsTable.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "DeleteAction",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 70
            });
        }

        private async void productsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var product = (Product)productsTable.Rows[e.RowIndex].DataBoundItem;

            if (productsTable.Columns[e.ColumnIndex].Name == "btnAction")
            {
                using (var edit = new EdiProducts(product, this))
                {
                    edit.ShowDialog();
                }
            }

            if (productsTable.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                if (MessageBox.Show("Delete this Product?", "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                await productService.DeleteProductAsync(product.ItemNo);

                // ✅ Remove locally instead of reloading everything
                allProducts.Remove(product);
                ApplyFilter();
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

        private async void FinalPaymentbtn_Click(object sender, EventArgs e)
        {
            using (var addProduct = new AddProductsForm(this, categoryList))
            {
                if (addProduct.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Update successful.");
                    await LoadProductsAsync();
                }
            }
        }

        private void SetupPagination()
        {
            totalPages = (int)Math.Ceiling((double)filteredProducts.Count / PageSize);

            if (currentPage > totalPages)
                currentPage = totalPages;

            if (currentPage < 1)
                currentPage = 1;
        }

        private void LoadCurrentPage()
        {
            var pageData = filteredProducts
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            productBinding.DataSource = pageData;

            lblPageInfo.Text = $"Page {currentPage} / {totalPages}";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadCurrentPage();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCurrentPage();
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            currentPage = 1;
            ApplyFilter();
        }
    }
}
