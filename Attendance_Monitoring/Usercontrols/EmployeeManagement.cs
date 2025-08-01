using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class EmployeeManagement : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmployee _emp;
        private static IEnumerable<Employee> emplist;
        private readonly AdminController _admin;

        public int DepartID { get; set; }
        public EmployeeManagement(IEmployee emp, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _admin = new AdminController();
        }

        public async Task Displayemployee(int depid)
        {
            var items = await _emp.GetEmployees();
            emplist = items.ToList();
            Employeetable.AutoGenerateColumns = false;
            //Employeetable.DataSource =  emplist;
            if (depid != 0)
            {
                var filteredList = emplist.Where(p => p.Department_ID == depid).ToList();
                Employeetable.DataSource =  filteredList;
            }
            else
            {
                Employeetable.DataSource =  emplist;
            }

            DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
        }


        public async Task InitializePage()
        {
            var items = await _admin.GetAllEmployees();
            emplist = items.ToList();
            Employeetable.AutoGenerateColumns = false;

            var filteredList = emplist.Where(p => p.Department_ID == DepartID).ToList();
            Employeetable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
            //MessageBox.Show("Running after set: " + DepartID);
            // Now you can load employees, etc.
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            EmployeeManage empv = new EmployeeManage(_emp, _serviceProvider);
            Add_Employee form2 = new Add_Employee(empv, _emp);
            form2.Show();
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.ToLower();
            var filteredList = new List<Employee>();


            if (filterText != "")
            {
                filteredList = emplist.Where(p => p.EmployeeID.ToLower().Contains(filterText) ||
                           p.Fullname.ToLower().Contains(filterText))
                           .ToList();
            }
            else
            {
                filteredList = emplist.ToList();
            }


            Employeetable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
        }
    }
}
