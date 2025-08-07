using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZebraPrinterLabel
{
    public partial class AddMasterlist : Form
    {
        private readonly IMasterlist _master;
        private readonly ZebraPrinter _print;
        public string partnumText;

        public AddMasterlist(ZebraPrinter print, string partnum, IMasterlist master)
        {
            InitializeComponent();
            _master = master;
            _print=print;
            partnumText = partnum; 
        }

        private void AddMasterlist_Load(object sender, EventArgs e)
        {
            Partnum.Text = partnumText;
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            var obj = new MasterlistData
            {
                Partnum = Partnum.Text.Trim(),
                WarehouseLocal = Warehouse.Text,
                Qty = Convert.ToInt32(Quantity.Text)
            };

            bool result = await _master.AddnewMasterlist(obj);

            if (result)
            {
                MessageBox.Show("Add Data Successfully");
                _print.SetDataBack(Partnum.Text, Warehouse.Text, Quantity.Text);
                this.Close();
            }

        }

        private void Cancebtn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !Quantity.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    string scannedCode = Quantity.Text.Trim();
                    Quantity.Text = scannedCode;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = true; // Cancel the keypress event

                }
            }
        }
    }
}
