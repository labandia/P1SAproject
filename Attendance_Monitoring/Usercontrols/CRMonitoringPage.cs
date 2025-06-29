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

        public int DepartmentID { get; set; }

        public CRMonitoringPage()
        {
            InitializeComponent();
        }

        private void CRMonitoringPage_Load(object sender, EventArgs e)
        {

        }


        public void InitializePage()
        {
            MessageBox.Show("Running after set: " + DepartmentID);
            // Now you can load employees, etc.
        }
    }
}
