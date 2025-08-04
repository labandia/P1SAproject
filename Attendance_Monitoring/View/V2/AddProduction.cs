using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.View.V2
{
    public partial class AddProduction : Form
    {
        public AddProduction()
        {
            InitializeComponent();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (!FormValidation()) return;
          
            MessageBox.Show("GOOD TO INSERT");
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
