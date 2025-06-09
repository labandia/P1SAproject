
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class AttendanceSelection : UserControl
    {
        private readonly EmployeeManagement _emp;

        public AttendanceSelection(EmployeeManagement emp)
        {
            InitializeComponent();
            _emp=emp;

            _emp.Dock = DockStyle.Fill;

            Controls.Add(_emp);
        }

        private void Employ_Click(object sender, System.EventArgs e)
        {
            _emp.BringToFront();
        }
    }
}
