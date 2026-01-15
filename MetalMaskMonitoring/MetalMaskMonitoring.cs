using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalMaskMonitoring
{
    public partial class MetalMaskMonitoring : Form
    {
        public MetalMaskMonitoring()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var stepper = new MetalMaskFormOut(1))
            {
                if (stepper.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Stepper Completed!");
                }
            }
        }
    }
}
