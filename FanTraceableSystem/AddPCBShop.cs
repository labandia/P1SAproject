using FanTraceableSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class AddPCBShop : Form
    {
        public List<TracePCBModel> PCBList { get; private set; } = new List<TracePCBModel>();

        public AddPCBShop()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Example: collect values from controls
            PCBList.Add(new TracePCBModel
            {
                PCBShopOrder = Shoptext.Text,
                Quantity = int.TryParse(textBox1.Text, out int qty) ? qty : 0
            });

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
