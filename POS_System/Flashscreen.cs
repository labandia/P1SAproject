using ADOX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class Flashscreen : Form
    {
        private int step = 0;

        public Flashscreen()
        {
            InitializeComponent();

            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.ForeColor = Color.SeaGreen;
            lblStatus.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private async void Flashscreen_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            lblStatus.Text = "Initializing...";

            await CreateDatabaseWithProgress();

            lblStatus.Text = "Opening Sari Sari Store...";
            await Task.Delay(500);

            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private async Task CreateDatabaseWithProgress()
        {
            string folderPath = @"C:\AccessDB";
            string mdbFile = Path.Combine(folderPath, "SariSariStore.mdb");

            UpdateProgress(10, "Checking database folder...");
            await Task.Delay(300);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // ✅ CHECK IF DATABASE EXISTS
            if (File.Exists(mdbFile))
            {
                UpdateProgress(100, "Database already exists. Skipping setup...");
                await Task.Delay(800);
                return; // 🚀 SKIP CREATION
            }

            try
            {
                UpdateProgress(20, "Creating new database...");
                await Task.Delay(300);

                var cat = new ADOX.Catalog();
                cat.Create($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFile};Jet OLEDB:Engine Type=5");
                cat = null;

                string connString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFile};";

                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    // PRODUCTS TABLE
                    UpdateProgress(40, "Creating Products table...");
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
                    await Task.Delay(200);

                    // SALES TABLE
                    UpdateProgress(55, "Creating Sales table...");
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
                    await Task.Delay(200);

                    // INVENTORY TABLE
                    UpdateProgress(70, "Creating Inventory table...");
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
                    await Task.Delay(200);

                    // RELATIONSHIPS
                    UpdateProgress(85, "Creating Relationships...");
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

                    // STOCK RULES
                    UpdateProgress(95, "Applying Stock Rules...");
                    cmd.CommandText = @"
                ALTER TABLE Products
                ADD CONSTRAINT CK_StockStatus_Valid
                CHECK (StockStatus IN ('GOOD','LOW STOCK','OUT OF STOCK'))";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                ALTER TABLE Products
                ADD CONSTRAINT CK_StockQty_Status
                CHECK (
                    (StockQty = 0 AND StockStatus = 'OUT OF STOCK')
                 OR (StockQty BETWEEN 1 AND 10 AND StockStatus = 'LOW STOCK')
                 OR (StockQty > 10 AND StockStatus = 'GOOD')
                )";
                    cmd.ExecuteNonQuery();
                }

                UpdateProgress(100, "Database created successfully!");
                await Task.Delay(800);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateProgress(int value, string status)
        {
            progressBar1.Value = value;
            lblStatus.Text = status;
            Application.DoEvents();
        }
    }
}
