using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace Attendance_Monitoring.View
{
    public partial class Summary : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminController _admin;
        private static IEnumerable<SummaryAttendanceModel> sumlist;

        // call from other class 
        //Dbconnection connect = new Dbconnection();
        Timeprocess tim = new Timeprocess();

        // Share variable to all
        public int sec;
        public string tb;
        public string shiftsday;
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DateTime currentDate = DateTime.Now;


        public Summary(int section, string tablename, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _admin = new AdminController();
            sec = section;
            tb = tablename;
            _serviceProvider=serviceProvider;
        }


        public async void DisplaysummarytableBySection()
        {   
            string newDateString = dstart.Value.ToString("yyyy-MM-dd");
            sumlist = Enumerable.Empty<SummaryAttendanceModel>();
            IEnumerable<SummaryAttendanceModel> items = await _admin.GetSummaryList(tb, newDateString, newDateString);
            sumlist = items.ToList();
            summarytable.DataSource = sumlist;
            label4.Text = "Total Results: " + summarytable.RowCount;


            // string newDateString = currentDate.ToString("yyyy-MM-dd");

            //string sqlquery = "SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, " +
            //                  "pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'HH:mm') as TimeIn, FORMAT(pc.TimeOut, 'HH:mm')  as TimeOut, pc.LateTime, pc.Regular, " +
            //                  "pc.Overtime, pc.Gtotal, pc.Shifts " +
            //                   "FROM "+ tb +" pc " +
            //                   "INNER JOIN dbo.Employee_tbl e " +
            //                   "ON e.Employee_ID = pc.Employee_ID " +
            //                   "WHERE   CAST(Date_today AS DATE) between '" + newDateString  + "' AND " +
            //                   "'" + newDateString  + "'";



            //DataTable dt = await connect.GetData(sqlquery);
            // summarytable.DataSource = dt;
            // label4.Text = "Total Results: " + summarytable.RowCount;
        }


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


            string sqlquery = GenerateSQLQuery(startd, enddate, searchbox.Text, shifts.Text);

            //Debug.WriteLine(sqlquery);

            // DataTable dt = await connect.GetData(sqlquery);
            IEnumerable<SummaryAttendanceModel> explist = await _admin.GetExportSummaryList(sqlquery, formattedStartDate, formattedEndDate, shifts.Text, searchbox.Text);

            if (explist.Any())
            {
                ExportToExcel(explist, sec);
            }
            else
            {
                MessageBox.Show("No data found.");
            }

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

        private  async void button1_Click(object sender, EventArgs e)
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

   

            string sqlquery = GenerateSQLQuery(startd, enddate, searchbox.Text, shifts.Text);
            IEnumerable<SummaryAttendanceModel> explist = await _admin.GetExportSummaryList(sqlquery, formattedStartDate, formattedEndDate, shifts.Text, searchbox.Text);
            sumlist = explist.ToList();
            summarytable.DataSource = sumlist;
            label4.Text = "Total Results: " + summarytable.RowCount;

            //string sqlquery;
            //DateTime startd = DateTime.Parse(dstart.Text);
            //DateTime enddate = DateTime.Parse(dend.Text);


            //if (shifts.SelectedIndex != -1)
            //{          
            //    string sdate = startd.ToString("yyyy-MM-dd");
            //    string fdate = enddate.ToString("yyyy-MM-dd");

            //    sqlquery = "SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, " +
            //                          "pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'HH:mm') as TimeIn,  FORMAT(pc.TimeOut, 'HH:mm')  as TimeOut, pc.Regular, " +
            //                          "pc.Overtime, pc.Gtotal, pc.Shifts " +
            //                          "FROM "+ tb +" pc " +
            //                          "INNER JOIN Employee_tbl e " +
            //                          "ON e.Employee_ID = pc.Employee_ID " +
            //                          "WHERE CAST(Date_today AS DATE) between '" + sdate  + "' AND " +
            //                          "'" + fdate  + "'  ORDER BY pc.RecordID DESC";

            //}
            //else
            //{
            //    string sdate = startd.ToString("yyyy-MM-dd");
            //    string fdate = enddate.ToString("yyyy-MM-dd");

            //    sqlquery = "SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, " +
            //                          "pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'HH:mm') as TimeIn,  FORMAT(pc.TimeOut, 'HH:mm')  as TimeOut, pc.Regular, " +
            //                          "pc.Overtime, pc.Gtotal, pc.Shifts " +
            //                          "FROM "+ tb +" pc " +
            //                          "INNER JOIN Employee_tbl e " +
            //                          "ON e.Employee_ID = pc.Employee_ID " +
            //                          "WHERE CAST(Date_today AS DATE) between '" + sdate  + "' AND " +
            //                          "'" + fdate  + "' AND pc.Shifts = '" + shiftsday + "'  ORDER BY pc.RecordID DESC ";

            //}
            //DataTable dt = await connect.GetData(sqlquery);
            //summarytable.DataSource = dt;
            //label4.Text = "Total Results: " + summarytable.RowCount;
        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            Attendance at = new Attendance(sec, tb, _serviceProvider);
            at.Show();
            Visible = false;
        }

        private void ExportToExcel(IEnumerable<SummaryAttendanceModel> data, int section)
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

        //private void ExportToExcel(DataTable dataTable, int section)
        //{
        //    try
        //    {
        //        Excel.Application excelApp = new Excel.Application();
        //        Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
        //        Excel.Worksheet worksheet = workbook.ActiveSheet;
        //        worksheet.Name = "Exported Data";

        //        int rowCount = dataTable.Rows.Count;
        //        int colCount = dataTable.Columns.Count;


        //        // Insert headers
        //        for (int i = 1; i < colCount; i++)
        //        {
        //            worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
        //        }

        //        object[,] dataArray = new object[rowCount, colCount];
        //        // Adding data rows
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            for (int j = 0; j < colCount; j++)
        //            {
        //                // Precede the value with an apostrophe to treat it as text
        //                dataArray[i, j] = section != 2 ? "'" + dataTable.Rows[i][j].ToString() : dataTable.Rows[i][j].ToString();
        //            }
        //        }

        //        // Assign data in one go for better performance
        //        Excel.Range dataRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[rowCount + 1, colCount]];
        //        dataRange.Value = dataArray;


        //        SaveFileDialog saveFileDialog = new SaveFileDialog
        //        {
        //            Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
        //            FilterIndex = 1,
        //            RestoreDirectory = true
        //        };

        //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            workbook.SaveAs(saveFileDialog.FileName);
        //            workbook.Close();
        //            MessageBox.Show("Export Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }

        //}

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
            if(shifts.SelectedIndex == 0) {
                shiftsday = "DAYSHIFT";
            }
            else
            {
                shiftsday = "NIGHTSHIFT";
            }          
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

            //DateTime startd, enddate;
            //if (!DateTime.TryParse(dstart.Text, out startd) || !DateTime.TryParse(dend.Text, out enddate))
            //{
            //    MessageBox.Show("Invalid date format.");
            //    return;
            //}


            //var filteredList = sumlist
            //                 .Where(p => DateTime.TryParse(p.Date_today, out DateTime dateToday) &&
            //                             dateToday >= startd && dateToday <= enddate)
            //                 .ToList();

            //summarytable.DataSource =  filteredList;
            //label4.Text = "Total Records: " + summarytable.RowCount;



            //string sqlquery;
            //DateTime startd = DateTime.Parse(dstart.Text);
            //DateTime enddate = DateTime.Parse(dend.Text);


            //if (string.IsNullOrEmpty(shifts.Text))
            //{
            //    string sdate = startd.ToString("yyyy-MM-dd");
            //    string fdate = enddate.ToString("yyyy-MM-dd");

            //    sqlquery = "SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, " +
            //                          "pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'hh:mm') as TimeIn,  FORMAT(pc.TimeOut, 'hh:mm')  as TimeOut, pc.LateTime, pc.Regular, " +
            //                          "pc.Overtime, pc.Gtotal, pc.Shifts " +
            //                          "FROM "+ tb +" pc " +
            //                          "INNER JOIN Employee_tbl e " +
            //                          "ON e.Employee_ID = pc.Employee_ID " +
            //                          "WHERE CAST(Date_today AS DATE) between '" + sdate  + "' AND " +
            //                          "'" + fdate  + "' AND (e.Employee_ID LIKE '%"+ searchbox.Text +"%' OR e.FullName LIKE '%"+ searchbox.Text +"%')  ORDER BY pc.RecordID DESC";

            //}
            //else
            //{
            //    string sdate = startd.ToString("yyyy-MM-dd");
            //    string fdate = enddate.ToString("yyyy-MM-dd");

            //    sqlquery = "SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, " +
            //                          "pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'hh:mm') as TimeIn,  FORMAT(pc.TimeOut, 'hh:mm')  as TimeOut, pc.LateTime, pc.Regular, " +
            //                          "pc.Overtime, pc.Gtotal, pc.Shifts " +
            //                          "FROM "+ tb +" pc " +
            //                          "INNER JOIN Employee_tbl e " +
            //                          "ON e.Employee_ID = pc.Employee_ID " +
            //                          "WHERE CAST(Date_today AS DATE) between '" + sdate  + "' AND " +
            //                          "'" + fdate  + "' AND pc.Shifts = '" + shiftsday + "' AND (e.Employee_ID LIKE '%"+ searchbox.Text +"%' OR e.FullName LIKE '%"+ searchbox.Text +"%')  ORDER BY pc.RecordID DESC ";

            //}
            //DataTable dt = await connect.GetData(sqlquery);
            //summarytable.DataSource = dt;
        }
    }
}
