using MSDMonitoring.Data;
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

namespace MSDMonitoring.View.Modals
{
    public partial class EditMasterlist : Form
    {
        private readonly IMSD _msd;
        private readonly MSDMasterlist _master;
        private readonly MSDMasterlistodel _msdinput;

        public EditMasterlist(IMSD msd, MSDMasterlist master, MSDMasterlistodel msdinput)
        {
            InitializeComponent();
            _msd = msd;
            _master=master;
            _msdinput = msdinput;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new MSDMasterlistodel
            {
                AmbassadorPartnum = _msdinput.AmbassadorPartnum,
                Partname = _msdinput.Partname,
                SupplyName = SupplierText.Text,
                SupplyPartName = SupplierNameText.Text,
                Level = Convert.ToInt32(levelText.Text),
                FloorLife = Convert.ToInt32(FloorlifeText.Text)
            };


            bool result = await _msd.AddEditMasterlistData(obj, 1);

            if (result)
            {
                MessageBox.Show("Edit Masterlist Successfully");
                await _master.DisplayData();
                _master.searchBox.Text = "";
                this.Close();
            }
        }

        private void EditMasterlist_Load(object sender, EventArgs e)
        {
            SupplierNameText.Text = _msdinput.SupplyPartName;
            SupplierText.Text = _msdinput.SupplyName;
            levelText.Text = _msdinput.Level.ToString();
            FloorlifeText.Text = _msdinput.FloorLife.ToString();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void levelText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }

        private void FloorlifeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }
    }
}
