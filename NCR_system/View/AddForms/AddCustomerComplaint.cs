using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCR_system.Interface;
using NCR_system.Models;
using static System.Collections.Specialized.BitVector32;

namespace NCR_system.View.AddForms
{
    public partial class AddCustomerComplaint : Form
    {
        private readonly ICustomerComplaint _cus;

        public AddCustomerComplaint(ICustomerComplaint cus)
        {
            InitializeComponent();
            _cus = cus;
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
                SectionID  = selectDepart.SelectedIndex,
                CCtype = 1
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
