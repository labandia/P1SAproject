using ADOX;
using POS_System.Utilities;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class Flashscreen : Form
    {
        private readonly string dbFolder;
        private readonly string dbPath;

        private int step = 0;

        public Flashscreen()
        {
            InitializeComponent();

            dbFolder = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
               "SariSariStore"
           );

            dbPath = Path.Combine(dbFolder, "SariSariStore.mdb");


            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.ForeColor = Color.SeaGreen;
            lblStatus.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private async void Flashscreen_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            lblStatus.Text = "Initializing...";

            await SetupDatabaseAsync();

            lblStatus.Text = "Opening POS System...";
            await Task.Delay(500);

            SariSariStoreLogin frm = new SariSariStoreLogin();
            frm.Show();
            this.Hide();
        }

        private async Task SetupDatabaseAsync()
        {
            UpdateProgress(10, "Checking database...");

            // ✅ If database already exists → skip everything
            if (File.Exists(dbPath))
            {
                UpdateProgress(100, "Database already exists.");
                await Task.Delay(500);
                return;
            }

            try
            {
                UpdateProgress(20, "Creating database file...");
                await Task.Delay(300);

                // CREATE MDB
                var cat = new Catalog();
                cat.Create(
                    $"Provider=Microsoft.Jet.OLEDB.4.0;" +
                    $"Data Source={dbPath};" +
                    $"Jet OLEDB:Engine Type=5");
                cat = null;

                string connStr =
                    $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbPath};";

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    // ================= USERS TABLE =================
                    UpdateProgress(30, "Creating Users table...");
                    cmd.CommandText = @"
                CREATE TABLE Users (
                    UserID AUTOINCREMENT PRIMARY KEY,
                    FullName TEXT(100) NOT NULL,
                    Username TEXT(50) NOT NULL,
                    [PasswordHash] TEXT(255) NOT NULL,
                    Role TEXT(20) NOT NULL,
                    IsActive YESNO DEFAULT TRUE,
                    CreatedAt DATETIME DEFAULT NOW()
                )";
                    cmd.ExecuteNonQuery();

                    // Insert Default Admin User
                    UpdateProgress(35, "Creating default admin...");
                    string defaultPassword = SecurityPassword.HashPassword("admin123"); // change if needed

                    cmd.CommandText = @"
                INSERT INTO Users 
                (FullName, Username, [PasswordHash], Role, IsActive, CreatedAt)
                VALUES (?, ?, ?, ?, TRUE, NOW())";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FullName", "System Administrator");
                    cmd.Parameters.AddWithValue("@Username", "admin");
                    cmd.Parameters.AddWithValue("@PasswordHash", defaultPassword);
                    cmd.Parameters.AddWithValue("@Role", "Admin");

                    cmd.ExecuteNonQuery();

                    // ================= PRODUCTS TABLE =================
                    UpdateProgress(45, "Creating Products table...");
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
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();

                    // ================= SALES TABLE =================
                    UpdateProgress(60, "Creating Sales table...");
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

                    // ================= INVENTORY TABLE =================
                    UpdateProgress(75, "Creating InventoryTracking...");
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

                    // ================= RELATIONSHIPS =================
                    UpdateProgress(85, "Creating relationships...");
                    cmd.CommandText = @"
                ALTER TABLE Sales
                ADD CONSTRAINT FK_Sales_Products
                FOREIGN KEY (ItemNo)
                REFERENCES Products(ItemNo)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                ALTER TABLE InventoryTracking
                ADD CONSTRAINT FK_Inventory_Products
                FOREIGN KEY (ItemNo)
                REFERENCES Products(ItemNo)";
                    cmd.ExecuteNonQuery();

                    // ================= STOCK RULE =================
                    UpdateProgress(95, "Applying stock rules...");
                    cmd.CommandText = @"
                ALTER TABLE Products
                ADD CONSTRAINT CK_StockStatus_Valid
                CHECK (StockStatus IN 
                ('GOOD','LOW STOCK','OUT OF STOCK'))";
                    cmd.ExecuteNonQuery();
                }

                // ================= IMPORT EXCEL =================
                UpdateProgress(98, "Importing initial products...");

                string excelPath =
                    Path.Combine(Application.StartupPath, "StoreDatabase.xlsx");

                if (File.Exists(excelPath))
                {
                    await ImportFromExcelBulkAsync(excelPath,
                        new Progress<int>(count =>
                        {
                            lblStatus.Text =
                                $"Importing products... {count}";
                        }));
                }

                UpdateProgress(100, "Database setup complete!");
                await Task.Delay(800);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Setup Error: " + ex.Message);
            }
        }

        // ==========================================================
        // 🔥 BULK IMPORT METHOD
        // ==========================================================

        public async Task ImportFromExcelBulkAsync(
            string excelFilePath,
            IProgress<int> progress = null)
        {
            await Task.Run(() =>
            {
                string extension =
                    Path.GetExtension(excelFilePath).ToLower();

                string excelConnStr = extension == ".xls"
                    ? @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                      $"Data Source={excelFilePath};" +
                      "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
                    : @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                      $"Data Source={excelFilePath};" +
                      "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";

                string accessConnStr =
                    $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbPath};";

                using (var excelConn =
                    new OleDbConnection(excelConnStr))
                using (var accessConn =
                    new OleDbConnection(accessConnStr))
                {
                    excelConn.Open();
                    accessConn.Open();

                    using (var transaction =
                        accessConn.BeginTransaction())
                    using (var cmd = new OleDbCommand(
                        @"INSERT INTO Products 
                          (Category, ItemName, UnitCost, Price, StockQty, StockStatus)
                          VALUES (?, ?, ?, ?, 0, 'OUT OF STOCK')",
                        accessConn, transaction))
                    {
                        cmd.Parameters.Add(
                            new OleDbParameter { OleDbType = OleDbType.VarChar });
                        cmd.Parameters.Add(
                            new OleDbParameter { OleDbType = OleDbType.VarChar });
                        cmd.Parameters.Add(
                            new OleDbParameter { OleDbType = OleDbType.Double });
                        cmd.Parameters.Add(
                            new OleDbParameter { OleDbType = OleDbType.Double });

                        using (var excelCmd =
                            new OleDbCommand(
                                "SELECT * FROM [Database$]",
                                excelConn))
                        using (var reader =
                            excelCmd.ExecuteReader())
                        {
                            int rowCount = 0;

                            while (reader.Read())
                            {
                                string itemName =
                                    reader["Item name"]?.ToString().Trim();

                                if (string.IsNullOrWhiteSpace(itemName))
                                    continue;

                                string category =
                                    reader["Category"]?.ToString().Trim();

                                decimal.TryParse(
                                    reader["Selling Price/pc"]?.ToString(),
                                    out decimal price);

                                decimal.TryParse(
                                    reader["Unit Cost"]?.ToString(),
                                    out decimal unitCost);

                                cmd.Parameters[0].Value = category;
                                cmd.Parameters[1].Value = itemName;
                                cmd.Parameters[2].Value = unitCost;
                                cmd.Parameters[3].Value = price;

                                cmd.ExecuteNonQuery();
                                rowCount++;

                                if (rowCount % 100 == 0)
                                    progress?.Report(rowCount);
                            }
                        }

                        transaction.Commit();
                    }
                }
            });
        }

        private void UpdateProgress(int value, string status)
        {
            progressBar1.Value = value;
            lblStatus.Text = status;
            Application.DoEvents();
        }
    }
}
