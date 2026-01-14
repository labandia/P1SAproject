using Attendance_Monitoring.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Attendance_Monitoring.View
{
    public partial class CRSummary : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private static List<CRmodel> critemlist;

        public int sec;
        public string shiftsday;

        public CRSummary(int section, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            sec = section;
            _serviceProvider=serviceProvider;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CR_Monitoring c = new CR_Monitoring(sec, _serviceProvider);
            c.Show();       
            Visible = false; 
        }

        private void CRSummary_Load(object sender, EventArgs e)
        {
            dstart.Value = DateTime.Now;
            dend.Value = DateTime.Now;

            DisplayCRMonitor();
        }

        //  ####################  DISPLAY  CR MONITORING  #################### //
        public async void DisplayCRMonitor()
        {
            //string newDateString = dstart.Value.ToString("yyyy-MM-dd");
            //critemlist = await _admin.GetCRMonitorSummarylist(sec, newDateString, newDateString);

            //CRtable.DataSource = critemlist;
            //DisplayTotal.Text = "Total Attendence: " + CRtable.RowCount;
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.ToLower();
            // Filter the list using LINQ
            var filteredList = critemlist.Where(p => p.Employee_ID.ToLower().Contains(filterText) ||
                            p.Fullname.ToLower().Contains(filterText))
                            .ToList();

            CRtable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + CRtable.RowCount;
        }

        private async void exportbtn_Click(object sender, EventArgs e)
        {
            //DateTime startd, enddate;
            //if (!DateTime.TryParse(dstart.Text, out startd) || !DateTime.TryParse(dend.Text, out enddate))
            //{
            //    MessageBox.Show("Invalid date format.");
            //    return;
            //}
            //string formattedStartDate = startd.ToString("yyyy-MM-dd");
            //string formattedEndDate = enddate.ToString("yyyy-MM-dd");

            //string sqlquery = GenerateSQLQuery(startd, enddate, searchbox.Text, shifts.Text);

            ////Debug.WriteLine(sqlquery);

            //// DataTable dt = await connect.GetData(sqlquery);
            //var explist = await _admin.GetExportCRMonitorSummarylist(sqlquery, formattedStartDate, formattedEndDate, shifts.Text, searchbox.Text);

            //if (explist.Any())
            //{
            //    ExportToExcel(explist, sec);
            //}
            //else
            //{
            //    MessageBox.Show("No data found.");
            //}


        }


        private string GenerateSQLQuery(DateTime startd, DateTime enddate, string searchText, string shiftText)
        {
            string strquery = @"SELECT c.RecordID, 
		                            FORMAT(c.TimeIn, 'MM/dd/yyyy') as Date_today, 
		                            c.Employee_ID, e.FullName, e.Affiliation, e.Process, 
		                            FORMAT(c.TimeIn, 'HH:mm') as TimeIn, 
		                            FORMAT(c.TimeOut, 'HH:mm') as TimeOut, 
		                            c.Duration
	                            FROM CR_Montoring_tbl c
	                            INNER JOIN Employee_tbl e ON e.Employee_ID = c.Employee_ID
	                            WHERE e.Department_ID = " + sec + @" AND CAST(c.TimeIn AS DATE) 
	                            between @StartDate AND @EndDate";


            if (!string.IsNullOrEmpty(shiftText))
            {
                strquery += " AND pc.Shifts = @Shift";
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                string searchFilter = searchText != "" ? "'%"+searchText+"%'" : "";
                strquery += " AND (e.Employee_ID LIKE "+searchFilter+" OR e.FullName LIKE "+searchFilter+")";
            }

            strquery += " ORDER BY RecordID DESC";

            return strquery;
        }



        private void ExportToExcel(List<ExportCRmodel> data, int section)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "CR_Summary_Data";

                var properties = typeof(ExportCRmodel).GetProperties();
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

        private void shifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (shifts.SelectedIndex == 0)
            {
                shiftsday = "DAYSHIFT";
            }
            else
            {
                shiftsday = "NIGHTSHIFT";
            }
        }

        private async void Filterbtn_Click(object sender, EventArgs e)
        {
            //DateTime startd, enddate;
            //if (!DateTime.TryParse(dstart.Text, out startd) || !DateTime.TryParse(dend.Text, out enddate))
            //{
            //    MessageBox.Show("Invalid date format.");
            //    return;
            //}
            //string formattedStartDate = startd.ToString("yyyy-MM-dd");
            //string formattedEndDate = enddate.ToString("yyyy-MM-dd");

            //string sqlquery = GenerateSQLQuery(startd, enddate, searchbox.Text, shifts.Text);

            ////Debug.WriteLine(sqlquery);

            //// DataTable dt = await connect.GetData(sqlquery);
            //var sumlist = await _admin.GetExportCRMonitorSummarylist(sqlquery, formattedStartDate, formattedEndDate, shifts.Text, searchbox.Text);
            //CRtable.DataSource = sumlist;
            //DisplayTotal.Text = "Total Results: " + CRtable.RowCount;
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            dstart.Format = DateTimePickerFormat.Custom;
            dstart.CustomFormat = "yyyy-MM-dd";
            dstart.Value = DateTime.Now;

            dend.Format = DateTimePickerFormat.Custom;
            dend.CustomFormat = "yyyy-MM-dd";
            dend.Value = DateTime.Now;



            DisplayCRMonitor();
        }
    }
}
