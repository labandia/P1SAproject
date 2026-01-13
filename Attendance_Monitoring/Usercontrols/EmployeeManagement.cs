using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.View.V2;
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

        // PURPOSE IS TO REFRESH THE TABLE WHEN DONE ADDING AND UPDATING
        public async Task Displayemployee(int depid)
        {
            var items = await _emp.GetEmployees("", depid);
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
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            AddProduction form2 = new AddProduction(this, _emp);
            form2.Show();
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.ToLower();
            var filteredList = (filterText != "")  
                        ? emplist.Where(p => p.Employee_ID.ToLower().Contains(filterText) ||
                           p.Fullname.ToLower().Contains(filterText))
                           .ToList() 
                        : emplist.ToList();

            Employeetable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
        }

        private void EmployeeManagement_Load(object sender, EventArgs e)
        {

        }

       


    }
}
