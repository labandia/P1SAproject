using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCR_system.Models;
using static System.Collections.Specialized.BitVector32;

namespace NCR_system.View.AddForms
{
    public partial class AddCustomerComplaint : Form
    {
        public AddCustomerComplaint()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new CustomerModel
            {
                ModelNo = ModelText.Text,
                LotNo = LotText.Text,
                NGQty = Convert.ToInt32(NGText.Text),
                Details = ProblemText.Text,
                SectionID  = selectDepart.SelectedIndex + 1,
                CCtype = 1
            };
        }
    }
}
