using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
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
            string empid = EmpID.Text.Replace("-", "");
            string tempid = TempID.Text.Replace("-", "");
            int depid = comboBox1.SelectedIndex;

            Employee emp = new Employee
            {
                EmployeeID = empid,
                Fullname = Fullname.Text,
                Process = Process.Text,
                Affiliation = Afili.Text,
                Department_ID = depid
            };

            bool success = await _admin.UpdateEmployee(emp, tempid);

            if (success)
            {
                MessageBox.Show("Update successfully");
                Clear();
                _emp.Displayemployee();
                _emp.comboBox1.SelectedIndex = comboBox1.SelectedIndex;
                Visible = false;
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
