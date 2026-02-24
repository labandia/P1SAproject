using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using POS_System.Services;

namespace POS_System
{
    public partial class Form1 : Form
    {
        private ProductService productService = new ProductService();
        private BindingSource productBinding = new BindingSource();


        BindingList<POSModel> prods = new BindingList<POSModel>();
        private List<Product> allProducts = new List<Product>();

        private SaleService saleService = new SaleService();

        private Dictionary<int, Panel> productCardCache = new Dictionary<int, Panel>();

        private const int PageSize = 50;
        private int currentPage = 1;
        private List<Product> filteredProducts = new List<Product>();

        private Timer searchTimer;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            typeof(FlowLayoutPanel)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(flowLayoutPanel1, true, null);

            SetupGrid();
            LoadProductsAsync();


            searchTimer = new Timer();
            searchTimer.Interval = 300; // 300ms debounce
            searchTimer.Tick += SearchTimer_Tick;
        }

        // =========================================================
        // LOAD PRODUCTS
        // =========================================================
        public async void LoadProductsAsync()
        {
            flowLayoutPanel1.SuspendLayout();

            allProducts = await productService.LoadProductsAsync();

            SetupCategories();

            currentPage = 1;
            LoadCurrentPage();

            //CreateAllProductCards();

            flowLayoutPanel1.ResumeLayout();
        }

        private void LoadCurrentPage()
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            var pageData = filteredProducts
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            foreach (var product in pageData)
            {
                flowLayoutPanel1.Controls.Add(CreateProductCard(product));
            }

            lblPageInfo.Text = $"Page {currentPage} / {TotalPages()}";

            flowLayoutPanel1.ResumeLayout();
        }

        private int TotalPages()
        {
            return (int)Math.Ceiling((double)filteredProducts.Count / PageSize);
        }

        private void SetupCategories()
        {
            var categories = allProducts
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            categories.Insert(0, "All");
            comboBox1.DataSource = categories;
        }

        private void LoadToday()
        {
            var summary = saleService.GetTodaySummary();
            UpdateSummaryUI(summary);

        }

        private void UpdateSummaryUI(SalesSummaryModel s)
        {
            SalesToday.Text = s.TotalRevenue.ToString("₱#,##0.00");
        }

        private void CreateAllProductCards()
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            productCardCache.Clear();

            foreach (var product in allProducts)
            {
                var card = CreateProductCard(product);
                productCardCache[product.ItemNo] = card;
                flowLayoutPanel1.Controls.Add(card);
            }

            flowLayoutPanel1.ResumeLayout();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            await saleService.LoadSalesAsync();

            LoadToday();
        }
        // =========================================================
        // FILTER (NO REBUILDING)
        // =========================================================
        private void DisplayFilteredProducts(List<Product> products)
        {
            flowLayoutPanel1.SuspendLayout();   // 🔥 STOP repaint
            flowLayoutPanel1.Controls.Clear();

            foreach (var product in products)
            {
                flowLayoutPanel1.Controls.Add(CreateProductCard(product));
            }

            flowLayoutPanel1.ResumeLayout();    // 🔥 Resume repaint ONCE
        }
        private void ApplyFilter()
        {
            string searchText = txtFilter.Text.ToLower();
            string selectedCategory = comboBox1.SelectedItem?.ToString();

            filteredProducts = allProducts
                .Where(p =>
                    (string.IsNullOrEmpty(searchText) || p.ItemName.ToLower().Contains(searchText)) &&
                    (selectedCategory == "All" || p.Category == selectedCategory))
                .ToList();

            currentPage = 1;
            LoadCurrentPage();
        }


        // =========================================================
        // PRODUCT CARD
        // =========================================================
        private Panel CreateProductCard(Product product)
        {
            Panel card = new Panel
            {
                Width = 200,
                Height = 80,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Tag = product, // store whole object
                Cursor = Cursors.Hand
            };

            Label lblName = new Label
            {
                Text = product.ItemName,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 8),
                AutoSize = true
            };

            Label lblDesc = new Label
            {
                Text = $"Stocks : {product.StockQty}",
                Location = new Point(120, 50),
                Size = new Size(100, 50),
                AutoSize = true
            };

            Label lblPrice = new Label
            {
                Text = $"₱ {product.Price:N2}",
                ForeColor = Color.Green,
                Location = new Point(10, 50),
                Size = new Size(200, 20),
                AutoSize = true
            };

            card.Controls.Add(lblName);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblPrice);

            // Make entire card clickable
            card.Click += Card_Click;

            // Also allow child controls to trigger same event
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += Card_Click;
            }

            return card;
        }


        private void Card_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;

            // If user clicked label, get parent panel
            Panel card = clickedControl as Panel ?? clickedControl.Parent as Panel;
           

            if (card?.Tag is Product selectedProduct)
            {
                Debug.WriteLine($@"ID : {selectedProduct.ItemNo} - Name : {selectedProduct.ItemName} - Price : {selectedProduct.Price}");
              
                SetProductToGrid(new POSModel
                {
                    Id = selectedProduct.ItemNo,
                    Name = selectedProduct.ItemName,
                    Price = selectedProduct.Price,
                    UnitCost = selectedProduct.UnitCost,
                    StockQty = selectedProduct.StockQty
                });
            }
        }
        BindingList<Product> selectedProducts = new BindingList<Product>();

        private void SetProductToGrid(POSModel product)
        {
            var existing = prods.FirstOrDefault(p => p.Id == product.Id);

            if (existing != null)
            {
                return;
            }
            else
            {
                prods.Add(product);
            }

            UpdateTotal();
        }


        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Product"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Price"
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Minus",
                Text = "-",
                UseColumnTextForButtonValue = true,
                Width = 30
            });
           
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Width = 50
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Plus",
                Text = "+",
                UseColumnTextForButtonValue = true,
                Width = 30
            });




            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 80
            });

            // ✅ Bind ONCE
            dataGridView1.DataSource = prods;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = (POSModel)dataGridView1.Rows[e.RowIndex].DataBoundItem;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Minus")
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    Debug.WriteLine($"Removing {item.Name}");
                    prods.Remove(item); // ✅ grid updates instantly
                }
                dataGridView1.Refresh(); // Refresh to update button states if needed   
                UpdateTotal();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Plus")
            {
                item.Quantity++;
                dataGridView1.Refresh();
                UpdateTotal();
            }
        }


        private void UpdateTotal()
        {
            double total = prods.Sum(p => p.Total);
            decimal totalProfit = prods.Sum(x => x.TotalProfit);
            TotalText.Text = $"₱ {total:N2}";

            lblTotalProfit.Text = $"₱ {totalProfit:N2}";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            //ApplyFilter();
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            ApplyFilter();   // Run filter only once after user stops typing
        }

        private async void Paymentbtn_Click(object sender, EventArgs e)
        {

            using (var addSalesForm = new AddSalesForm(prods, this))
            {
                if (addSalesForm.ShowDialog() == DialogResult.OK)
                {
                    await saleService.LoadSalesAsync();
                    // Payment completed successfully
                    // Cart is already cleared in modal
                    MessageBox.Show("Payment successful.");
                    LoadProductsAsync(); // Refresh products to update stocks   
                    LoadToday();
                    ClearAll();
                }
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            ProductsPageForm productsPage = new ProductsPageForm();
            productsPage.ShowDialog();  
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalesHistoryPage productsPage = new SalesHistoryPage();
            productsPage.ShowDialog();
        }


        public void ClearAll()
        {
            TotalText.Text = "₱ 0.00";
            lblTotalProfit.Text = "₱ 0.00";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InvoiceSummaryPage invoice = new InvoiceSummaryPage();
            invoice.ShowDialog();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCurrentPage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < TotalPages())
            {
                currentPage++;
                LoadCurrentPage();
            }
        }

        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (flowLayoutPanel1.VerticalScroll.Value
                >= flowLayoutPanel1.VerticalScroll.Maximum - flowLayoutPanel1.Height)
            {
                if (currentPage < TotalPages())
                {
                    currentPage++;
                    LoadCurrentPage();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            prods.Clear();
            ClearAll();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
