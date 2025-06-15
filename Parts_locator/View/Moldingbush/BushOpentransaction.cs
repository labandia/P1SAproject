using Parts_locator.Interface;
using System;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class BushOpentransaction : Form
    {
        private readonly IRawMats _raw;
        public string part;
        public int currentquan;
        public int rack;
        public int action;

        public BushOpentransaction(IRawMats raw, string part, int currentquan, int rack, int act)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.rack = rack;  
            this.action = act;
            _raw = raw;
        }

        private void Button1_Click(object sender, EventArgs e) => Visible = false;
        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(quan.Text))
            {
                MessageBox.Show("Please input a quantity first");
                return;
            }

            // CHECK IF THE LOCATION IS ALSO THE SAME WITH THE SELECTION
            int newquantity = String.IsNullOrEmpty(quan.Text) ? 0 : Convert.ToInt32(quan.Text.Trim());
            MoldShoporder_IN_dialog mp = new MoldShoporder_IN_dialog(_raw, part, currentquan, newquantity, rack, action);
            mp.ShowDialog();
            Visible = false;
            this.Hide();
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
