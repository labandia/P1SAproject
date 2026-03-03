using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Details
{
    public partial class CustomerDetails : Form
    {

        public CustomerDetails(CustomerModel cus)
        {
            InitializeComponent();

            MessageBox.Show(cus.RegNo);
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
