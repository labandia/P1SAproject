using System;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class Opentransaction_In : Form
    {
        private string part;
        private int currentquan;
        private int palID;


        public Opentransaction_In(int palID, string part, int currentquan)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan; 
            this.palID = palID; 
        }

        public Opentransaction_In()
        {
            InitializeComponent();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            int newlocation = selectedpallet.SelectedIndex + 1;

            if (!String.IsNullOrEmpty(Quantext.Text))
            {
                // CHECK IF THE LOCATION IS ALSO THE SAME WITH THE SELECTION
                if (palID ==  newlocation)
                {
                    MessageBox.Show("Part number IS CURRENTLY IN THE SAME PLAEadasd");
                }
                else
                {
                    int newquantity;

                    if (string.IsNullOrWhiteSpace(Quantext.Text))
                    {
                        newquantity = 0;
                    }
                    else
                    {
                        newquantity = Convert.ToInt32(Quantext.Text);
                    }

                    Shoporder_IN sp = new Shoporder_IN(palID, part, currentquan, newquantity, newlocation);
                    this.Hide();
                    sp.ShowDialog();
                  


                }
            }
            else
            {
                MessageBox.Show("Please input a quantity first");
            }
            
        }

        private void Quantext_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Quantext.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }

        private void Opentransaction_In_Load(object sender, EventArgs e)
        {

        }
    }
}
