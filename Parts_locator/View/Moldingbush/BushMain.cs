
using Parts_locator.Models;
using Parts_locator.View.Moldingbush.Maincontent;
using Parts_locator.View.Moldingbush.Modules;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class BushMain : Form
    {
        private readonly Bushlocation _bush;
        private readonly BushMasterlist _master;
        private readonly BushSummary_in _sumin;
        private readonly BushSummary_out _sumout;


        private Transaction_Bush b;
        private BushProducts p; 

        public BushMain(Bushlocation bush, BushMasterlist master, BushSummary_in sumin, BushSummary_out sumout)
        {
            InitializeComponent();
            _bush = bush;
            _master = master;
            _sumin = sumin;
            _sumout = sumout;

            _bush.Dock = DockStyle.Fill;
            _master.Dock = DockStyle.Fill;
            _sumin.Dock = DockStyle.Fill;
            _sumout.Dock = DockStyle.Fill;
            Controls.Add(_bush);
            Controls.Add(_master);
            Controls.Add(_sumin);
            Controls.Add(_sumout);
        }

        private void bushpartbtn_Click(object sender, EventArgs e)
        {
            bushpartbtn.BackColor = Color.FromArgb(127, 93, 232);
            bushpartbtn.ForeColor = Color.FromArgb(255, 255, 255);
            Masterlistbtn.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;

            _bush.BringToFront();
        }

        

        private void Masterlistbtn_Click(object sender, EventArgs e)
        {
            b = new Transaction_Bush();
            _sumin.BringToFront();

            Masterlistbtn.BackColor = Color.FromArgb(127, 93, 232);
            Masterlistbtn.ForeColor = Color.FromArgb(255, 255, 255);
            bushpartbtn.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;

            _sumin.shoptable.DataSource = null;
            _sumin.shoptable.DataSource = b.GetMoldingShoporder(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b = new Transaction_Bush(); 
            button3.BackColor = Color.FromArgb(127, 93, 232);
            button3.ForeColor = Color.FromArgb(255, 255, 255);
            Masterlistbtn.BackColor = Color.Transparent;
            bushpartbtn.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;

            _sumout.BringToFront();

            _sumout.shoptable.DataSource = null;
            _sumout.shoptable.DataSource = b.GetMoldingShoporder(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p = new BushProducts();
            _master.BringToFront();

            button1.BackColor = Color.FromArgb(127, 93, 232);
            button1.ForeColor = Color.FromArgb(255, 255, 255);
            bushpartbtn.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            Masterlistbtn.BackColor = Color.Transparent;

            //bushMasterlist.shafttable.DataSource = null;
            _master.shafttable.DataSource = p.getModelingRowByType(1);
            _master.shafttable.Columns["Edit"].DisplayIndex = 5;
        }


        private void exitbtn_Click(object sender, EventArgs e) => Application.Exit();
     
        private void BushMain_Load(object sender, EventArgs e)
        {
            _bush.BringToFront();
        }
    }
}
