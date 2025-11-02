using Attendance_Monitoring.Usercontrols;
using Attendance_Monitoring.View.V2;
using Microsoft.Office.Interop.Excel;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
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
            Attendance.BackColor = Color.FromArgb(54, 97, 235);
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
            Attendance.BackColor = Color.FromArgb(54, 97, 235);
            Attendance.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            CRMonitor.BackColor = Color.Transparent;
            CRMonitor.ForeColor = Color.FromArgb(170, 176, 192);
            EmployeeMenu.BackColor = Color.Transparent;
            EmployeeMenu.ForeColor = Color.FromArgb(170, 176, 192);
            _attend.DepartmentID = _DepartmentID;
            //_attend.InitializePage();
            _attend.BringToFront();
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
            _cr.DepartmentID = _DepartmentID;
            _cr.InitializePage();

            _cr.BringToFront();
            await _cr.DisplayCRMonitor();
            var crlist = _cr.critemlist;
            _cr.Crgrid.DataSource = crlist;
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
            _emp.DepartID = _DepartmentID;
            await _emp.InitializePage();
            _emp.BringToFront();
        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            AttendanceMain n = new AttendanceMain(_attend, _cr, _emp);
            n.Show();
            this.Visible = false;
        }

        private void sidepanel_Paint(object sender, PaintEventArgs e)
        {

        }


        //public void CheckAccessMenu()
        //{
        //    try
        //    {
        //        string hostName = Dns.GetHostName();
        //        string ipAddress = Dns.GetHostAddresses(hostName)
        //                              .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
        //        .ToString();


        //        var emp = await _admin.GetCRaccess();

        //        var employee = emp.FirstOrDefault(p => p.IPaddress.Equals(ipAddress, StringComparison.OrdinalIgnoreCase));

        //        if (employee != null)
        //        {
        //            if (employee.Active == 1)
        //            {
        //                Attendance.Enabled = true;
        //            }
        //            else
        //            {
        //                Attendance.Enabled = false;
        //            }

        //            if (employee.CRactive == 1)
        //            {
        //                CRMonitor.Enabled = true;
        //            }
        //            else
        //            {
        //                CRMonitor.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            CRMonitor.Enabled = false;
        //        }
        //    }
        //    catch (FormatException)
        //    {
        //        MessageBox.Show("Can Detect local ip address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }
}
