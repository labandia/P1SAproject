using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            MonitorTable.Columns[0].Width = 200;
            MonitorTable.Columns[1].Width = 200;
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

            if (e.ColumnIndex == 16 && e.Value != null)
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

            int currentValue = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[0].Value);
            int QuanValue = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[8].Value);

            ChangeQuantity c = new ChangeQuantity(_msd, currentValue, QuanValue,  this);
            c.Show();

        }


        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultValue };

            Button okButton = new Button() { Text = "OK", Left = 200, Width = 70, Top = 80, DialogResult = DialogResult.OK };
            Button cancelButton = new Button() { Text = "Cancel", Left = 280, Width = 70, Top = 80, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(okButton);
            prompt.Controls.Add(cancelButton);

            prompt.AcceptButton = okButton;
            prompt.CancelButton = cancelButton;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
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



    }
}
