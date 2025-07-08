using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class CRMonitoringPage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminController _admin;
        private static List<CRmodel> critemlist;
        private static List<Employee> emplist;
        private readonly Timer timer;


        public int DepartmentID { get; set; }

        public CRMonitoringPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _admin = new AdminController();
            timer = new Timer();
            _serviceProvider=serviceProvider;
        }

        private async void CRMonitoringPage_Load(object sender, EventArgs e)
        {
            emplist = await _admin.GetAllEmployees();
            EmployID.Focus();
        }


        public void InitializePage()
        {
            MessageBox.Show("Running after set: " + DepartmentID);
            // Now you can load employees, etc.
        }

        public async void DisplayCRMonitor()
        {
            var dateToday = DateTime.Now.ToString("yyyy-MM-dd");
            string shift = Timeprocess.TimeIncheck(DateTime.Now);

            critemlist = await _admin.GetCRMonitorlist(dateToday, shift, DepartmentID);

            CRtable.DataSource = critemlist;
            DisplayTotal.Text = "Total Attendence: " + CRtable.RowCount;
        }


        private async void EnterEmployee(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string empid = EmployID.Text.Replace("-", "").Trim();
            string shift = Timeprocess.TimeIncheck(DateTime.Now);

            // Filter employee once
            var employee = emplist.FirstOrDefault(p => p.EmployeeID.Equals(empid, StringComparison.OrdinalIgnoreCase) &&
                                                    p.Department_ID == DepartmentID);

            if (employee == null)
            {
                MessageBox.Show("Wrong ID number");
                EmployID.Focus();
                return;
            }

            var CRoutcheck = critemlist.FirstOrDefault(i =>
                    i.Employee_ID.Equals(empid, StringComparison.OrdinalIgnoreCase) &&
                    i.TimeOut == null && i.Date_today.Date == DateTime.Today);


            if (CRoutcheck != null)
            {
                DateTime now = DateTime.Now;
                TimeSpan Duration = TimeSpan.Zero;

                // Ensure TimeIn is not null or empty before parsing
                if (!string.IsNullOrEmpty(CRoutcheck.TimeIn) && DateTime.TryParse(CRoutcheck.TimeIn, out DateTime timeIn))
                {
                    Duration = now > timeIn ? (now - timeIn) : TimeSpan.Zero;
                }
                else
                {
                    MessageBox.Show("Invalid TimeIn format.");
                    return; // Exit if TimeIn is invalid
                }
                var Timeout = DateTime.Now;
                var duration = Duration.ToString(@"hh\:mm");
                var dateToday = DateTime.Now.ToString("yyyy-MM-dd");

                bool updateresult = await _admin.CRMonitoringOut(empid, Timeout, duration, dateToday);

                if (updateresult)
                {
                    TextName.Text = employee.Fullname;
                    if (timer == null)
                    {
                        timer.Interval = 1000;
                        timer.Tick += Timer_Tick;
                    }

                    timer.Start();

                    DisplayCRMonitor();
                }




                // Update process
                //MessageBox.Show("EmployeeID: " + empid);
                //MessageBox.Show("Time Out: " + DateTime.Now);
                //MessageBox.Show("Duration: " + Duration.ToString(@"hh\:mm"));
                //MessageBox.Show("UPDATE PROCESSING: " + DateTime.Now.ToString("yyyy-MM-dd"));

            }
            else
            {
                // Perform GOING TIME
                bool result = await _admin.CRMonitoringIN(empid, shift);

                if (result)
                {
                    TextName.Text = employee.Fullname;
                    //Statustext.BackColor = Color.FromArgb(50, 181, 111);
                    //Statustext.Text = "Successfully Going Out";

                    // Reuse Timer instead of creating a new one every time
                    if (timer == null)
                    {
                        timer.Interval = 1000;
                        timer.Tick += Timer_Tick;
                    }

                    timer.Start();
                    DisplayCRMonitor();
                }

            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            EmployID.Text = "";
        }

        private void SearchInput(object sender, EventArgs e)
        {
            string filterText = textBox2.Text.ToLower();
            // Filter the list using LINQ
            var filteredList = critemlist.Where(p => p.Employee_ID.ToLower().Contains(filterText) ||
                            p.Fullname.ToLower().Contains(filterText))
                            .ToList();

            CRtable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + CRtable.RowCount;
        }
    }
}
