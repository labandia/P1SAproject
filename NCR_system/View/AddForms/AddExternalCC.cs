using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddExternalCC : Form
    {
        private readonly ICustomerComplaint _cus;
        private readonly Customer_Complaint_user _user;

        public AddExternalCC(ICustomerComplaint cus, Customer_Complaint_user user)
        {
            InitializeComponent();
            _cus = cus;
            _user = user;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {

            var obj = new CustomerModel
            {
                RegNo = EditRegNo.Text,
                CustomerName = EditCustomerText.Text,
                SectionID = selectDepart.SelectedIndex,
                ModelNo = EditModelText.Text,
                LotNo = EditLotText.Text,
                NGQty = Convert.ToInt32(EditNGText.Text),
                Details = EditProblemText.Text,
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
    }
}
