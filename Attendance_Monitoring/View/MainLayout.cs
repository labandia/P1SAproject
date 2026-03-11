using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.Usercontrols;
using Attendance_Monitoring.View.V2;
using Microsoft.Office.Interop.Excel;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class MainLayout : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private AttendancePage _attend;
        private CRMonitoringPage _cr;
        private EmployeeManagement _emp;

        private readonly IEmployee _iemp;
        private readonly IAttendanceMonitor attendanceMonitor;
        private readonly ICRmonitorV2 _Crmonitor; 

        public int _DepartmentID;

        public MainLayout(int departID,
            IAttendanceMonitor attend,
            ICRmonitorV2 cr,
            IEmployee emp)
        {
            InitializeComponent();
            attendanceMonitor = attend;
            _Crmonitor = cr;
            _iemp = emp;
            _DepartmentID = departID;
 
        }

        private void MainLayout_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;

            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            //Attendance.BackColor = Color.FromArgb(54, 97, 235);
            //Attendance.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            //CRMonitor.BackColor = Color.Transparent;
            //CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            //EmployeeMenu.BackColor = Color.Transparent;
            //EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            SetAttendance();   
        }
        private  void Attendance_Click_1(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            Attendance.BackColor = Color.FromArgb(54, 97, 235);
            Attendance.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            CRMonitor.BackColor = Color.Transparent;
            CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            EmployeeMenu.BackColor = Color.Transparent;
            EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            SetAttendance();
        }
        private async void CRMonitor_Click(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            CRMonitor.BackColor = Color.FromArgb(54, 97, 235);
            CRMonitor.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            Attendance.BackColor = Color.Transparent;
            Attendance.ForeColor = Color.FromArgb(170, 176, 192);
            EmployeeMenu.BackColor = Color.Transparent;
            EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            await LoadCRList();

        }

        private async void EmployeeMenu_Click(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            EmployeeMenu.BackColor = Color.FromArgb(54, 97, 235);
            EmployeeMenu.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            Attendance.BackColor = Color.Transparent;
            Attendance.ForeColor = Color.FromArgb(170, 176, 192);
            CRMonitor.BackColor = Color.Transparent;
            CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            await LoadEmployeeList();

        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            AttendanceMain n = new AttendanceMain(_iemp, attendanceMonitor, _Crmonitor);
            n.Show();
            this.Visible = false;
        }

        private void sidepanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private async Task LoadEmployeeList()
        {
            if (_emp == null)
            {
                _emp = new EmployeeManagement(_iemp, _serviceProvider);
                _emp.Dock = DockStyle.Fill; 
                panel1.Controls.Add(_emp);
            }
            _emp.DepartID = _DepartmentID;
            _emp.BringToFront();
            await _emp.Displayemployee("", _DepartmentID);
        }

        private async Task LoadCRList()
        {
            if (_cr == null)
            {
                _cr = new CRMonitoringPage(_Crmonitor, _serviceProvider);
                _cr.Dock = DockStyle.Fill;
                panel1.Controls.Add(_cr);
            }

            _cr.BringToFront();
            _cr.DepartID = _DepartmentID;
            await _cr.DisplayCRMonitor("", _DepartmentID);
        }

        private void SetAttendance()
        {
            if (_attend == null)
            {
                _attend = new AttendancePage(attendanceMonitor, _iemp, _DepartmentID);
                _attend.Dock = DockStyle.Fill;
                panel1.Controls.Add(_attend);
            }

            _attend.BringToFront();
            _attend.DepartmentID = _DepartmentID;
        }


    }
}
