using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using System;
using System.Collections.Generic;
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
            IEnumerable<Department> items = await _admin.GetDepartments();

            // Convert the items to a list and add a default item
            List<Department> itemList = items.ToList();
            itemList.Insert(0, new Department { Department_ID = 0, Department_name = "All Section" });

            selectsection.DataSource = itemList;
            selectsection.DisplayMember = "Department_name";
            selectsection.ValueMember = "Department_ID";

            // Optionally set the default selected index to the first item
            selectsection.SelectedIndex = 0;
        }
        public bool ValidateData()
        {
            int selectedIndex = selectsection.SelectedIndex;
           
            if (string.IsNullOrEmpty(EmpID.Text) || string.IsNullOrEmpty(Fullname.Text) || string.IsNullOrEmpty(selectsection.Text))
            {
                if (string.IsNullOrEmpty(EmpID.Text))
                {
                    Emp_error.Visible = true;
                }
                else
                {
                    Emp_error.Visible = false;
                }

                if (string.IsNullOrEmpty(Fullname.Text))
                {
                    Name_error.Visible = true;
                }
                else
                {
                    Name_error.Visible = false;
                }
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
            if (ValidateData())
            {
                Employee emp = new Employee
                {
                    EmployeeID = EmpID.Text.Replace("-", ""),
                    Fullname = Fullname.Text,
                    Process = process.Text,
                    Affiliation = Affili.Text,
                    Department_ID = selectsection.SelectedIndex
                };

                bool success =  await _admin.AddEmployee(emp);
               
                if (success)
                {
                    Clear();
                    _emp.Displayemployee();
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

        private void Cancebtn_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

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
    }
}
