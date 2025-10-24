using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.EditForms
{
    public partial class EditCC_External : Form
    {
        private readonly ICustomerComplaint _cus;
        private readonly Customer_Complaint_user _user;
        public int storeID;

        public EditCC_External(ICustomerComplaint cus, int RecordID, int type, Customer_Complaint_user user)
        {
            InitializeComponent();
            _cus = cus;
            _user = user;
            storeID = RecordID;
            SetCustomerDetails(RecordID, type);
        }

        public async void SetCustomerDetails(int RecordID, int type)
        {
            var data = await _cus.GetCustomerData(type);
            var filterdata = data.SingleOrDefault(res => res.RecordID == RecordID);

            if (filterdata != null)
            {
                EditRegNo.Text = filterdata.RegNo;
                EditCustomerText.Text = filterdata.CustomerName;
                EditModelText.Text = filterdata.ModelNo;    
                EditLotText.Text = filterdata.LotNo;
                EditNGText.Text = filterdata.NGQty.ToString();
                EditProblemText.Text = filterdata.Details;
            }
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            string status = comboBox1.SelectedItem.ToString();

            var obj = new CustomerModel
            {
                RecordID = storeID, 
                RegNo = EditRegNo.Text,
                CustomerName = EditCustomerText.Text,
                ModelNo = EditModelText.Text,
                LotNo = EditLotText.Text,
                Status = status == "Open" ? 1 : 0,
                NGQty = Convert.ToInt32(EditNGText.Text),
                Details = EditProblemText.Text  
            };

            bool result = await _cus.UpdateCustomerData(obj, 0);

            if (result)
            {
                MessageBox.Show("Edit Successfully");
                await _user.DisplayCustomer(0);
                this.Close();
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
