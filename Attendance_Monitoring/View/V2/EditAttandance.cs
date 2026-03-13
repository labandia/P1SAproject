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

        private DateTime _timeIn;

        public EditAttandance(P1SA_AttendanceModel p1sa, IAttendanceMonitor attend)
        {
            InitializeComponent();

            _p1sa = p1sa;
            _attend = attend;

            IDText.Text = "Employee ID: " + _p1sa.Employee_ID;
            Fullname.Text = _p1sa.Fullname;

            // Hide timeout controls if no timeout
            TimeOutText.Visible = !string.IsNullOrEmpty(_p1sa.TimeOut);
            label4.Visible = TimeOutText.Visible;

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
            if (!DateTime.TryParseExact(
                  _p1sa.TimeOut,
                  "MM/dd/yy HH:mm:ss",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out DateTime DateOut))
            {
                MessageBox.Show("Invalid TimeIn format.");
                return;
            }

            _timeIn = DateIn;
            TimeInText.Value = DateIn;
            TimeOutText.Value = DateOut;

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
            DateTime DateOut = TimeOutText.Value;

            double regHours = Timeprocess.CalculateWorkingHoursV2(DateIn, DateOut);
            double overStr = ComputetheOvertime(DateIn, DateOut);
            
            double getotal = regHours + overStr;


            regText.Text = regHours.ToString("0.00");
            overText.Text = overStr.ToString("0.00");
            gTotalText.Text = getotal.ToString("0.00");
        }


        public double ComputetheOvertime(DateTime timein, DateTime timeout)
        {
            DateTime overtimeStart;

            TimeSpan tin = timein.TimeOfDay;

            if (tin <= new TimeSpan(6, 30, 0))
            {
                // First shift (06:00)
                overtimeStart = timein.Date.AddHours(15).AddMinutes(30);
            }
            else if (tin <= new TimeSpan(7, 59, 59))
            {
                // Second shift (07:30)
                overtimeStart = timein.Date.AddHours(16).AddMinutes(30);
            }
            else
            {
                // Third shift (08:30)
                overtimeStart = timein.Date.AddHours(17).AddMinutes(30);
            }

            return Timeprocess.CalculateOTHoursV2(overtimeStart, timeout);
        }

        private void TimeOutText_ValueChanged(object sender, EventArgs e)
        {
            displaytime(_timeIn);
        }
    }
}
