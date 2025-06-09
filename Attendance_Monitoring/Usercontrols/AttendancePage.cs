using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Timer = System.Windows.Forms.Timer;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class AttendancePage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminController _admin;
        private static List<AttendanceModel> itemattends;
        private static List<Employee> emplist;
        // call from other class 
        Timeprocess tim = new Timeprocess();

        private Timer timer;

        // Share variable to all
        public int sec;
        public string tb;
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        public AttendancePage(int section, string tablename, IServiceProvider serviceProvider)
        {
            sec = section;
            tb = tablename;
            _admin = new AdminController();
            _serviceProvider=serviceProvider;
        }

        public AttendancePage()
        {
            InitializeComponent();
          
        }

        //  ####################  DISPLAY  THE TIME IN AND OUT  #################### //
        public async void TimeAttendanceDisplay(int selectime)
        {
            string shift = "";
            string timecheck = "";
            string Timeout_date = "";


            // TIME OUT NIGHT SHIFT DATE TIME
            DateTime yesterday = DateTime.Today.AddDays(-1);
            // Change format of the date and time
            string yest = yesterday.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (selectime == 0)
            {
                shift = tim.TimeIncheck(DateTime.Now);
                timecheck = tdate;
            }
            else
            {
                shift = tim.timeoutcheck(DateTime.Now);
                Timeout_date = shift == "DAYSHIFT" ? tdate : yest;
                timecheck = Timeout_date;
            }

            IEnumerable<AttendanceModel> attend = await _admin.GetAttendanceMonitor(tb, timecheck, shift, selectime);
            itemattends = attend.ToList();
 
            attendancetable.DataSource = itemattends;
            DisplayTotal.Text = "Total Attendence: " + attendancetable.RowCount;
            EmployID.Focus();
        }

        private  void AttendancePage_Load(object sender, EventArgs e)
        {
           // IEnumerable<Employee> emp = await _admin.GetAllEmployees();
           // emplist = emp.ToList();
        }


        private async void EnterTime(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            // Process Employee ID
            string empid = EmployID.Text.Replace("-", "");
            string shift = tim.TimeIncheck(DateTime.Now);

            // Filter employee once
            var employee = emplist.FirstOrDefault(p => p.EmployeeID.Equals(empid, StringComparison.OrdinalIgnoreCase) &&
                                                  p.Department_ID == sec);

            if (employee == null)
            {
                MessageBox.Show("Wrong ID number");
                EmployID.Focus();
                return;
            }

            // Time In Process
            if (selecttime.SelectedIndex == 0)
            {
                // Check if employee has already timed in
                bool alreadyTimedIn = itemattends.Any(i =>
                    i.Employee_ID.Equals(empid, StringComparison.OrdinalIgnoreCase) &&
                    i.Date_today.Date == DateTime.Today);

                if (alreadyTimedIn)
                {
                    MessageBox.Show("ALREADY TIME IN", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Perform Time In
                bool result = await _admin.AttendanceTimeINandOut(empid, shift, tim.CalculateLateTime(), tb);

                if (result)
                {
                    TextName.Text = employee.Fullname;
                    Statustext.BackColor = Color.FromArgb(50, 181, 111);
                    Statustext.Text = "Successfully Time In";

                    // Reuse Timer instead of creating a new one every time
                    if (timer == null)
                    {
                        timer = new Timer();
                        timer.Interval = 1000;
                        timer.Tick += Timer_Tick;
                    }

                    timer.Start();
                    TimeAttendanceDisplay(selecttime.SelectedIndex);
                }
            }
            // Time Out Process
            else if (selecttime.SelectedIndex == 1)
            {
                string shiftname = tim.timeoutcheck(DateTime.Now);

                if (shiftname == "DAYSHIFT")
                {
                    TimeoutDay(empid, employee.Fullname);
                }
                else
                {
                    TimeoutNight(empid, employee.Fullname);
                }
            }
            else
            {
                MessageBox.Show("SELECT TIME IN / OUT");
            }

        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer after it has ticked
            timer.Stop();

            // Code to execute after timeout
            Statustext.BackColor = Color.FromArgb(26, 36, 59);
            Statustext.Text = "Checking status ...";
            EmployID.Text = "";
        }
        private void TimerOut_Tick(object sender, EventArgs e)
        {
            // Stop the timer after it has ticked
            timer.Stop();

            // Code to execute after timeout
            Statustext.BackColor = Color.FromArgb(26, 36, 59);
            Statustext.Text = "Checking status ...";
            EmployID.Text = "";
            TextName.Text = "";
            EmployID.Focus();
        }


        public async void TimeoutDay(string empid, string fullname)
        {
            double reg, oTHours, gTotal;
            // Use CultureInfo("en-US") for consistent formatting
            CultureInfo culture = new CultureInfo("en-US");

            string dtDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff", culture);
            // Check if the user is already timed in
            string timeincheck = "SELECT TOP 1 TimeIn FROM " + tb + " WHERE Employee_ID = '" + empid + "' AND CAST(Date_today AS DATE) = '" + dtDate + "' ";
            DataTable summary = new DataTable();

            summary = await SqlDataAccess.GetDataByDataTable(timeincheck);

            if (summary.Rows.Count > 0)
            {
                DataRow time = summary.Rows[0];
                //string fullname = summary["FullName"]?.ToString() ?? string.Empty;

                // Check if the user is already timed In
                string timecheckout = "SELECT TOP 1 TimeIn  FROM " + tb + " WHERE Employee_ID = '" + empid + "' AND CAST(Date_today AS DATE) = '" + dtDate + "' AND TimeOut IS NOT NULL";

                bool result = await SqlDataAccess.Checkdata(timecheckout);

                if (result == true)
                {
                    MessageBox.Show("Already timed out in the attendance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DateTime timeIn = (DateTime)time["TimeIn"];


                    // Time in from the database
                    //TimeSpan timeInSpan = TimeSpan.Parse(time["TimeIn"].ToString("HH:mm:ss"), culture);
                    TimeSpan timeInSpan = timeIn.TimeOfDay;

                    // Get the Current Time from the PC settings
                    string currentTime = DateTime.Now.ToString(@"HH\:mm\:ss", culture);



                    TimeSpan earlyStart = TimeSpan.Parse("03:30:00", culture);
                    TimeSpan lateStart = TimeSpan.Parse("06:30:00", culture);
                    TimeSpan cutoffEarly = TimeSpan.Parse("06:00:00", culture);
                    TimeSpan cutoffLate = TimeSpan.Parse("10:00:00", culture);
                    TimeSpan dayEnd = TimeSpan.Parse("14:30:00", culture);
                    TimeSpan defaultDayLength = TimeSpan.FromHours(7.67);


                    // Currenttime >= 3:30 am  AND  Currenttime <  06:30:00
                    if (timeInSpan >= earlyStart && timeInSpan < lateStart)
                    {
                        /// Currenttime >= 3:30 am  AND  Currenttime <  06:00:00
                        if (timeInSpan >= earlyStart && timeInSpan < cutoffEarly)
                        {

                            reg = tim.CalculateWorkingHours("05:30:00", "14:30:00");
                        }
                        else
                        {

                            reg = tim.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "14:30:00");
                        }
                        oTHours = tim.CalculateOTHours("14:30:00", currentTime);
                    }
                    else if (timeInSpan >= lateStart && timeInSpan < cutoffLate)
                    {
                        if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("20:00:00"))
                        {

                            reg = tim.CalculateWorkingHours("07:30:00", "16:30:00");
                        }
                        else
                        {

                            reg = tim.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "16:30:00");
                        }
                        oTHours = tim.CalculateOTHours("16:30:00", currentTime);
                    }
                    else
                    {
                        reg = defaultDayLength.TotalHours;
                        oTHours = 0;
                    }

                    gTotal = reg + oTHours;



                    string updateQuery = " UPDATE " + tb + " SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal" +
                                         " WHERE CAST(Date_today AS DATE) = @Date_today AND Employee_ID = @Employee_ID";

                
                    var parameters = new
                    {
                        Employee_ID = empid,
                        Date_today = dtDate,
                        Regular = reg,
                        TimeOut = dtDate,
                        Gtotal = gTotal,
                        Overtime = oTHours
                    };

                    bool success = await SqlDataAccess.UpdateInsertQuery(updateQuery, parameters); 

                    if (success)
                    {
                        TimeAttendanceDisplay(selecttime.SelectedIndex);
                        TextName.Text = fullname;
                        // Code to execute after the delay
                        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                        Statustext.Text = "Successfully Time Out";
                        //Statustext.Text = string.Empty;
                        timer = new Timer();
                        timer.Interval = 1000; // 2000 milliseconds = 2 seconds
                        timer.Tick += TimerOut_Tick;
                        timer.Start();


                    }
                    else
                    {
                        MessageBox.Show("ERROR CHECK THE CODE");
                    }

                }

            }
            else
            {
                MessageBox.Show("Please time-in first before you time-out");
            }

        }
        public async void TimeoutNight(string empid, string fullname)
        {
            double reg, oTHours, gTotal;

            // Use CultureInfo("en-US") for consistent formatting
            CultureInfo culture = new CultureInfo("en-US");


            DateTime currentDate = DateTime.Now;
            DateTime previousDate = currentDate.AddDays(-1);

            string dtDate = previousDate.ToString("yyyy-MM-dd HH:mm:ss.ff", culture);

            // Check if the user is already timed in
            string timeincheck = "SELECT TOP 1 TimeIn FROM " + tb + " WHERE Employee_ID = '" + empid + "' AND CAST(Date_today AS DATE) = '" + dtDate + "' ";
            DataTable summary = new DataTable();

            Debug.WriteLine(timeincheck);
            summary = await SqlDataAccess.GetDataByDataTable(timeincheck);

            if (summary.Rows.Count > 0)
            {
                DataRow time = summary.Rows[0];
                //string fullname = summary["FullName"]?.ToString() ?? string.Empty;

                // Check if the user is already timed out
                string timecheckout = "SELECT TOP 1 TimeIn FROM " + tb + " WHERE Employee_ID = '" + empid + "' AND CAST(Date_today AS DATE) = '" + dtDate + "' AND TimeOut IS NOT NULL";

                bool result = await SqlDataAccess.Checkdata(timecheckout);

                if (result == true)
                {
                    MessageBox.Show("Already timed out in the attendance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DateTime timeIn = (DateTime)time["TimeIn"];

                    //string timeInStr = time["TimeIn"]?.ToString() ?? string.Empty;

                    string currentTime = DateTime.Now.ToString(@"HH\:mm\:ss", culture);

                    TimeSpan timeInSpan = timeIn.TimeOfDay;

                    TimeSpan earlyStart = TimeSpan.Parse("15:30:00", culture);
                    TimeSpan lateStart = TimeSpan.Parse("18:30:00", culture);
                    TimeSpan cutoffEarly = TimeSpan.Parse("18:00:00", culture);
                    TimeSpan cutoffLate = TimeSpan.Parse("22:00:00", culture);
                    TimeSpan dayEnd = TimeSpan.Parse("02:30:00", culture);
                    TimeSpan defaultDayLength = TimeSpan.FromHours(7.67);


                    // Current time >= 15:30  AND  Current time < 18:30
                    if (timeInSpan >= earlyStart && timeInSpan < lateStart)
                    {

                        if (timeInSpan >= earlyStart && timeInSpan < cutoffEarly)
                        {
                            reg = tim.CalculateWorkingHours("17:30:00", "02:30:00");
                        }
                        else
                        {
                            reg = tim.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "02:30:00");
                        }
                        oTHours = tim.CalculateOTHours("02:30:00", currentTime);
                    }
                    else if (timeInSpan >= lateStart && timeInSpan < cutoffLate)
                    {
                        if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("20:00:00"))
                        {
                            reg = tim.CalculateWorkingHours("19:30:00", "04:30:00");
                        }
                        else
                        {
                            reg = tim.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "04:30:00");
                        }
                        oTHours = tim.CalculateOTHours("04:30:00", currentTime);
                    }
                    else
                    {
                        reg = defaultDayLength.TotalHours;
                        oTHours = 0;
                    }

                    gTotal = reg + oTHours;



                    //MessageBox.Show("Regular hours : " +  reg + " OT hours : " + oTHours);
                    string updateQuery = " UPDATE " + tb + " SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal" +
                                         " WHERE CAST(Date_today AS DATE) = @Date_today AND Employee_ID = @Employee_ID";

                    var parameters = new
                    {
                        Employee_ID = empid,
                        Date_today = dtDate,
                        Regular = reg,
                        TimeOut = dtDate,
                        Gtotal = gTotal,
                        Overtime = oTHours
                    };
                    
                    bool success = await SqlDataAccess.UpdateInsertQuery(updateQuery, parameters);

                    if (success)
                    {
                        TimeAttendanceDisplay(selecttime.SelectedIndex);
                        TextName.Text = fullname;
                        // Code to execute after the delay
                        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                        Statustext.Text = "Successfully Time Out";
                        //Statustext.Text = string.Empty;
                        timer = new Timer();
                        timer.Interval = 1000; // 2000 milliseconds = 2 seconds
                        timer.Tick += TimerOut_Tick;
                        timer.Start();


                    }
                    else
                    {
                        MessageBox.Show("ERROR CHECK THE CODE");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please time-in first before you time-out");
            }

        }
    }
}
