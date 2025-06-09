
using Parts_locator.Data;
using Parts_locator.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class BushMain : Form
    {
        private Transaction_Bush b;
        private BushProducts p; 

        public BushMain()
        {
            InitializeComponent();
            bushlocation.BringToFront();
        }

        private void bushpartbtn_Click(object sender, EventArgs e)
        {
            bushpartbtn.BackColor = Color.FromArgb(127, 93, 232);
            bushpartbtn.ForeColor = Color.FromArgb(255, 255, 255);
            Masterlistbtn.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;

            bushlocation.BringToFront();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BushMain_Load(object sender, EventArgs e)
        {

        }

        private void Masterlistbtn_Click(object sender, EventArgs e)
        {
            b = new Transaction_Bush();
            bushSummary_in.BringToFront();

            Masterlistbtn.BackColor = Color.FromArgb(127, 93, 232);
            Masterlistbtn.ForeColor = Color.FromArgb(255, 255, 255);
            bushpartbtn.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;

            bushSummary_in.shoptable.DataSource = null;
            bushSummary_in.shoptable.DataSource = b.GetMoldingShoporder(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b = new Transaction_Bush();
            bushSummary_out.BringToFront();

            button3.BackColor = Color.FromArgb(127, 93, 232);
            button3.ForeColor = Color.FromArgb(255, 255, 255);
            Masterlistbtn.BackColor = Color.Transparent;
            bushpartbtn.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;

            bushSummary_out.shoptable.DataSource = null;
            bushSummary_out.shoptable.DataSource = b.GetMoldingShoporder(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p = new BushProducts();
            bushMasterlist.BringToFront();

            button1.BackColor = Color.FromArgb(127, 93, 232);
            button1.ForeColor = Color.FromArgb(255, 255, 255);
            bushpartbtn.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            Masterlistbtn.BackColor = Color.Transparent;

            //bushMasterlist.shafttable.DataSource = null;
            bushMasterlist.shafttable.DataSource = p.getModelingRowByType(1);
            bushMasterlist.shafttable.Columns["Edit"].DisplayIndex = 5;
        }
    }
}
