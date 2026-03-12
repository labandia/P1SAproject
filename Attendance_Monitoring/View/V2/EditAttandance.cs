using Attendance_Monitoring.Global;
using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Attendance_Monitoring.View.V2
{
    public partial class EditAttandance : Form
    {
        public readonly P1SA_AttendanceModel _p1sa;
        private readonly IAttendanceMonitor _attend;


        public EditAttandance(P1SA_AttendanceModel p1sa, IAttendanceMonitor attend)
        {
            InitializeComponent();

            _p1sa = p1sa;
            _attend = attend;

            IDText.Text = "Employee ID: " + _p1sa.Employee_ID;
            Fullname.Text = _p1sa.Fullname;

            // Hide timeout controls if no timeout
            if (string.IsNullOrEmpty(_p1sa.TimeOut)){
                label4.Visible = false; 
                TimeOutText.Visible = false;
            }

            InitializeData();

        }

        private void InitializeData()
        {
            if (!DateTime.TryParseExact(
                  _p1sa.TimeIn,
                  "MM/dd/yy HH:mm:ss",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out DateTime DateIn))
            {
                MessageBox.Show("Invalid TimeIn format.");
                return;
            }

            TimeInText.Value = DateIn;

            if (!string.IsNullOrEmpty(_p1sa.TimeOut))
            {
                displaytime(DateIn);
            }
            else
            {
                regText.Text = "";
                overText.Text = "";
                gTotalText.Text = "";
            }
        }


        private void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new P1SA_AttendanceModel
                {
                    TimeIn = TimeInText.Value.ToString("MM/dd/yy HH:mm:ss"),
                    TimeOut = TimeOutText.Value.ToString("MM/dd/yy HH:mm:ss"),
                    Overtime = double.Parse(overText.Text),
                    Regular = double.Parse(regText.Text),
                    RecordID = _p1sa.RecordID
                };

                MessageBox.Show($@"Time : {obj.TimeIn} - Time Out : {obj.TimeOut} - RecordID : {obj.RecordID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void displaytime(DateTime DateIn)
        {
            DateTime? DateOut = null;
            if (!string.IsNullOrEmpty(_p1sa.TimeOut))
            {
                DateOut = DateTime.ParseExact(
                    _p1sa.TimeOut,
                    "MM/dd/yy HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture
                );
            }

            double regHours = 0;
            double overStr = 0;

            if (DateOut.HasValue)
            {
                regHours = Timeprocess.CalculateWorkingHoursV2(DateIn, DateOut.Value);
                MessageBox.Show($"Regular Hours: {regHours}");  
                overStr = ComputetheOvertime(DateIn, DateOut.Value);
                MessageBox.Show($"Overtime : {overStr}");
            }

            double getotal = regHours + overStr;

            

            if (DateOut.HasValue)
                TimeOutText.Value = DateOut.Value;

            regText.Text = regHours.ToString("0.00");
            overText.Text = overStr.ToString("0.00");
            gTotalText.Text = getotal.ToString("0.00");
        }


        public double ComputetheOvertime(DateTime timein, DateTime timeout)
        {
            DateTime overtimestart;

            if (timein.TimeOfDay >= new TimeSpan(5, 0, 0) && timein.TimeOfDay < new TimeSpan(6, 30, 0))
            {
                overtimestart = new DateTime(
                    timein.Year,
                    timein.Month,
                    timein.Day,
                    15, 30, 0);
            }
            else if (timein.TimeOfDay >= new TimeSpan(6, 30, 0) && timein.TimeOfDay < new TimeSpan(7, 30, 0))
            {
                overtimestart = new DateTime(
                    timein.Year,
                    timein.Month,
                    timein.Day,
                    16, 30, 0);
            }
            else
            {
                overtimestart = new DateTime(
                    timein.Year,
                    timein.Month,
                    timein.Day,
                    17, 30, 0);
            }

            double overtimestr = Timeprocess.CalculateOTHoursV2(overtimestart, timeout);

            return overtimestr;
        }
    }
}
