using System;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class Opentransaction_out : Form
    {
        private readonly int palID;
        private readonly string part;
        private readonly int currentquan;


        public Opentransaction_out(int palID, string part, int currentquan)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.palID = palID;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            ShopOrder_out sp = new ShopOrder_out(palID, part, currentquan, Convert.ToInt32(quan.Text));
            sp.ShowDialog();
            Visible = false;
            this.Hide();
        }

     


        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !quan.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }

        private void Opentransaction_out_Load(object sender, EventArgs e)
        {

        }
    }
}
