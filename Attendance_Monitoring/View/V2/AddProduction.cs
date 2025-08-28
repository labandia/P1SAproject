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


        public AddProduction(EmployeeManagement employ, IEmployee emp)
        {
            InitializeComponent();
            _employ = employ;
            _emp = emp;
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            if (!FormValidation()) return;

            var obj = new Employee
            {
                Employee_ID = EmpID.Text,
                Affiliation = Affili.Text,
                Fullname = Fullname.Text,
                Process = process.Text,
                Department_ID = selectsection.SelectedIndex
            };

            bool result = await _emp.AddEmployee(obj);
            if (!result) return;
            await _employ.Displayemployee(selectsection.SelectedIndex);
        }



        //===================== FOR FORM VALIDATION ======================
        public bool FormValidation()
        {
            bool isEmpIDEmpty = string.IsNullOrWhiteSpace(EmpID.Text);
            bool isFullnameEmpty = string.IsNullOrWhiteSpace(Fullname.Text);
            bool isSectionInvalid = (selectsection.SelectedIndex <= 0 || selectsection.SelectedIndex == 0);

            Emp_error.Visible = isEmpIDEmpty;
            Name_error.Visible = isFullnameEmpty;
            label9.Visible = isSectionInvalid;

            return !(isEmpIDEmpty || isFullnameEmpty || isSectionInvalid);
        }
    }
}
