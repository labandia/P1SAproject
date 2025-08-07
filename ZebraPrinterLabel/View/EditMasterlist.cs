using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZebraPrinterLabel.View
{
    public partial class EditMasterlist : Form
    {
        private readonly IMasterlist _master;
        private readonly ZebraPrinter _print;

        public string strpartnum;

        public EditMasterlist(ZebraPrinter print, IMasterlist master, string partnum)
        {
            InitializeComponent();
            _master=master;
            _print=print;
            strpartnum = partnum;
        }

        private async void Addbtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(quan.Text))
            {
                Quan_error.Visible = true;
                return;
            }


            bool result = await _master.EditMasterlist(Convert.ToInt32(quan.Text), strpartnum);

            if (result)
            {
                MessageBox.Show("Update Quantity Successfully") ;
                _print.EditQuantityBack(quan.Text);
                this.Close();
            }

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
                }
                else
                {
                    string scannedCode = quan.Text.Trim();
                    quan.Text = scannedCode;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = true; // Cancel the keypress event

                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
