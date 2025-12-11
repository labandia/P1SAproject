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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FootWristStrapsAnalysis
{
    public partial class Startup : Form
    {
        private string templateFilepath = @"C:\\Users\\jaye-labandia\\Desktop\\Record_STATIC.xlsx";

        private readonly IFootWrist _foot;
        private readonly System.Timers.Timer _importTimer;
        List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
        private IEnumerable<IFootWristModel> getData = new List<IFootWristModel>();

        private List<int> selectedRecordIds = new List<int>();

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
            var selectedDate = dateTimePicker1.Value.Date; // Get only the date part
            this.WindowState = FormWindowState.Maximized;

            // Step 1: Get the Folder path if exist
            string folderpath = await GetTheFileFolder();

            if (folderpath != "")
            {
                folderText.Text = folderpath;   
                // Step 2: Starts the check file The Data from the exist file folder and Upload it
                await StartAsync();

                getData = await _foot.GetFootAnalysisData();
                var displayByDate = getData.Where(res => res.TestDate.HasValue && res.TestDate.Value.Date == selectedDate).ToList();

                if(displayByDate != null)
                {
                    MessageBox.Show("No Data Found.",
                         "Warning",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Warning);
                    return;
                }

                footlist = displayByDate; // Use the filtered list instead of all data

                CountTable.Text = footlist.Count().ToString();

                AnalysisTable.DataSource = footlist;

                AnalysisTable.Columns["TestTime"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["TestTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["TestTime"].Width = 120;

                // Employee ID
                AnalysisTable.Columns["EmployeeID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["EmployeeID"].Width = 120;

                // Employee Name
                AnalysisTable.Columns["EmployeeName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["EmployeeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["EmployeeName"].Width = 150;

                // Comprehensive Result 
                AnalysisTable.Columns["ComprehensiveResult"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["ComprehensiveResult"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["ComprehensiveResult"].Width = 200;

                // Left Foot Resis 
                AnalysisTable.Columns["LeftFootResistance"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["LeftFootResistance"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["LeftFootResistance"].Width = 150;

                // Left Foot Result 
                AnalysisTable.Columns["LeftFootResult"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["LeftFootResult"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["LeftFootResult"].Width = 150;

                // Right Foot Resis 
                AnalysisTable.Columns["RightFootResistance"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["RightFootResistance"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["RightFootResistance"].Width = 200;

                // Right Foot Resis 
                AnalysisTable.Columns["RightFootResult"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["RightFootResult"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["RightFootResult"].Width = 150;

                // Wrist Strap resis
                AnalysisTable.Columns["WristStrapResult"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["WristStrapResult"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["WristStrapResult"].Width = 200;

                //ConductivityEvaluation
                AnalysisTable.Columns["ConductivityEvaluation"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["ConductivityEvaluation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["ConductivityEvaluation"].Width = 200;

                // ----------------------------------------------------------------
                // Lower Evaluation Limit
                AnalysisTable.Columns["LowerEvaluationLimit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["LowerEvaluationLimit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["LowerEvaluationLimit"].Width = 200;

                // Upper Evaluation Limit
                AnalysisTable.Columns["UpperEvaluationLimit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["UpperEvaluationLimit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["UpperEvaluationLimit"].Width = 200;

                // Evaluation Buzzer
                AnalysisTable.Columns["EvaluationBuzzer"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["EvaluationBuzzer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["EvaluationBuzzer"].Width = 200;

                // Evaluation External Output
                AnalysisTable.Columns["EvaluationExternalOutput"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AnalysisTable.Columns["EvaluationExternalOutput"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AnalysisTable.Columns["EvaluationExternalOutput"].Width = 200;
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
                    Debug.WriteLine("⚠️ Folder not found: " + folderPath);
                    return;
                }

                string[] csvFiles = Directory.GetFiles(folderPath, "*.csv", SearchOption.TopDirectoryOnly);
                if (csvFiles.Length == 0)
                {
                    Debug.WriteLine("⚠️ No CSV files found in the folder.");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error during previous files import: " + ex.Message);
            }
        }

        private async void prevbtn_Click(object sender, EventArgs e)
        {
            await ImportPreviousFiles();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
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
            string recordCol = "RecordID";  // <- your RecordID column name

            // Toggle checkbox
            bool current = Convert.ToBoolean(
                AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value ?? false
            );

            bool newValue = !current;
            AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value = newValue;

            var value = AnalysisTable.Rows[e.RowIndex].Cells[recordCol].Value;

            if (value == null || value == DBNull.Value)
                return;


            int recordId;
            if (!int.TryParse(value.ToString(), out recordId))
                return;


            // Add to list if checked
            if (newValue)
            {
                if (!selectedRecordIds.Contains(recordId))
                    selectedRecordIds.Add(recordId);
            }
            else
            {
                // Remove if unchecked
                selectedRecordIds.Remove(recordId);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save Excel File",
                    FileName = $"ESD_Test_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PopulateTemplateFromDatabase(saveFileDialog.FileName);
                    MessageBox.Show("Export completed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string fileCheck = await SqlDataAccess.GetOneData("SELECT FolderPath FROM FolderPaths WHERE ProjectName = @ProjectName ", new { ProjectName = "FootWrist" });
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
            string checkSql = "SELECT COUNT(PathID) FROM FolderPaths WHERE ProjectName = @ProjectName";
            bool isExist = await SqlDataAccess.Checkdata(checkSql, new { ProjectName = "FootWrist" });

            string strsql;
            if (!isExist)
            {
                strsql = "INSERT INTO FolderPaths(FolderPath, ProjectName) VALUES(@FolderPath, @ProjectName)";
            }
            else
            {
                strsql = "UPDATE FolderPaths SET FolderPath =@FolderPath WHERE ProjectName =@ProjectName";
            }


            var obj = new
            {
                FolderPath = folderText.Text,
                ProjectName = "FootWrist"
            };


            bool result = await SqlDataAccess.UpdateInsertQuery(strsql, obj);

            if (result)
            {
                MessageBox.Show("Save folder path Successfully",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
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


        private async void PopulateTemplateFromDatabase(string outputPath)
        {
            // 1. Copy template to output path
            File.Copy(templateFilepath, outputPath, true);

            var getData = await _foot.GetTestDataForMonth(11, 2025);

            //Dictionary<DateTime, Dictionary<string, ESDTestData>> organizedData =
            //   OrganizeDataByDateAndEmployee(getData);

            //foreach (int id in selectedRecordIds)
            //{
            //    Debug.WriteLine(id);
            //}

            // 3. Open the copied template
            using (var package = new ExcelPackage(new FileInfo(outputPath)))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"]; // Or worksheet index

                if (worksheet == null)
                {
                    worksheet = package.Workbook.Worksheets[0];
                }

                //// 4. Extract employee column mapping from template
                Dictionary<string, (int rightCol, int leftCol)> employeeColumnMap =
                    ExtractEmployeeColumnsFromTemplate(worksheet);

                //// 5. Organize data from database
                Dictionary<DateTime, Dictionary<string, ESDTestData>> organizedData =
                    OrganizeDataByDateAndEmployee(getData);

                //// 6. Map database IDs to template IDs (if needed)
                Dictionary<string, string> dbToTemplateMapping = GetDatabaseToTemplateMapping();

                //// 7. Populate the data into template
                PopulateDataIntoTemplate(worksheet, organizedData, employeeColumnMap, dbToTemplateMapping);

                // 8. Save changes
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


        private void PopulateDataIntoTemplate(ExcelWorksheet worksheet,
           Dictionary<DateTime, Dictionary<string, ESDTestData>> organizedData,
           Dictionary<string, (int rightCol, int leftCol)> templateColumnMap,
           Dictionary<string, string> dbToTemplateMapping)
        {
            // Populate data for each day of December (rows 11-41)
            for (int day = 1; day <= 31; day++)
            {
                int row = 10 + day; // Row 11 for day 1, 12 for day 2, etc.
                DateTime currentDate = new DateTime(2023, 12, day);

                // Check each employee that exists in the template
                foreach (var templateEntry in templateColumnMap)
                {
                    string templateEmployeeID = templateEntry.Key;
                    int rightCol = templateEntry.Value.rightCol;
                    int leftCol = templateEntry.Value.leftCol;

                    // Find the corresponding database ID
                    string dbEmployeeID = FindDatabaseEmployeeID(templateEmployeeID, dbToTemplateMapping);

                    // Check if we have data for this date and employee
                    bool hasData = organizedData.ContainsKey(currentDate) &&
                                  organizedData[currentDate].ContainsKey(dbEmployeeID);

                    if (hasData)
                    {
                        ESDTestData testData = organizedData[currentDate][dbEmployeeID];

                        // Populate Right foot result (O for pass, X for fail)
                        string rightValue = testData.RightFootResult ? "O" : "X";
                        worksheet.Cells[row, rightCol].Value = rightValue;
                        worksheet.Cells[row, rightCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        // Apply color
                        if (testData.RightFootResult)
                        {
                            worksheet.Cells[row, rightCol].Style.Font.Color.SetColor(Color.Green);
                        }
                        else
                        {
                            worksheet.Cells[row, rightCol].Style.Font.Color.SetColor(Color.Red);
                        }

                        // Populate Left foot result
                        string leftValue = testData.LeftFootResult ? "O" : "X";
                        worksheet.Cells[row, leftCol].Value = leftValue;
                        worksheet.Cells[row, leftCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        // Apply color
                        if (testData.LeftFootResult)
                        {
                            worksheet.Cells[row, leftCol].Style.Font.Color.SetColor(Color.Green);
                        }
                        else
                        {
                            worksheet.Cells[row, leftCol].Style.Font.Color.SetColor(Color.Red);
                        }
                    }
                    else
                    {
                        // No data for this day - clear the cells but keep formatting
                        worksheet.Cells[row, rightCol].Value = "";
                        worksheet.Cells[row, leftCol].Value = "";
                    }
                }
            }
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
