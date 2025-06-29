using Attendance_Monitoring.Usercontrols;
using System;
using System.Windows.Forms;

namespace Attendance_Monitoring.View.V2
{
    public partial class AttendanceMain : Form
    {
        private readonly AttendancePage _attend;
        private readonly CRMonitoringPage _cr;
        private readonly EmployeeManagement _emp;

        public AttendanceMain(AttendancePage attend, CRMonitoringPage cr, EmployeeManagement emp)
        {
            InitializeComponent();
            _attend = attend;
            _cr = cr;
            _emp = emp;
        }

        // ATTENDANCE BASED ON THE DEPARTMENTS
        private void Moldingbtn_Click(object sender, EventArgs e) => EnterPageAttendance(1);
        private void Pressbtn_Click(object sender, EventArgs e) => EnterPageAttendance(2);
        private void Rotorbtn_Click(object sender, EventArgs e) => EnterPageAttendance(3);
        private void Windingbtn_Click(object sender, EventArgs e) => EnterPageAttendance(4);
        private void Circuitbtn_Click(object sender, EventArgs e) => EnterPageAttendance(5);
        private void Processbtn_Click(object sender, EventArgs e) => EnterPageAttendance(6);

        // CLOSE THE APPLICATION
        private void ExitButton(object sender, EventArgs e) => Application.Exit();


        public void EnterPageAttendance(int id)
        {
            MainLayout page = new MainLayout(id, _attend, _cr, _emp);
            page.Show();
            Visible = false;
        }

      
    }
}
