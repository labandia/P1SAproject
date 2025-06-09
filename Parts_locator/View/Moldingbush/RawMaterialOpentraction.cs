using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class RawMaterialOpentraction : Form
    {
        public string part;
        public int currentquan;
        public int rack;
        public int action;

        public RawMaterialOpentraction(string part, int currentquan, int rack, int act)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.rack = rack;
            this.action = act;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(quan.Text))
            {
                // CHECK IF THE LOCATION IS ALSO THE SAME WITH THE SELECTION
                int newquantity;

                if (string.IsNullOrWhiteSpace(quan.Text))
                {
                    newquantity = 0;
                }
                else
                {
                    newquantity = Convert.ToInt32(quan.Text);
                }

                RawShoporderIn_dialog mp = new RawShoporderIn_dialog(part, currentquan, newquantity, rack, action);
                mp.ShowDialog();
                Visible = false;
                this.Hide();
               
            }
            else
            {
                MessageBox.Show("Please input a quantity first");
            }
        }
    }
}
