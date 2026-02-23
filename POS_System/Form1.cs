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

        public Form1()
        {
            InitializeComponent();
            LoadProductsAsync();
        }


        public async void LoadProductsAsync()
        {
            SetupGrid();


            flowLayoutPanel1.Controls.Clear();
            allProducts = await productService.LoadProductsAsync();


            // Optional: filter Category to alphabetic only
            var categories = allProducts
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c) && c.All(ch => char.IsLetter(ch) || char.IsWhiteSpace(ch))) // only letters & spaces
                .Distinct()
                .ToList();

            categories.Insert(0, "All");
            comboBox1.DataSource = categories;

            DisplayFilteredProducts(allProducts);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
           
            
        }

        private void DisplayFilteredProducts(List<Product> products)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in products)
            {
                flowLayoutPanel1.Controls.Add(CreateProductCard(product));
            }
        }
        private void ApplyFilter()
        {
            string searchText = txtFilter.Text.ToLower();
            string selectedCategory = comboBox1.SelectedItem?.ToString();

            var filtered = allProducts.Where(p =>
                (string.IsNullOrEmpty(searchText) || p.ItemName.ToLower().Contains(searchText))
                && (selectedCategory == "All" || p.Category == selectedCategory)
            );

            DisplayFilteredProducts(filtered.ToList());
        }

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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(200, 25)
            };

            Label lblDesc = new Label
            {
                Text = $"Stocks : {product.StockQty}",
                Location = new Point(120, 50),
                Size = new Size(100, 50)
            };

            Label lblPrice = new Label
            {
                Text = $"₱ {product.Price:N2}",
                ForeColor = Color.Green,
                Location = new Point(10, 50),
                Size = new Size(200, 20)
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

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Width = 50
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Minus",
                Text = "-",
                UseColumnTextForButtonValue = true,
                Width = 30
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
            ApplyFilter();
        }

        private void Paymentbtn_Click(object sender, EventArgs e)
        {

            using (var addSalesForm = new AddSalesForm(prods, this))
            {
                if (addSalesForm.ShowDialog() == DialogResult.OK)
                {
                    // Payment completed successfully
                    // Cart is already cleared in modal
                    MessageBox.Show("Payment successful.");
                    LoadProductsAsync(); // Refresh products to update stocks   
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string folderPath = @"C:\AccessDB";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string mdbFile = Path.Combine(folderPath, "MyDatabase.mdb");

            // Delete if already exists
            if (File.Exists(mdbFile))
                File.Delete(mdbFile);

            try
            {
                // Create MDB
                var cat = new ADOX.Catalog();
                cat.Create($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFile};Jet OLEDB:Engine Type=5");
                cat = null;

                string connString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFile};";

                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    // PRODUCTS TABLE (with stock + status)
                    cmd.CommandText = @"
                        CREATE TABLE Products (
                            ItemNo AUTOINCREMENT PRIMARY KEY,
                            Category TEXT(50),
                            ItemName TEXT(100),
                            UnitCost DOUBLE,
                            Price DOUBLE,
                            StockQty LONG DEFAULT 0,
                            StockStatus TEXT(20),
                            Remarks TEXT(255), 
                            IsDeleted YESNO DEFAULT FALSE
                        )";
                    cmd.ExecuteNonQuery();

                    // SALES TABLE
                    cmd.CommandText = @"
                        CREATE TABLE Sales (
                            SaleID AUTOINCREMENT PRIMARY KEY,
                            InvoiceNo TEXT(20),
                            [Date] DATETIME,
                            ItemNo LONG,
                            Price DOUBLE,
                            Quantity LONG
                        )";
                    cmd.ExecuteNonQuery();

                    // INVENTORY TRACKING TABLE
                    cmd.CommandText = @"
                            CREATE TABLE InventoryTracking (
                                InventoryID AUTOINCREMENT PRIMARY KEY,
                                [Date] DATETIME,
                                InvoiceNo TEXT(20),
                                ItemNo LONG,
                                QtyIN LONG,
                                QtyOut LONG,
                                Remarks TEXT(255)
                            )";
                    cmd.ExecuteNonQuery();

                    // RELATIONSHIPS
                    cmd.CommandText = @"
        ALTER TABLE Sales
        ADD CONSTRAINT FK_Sales_Products
        FOREIGN KEY (ItemNo) REFERENCES Products(ItemNo)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
        ALTER TABLE InventoryTracking
        ADD CONSTRAINT FK_Inventory_Products
        FOREIGN KEY (ItemNo) REFERENCES Products(ItemNo)";
                    cmd.ExecuteNonQuery();

                    // VALID STOCK STATUS VALUES
                    cmd.CommandText = @"
        ALTER TABLE Products
        ADD CONSTRAINT CK_StockStatus_Valid
        CHECK (StockStatus IN ('GOOD','LOW STOCK','OUT OF STOCK'))";
                    cmd.ExecuteNonQuery();

                    // STOCK QTY ↔ STATUS LOGIC
                    cmd.CommandText = @"
        ALTER TABLE Products
        ADD CONSTRAINT CK_StockQty_Status
        CHECK (
            (StockQty = 0 AND StockStatus = 'OUT OF STOCK')
         OR (StockQty BETWEEN 1 AND 10 AND StockStatus = 'LOW STOCK')
         OR (StockQty > 10 AND StockStatus = 'GOOD')
        )";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("MDB created successfully with stock rules:\n" + mdbFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
    }
}
