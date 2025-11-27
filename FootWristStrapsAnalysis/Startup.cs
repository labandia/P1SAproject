using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FootWristStrapsAnalysis
{
    public partial class Startup : Form
    {
        private readonly IFootWrist _foot;
        private readonly System.Timers.Timer _importTimer;

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
            this.WindowState = FormWindowState.Maximized;

            await StartAsync();

            var getData = await _foot.GetFootAnalysisData();
            footlist = getData.ToList();
            AnalysisTable.DataSource = footlist;
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
                string folderPath = @"D:\PC_system\CIRCUIT";

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
                string folderPath = @"D:\PC_system\CIRCUIT";

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
        List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();

        private void AnalysisTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string checkCol = "Select";

            // Toggle checkbox
            bool current = Convert.ToBoolean(
                AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value ?? false
            );

            bool newValue = !current;
            AnalysisTable.Rows[e.RowIndex].Cells[checkCol].Value = newValue;

            // Add to list if checked
            if (newValue)
            {
                if (!selectedRows.Contains(AnalysisTable.Rows[e.RowIndex]))
                    selectedRows.Add(AnalysisTable.Rows[e.RowIndex]);
            }
            else
            {
                // Remove if unchecked
                selectedRows.Remove(AnalysisTable.Rows[e.RowIndex]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = textBox1.Text.ToLower();
            var filteredList = new List<IFootWristModel>();

            if (filterText != "")
            {
                filteredList = footlist.Where(p => p.EmployeeID.ToLower().Contains(filterText)).ToList();
            }
            else
            {
                filteredList = footlist.ToList();
            }

            AnalysisTable.DataSource = filteredList;
        }
    }
}
