using Attendance_Monitoring.Usercontrols;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class MainLayout : Form
    {
        private readonly AttendancePage _attend;
        private readonly CRMonitoringPage _cr;
        private readonly EmployeeManagement _emp;

        private readonly int _DepartmentID;

        public MainLayout(int departID, AttendancePage attend, CRMonitoringPage cr, EmployeeManagement emp)
        {
            InitializeComponent();
            _attend = attend;
            _cr = cr;
            _emp = emp;
            _DepartmentID = departID;

            _attend.Dock = DockStyle.Fill;
            _cr.Dock = DockStyle.Fill;
            _emp.Dock = DockStyle.Fill;
       
            Controls.Add(_attend);
            Controls.Add(_cr);
            Controls.Add(_emp);
        }

        private void MainLayout_Load(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            Attendance.BackColor = Color.FromArgb(95, 34, 200);
            Attendance.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            CRMonitor.BackColor = Color.Transparent;
            CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            EmployeeMenu.BackColor = Color.Transparent;
            EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            _attend.DepartmentID = _DepartmentID;
            _attend.InitializePage();  
            _attend.BringToFront();
        }
        private void Attendance_Click_1(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            Attendance.BackColor = Color.FromArgb(95, 34, 200);
            Attendance.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            CRMonitor.BackColor = Color.Transparent;
            CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            EmployeeMenu.BackColor = Color.Transparent;
            EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            _attend.DepartmentID = _DepartmentID;
            _attend.InitializePage(); 
            _attend.BringToFront();
        }
        private void CRMonitor_Click(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            CRMonitor.BackColor = Color.FromArgb(95, 34, 200);
            CRMonitor.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            Attendance.BackColor = Color.Transparent;
            Attendance.ForeColor = Color.FromArgb(170, 176, 192);
            EmployeeMenu.BackColor = Color.Transparent;
            EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            _cr.DepartmentID = _DepartmentID;
            _cr.InitializePage();
            _cr.BringToFront();
        }

        private void EmployeeMenu_Click(object sender, EventArgs e)
        {
            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            EmployeeMenu.BackColor = Color.FromArgb(95, 34, 200);
            EmployeeMenu.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            Attendance.BackColor = Color.Transparent;
            Attendance.ForeColor = Color.FromArgb(170, 176, 192);
            CRMonitor.BackColor = Color.Transparent;
            CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            _emp.DepartID = _DepartmentID;
            _emp.InitializePage();
            _emp.BringToFront();
        }
    }
}
