using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class Add_Employee : Form
    {
        private readonly EmployeeManage _emp;
        private readonly IEmployee _admin;
      
        public string samplestr {  get; set; }
        public Add_Employee(EmployeeManage emp, IEmployee admin)
        {
            InitializeComponent();
            _emp=emp;
            _admin = admin;
        }

        private async void PopulateComboBox()
        {
            try
            {
                // 1. Get all employees
                var items = await _admin.GetEmployees();

                // 2. Group by Department_ID and project into Department objects
                var itemlist = items
                    .GroupBy(emp => emp.Department_ID)   // group by ID to remove duplicates
                    .Select(g => new Department
                    {
                        Department_ID = g.Key,
                        Department_name = GetDepartmentName(g.Key)
                    })
                    .ToList();

             


                // 3. Add "All Section" at the top
                itemlist.Insert(0, new Department { Department_ID = 0, Department_name = "All Section" });

                // 4. Bind to ComboBox
                selectsection.DataSource = itemlist;
                selectsection.DisplayMember = "Department_name";
                selectsection.ValueMember = "Department_ID";
                selectsection.SelectedIndex = 0;
            }
            catch (FormatException)
            {
                MessageBox.Show("Error found at the Combobox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool ValidateData()
        {
            int selectedIndex = selectsection.SelectedIndex;
           
            // If all the Input is Empty
            if (string.IsNullOrEmpty(EmpID.Text) || string.IsNullOrEmpty(Fullname.Text) || string.IsNullOrEmpty(selectsection.Text))
            {
                Emp_error.Visible = string.IsNullOrEmpty(EmpID.Text) ? true : false;
                Name_error.Visible = string.IsNullOrEmpty(EmpID.Text) ? true : false;
                label9.Visible = true;
                return false;
            }
            else
            {
                if (selectedIndex == 0)
                {
                    label9.Visible = true;
                    return false;
                }
                else
                {
                    // EVERYTHING IS SET TO INSERT
                    Emp_error.Visible = false;
                    Name_error.Visible = false;
                    label9.Visible = false;
                    return true;
                }

            }

        
        }

      
        private  async void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData())
                {
                    var emp = new Employee
                    {
                        Employee_ID = EmpID.Text.Replace("-", "").Trim(),
                        Fullname = string.IsNullOrEmpty(Fullname.Text) ? "" : Fullname.Text,
                        Process = string.IsNullOrEmpty(process.Text) ? "" : process.Text,
                        Affiliation = string.IsNullOrEmpty(Affili.Text) ? "" : Affili.Text,
                        Department_ID = selectsection.SelectedIndex 
                    };

                    if (await _admin.AddEmployee(emp))
                    {
                        Clear();
                        await _emp.Displayemployee(selectsection.SelectedIndex);
                        MessageBox.Show("Add employee Successfully");
                        Visible = false;
                    }
                    else
                    {
                        MessageBox.Show($"Employee ID : {EmpID.Text} is already in the database");
                        EmpID.Focus();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Error found at inserting new Employee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancebtn_Click(object sender, EventArgs e) => Visible = false;
        private void Add_Employee_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
        }
        

        public void Clear()
        {
            EmpID.Text = "";
            Fullname.Text = "";
            Affili.Text = "";
            process.Text = "";
        }

        private string GetDepartmentName(int id)
        {
            switch (id)
            {
                case 1: return "Molding";
                case 2: return "Press";
                case 3: return "Rotor";
                case 4: return "Winding";
                case 5: return "Circuit";
                case 6: return "Process Control";
                default: return "";
            }
        }

    }
}
