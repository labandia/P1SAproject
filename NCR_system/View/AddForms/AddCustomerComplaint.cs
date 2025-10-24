using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;

namespace NCR_system.View.AddForms
{
    public partial class AddCustomerComplaint : Form
    {
        private readonly ICustomerComplaint _cus;
        private readonly Customer_Complaint_user _user;


        public AddCustomerComplaint(ICustomerComplaint cus, Customer_Complaint_user user)
        {
            InitializeComponent();
            _cus = cus;
            _user = user;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new CustomerModel
            {
                ModelNo = ModelText.Text,
                LotNo = LotText.Text,
                NGQty = Convert.ToInt32(NGText.Text),
                Status = 1,
                Details = ProblemText.Text,
                SectionID  = selectDepart.SelectedIndex + 1,
                CCtype = 1
            };

            bool result = await _cus.InsertCustomerData(obj, 1);

            if (result)
            {
                MessageBox.Show("Data saved successfully.");
                await _user.DisplayCustomer(1);
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
