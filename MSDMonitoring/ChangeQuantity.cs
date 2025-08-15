using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring
{
    public partial class ChangeQuantity : Form
    {
        private readonly IMSD _msd;
        private readonly MSDHIstory _msdhistory;
        public int _ID;
        public int _Quantity;

        public ChangeQuantity(IMSD msd, int ID, int Quantity,  MSDHIstory msdhistory)
        {
            InitializeComponent();
            _msd = msd;
            _ID = ID;
            _msdhistory = msdhistory;   
            _Quantity = Quantity;
        }

        private void quan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }

        private async void Addbtn_Click(object sender, EventArgs e)
        {
            int Quantity = Convert.ToInt32(quan.Text);

            bool result = await _msd.EditComponentsData(_ID, Quantity);

            if (result)
            {
                MessageBox.Show("Update Successfully ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _msdhistory.LoadData("");
                this.Close();   
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeQuantity_Load(object sender, EventArgs e)
        {
            quan.Text = _Quantity.ToString();
        }
    }
}
