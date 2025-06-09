using ProductConfirm.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm
{
    public partial class Splashscreen : Form
    {

        public Splashscreen()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;

            if(panel2.Width >= 586) { 
                timer1.Stop();
                //Loginpage m = new Loginpage();
                //m.Show();
                this.Hide();

            }
        }

        private void Splashscreen_Load(object sender, EventArgs e)
        {
           
        }
    }
}
