using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring.View.Modals
{
    public partial class AddMasterList : Form
    {
        private readonly IMSD _msd;
        private readonly MSDMasterlist _master;


        private List<MSDMasterlistodel> _masterData = new List<MSDMasterlistodel>();

        public AddMasterList(IMSD msd, MSDMasterlist master)
        {
            InitializeComponent();
            _msd = msd;
            _master=master;
        }

        private async void AddMasterList_Load(object sender, EventArgs e)
        {
            _masterData = await _msd.GetMSDMasterlist();
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            if (FormValidation())
            {
                var obj = new MSDMasterlistodel
                {
                    AmbassadorPartnum = Ambassador.Text,
                    Partname = partnameText.Text,
                    SupplyName = SupplierText.Text,
                    SupplyPartName = SupplierNameText.Text,
                    Level = Convert.ToInt32(levelText.Text),
                    FloorLife = Convert.ToInt32(FloorlifeText.Text)
                };

                var checkdata =  _masterData.Where(res => res.AmbassadorPartnum == obj.AmbassadorPartnum);
                if(!checkdata.Any())
                {
                    bool result = await _msd.AddEditMasterlistData(obj, 0);

                    if (result)
                    {
                        MessageBox.Show("Added Masterlist Successfully");
                        await _master.DisplayData();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ambassador Part number is Already Inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public bool FormValidation()
        {

            if (string.IsNullOrEmpty(Ambassador.Text) || string.IsNullOrEmpty(partnameText.Text) || string.IsNullOrEmpty(SupplierNameText.Text) || string.IsNullOrEmpty(SupplierText.Text) || string.IsNullOrEmpty(levelText.Text) || string.IsNullOrEmpty(FloorlifeText.Text))
            {
                ambassadorError.Visible = string.IsNullOrEmpty(Ambassador.Text) ? true : false;
                PartnameError.Visible = string.IsNullOrEmpty(partnameText.Text) ? true : false;
                SupplierError.Visible = string.IsNullOrEmpty(SupplierNameText.Text) ? true : false;
                SupplierNameError.Visible = string.IsNullOrEmpty(SupplierText.Text) ? true : false;
                LevelError.Visible = string.IsNullOrEmpty(levelText.Text) ? true : false;
                FloorLifeError.Visible = string.IsNullOrEmpty(FloorlifeText.Text) ? true : false;
                return false;
            }          

            return true;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FloorlifeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }

        private void levelText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }
    }
}
