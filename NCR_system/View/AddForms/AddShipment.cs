using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddShipment : Form
    {
        public AddShipment()
        {
            InitializeComponent();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
