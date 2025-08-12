using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class AttendancePage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAttendanceV2 _attend;
        private readonly AdminController _admin;
        private static List<AttendanceModel> itemattends;
        private static List<Employee> emplist;


        private readonly Timer timer;

        // Share variable to all
        //public int sec;
        public string tb = "R_summary";
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


        public int DepartmentID { get; set; }

        public AttendancePage(IServiceProvider serviceProvider, IAttendanceV2 attend)
        {
            InitializeComponent();
            //sec = section;
            //tb = tablename;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            _admin = new AdminController();
            _attend = attend;
            _serviceProvider=serviceProvider;
        }

        //  ####################  DISPLAY  THE TIME IN AND OUT  #################### //
        public async Task TimeAttendanceDisplay(int selectime)
        {
            try
            {
                int Shift;
                string timecheck;
                string Timeout_date;


                // TIME OUT NIGHT SHIFT DATE TIME
                DateTime yesterday = DateTime.Today.AddDays(-1);
                // Change format of the date and time
                string yest = yesterday.ToString("yyyy-MM-dd HH:mm:ss.fff");

                if (selectime == 0)
                {
                    Shift = Timeprocess.TimeIncheckAsInt(DateTime.Now);
                    timecheck = tdate;
                }
                else
                {
                    Shift = Timeprocess.TimeIncheckAsInt(DateTime.Now);
                    Timeout_date = Shift == 1 ? tdate : yest;
                    timecheck = Timeout_date;
                }

                itemattends = await _attend.GetAttendanceRecordsList(timecheck, Shift, selectime, DepartmentID);
                //DataTable dt = await connect.GetData(query);
                attendancetable.DataSource = itemattends;
                DisplayTotal.Text = "Total Attendence: " + attendancetable.RowCount;
                EmployID.Focus();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error found at Retreiving Employee Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void Selecttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            await TimeAttendanceDisplay(selecttime.SelectedIndex);
        }



        //  ####################  PERFORMS THE TIME IN AND OUT  #################### //
        private async void EnterTime(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter) return;

                // Process Employee ID
                string empid = EmployID.Text.Replace("-", "").Trim();
                string shift = Timeprocess.TimeIncheck(DateTime.Now);

                // Filter employee once
                var employee = emplist.FirstOrDefault(p => p.EmployeeID.Equals(empid, StringComparison.OrdinalIgnoreCase) &&
                                                      p.Department_ID == DepartmentID);

                if (employee == null)
                {
                    Statustext.BackColor = Color.FromArgb(198, 17, 17);
                    Statustext.Text = "Wrong ID number ";

                    // Reuse Timer instead of creating a new one every time
                    timer.Start();

                    EmployID.Focus();
                    return;
                }

                // Time In Process
                if (selecttime.SelectedIndex == 0)
                {
                    // Check if employee has already timed in
                    bool alreadyTimedIn = await _admin.CheckAttendanceTimeIN(empid, shift, tb);
                    if (alreadyTimedIn)
                    {
                        MessageBox.Show("YOU ALREADY TIME IN", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Perform Time In
                    bool result = await _admin.AttendanceTimeINandOut(empid, shift, Timeprocess.CalculateLateTime(), tb);

                    if (result)
                    {
                        TextName.Text = employee.Fullname;
                        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                        Statustext.Text = "Successfully Time In";


                        timer.Start();
                        await TimeAttendanceDisplay(selecttime.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("YOU ALREADY TIME IN", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                // Time Out Process
                else if (selecttime.SelectedIndex == 1)
                {
                    string shiftname = Timeprocess.Timeoutcheck(DateTime.Now);

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
            catch (FormatException)
            {
                MessageBox.Show("Error Encounter During Time IN / OUT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void TimeoutDay(string empid, string fullname)
        {
            try
            {
                double reg, oTHours, gTotal;
                // Use CultureInfo("en-US") for consistent formatting
                CultureInfo culture = new CultureInfo("en-US");

                string dtDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff", culture);
                // Check if the user is already timed in
                string timeincheck = $@"SELECT TOP 1 TimeIn 
                                        FROM {tb} 
                                        WHERE Employee_ID = '{empid}' 
                                        AND CAST(Date_today AS DATE) = '{dtDate}'";
                DataTable summary = new DataTable();

                summary = await SqlDataAccess.GetDataByDataTable(timeincheck);

                if (summary.Rows.Count > 0)
                {
                    DataRow time = summary.Rows[0];
                    //string fullname = summary["FullName"]?.ToString() ?? string.Empty;

                    // Check if the user is already timed In
                    string timecheckout = "SELECT COUNT(1)  FROM " + tb + " WHERE Employee_ID = '" + empid + "' AND CAST(Date_today AS DATE) = '" + dtDate + "' AND TimeOut IS NOT NULL";

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
                                reg = Timeprocess.CalculateWorkingHours("05:30:00", "14:30:00");
                            }
                            else
                            {
                                reg = Timeprocess.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "14:30:00");
                            }
                            oTHours = Timeprocess.CalculateOTHours("14:30:00", currentTime);
                        }
                        else if (timeInSpan >= lateStart && timeInSpan < cutoffLate)
                        {
                            if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("20:00:00"))
                            {
                                reg = Timeprocess.CalculateWorkingHours("07:30:00", "16:30:00");
                            }
                            else
                            {
                                reg = Timeprocess.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "16:30:00");
                            }
                            oTHours = Timeprocess.CalculateOTHours("16:30:00", currentTime);
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
                            await TimeAttendanceDisplay(selecttime.SelectedIndex);
                            TextName.Text = fullname;
                            // Code to execute after the delay
                            Statustext.BackColor = Color.FromArgb(50, 181, 111);
                            Statustext.Text = "Successfully Time Out";
                            //Statustext.Text = string.Empty;
                            timer.Interval = 1000; // 2000 milliseconds = 2 seconds
                            timer.Tick += TimerOut_Tick;
                            timer.Start();

                        }

                    }

                }
                else
                {
                    MessageBox.Show("Please time-in first before you time-out");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Error Encounter During Time OUT Dayshift.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void TimeoutNight(string empid, string fullname)
        {
            try
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

                summary = await SqlDataAccess.GetDataByDataTable(timeincheck);

                if (summary.Rows.Count > 0)
                {
                    DataRow time = summary.Rows[0];
                    //string fullname = summary["FullName"]?.ToString() ?? string.Empty;

                    // Check if the user is already timed out
                    string timecheckout = "SELECT COUNT(1) FROM " + tb + " WHERE Employee_ID = '" + empid + "' AND CAST(Date_today AS DATE) = '" + dtDate + "' AND TimeOut IS NOT NULL";
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
                                reg = Timeprocess.CalculateWorkingHours("17:30:00", "02:30:00");
                            }
                            else
                            {
                                reg = Timeprocess.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "02:30:00");
                            }
                            oTHours = Timeprocess.CalculateOTHours("02:30:00", currentTime);
                        }
                        else if (timeInSpan >= lateStart && timeInSpan < cutoffLate)
                        {
                            if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("20:00:00"))
                            {
                                reg = Timeprocess.CalculateWorkingHours("19:30:00", "04:30:00");
                            }
                            else
                            {
                                reg = Timeprocess.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "04:30:00");
                            }
                            oTHours = Timeprocess.CalculateOTHours("04:30:00", currentTime);
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
                            await TimeAttendanceDisplay(selecttime.SelectedIndex);
                            TextName.Text = fullname;
                            // Code to execute after the delay
                            Statustext.BackColor = Color.FromArgb(50, 181, 111);
                            Statustext.Text = "Successfully Time Out";
                            //Statustext.Text = string.Empty;
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
            catch (FormatException)
            {
                MessageBox.Show("Error Encounter During Time OUT NIGHTSHIFT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void InitializePage()
        {
            //MessageBox.Show("Running after set: " + DepartmentID);
            // Now you can load employees, etc.
        }

        private async void AttendancePage_Load(object sender, EventArgs e)
        {
            try
            {
                selecttime.DropDownStyle = ComboBoxStyle.DropDownList;
                Timeclock.Text = DateTime.Now.ToLongTimeString();
                emplist = await _admin.GetAllEmployees();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error from retrieve Employee Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Attendancetable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (attendancetable.Columns[e.ColumnIndex].Name == "LateTime") // Change "Status" to your column name
            {
                if (e.Value != null)
                {
                    string cellValue = e.Value.ToString();

                    if (cellValue == "00:00")
                    {
                        e.Value = '-';
                    }  
                    else 
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }

        private async void Searchinput(object sender, EventArgs e)
        {
            string filterText = textBox2.Text.ToLower();

            // If the input Text is String only returns to Default Data
            if (String.IsNullOrEmpty(filterText))
            {
                await TimeAttendanceDisplay(selecttime.SelectedIndex);
                textBox2.Focus();
                return;
            }
            // Search by Filter
            var filteredList = itemattends.Where(p => p.Employee_ID.ToLower().Contains(filterText) ||
                            p.Fullname.ToLower().Contains(filterText))
                            .ToList();

            attendancetable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + attendancetable.RowCount;
        }


       

       
        
    }
}
