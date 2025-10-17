using System;
using System.Windows.Forms;
using NCR_system.Interface;
using NCR_system.Models;

namespace NCR_system.View.AddForms
{
    public partial class Form1 : Form
    {
        private readonly ICustomerComplaint _cus;

        public Form1(ICustomerComplaint cus)
        {
            InitializeComponent();
            _cus = cus;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
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
                CCtype = 0
            };

            bool result = await _cus.InsertCustomerData(obj);

            if (result)
            {
                MessageBox.Show("Data saved successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save data.");
            }

        }
    }
}
