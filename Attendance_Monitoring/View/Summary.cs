using Attendance_Monitoring.Global;
using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Attendance_Monitoring.View
{
    public partial class Summary : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private static List<SummaryAttendanceModel> sumlist;


        // Share variable to all
        public int sec;
        public string tb;
        public string shiftsday;
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DateTime currentDate = DateTime.Now;


        public Summary(int section, string tablename, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            sec = section;
            tb = tablename;
            _serviceProvider=serviceProvider;
        }

        // ================= DISPLAY ATTENDANCE SUMMARY =========================
        public async void DisplaysummarytableBySection()
        {
            try
            {
                string newDateString = dstart.Value.ToString("yyyy-MM-dd");
                //sumlist = await _admin.GetSummaryList(tb, newDateString, newDateString);
                //summarytable.DataSource = sumlist;

                summarytable.Columns["Date_today"].DisplayIndex = 0;
                summarytable.Columns["Employee_ID"].DisplayIndex = 1;
                summarytable.Columns["FullName"].DisplayIndex = 2;
                summarytable.Columns["TimeIn"].DisplayIndex = 3;
                summarytable.Columns["TimeOut"].DisplayIndex = 4;
                summarytable.Columns["LateTime"].DisplayIndex = 5;
                summarytable.Columns["Regular"].DisplayIndex = 6;

                summarytable.Columns["Gtotal"].DisplayIndex = 7;
                summarytable.Columns["Overtime"].DisplayIndex = 8;
                summarytable.Columns["ShiftsTime"].DisplayIndex = 9;

                summarytable.Columns["Action"].DisplayIndex = 10;
                summarytable.Columns["Edit"].DisplayIndex = 11;

                label4.Text = "Total Results: " + summarytable.RowCount;
            }
            catch (FormatException) {
                MessageBox.Show("No Summary Data found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // ================= EXPORT DATA =========================
        private async void exportbtn_Click(object sender, EventArgs e)
        {
            DateTime startd, enddate;
            if (!DateTime.TryParse(dstart.Text, out startd) || !DateTime.TryParse(dend.Text, out enddate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }
            string formattedStartDate = startd.ToString("yyyy-MM-dd");
            string formattedEndDate = enddate.ToString("yyyy-MM-dd");

            //string sqlquery = GenerateSQLQuery(startd, enddate, searchbox.Text, shifts.Text);
            //var  explist = await _admin.GetExportSummaryList(sqlquery, formattedStartDate, formattedEndDate, shifts.Text, searchbox.Text);

            //if (explist == null)
            //{
            //    MessageBox.Show("No data found.");
            //    return;
            //}
           
            //ExportToExcel(explist, sec);
        }

        private string GenerateSQLQuery(DateTime startd, DateTime enddate, string searchText, string shiftText)
        {
            string query = @"SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, 
                            pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'hh:mm') AS TimeIn,  
                            FORMAT(pc.TimeOut, 'hh:mm') AS TimeOut, pc.LateTime, pc.Regular, 
                            pc.Overtime, pc.Gtotal, pc.Shifts 
                     FROM " + tb + @" pc 
                     INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID 
                     WHERE CAST(pc.Date_today AS DATE) BETWEEN @StartDate AND @EndDate";

           

            if (!string.IsNullOrEmpty(shiftText))
            {
                query += " AND pc.Shifts = @Shift";
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                string searchFilter = searchText != "" ? "'%"+searchText+"%'" : "";
                query += " AND (e.Employee_ID LIKE "+searchFilter+" OR e.FullName LIKE "+searchFilter+")";
            }
               

            query += " ORDER BY pc.RecordID DESC";

            return query;
        }



        // ================= FILTER DATA BASED ON DATE AND SHIFT =========================
        private async void button1_Click(object sender, EventArgs e)
        {
            //sumlist = Enumerable.Empty<SummaryAttendanceModel>();
            DateTime startd, enddate;
            if (!DateTime.TryParse(dstart.Text, out startd) || !DateTime.TryParse(dend.Text, out enddate))
            {
                MessageBox.Show("Invalid date format.");
                return;
            }
            string formattedStartDate = startd.ToString("yyyy-MM-dd");
            string formattedEndDate = enddate.ToString("yyyy-MM-dd");

   

            //string sqlquery = GenerateSQLQuery(startd, enddate, searchbox.Text, shifts.Text);
            //sumlist = await _admin.GetExportSummaryList(sqlquery, formattedStartDate, formattedEndDate, shifts.Text, searchbox.Text);
            //summarytable.DataSource = sumlist;
            //summarytable.Columns["Action"].DisplayIndex = 10;
            //label4.Text = "Total Results: " + summarytable.RowCount;
        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
            Attendance at = new Attendance(sec, tb, _serviceProvider);
            at.Show();
            Visible = false;
        }

        private void ExportToExcel(List<SummaryAttendanceModel> data, int section)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported Data";

                var properties = typeof(SummaryAttendanceModel).GetProperties();
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
                        dataArray[row, col] = section != 2 ? "'" + value?.ToString() : value?.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            dstart.Format = DateTimePickerFormat.Custom;
            dstart.CustomFormat = "yyyy-MM-dd";
            dstart.Value = DateTime.Now;

            dend.Format = DateTimePickerFormat.Custom;
            dend.CustomFormat = "yyyy-MM-dd";
            dend.Value = DateTime.Now;

            DisplaysummarytableBySection();
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            dstart.Value = DateTime.Now;
            dend.Value = DateTime.Now;
            DisplaysummarytableBySection();
        }

        private void shifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            shiftsday = (shifts.SelectedIndex == 0) ? "DAYSHIFT" : "NIGHTSHIFT";             
        }

        private  void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.ToLower();
            // Filter the list using LINQ
            var filteredList = sumlist.Where(p => p.Employee_ID.ToLower().Contains(filterText) ||
                            p.Fullname.ToLower().Contains(filterText))
                            .ToList();

            summarytable.DataSource =  filteredList;
            label4.Text = "Total Records: " + summarytable.RowCount;
        }
    }
}
