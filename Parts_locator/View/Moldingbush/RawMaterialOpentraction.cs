using Newtonsoft.Json.Linq;
using Parts_locator.Interface;
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
    public partial class RawMaterialOpentraction : Form
    {
        private readonly IRawMats _raw;
        public string part;
        public int currentquan;
        public int rack;
        public int action;

        public RawMaterialOpentraction(IRawMats raw, string part, int currentquan, int rack, int act)
        {
            InitializeComponent();
            this.part = part;
            this.currentquan = currentquan;
            this.rack = rack;
            this.action = act;
            _raw = raw;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(quan.Text))
            {
                MessageBox.Show("Please input a quantity first");
                return;
            }

            int newquantity = String.IsNullOrEmpty(quan.Text) ? 0 : Convert.ToInt32(quan.Text.Trim());
            RawShoporderIn_dialog mp = new RawShoporderIn_dialog(_raw, part, currentquan, newquantity, rack, action);
            mp.ShowDialog();
            Visible = false;
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
