using System;
using System.Windows.Forms;
using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;

namespace NCR_system.View.AddForms
{
    public partial class Form1 : Form
    {
        private readonly ICustomerComplaint _cus;
        private readonly Customer_Complaint_user _user;

        public Form1(ICustomerComplaint cus, Customer_Complaint_user user)
        {
            InitializeComponent();
            _cus = cus;
            _user = user;
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

            bool result = await _cus.InsertCustomerData(obj, 0);

            if (result)
            {
                MessageBox.Show("Data saved successfully.");
                await _user.DisplayCustomer(0); 
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save data.");
            }

        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
