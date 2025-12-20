using Attendance_Monitoring.Global;
using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.View.V2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class AttendancePage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAttendanceMonitor _monitor;
        private readonly IEmployee _emp;
        private static List<P1SA_AttendanceModel> itemattends;
        private static List<Employee> emplist;
        private readonly Timer timer;

        // Share variable to all
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public int DepartmentID { get; set; }

        string[] SectionName = { "Molding", "Press", "Rotor", "Winding", "Circuit" };

        public AttendancePage(IServiceProvider serviceProvider, IAttendanceMonitor monitor, IEmployee emp)
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            _monitor = monitor;
            _emp = emp;
            _serviceProvider = serviceProvider;
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
                    Shift = Timeprocess.TimeIncheckAsIntV2(DateTime.Now);
                    timecheck = tdate;
                }
                else
                {
                    Shift = Timeprocess.TimeoutcheckV2(DateTime.Now);
                    Timeout_date = Shift == 0 ? tdate : yest;
                    timecheck = Timeout_date;
                }

                var getdata = await _monitor.GetAttendanceRecordsList(timecheck, Shift, selectime, DepartmentID);

                itemattends = (getdata.Success && getdata.Payload != null) ? getdata.Payload.ToList() : new List<P1SA_AttendanceModel>();
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
                var employee = emplist.FirstOrDefault(p => p.Employee_ID.Equals(empid, StringComparison.OrdinalIgnoreCase) &&
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
                    var res = await _monitor.AttendanceTimeIn(empid, Timeprocess.CalculateLateTime());

                    if(res.Success)
                    {
                        TextName.Text = employee.Fullname;
                        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                        Statustext.Text = res.Message;
                        timer.Start();
                        await TimeAttendanceDisplay(selecttime.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show(res.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }  
                }
                // Time Out Process
                else if (selecttime.SelectedIndex == 1)
                {
                    var Outresult = await _monitor.AttendanceTimeOut(empid);
                    if (Outresult.Success)
                    {
                        TextName.Text = employee.Fullname;
                        // Code to execute after the delay
                        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                        Statustext.Text = Outresult.Message;
                        timer.Interval = 1000; // 2000 milliseconds = 2 seconds
                        timer.Tick += TimerOut_Tick;
                        timer.Start();
                        await TimeAttendanceDisplay(selecttime.SelectedIndex);
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
        private async void AttendancePage_Load(object sender, EventArgs e)
        {
            try
            {
                label7.Text  = "Daily Attendance : " + SectionName[DepartmentID + 1];

                selecttime.DropDownStyle = ComboBoxStyle.DropDownList;
                Timeclock.Text = DateTime.Now.ToLongTimeString();
                emplist = await _emp.GetEmployees();
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

        private void Summary_data_Click(object sender, EventArgs e)
        {
            SummaryV2 sm = new SummaryV2(DepartmentID, _monitor, _serviceProvider);
            sm.Show();
        }
    }
}
