using Parts_locator.Interface;
using Parts_locator.Modals;
using Parts_locator.Models;
using System;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class MoldShoporder_IN_dialog : Form
    {
        private readonly IRawMats _raw;
        public string part;
        public int currentquan;
        public int newquan;
        public int rack;
        public int action;

        public MoldShoporder_IN_dialog(IRawMats raw, string part, int currentquan, int newquan, int rack, int act)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.rack = rack;
            this.action = act;
            this.newquan = newquan;
            _raw = raw;
        }

        private async void savebtn_Click(object sender, EventArgs e)
        {
            int total = (action == 0) ? currentquan + newquan : currentquan - newquan;

            var obj = new RawMatInputModel {
                PartNumber = part,
                Quantity = newquan,
                Inputby = inputText.Text.Trim(),
                Action = action
            };

            bool result = await _raw.UpdateRawMatsQuantity(obj);

            if (result)
            {
                //MessageBox.Show("New shoporder Entered ");
                BushPartDetails.instanceform.RawQuantity.Text = Convert.ToString(total);
                Visible = false;
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
