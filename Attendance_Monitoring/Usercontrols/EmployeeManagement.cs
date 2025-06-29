using System;
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class EmployeeManagement : UserControl
    {
        public int DepartID { get; set; }
        public EmployeeManagement()
        {
            InitializeComponent();
        }


        public void InitializePage()
        {
            MessageBox.Show("Running after set: " + DepartID);
            // Now you can load employees, etc.
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {

        }
    }
}
