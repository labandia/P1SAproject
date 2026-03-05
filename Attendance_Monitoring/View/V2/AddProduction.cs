using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.Usercontrols;

namespace Attendance_Monitoring.View.V2
{
    public partial class AddProduction : Form
    {
        private readonly EmployeeManagement _employ;
        private readonly IEmployee _emp;


        public int DepID { get; set; }

        public AddProduction(EmployeeManagement employ, IEmployee emp, int depID)
        {
            InitializeComponent();
            _employ = employ;
            _emp = emp;
            DepID = depID;
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            if (!FormValidation()) return;

            try
            {
                 await _emp.AddEmployee(new Employee
                {
                    Employee_ID = EmpID.Text.Trim(),
                    Affiliation = Affili.Text,
                    Fullname = Fullname.Text,
                    Process = process.Text,
                    Department_ID = DepID
                 });
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error Encounter : " + ex.Message);
            }
        }



        //===================== FOR FORM VALIDATION ======================
        public bool FormValidation()
        {
            bool isEmpIDEmpty = string.IsNullOrWhiteSpace(EmpID.Text);
            bool isFullnameEmpty = string.IsNullOrWhiteSpace(Fullname.Text);

            Emp_error.Visible = isEmpIDEmpty;
            Name_error.Visible = isFullnameEmpty;

            return !(isEmpIDEmpty || isFullnameEmpty);
        }

       

    }
}
