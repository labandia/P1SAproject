using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Microsoft.Office.Interop.Excel;

namespace Attendance_Monitoring.View.V2
{
    public partial class SummaryV2 : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAttendanceMonitor _monitor;
        private static List<P1SA_AttendanceModel> sumlist;

        // Share variable to all
        public int sec;
        public string shiftsday;
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DateTime currentDate = DateTime.Now;

        public SummaryV2(int section, IAttendanceMonitor monitor, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MessageBox.Show($@"Section ID: {section}");
            sec = section;
            _monitor = monitor;
            _serviceProvider = serviceProvider;
        }

        // ================= DISPLAY ATTENDANCE SUMMARY =========================
        public async void DisplaysummarytableBySection()
        {
            try
            {
                string newDateString = dstart.Value.ToString("yyyy-MM-dd");
                var getRecord = await _monitor.GetAttendanceSummaryList(newDateString, newDateString, sec, "");

                sumlist = (getRecord.Success) ? getRecord.Payload.ToList() : new List<P1SA_AttendanceModel>();
                summarytable.DataSource = sumlist;

                //summarytable.Columns["Date_today"].DisplayIndex = 0;
                //summarytable.Columns["Employee_ID"].DisplayIndex = 1;
                //summarytable.Columns["FullName"].DisplayIndex = 2;
                //summarytable.Columns["TimeIn"].DisplayIndex = 3;
                //summarytable.Columns["TimeOut"].DisplayIndex = 4;
                //summarytable.Columns["LateTime"].DisplayIndex = 5;
                //summarytable.Columns["Regular"].DisplayIndex = 6;

                //summarytable.Columns["Overtime"].DisplayIndex = 7;
                //summarytable.Columns["Gtotal"].DisplayIndex = 8;
                //summarytable.Columns["Shifts"].DisplayIndex = 9;
                summarytable.Columns["Action"].DisplayIndex = 10;

                label4.Text = "Total Results: " + summarytable.RowCount;
            }
            catch (FormatException)
            {
                MessageBox.Show("No Summary Data found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SummaryV2_Load(object sender, EventArgs e)
        {
            dstart.Value = DateTime.Now;
            dend.Value = DateTime.Now;
            DisplaysummarytableBySection();
        }
    }
}
