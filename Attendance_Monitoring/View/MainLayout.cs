using Attendance_Monitoring.Usercontrols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class MainLayout : Form
    {
        private readonly AttendanceSelection _attend;
        private readonly CRSelection _cr;


        public MainLayout(AttendanceSelection attend, CRSelection cr)
        {
            InitializeComponent();
            _attend = attend;
            _cr = cr;

            _attend.Dock = DockStyle.Fill;
            _cr.Dock = DockStyle.Fill;
       
            Controls.Add(_attend);
            Controls.Add(_cr);
        }

        private void MainLayout_Load(object sender, EventArgs e)
        {
            _attend.BringToFront();
        }

        private void AttendanceMenu_Click(object sender, EventArgs e)
        {
            _attend.BringToFront();
        }

        private void CRmonitorMenu_Click(object sender, EventArgs e)
        {
            _cr.BringToFront();
        }
    }
}
