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
            if (!FormValid()) return;

            var obj = new CustomerModel
            {
                ModelNo = ModelText.Text,
                LotNo = LotText.Text,
                NGQty = (int)NGText.Value,
                Status = 1,
                Details = ProblemText.Text,
                SectionID = selectDepart.SelectedIndex + 1,
                CCtype = 1
            };

            bool result = await _cus.InsertCustomerData(obj, 1);

            if (!result)
            {
                MessageBox.Show("Failed to save data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }


            MessageBox.Show("Data saved successfully.");
            await _user.DisplayCustomer(1);
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public bool FormValid()
        {
            if (string.IsNullOrWhiteSpace(ModelText.Text) ||
                string.IsNullOrWhiteSpace(LotText.Text) ||
                string.IsNullOrWhiteSpace(NGText.Text) ||
                string.IsNullOrWhiteSpace(ProblemText.Text) ||
                selectDepart.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(NGText.Text, out _))
            {
                MessageBox.Show("NG Quantity must be a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
