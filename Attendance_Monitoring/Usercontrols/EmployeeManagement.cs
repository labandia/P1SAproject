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

        public int DepartID { get; set; }

        public EmployeeManagement(IEmployee emp, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _emp = emp;
        }

        public async Task Displayemployee(string empcode, int depid)
        {
            try
            {
                string searchText = string.IsNullOrEmpty(searchbox.Text) ? "" : searchbox.Text?.Trim();
                DepartID = depid;

                var items = await _emp.GetEmployees(empcode, depid, searchText);
                Employeetable.AutoGenerateColumns = false;

                Employeetable.DataSource = items.ToList() ?? new List<Employee>();
                DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees:\n{ex.Message}",
                          "System Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);

                Employeetable.DataSource = new List<Employee>();
                DisplayTotal.Text = "Total Records: 0";
            }
        }


        private async void Addbtn_Click(object sender, EventArgs e)
        { 
            using(var addEmp = new AddProduction(this, _emp, DepartID))
            {
                if (addEmp.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Add Employee successful.");
                    await Displayemployee("", DepartID);
                }
            }
        }

        private async void searchbox_TextChanged(object sender, EventArgs e)
        {
            await Displayemployee("", DepartID);
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
                            //await Displayemployee(DepartID);
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
