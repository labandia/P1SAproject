using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class EmployeeManagement : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminController _admin;
        private static List<Employee> emplist;

        public int DepartID { get; set; }
        public EmployeeManagement()
        {
            InitializeComponent();
        }


        public void InitializePage()
        {
            var emp = _admin.GetAllEmployees(); 

            MessageBox.Show("Running after set: " + DepartID);
            // Now you can load employees, etc.
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {

        }
    }
}
