using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootWristStrapsAnalysis
{
    public partial class UploadPreviousData : Form
    {
        private string templateFilepath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\ExportTemplate\Record_STATIC.xlsx";

        private readonly IFootWrist _foot;
        public string _filePath = "";

        private BindingList<IFootWristModel> _gridData;

        public UploadPreviousData(IFootWrist foot, string filepath)
        {
            InitializeComponent();
            _foot = foot;
            _filePath = filepath;

            this.Shown += UploadPreviousData_Shown;
        }


        public async Task ImportPreviousFiles()
        {
            try
            {
                //string folderPath = @"D:\PC_system\CIRCUIT";
                string folderPath = _filePath;

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

                        // 🔹 Display row one-by-one
                        AnalysisTable.Invoke(new Action(() =>
                        {
                            _gridData.Add(data);
                            AnalysisTable.FirstDisplayedScrollingRowIndex =
                                AnalysisTable.RowCount - 1;
                        }));

                        // ⏳ Optional delay so user sees rows appear
                        await Task.Delay(50);


                    }
                }

                MessageBox.Show("✅ Previous files import completed.");


          
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error during previous files import: " + ex.Message);
            }
        }

        private void UploadPreviousData_Load(object sender, EventArgs e)
        {
            _gridData = new BindingList<IFootWristModel>();
            AnalysisTable.AutoGenerateColumns = true;
            AnalysisTable.DataSource = _gridData;
        }

        private async void UploadPreviousData_Shown(object sender, EventArgs e)
        {
            // Prevent double execution
            this.Shown -= UploadPreviousData_Shown;

            await ImportPreviousFiles();
        }
    }
}
