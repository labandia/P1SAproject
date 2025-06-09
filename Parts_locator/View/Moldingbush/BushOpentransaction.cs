using Parts_locator.Modals;
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
    public partial class BushOpentransaction : Form
    {
        public string part;
        public int currentquan;
        public int rack;
        public int action;

        public BushOpentransaction(string part, int currentquan, int rack, int act)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.rack = rack;  
            this.action = act;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Visible = false;
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

                MoldShoporder_IN_dialog mp = new MoldShoporder_IN_dialog(part, currentquan, newquantity,  rack, action);
                mp.ShowDialog();
                Visible = false;
                this.Hide();
                //Shoporder_IN sp = new Shoporder_IN(palID, part, currentquan, newquantity, newlocation);
                //sp.Show();
            }
            else
            {
                MessageBox.Show("Please input a quantity first");
            }
        }

        private void BushOpentransaction_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("Part number : " + part);
           // MessageBox.Show("Quantity : " + currentquan);
           /// MessageBox.Show("Racks : " + rack);
           // MessageBox.Show("Action : " + action);
        }

        private void quan_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !quan.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                    Type_error.Visible = false;
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                    Type_error.Visible = true;
                }
            }
        }
    }
}
