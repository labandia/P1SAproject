using System;
using System.Windows.Forms;
using NCR_system.Models;

namespace NCR_system.View.AddForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new CustomerModel
            {
                RegNo = RegNo.Text,
                CustomerName = CustomerText.Text,
                ModelNo = ModelText.Text,
                LotNo = LotText.Text,
                NGQty = Convert.ToInt32(NGText.Text),
                Details = ProblemText.Text,
                SectionID = selectDepart.SelectedIndex + 1,
                CCtype = 1
            };
        }
    }
}
