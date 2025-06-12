
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using System;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class EditEmployee : Form
    {
        private readonly EmployeeManage _emp;
        private readonly IEmployee _admin;
      
        public EditEmployee(EmployeeManage emp, IEmployee admin)
        {
            InitializeComponent();
            _admin = admin;
            _emp =emp;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try {
                string empid = EmpID.Text.Replace("-", "");
                string tempid = TempID.Text.Replace("-", "");
                int depid = comboBox1.SelectedIndex;

                var emp = new Employee
                {
                    EmployeeID = empid,
                    Fullname = string.IsNullOrEmpty(Fullname.Text) ? "" : Fullname.Text,
                    Process = string.IsNullOrEmpty(Process.Text) ? "" : Process.Text,
                    Affiliation = string.IsNullOrEmpty(Afili.Text) ? "" : Afili.Text,
                    Department_ID = depid
                };

                if (await _admin.UpdateEmployee(emp, tempid))
                {
                    MessageBox.Show("Update successfully");
                    Clear();
                    _emp.Displayemployee();
                    _emp.comboBox1.SelectedIndex = comboBox1.SelectedIndex;
                    Visible = false;
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Update successfully");
            }
           
        }

        public void Clear()
        {
            EmpID.Text = "";
            Fullname.Text = "";
            Process.Text = "";
            Afili.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void EmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13 || e.KeyChar == (char)10)
            {
                e.Handled = true;
                string scannedCode = EmpID.Text.Trim();
                EmpID.Text = scannedCode;
            }
        }
    }
}
