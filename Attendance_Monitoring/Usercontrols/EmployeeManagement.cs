using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.View;
using Attendance_Monitoring.View.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class EmployeeManagement : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmployee _emp;
        private static IEnumerable<Employee> emplist;

        private readonly EmployeeManagement empform;

        public int DepartID { get; set; }
        public EmployeeManagement(IEmployee emp, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _emp = emp;
        }

        // PURPOSE IS TO REFRESH THE TABLE WHEN DONE ADDING AND UPDATING
        public async Task Displayemployee(int depid)
        {
            var items = await _emp.GetEmployees("", depid, "");
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
            //var items = await _admin.GetAllEmployees();
            //emplist = items.ToList();
            //Employeetable.AutoGenerateColumns = false;

            //var filteredList = emplist.Where(p => p.Department_ID == DepartID).ToList();
            //Employeetable.DataSource =  filteredList;
            //DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
            //MessageBox.Show("Running after set: " + DepartID);
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            AddProduction form2 = new AddProduction(this, _emp);
            form2.Show();
        }

        private async void searchbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.ToLower();
            var filteredList = await _emp.GetEmployees("", DepartID, filterText);

            Employeetable.DataSource =  filteredList;
            DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
        }

        private void EmployeeManagement_Load(object sender, EventArgs e)
        {

        }

        private async void Employeetable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                string EmployeeID = Employeetable.Rows[e.RowIndex].Cells[0].Value.ToString();

                // ====================== EDIT EMPLOYEES ================================== //
                if (e.ColumnIndex == 4)
                {
                    //EditEmployee ed = new EditEmployee(this, _emp);
                    //ed.EmpID.Text = EmployeeID;
                    //ed.Fullname.Text = Employeetable.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //ed.Process.Text = Employeetable.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //ed.TempID.Text = EmployeeID;
                    //ed.Afili.Text = Employeetable.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //ed.comboBox1.SelectedIndex = Convert.ToInt32(Employeetable.Rows[e.RowIndex].Cells[6].Value.ToString());
                    //ed.ShowDialog();
                }
                // ===================== DELETE EMPLOYEES ================================= //
                else if (e.ColumnIndex == 5)
                {
                    DialogResult exit = MessageBox.Show("Are you  sure you want to delete this Employee ID", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (exit == DialogResult.Yes)
                    {
                        bool result = await _emp.DeleteEmployee(EmployeeID);

                        if (result)
                        {
                            MessageBox.Show($@"Employee ID: ${EmployeeID} is Already Deleted!!");
                            await Displayemployee(DepartID);
                        }
                    }

                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
