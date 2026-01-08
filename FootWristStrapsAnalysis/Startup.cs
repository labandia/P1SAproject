using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FootWristStrapsAnalysis
{
    public partial class Startup : Form
    {
        private string templateFilepath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\ExportTemplate\Record_STATIC.xlsx";

        private readonly IFootWrist _foot;
        private readonly System.Timers.Timer _importTimer;
        List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
        private IEnumerable<IFootWristModel> getData = new List<IFootWristModel>();

        private List<int> selectedRecordIds = new List<int>();
        private List<string> selectedEmployeeID = new List<string>();

        private static IEnumerable<IFootWristModel> footlist;

        public Startup(IFootWrist foot)
        {
            InitializeComponent();
          


            _foot = foot;

            // 🔁 Run every 15 minutes (900,000 ms)
            _importTimer = new System.Timers.Timer(15 * 60 * 1000);
            _importTimer.Elapsed += async (s, e) => await AutomaticImportData();
            _importTimer.AutoReset = true;
            _importTimer.Enabled = false; // start manually after load
        }

        private async void Startup_Load(object sender, EventArgs e)
        {
            this.Enabled = false;

            var waitForm = new WaitFormDialog
            {
                TopMost = true,
                ShowInTaskbar = false
            };

            waitForm.Show();
            waitForm.BringToFront();
            waitForm.Activate();

            try
            {
                var minDelay = Task.Delay(1500);
                var startupWork = InitializeApplicationAsync(waitForm);

                await Task.WhenAll(minDelay, startupWork);
            }
            finally
            {
                waitForm.TopMost = false;
                waitForm.Close();
                waitForm.Dispose();

                // Re-enable main form AFTER waitForm is closed
                this.Enabled = true;
                this.Activate();
            }
        }

        private async Task InitializeApplicationAsync(WaitFormDialog waitForm)
        {
            try
            {
                var selectedDate = dateTimePicker1.Value.Date;

                await ShowStep(waitForm, "Checking folder path if exist...", 10);

                string folderpath = await GetTheFileFolder();

                // 🚨 NO PATH → ASK USER
                if (string.IsNullOrWhiteSpace(folderpath) || !Directory.Exists(folderpath))
                {
                    waitForm.Hide();
                    await Task.Yield();

                    using (var dialog = new FolderPathInputDialog())
                    {
                        dialog.StartPosition = FormStartPosition.CenterScreen;

                        if (dialog.ShowDialog() != DialogResult.OK)
                        {
                            waitForm.Close();   // ✅ CLOSE
                            return;
                        }

                        folderpath = dialog.FolderPath;
                    }

                    waitForm.Show();
                    waitForm.BringToFront();

                    await SaveFolderPath(folderpath);
                }

                folderText.Text = folderpath;

                await ShowStep(waitForm, "Importing today's CSV file...", 35);
                await StartAsync();

                await ShowStep(waitForm, "Loading records...", 65);
                getData = await _foot.GetFootAnalysisData();

                var displayByDate = getData
                    .Where(res => res.TestDate.HasValue &&
                                  res.TestDate.Value.Date == selectedDate)
                    .ToList();

                if (!displayByDate.Any())
                {
                    waitForm.Close();   // ✅ CLOSE
                    MessageBox.Show(
                        "No Data Found.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                footlist = displayByDate;
                CountTable.Text = footlist.Count().ToString();
                AnalysisTable.DataSource = footlist;

                await ShowStep(waitForm, "Preparing display...", 90);
                ConfigureGrid();

                await ShowStep(waitForm, "Completed", 100, 500);
            }
            finally
            {
                // ✅ GUARANTEED cleanup (even if exception occurs)
                if (!waitForm.IsDisposed)
                    waitForm.Close();
            }
        }

        private async Task ShowStep(
            WaitFormDialog waitForm,
            string message,
            int progress,
            int delayMs = 400)
        {
            waitForm.SetStatus(message);
            waitForm.SetProgress(progress);

            // Force UI refresh (important)
            await Task.Yield();

            // Keep message visible
            await Task.Delay(delayMs);
        }

        private void ConfigureGrid()
        {
            AnalysisTable.Columns["TestTime"].Width = 120;
            AnalysisTable.Columns["EmployeeID"].Width = 120;
            AnalysisTable.Columns["EmployeeName"].Width = 150;
            AnalysisTable.Columns["ComprehensiveResult"].Width = 200;
            AnalysisTable.Columns["LeftFootResistance"].Width = 150;
            AnalysisTable.Columns["LeftFootResult"].Width = 150;
            AnalysisTable.Columns["RightFootResistance"].Width = 200;
            AnalysisTable.Columns["RightFootResult"].Width = 150;
            AnalysisTable.Columns["WristStrapResult"].Width = 200;
            AnalysisTable.Columns["ConductivityEvaluation"].Width = 200;
            AnalysisTable.Columns["LowerEvaluationLimit"].Width = 200;
            AnalysisTable.Columns["UpperEvaluationLimit"].Width = 200;
            AnalysisTable.Columns["EvaluationBuzzer"].Width = 200;
            AnalysisTable.Columns["EvaluationExternalOutput"].Width = 200;

            foreach (DataGridViewColumn col in AnalysisTable.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
        }


        public async Task StartAsync()
        {
            await AutomaticImportData();  // initial run
            _importTimer.Start();         // start 15-min loop
            Debug.WriteLine("⏰ Auto import timer started (every 15 minutes).");
        }


        // ✅ Core function that runs every 15 min
        public async Task AutomaticImportData()
        {
            try
            {
                //string folderPath = @"D:\PC_system\CIRCUIT";
                string folderPath = folderText.Text;

                if (!Directory.Exists(folderPath))
                {
                    //Debug.WriteLine("⚠️ Folder not found: " + folderPath);
                    MessageBox.Show("⚠️ Folder not found: " + folderPath);
                    return;
                }

                string[] csvFiles = Directory.GetFiles(folderPath, "*.csv", SearchOption.TopDirectoryOnly);
                if (csvFiles.Length == 0)
                {
                    //Debug.WriteLine("⚠️ No CSV files found in the folder.");
                    MessageBox.Show("⚠️ No CSV files found in the folder.");
                    return;
                }

                DateTime today = DateTime.Today;
                bool foundTodayFile = false;

                foreach (string file in csvFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    Debug.WriteLine($"📄 Checking file: {fileName}");

                    // ===================== STEP 2: Extract date from filename =====================
                    string[] parts = fileName.Split('_');
                    if (parts.Length < 2)
                    {
                        Debug.WriteLine("Invalid filename format: " + fileName);
                        continue;
                    }

                    string datePart = parts[1];
                    if (!DateTime.TryParseExact(datePart, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                    {
                        Debug.WriteLine("Invalid date format in file name: " + fileName);
                        continue;
                    }

                    // ===================== STEP 3: Only today's file =====================
                    if (fileDate.Date != today)
                    {
                        Debug.WriteLine($"Skipping {fileName} — not today's file.");
                        continue;
                    }

                    foundTodayFile = true;
                    string[] lines = File.ReadAllLines(file);
                    if (lines.Length < 2)
                        continue;

                    // ===================== STEP 4: Process CSV rows =====================
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] col = lines[i].Split(',');

                        if (col.Length < 18)
                        {
                            Debug.WriteLine($"Skipping invalid row {i} in {fileName}");
                            continue;
                        }

                        string employeeId = col[2].Trim();
                        if (string.IsNullOrWhiteSpace(employeeId))
                            continue;

                        // ===================== STEP 5: Check if EmployeeID already exists =====================
                        bool exists = await _foot.CheckIfEmployeeIDImportToday(employeeId, fileDate);
                        if (exists)
                        {
                            Debug.WriteLine($"Skipping {employeeId} — already exists for {fileDate:yyyy-MM-dd}");
                            continue;
                        }


                        TimeSpan testTime;
                        string timeString = col[1].Trim().Trim('"'); // ✅ remove surrounding quotes

                        if (!TimeSpan.TryParseExact(timeString, @"h\:mm", null, out testTime) &&
                            !TimeSpan.TryParseExact(timeString, @"hh\:mm", null, out testTime))
                        {
                            // fallback if parsing fails
                            testTime = TimeSpan.Zero;
                            Debug.WriteLine($"⚠️ Invalid time format for Employee {col[2].Trim()} in row {i}: '{timeString}'");
                        }

                        // ===================== STEP 6: Insert the new record =====================
                        IFootWristModel data = new IFootWristModel
                        {
                            TestDate = fileDate,
                            TestTime = testTime,
                            EmployeeID = employeeId.Trim('"'),
                            EmployeeName = col[3].Trim().Trim('"'),
                            ComprehensiveResult = col[5].Trim().Trim('"') == "PASS",
                            LeftFootResistance = col[6].Trim().Trim('"'),
                            LeftFootResult = col[7].Trim().Trim('"') == "PASS",
                            RightFootResistance = col[8].Trim().Trim('"'),
                            RightFootResult = col[9].Trim().Trim('"') == "PASS",
                            WristStrapResult = col[10].Trim().Trim('"'),
                            ConductivityEvaluation = col[11].Trim().Trim('"'),
                            LowerEvaluationLimit = col[12].Trim().Trim('"'),
                            UpperEvaluationLimit = col[13].Trim().Trim('"'),
                            EvaluationBuzzer = col[14].Trim().Trim('"') == "PASS",
                            EvaluationExternalOutput = col[15].Trim().Trim('"') == "PASS",
                            FG470 = col[16].Trim().Trim('"'),
                            Note = col[17].Trim().Trim('"') == "DS"
                        };

                        await _foot.ImportSetFootAnalysis(data);
                        Debug.WriteLine($"✅ Inserted Employee: {data.EmployeeID} ({data.EmployeeName})");
                    }
                }

                if (foundTodayFile)
                    Debug.WriteLine($"✅ Import complete for today's file(s). Time: {DateTime.Now:HH:mm:ss}");
                else
                    Debug.WriteLine($"ℹ️ No file found for today's date {today:yyyy-MM-dd}.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("❌ Error in AutomaticImportData: " + ex.Message);
            }
        }

        public async Task ImportPreviousFiles()
        {
            try
            {
                //string folderPath = @"D:\PC_system\CIRCUIT";
                string folderPath = folderText.Text;

                if (!Directory.Exists(folderPath))
                {
                    MessageBox.Show("Folder not found: " + folderPath);
                    return;
                }

                string[] csvFiles = Directory.GetFiles(folderPath, "*.csv", SearchOption.TopDirectoryOnly);
                if (csvFiles.Length == 0)
                {
                    MessageBox.Show("No CSV files found in the folder.");
                    return;
                }

                foreach (string file in csvFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string[] parts = fileName.Split('_');
                    if (parts.Length < 2)
                    {
                        Debug.WriteLine("Invalid filename format: " + fileName);
                        continue;
                    }

                    if (!DateTime.TryParseExact(parts[1], "yyyyMMdd", null,
                        System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                    {
                        Debug.WriteLine("Invalid date format in file name: " + fileName);
                        continue;
                    }

                    // Only previous dates (before today)
                    if (fileDate.Date >= DateTime.Today)
                    {
                        Debug.WriteLine($"Skipping {fileName} — not a previous file.");
                        continue;
                    }

                    string[] lines = File.ReadAllLines(file);
                    if (lines.Length < 2)
                        continue;

                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] col = lines[i].Split(',');

                        if (col.Length < 18)
                        {
                            Debug.WriteLine($"Skipping invalid row {i} in {fileName}");
                            continue;
                        }

                        string employeeId = col[2].Trim().Trim('"');
                        if (await _foot.CheckIfEmployeeIDImportPrevious(employeeId, fileDate))
                        {
                            Debug.WriteLine($"Skipping {employeeId} — already exists for {fileDate:yyyy-MM-dd}");
                            continue;
                        }



                        TimeSpan testTime;
                        string timeString = col[1].Trim().Trim('"'); // ✅ remove surrounding quotes

                        if (!TimeSpan.TryParseExact(timeString, @"h\:mm", null, out testTime) &&
                            !TimeSpan.TryParseExact(timeString, @"hh\:mm", null, out testTime))
                        {
                            // fallback if parsing fails
                            testTime = TimeSpan.Zero;
                            Debug.WriteLine($"⚠️ Invalid time format for Employee {col[2].Trim()} in row {i}: '{timeString}'");
                        }


                        IFootWristModel data = new IFootWristModel
                        {
                            TestDate = fileDate,
                            TestTime = testTime,
                            EmployeeID = employeeId,
                            EmployeeName = col[3].Trim().Trim('"'),
                            ComprehensiveResult = col[5].Trim().Trim('"') == "PASS",
                            LeftFootResistance = col[6].Trim().Trim('"'),
                            LeftFootResult = col[7].Trim().Trim('"') == "PASS",
                            RightFootResistance = col[8].Trim().Trim('"'),
                            RightFootResult = col[9].Trim().Trim('"') == "PASS",
                            WristStrapResult = col[10].Trim().Trim('"'),
                            ConductivityEvaluation = col[11].Trim().Trim('"'),
                            LowerEvaluationLimit = col[12].Trim().Trim('"'),
                            UpperEvaluationLimit = col[13].Trim().Trim('"'),
                            EvaluationBuzzer = col[14].Trim().Trim('"') == "PASS",
                            EvaluationExternalOutput = col[15].Trim().Trim('"') == "PASS",
                            FG470 = col[16].Trim().Trim('"'),
                            Note = col[17].Trim().Trim('"') == "DS"
                        };

                        await _foot.ImportSetFootAnalysis(data);
                        Debug.WriteLine($"✅ Imported previous Employee: {data.EmployeeID}");
                    }
                }

                MessageBox.Show("✅ Previous files import completed.");


                var selectedDate = dateTimePicker1.Value.Date;
                footlist = getData
                    .Where(res => res.TestDate.HasValue && res.TestDate.Value.Date == selectedDate)
                    .ToList();
                CountTable.Text = footlist.Count().ToString();

                AnalysisTable.DataSource = footlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error during previous files import: " + ex.Message);
            }
        }

        private  void prevbtn_Click(object sender, EventArgs e)
        {
            //await ImportPreviousFiles();
            UploadPreviousData uploadForm = new UploadPreviousData();
            uploadForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            selectedRecordIds = new List<int>();

            selecteditem.Text = "0 / 10";

            string filterText = textBox1.Text.ToLower();
            var filteredList = new List<IFootWristModel>();

            if (filterText != "")
            {
                filteredList = footlist.Where(p =>
                    (!string.IsNullOrEmpty(p.EmployeeID) && p.EmployeeID.ToLower().Contains(filterText)) ||
                    (!string.IsNullOrEmpty(p.EmployeeName) && p.EmployeeName.ToLower().Contains(filterText))).ToList();
            }
            else
            {
                filteredList = footlist.ToList();
            }

            CountTable.Text = filteredList.Count().ToString();
            AnalysisTable.DataSource = filteredList;
        }

        private void AnalysisTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string checkCol = "Select";
            string recordCol = "RecordID";

            string selectedEmployee  = AnalysisTable.Rows[e.RowIndex].Cells["EmployeeID"].Value.ToString();

            // Current checkbox value
            bool current = Convert.ToBoolean(
                AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value ?? false
            );
   
            Debug.WriteLine("GET Employee" + AnalysisTable.Rows[e.RowIndex].Cells["EmployeeID"].Value);
            // Get RecordID
            var value = AnalysisTable.Rows[e.RowIndex].Cells[recordCol].Value;
            if (value == null || value == DBNull.Value) return;

            if (!int.TryParse(value.ToString(), out int recordId)) return;

            // 🔹 IF USER IS TRYING TO CHECK (currently unchecked)
            if (!current)
            {
                // Reached limit → block new selection
                if (selectedRecordIds.Count >= 10)
                {
                    MessageBox.Show(
                        "You can only select up to 10 records for export.",
                        "Limit Exceeded",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Allow checking
                AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value = true;

                if (!selectedRecordIds.Contains(recordId))
                {
                    selectedRecordIds.Add(recordId);
                    selectedEmployeeID.Add(selectedEmployee);
                }
            }
            else
            {
                // 🔹 USER IS UNCHECKING → always allowed
                AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value = false;
                selectedRecordIds.Remove(recordId);

               
                if (selectedEmployeeID.Contains(selectedEmployee))
                {
                    selectedEmployeeID.Remove(selectedEmployee);
                }
            }

            selecteditem.Text = selectedRecordIds.Count.ToString() + " / 10";

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

                if(selectedRecordIds.Count == 0)
                {
                    MessageBox.Show("No Records Selected for Export... ", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save Excel File",
                    FileName = $"ESD_Test_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await PopulateTemplateFromDatabase(saveFileDialog.FileName);
                    MessageBox.Show("Export completed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedRecordIds = new List<int>();
                    selectedEmployeeID = new List<string>();

                    selecteditem.Text = "0 / 10";

                    foreach (DataGridViewRow row in AnalysisTable.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            row.Cells["Select"].Value = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            selectedRecordIds = new List<int>();
            selectedEmployeeID = new List<string>();

            selecteditem.Text = "0 / 10";

            // Assuming getData is already loaded somewhere else (e.g., Form_Load)
            if (getData == null) return;

            var selectedDate = dateTimePicker1.Value.Date;
            footlist = getData
                .Where(res => res.TestDate.HasValue && res.TestDate.Value.Date == selectedDate)
                .ToList();
            CountTable.Text = footlist.Count().ToString();  

            AnalysisTable.DataSource = footlist;
        }


       

        public async Task<string> GetTheFileFolder()
        {
            string fileCheck = await SqlDataAccess.GetOneData($@"SELECT FolderPath 
                                FROM FolderPaths WHERE ProjectName = @ProjectName ", 
                                new { ProjectName = "FootWrist" });
            return fileCheck;
        }



        private void btnOpenfolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                dialog.ShowNewFolderButton = true; // optional

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    folderText.Text = selectedPath.Trim();
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await SaveFolderPath(folderText.Text);       
        }

        private async Task SaveFolderPath(string folderPath)
        {
            string checkSql =
                "SELECT COUNT(PathID) FROM FolderPaths WHERE ProjectName = @ProjectName";

            bool exists = await SqlDataAccess.Checkdata(
                checkSql, new { ProjectName = "FootWrist" });

            string sql = exists
                ? "UPDATE FolderPaths SET FolderPath = @FolderPath WHERE ProjectName = @ProjectName"
                : "INSERT INTO FolderPaths(FolderPath, ProjectName) VALUES(@FolderPath, @ProjectName)";

            await SqlDataAccess.UpdateInsertQuery(sql, new
            {
                FolderPath = folderPath,
                ProjectName = "FootWrist"
            });
        }

        private Dictionary<DateTime, Dictionary<string, ESDTestData>> OrganizeDataByDateAndEmployee(IEnumerable<IFootWristModel> data)
        {
            var organizedData = new Dictionary<DateTime, Dictionary<string, ESDTestData>>();

            foreach (var  row in data)
            {

                DateTime testDate = Convert.ToDateTime(row.TestDate);

                string employeeID = row.EmployeeID;

                var testData = new ESDTestData
                {
                    TestDate = testDate,
                    EmployeeID = employeeID,
                    EmployeeName = row.EmployeeName,
                    LeftFootResistance = row.LeftFootResistance,
                    LeftFootResult = row.LeftFootResult,
                    RightFootResistance = row.RightFootResistance,
                    RightFootResult = row.RightFootResult
                };

                if (!organizedData.ContainsKey(testDate))
                {
                    organizedData[testDate] = new Dictionary<string, ESDTestData>();
                }

                organizedData[testDate][employeeID] = testData;
            }

            return organizedData;
        }

        string[] monthNames =
        {
            "January", "February", "March", "April",
            "May", "June", "July", "August",
            "September", "October", "November", "December"
        };
        private async Task PopulateTemplateFromDatabase(string outputPath)
        {
            DateTime selectedDate = dateTimePicker1.Value;

            int month = selectedDate.Month;   // 1–12
            int year = selectedDate.Year;

            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (!File.Exists(templateFilepath))
                throw new FileNotFoundException("Template file not found.", templateFilepath);

            // 1. Copy template to output path
            File.Copy(templateFilepath, outputPath, true);

            var getData = await _foot.GetTestDataForMonth(month, year);



            if (getData == null || !getData.Any())
                throw new Exception("No data returned from database.");
            //Dictionary<DateTime, Dictionary<string, ESDTestData>> organizedData =
            //   OrganizeDataByDateAndEmployee(getData);





            // 3. Open the copied template
            using (var package = new ExcelPackage(new FileInfo(outputPath)))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"]
                    ?? package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                    throw new Exception("No worksheet found in Excel template.");

                // 4. Get employee column mapping FROM DATABASE
                Dictionary<string, (int rightCol, int leftCol)> employeeColumnMap =
                    GetEmployeeColumnMappingFromDatabase();

                string monthName = monthNames[month - 1];


                worksheet.Cells[5, 5].Value = year;
                worksheet.Cells[5, 9].Value = $@"Month: {monthName}";


                //// 5. Organize data from database
                Dictionary<DateTime, Dictionary<string, ESDTestData>> organizedData =
                    OrganizeDataByDateAndEmployee(getData);

                //// 6. Map database IDs to template IDs (if needed)
                Dictionary<string, string> dbToTemplateMapping = new Dictionary<string, string>();


                WriteEmployeeHeaders(worksheet, employeeColumnMap);

                //// 7. Populate the data into template
                PopulateDataIntoTemplate(worksheet, organizedData, employeeColumnMap, dbToTemplateMapping);

                //// 8. Save changes
                package.Save();
            }
        }

        private Dictionary<string, (int rightCol, int leftCol)> ExtractEmployeeColumnsFromTemplate(ExcelWorksheet worksheet)
        {
            var columnMap = new Dictionary<string, (int rightCol, int leftCol)>();

            // Employee IDs are in row 10 (Excel row 10, which is EPPlus row 10)
            int employeeNameRow = 10;

            // Scan from column C to V (3 to 22 in EPPlus)
            for (int col = 3; col <= 22; col += 2)
            {
                // Get the employee ID from the template
                var cell = worksheet.Cells[employeeNameRow, col];
                string employeeID = cell.Value?.ToString()?.Trim();

                if (!string.IsNullOrEmpty(employeeID))
                {
                    // Employee occupies this column and the next one
                    int rightCol = col;      // Right foot column
                    int leftCol = col + 1;   // Left foot column

                    columnMap[employeeID] = (rightCol, leftCol);

                    // Debug output
                    Debug.WriteLine($"Template has employee '{employeeID}' at columns {GetExcelColumnName(rightCol)}-{GetExcelColumnName(leftCol)}");
                }
            }

            return columnMap;
        }

        private Dictionary<string, string> GetDatabaseToTemplateMapping()
        {
            // Map database EmployeeIDs to template EmployeeIDs
            // This is needed if they use different IDs
            return new Dictionary<string, string>
            {
                {"SOC027", "S2CC827"},    // Database -> Template
                {"SCC573", "S1CC573"},    // Database -> Template
                // Add more mappings as needed
                // If IDs are the same, you don't need to map them
            };
        }

        private void WriteEmployeeHeaders(
            ExcelWorksheet worksheet,
            Dictionary<string, (int rightCol, int leftCol)> employeeColumnMap)
                {
                    int headerRow = 10; // Row above C10

                    foreach (var emp in employeeColumnMap)
                    {
                        // Employee ID centered across both columns
                        worksheet.Cells[headerRow, emp.Value.rightCol].Value = emp.Key;
                        worksheet.Cells[headerRow, emp.Value.leftCol].Value = emp.Key;

                        // Rotate text vertically
                        worksheet.Cells[headerRow, emp.Value.rightCol].Style.TextRotation = 90;
                        worksheet.Cells[headerRow, emp.Value.leftCol].Style.TextRotation = 90;

                        worksheet.Cells[headerRow, emp.Value.rightCol].Style.HorizontalAlignment =
                            ExcelHorizontalAlignment.Center;
                        worksheet.Cells[headerRow, emp.Value.leftCol].Style.HorizontalAlignment =
                            ExcelHorizontalAlignment.Center;
                    }
        }


        private void PopulateDataIntoTemplate(ExcelWorksheet worksheet,
           Dictionary<DateTime, Dictionary<string, ESDTestData>> organizedData,
           Dictionary<string, (int rightCol, int leftCol)> templateColumnMap,
           Dictionary<string, string> dbToTemplateMapping)
        {

            int startRow = 11; // ✅ MUST match A11 = Day 1
            //int daysInMonth = DateTime.DaysInMonth(2025, 11);

            DateTime selectedDate = dateTimePicker1.Value;

            int month = selectedDate.Month;   // 1–12
            int year = selectedDate.Year;

            int daysInMonth = DateTime.DaysInMonth(year, month);

            // Populate data for each day of December (rows 11-41)
            for (int day = 1; day <= daysInMonth; day++)
            {
                int row = 11 + day; // Row 11 for day 1, 12 for day 2, etc.
                DateTime currentDate = new DateTime(year, month, day); // ✅ FIXED

                foreach (var emp in templateColumnMap)
                {
                    int rightCol = emp.Value.rightCol;
                    int leftCol = emp.Value.leftCol;

                    // First: check if the DATE exists in DB
                    if (!organizedData.TryGetValue(currentDate, out var empData))
                    {
                        continue;
                    }

                    // Second: check if EMPLOYEE exists for that date
                    if (!empData.TryGetValue(emp.Key, out var testData))
                    {
                        continue;
                    }

                    // ✅ Date and employee found → write to Excel
                    SetResultCell(worksheet.Cells[row, rightCol], testData.RightFootResult);
                    SetResultCell(worksheet.Cells[row, leftCol], testData.LeftFootResult);
                }


        
            }
        }


        private void SetResultCell(ExcelRange cell, bool isPass)
        {
            cell.Value = isPass ? "O" : "X";
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.Font.Bold = false;

            cell.Style.Font.Color.SetColor(
                isPass ? System.Drawing.Color.Green : System.Drawing.Color.Red
            );
        }



        private string FindDatabaseEmployeeID(string templateEmployeeID, Dictionary<string, string> mapping)
        {
            // First check if template ID is directly in mapping
            if (mapping.ContainsValue(templateEmployeeID))
            {
                // Find the database ID that maps to this template ID
                foreach (var entry in mapping)
                {
                    if (entry.Value == templateEmployeeID)
                        return entry.Key;
                }
            }

            // If no mapping found, assume they're the same
            return templateEmployeeID;
        }


        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }


        private Dictionary<string, (int rightCol, int leftCol)> GetEmployeeColumnMappingFromDatabase()
        {
            var columnMap = new Dictionary<string, (int rightCol, int leftCol)>();

            // Get distinct employee IDs from database
            //List<string> employeeIDs = GetDistinctEmployeeIDsForMonth(12, 2023);

            if (selectedEmployeeID.Count == 0)
            {
                MessageBox.Show("No employee data found in database for December 2023",
                    "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return columnMap;
            }

            // Map each employee to Excel columns starting from column C
            // Each employee gets 2 columns: Right (even columns) and Left (odd columns)
            int startCol = 3; // Column C

            for (int i = 0; i < selectedEmployeeID.Count; i++)
            {
                string employeeID = selectedEmployeeID[i];
                int rightCol = startCol + (i * 2);     // C, E, G, I, etc.
                int leftCol = rightCol + 1;            // D, F, H, J, etc.

                // Stop if we exceed column V (max 22 columns)
                if (rightCol > 22 || leftCol > 22) // 22 = Column V
                {
                    MessageBox.Show($"Warning: Too many employees ({selectedEmployeeID.Count}). " +
                                   $"Only first {i} employees will be shown in columns C-V.",
                                   "Capacity Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }

                columnMap[employeeID] = (rightCol, leftCol);

                Console.WriteLine($"Database employee '{employeeID}' mapped to columns " +
                                 $"{GetExcelColumnName(rightCol)} (Right) and {GetExcelColumnName(leftCol)} (Left)");
            }

            return columnMap;
        }

        private List<string> GetDistinctEmployeeIDsForMonth(int month, int year)
        {
            List<string> employeeIDs = new List<string>();

            return employeeIDs;
        }



        public class ESDTestData
        {
            public DateTime TestDate { get; set; }
            public string EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public string LeftFootResistance { get; set; }
            public bool LeftFootResult { get; set; }
            public string RightFootResistance { get; set; }
            public bool RightFootResult { get; set; }
            public bool ComprehensiveResult { get; set; }
        }
    }
}
