using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace MSDMonitoring
{
    public partial class MSDHIstory : Form
    {
        private readonly IMSD _msd;

        private const int PageSize = 50;
        private int CurrentPageIndex = 1;
        private int TotalPages = 0;
        private int TotalRows = 0;

        public MSDHIstory(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        public async Task LoadData(string searchTerm = "")
        {
            int TotalrowCount = await _msd.GetTotalHistoryList();
            TotalRows = TotalrowCount;
            TotalPages = (int)Math.Ceiling((double)TotalRows / PageSize);
            lblTotalPages.Text = TotalPages.ToString();
            lblCurrentPage.Text = CurrentPageIndex.ToString();


            MonitorTable.DataSource = await _msd.GetMSDHistoryList(CurrentPageIndex, PageSize, searchTerm);
            //MonitorTable.Columns[0].Width = 200;
            //MonitorTable.Columns[1].Width = 200;

            MonitorTable.Columns["Print"].DisplayIndex = 22;
            MonitorTable.Columns["Print"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            MonitorTable.Columns["Print"].Width = 70;
        }

        private async void MSDHIstory_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable; // keeps title bar
            // // First column, index 0
            await LoadData();
          

        }

        private void MonitorTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Bold the first column (index 0)
            if (e.ColumnIndex == 1 && e.Value != null)
            {
                e.CellStyle.Font = new Font(MonitorTable.Font, FontStyle.Bold);
            }

            if (e.ColumnIndex == 19 && e.Value != null)
            {
                if (e.Value.ToString().Trim().Equals("CLOSE", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(MonitorTable.Font, FontStyle.Bold);
                }
            }
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex < TotalPages)
            {
                CurrentPageIndex++;
                await LoadData();
                lblCurrentPage.Text = CurrentPageIndex.ToString();
            }
        }

        private async void BtnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex--;
                await LoadData();
                lblCurrentPage.Text = CurrentPageIndex.ToString();
            }
        }

        private async void BtnLast_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = TotalPages;
            await LoadData();
            lblCurrentPage.Text = CurrentPageIndex.ToString();
        }

        private async void BtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = 1;
            await LoadData();
            lblCurrentPage.Text = CurrentPageIndex.ToString();
        }

        private async void searchBox_TextChanged(object sender, EventArgs e)
        {
            CurrentPageIndex = 1; // Reset to first page when searching
            await LoadData(searchBox.Text.Trim());
        }

        private void MonitorTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure a valid row is clicked (not header)
            if (e.RowIndex < 0) return;
            int currentValue = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[1].Value);
            int QuanValue = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[10].Value);

            string strDate = MonitorTable.Rows[e.RowIndex].Cells[8].Value.ToString();
            string strTime = MonitorTable.Rows[e.RowIndex].Cells[9].Value.ToString();
            Debug.WriteLine("DATe : " + MonitorTable.Rows[e.RowIndex].Cells[8].Value.ToString());
            Debug.WriteLine("Time : " + MonitorTable.Rows[e.RowIndex].Cells[9].Value.ToString());
            
            ChangeQuantity c = new ChangeQuantity(_msd, currentValue, QuanValue, this, strDate, strTime);
            c.Show();

        }

        private async void Exportbtn_Click(object sender, EventArgs e)
        {
            var getexportData = await _msd.GetMSDExportList();

            if (getexportData != null)
            {
                foreach(var item in getexportData)
                {
                    await _msd.UpdateExportHistory(item.RecordID);
                }
                ExportToExcel(getexportData);
                await LoadData();
            }
            else
            {
                MessageBox.Show($"No Close Data found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(List<MSDmodel> data)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported Data";

                var properties = typeof(MSDmodel).GetProperties();
                int colCount = properties.Length;
                int rowCount = data.Count();

                // Insert headers
                for (int i = 0; i < colCount; i++)
                {
                    worksheet.Cells[1, i + 1] = properties[i].Name;
                }

                object[,] dataArray = new object[rowCount, colCount];

                // Adding data rows
                int row = 0;
                foreach (var item in data)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        var value = properties[col].GetValue(item, null);
                        dataArray[row, col] = value?.ToString();
                    }
                    row++;
                }

                // Assign data in one go for better performance
                Excel.Range dataRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[rowCount + 1, colCount]];
                dataRange.Value = dataArray;

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savedFilePath = saveFileDialog.FileName;
                    workbook.SaveAs(savedFilePath);
                    workbook.Close();
                    excelApp.Quit();

                    MessageBox.Show("Export Successful");

                    // Reopen the saved file
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = savedFilePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MonitorTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        private void MonitorTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure a valid row is clicked (not header)
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {

                Debug.WriteLine("4" + MonitorTable.Rows[e.RowIndex].Cells[21].Value.ToString());
                Debug.WriteLine("5" + MonitorTable.Rows[e.RowIndex].Cells[22].Value.ToString());

                var obj = new PrintLabelModel
                {
                    ReelID = MonitorTable.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Partnumber = MonitorTable.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    FloorLife = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[5].Value),
                    Level = MonitorTable.Rows[e.RowIndex].Cells[6].Value.ToString(),
                    LotNo = MonitorTable.Rows[e.RowIndex].Cells[7].Value.ToString(),
                    Date_IN = MonitorTable.Rows[e.RowIndex].Cells[12].Value.ToString(),
                    Quantity_IN = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[14].Value),
                    SupplierName =  MonitorTable.Rows[e.RowIndex].Cells[20].Value?.ToString()
                };
                // Template path
                string templatePath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Templates\MSDPrintLabel.xlsx";
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("Template file not found: " + templatePath);
                    return;
                }

                // Create Excel application
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Optional: show Excel

                // Open the template workbook
                Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);

                // Reference worksheet by name
                Excel.Worksheet worksheet = workbook.Sheets["Label"] as Excel.Worksheet;

                // Populate cells C2:C10
                worksheet.Range["C3"].Value = obj.ReelID;
                worksheet.Range["C4"].Value = obj.Partnumber;
                worksheet.Range["C5"].Value = obj.FloorLife;
                worksheet.Range["C6"].Value = obj.Level;
                worksheet.Range["C7"].Value = obj.SupplierName;
                worksheet.Range["C8"].Value = obj.LotNo;
                worksheet.Range["C9"].Value = obj.Date_IN;
                worksheet.Range["C10"].Value = obj.Quantity_IN;
                worksheet.Range["C11"].Value =  MonitorTable.Rows[e.RowIndex].Cells[18].Value.ToString();
                worksheet.Range["C12"].Value = MonitorTable.Rows[e.RowIndex].Cells[21].Value.ToString() + "-" + MonitorTable.Rows[e.RowIndex].Cells[22].Value.ToString();
                // Show print dialog
                worksheet.PrintPreview();


                // Optional: close workbook without saving
                // workbook.Close(false);
                // excelApp.Quit();

                // Optional: release COM objects
                // Marshal.ReleaseComObject(worksheet);
                // Marshal.ReleaseComObject(workbook);
                // Marshal.ReleaseComObject(excelApp);


                //Debug.WriteLine($@"Reel ID : {ReeID} - Partnumber {Partnumber}");
            }


        }
    }
}
