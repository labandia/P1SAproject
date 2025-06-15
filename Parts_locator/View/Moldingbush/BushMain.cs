
using Parts_locator.Interface;
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

        private readonly IRawMats _raw;

        public BushMain(Bushlocation bush, BushMasterlist master, 
            BushSummary_in sumin, BushSummary_out sumout, IRawMats raw)
        {
            InitializeComponent();
            _bush = bush;
            _master = master;
            _sumin = sumin;
            _sumout = sumout;

            _raw = raw;

            _bush.Dock = DockStyle.Fill;
            _master.Dock = DockStyle.Fill;
            _sumin.Dock = DockStyle.Fill;
            _sumout.Dock = DockStyle.Fill;
            Controls.Add(_bush);
            Controls.Add(_master);
            Controls.Add(_sumin);
            Controls.Add(_sumout);
        }

        private async void SumOut_Click(object sender, EventArgs e)
        {
            SumOut.BackColor = Color.FromArgb(127, 93, 232);
            SumOut.ForeColor = Color.FromArgb(255, 255, 255);
            SumIN.BackColor = Color.Transparent;
            Locatorbtn.BackColor = Color.Transparent;
            Masterbtn.BackColor = Color.Transparent;

            _sumout.BringToFront();

            _sumout.shoptable.DataSource = null;
            _sumout.shoptable.DataSource = await _raw.GetShopOrderlist(1);
        }
        private async void SumIN_Click(object sender, EventArgs e)
        {
            _sumin.BringToFront();

            SumIN.BackColor = Color.FromArgb(127, 93, 232);
            SumIN.ForeColor = Color.FromArgb(255, 255, 255);
            Locatorbtn.BackColor = Color.Transparent;
            Masterbtn.BackColor = Color.Transparent;
            SumOut.BackColor = Color.Transparent;

            _sumin.shoptable.DataSource = null;
            _sumin.shoptable.DataSource = await _raw.GetShopOrderlist(1);
        }
        private void Locatorbtn_Click(object sender, EventArgs e)
        {
            Locatorbtn.BackColor = Color.FromArgb(127, 93, 232);
            Locatorbtn.ForeColor = Color.FromArgb(255, 255, 255);
            SumIN.BackColor = Color.Transparent;
            Masterbtn.BackColor = Color.Transparent;
            SumOut.BackColor = Color.Transparent;

            _bush.BringToFront();
        }
        private void Masterbtn_Click(object sender, EventArgs e)
        {
            _master.BringToFront();

            Masterbtn.BackColor = Color.FromArgb(127, 93, 232);
            Masterbtn.ForeColor = Color.FromArgb(255, 255, 255);
            Locatorbtn.BackColor = Color.Transparent;
            SumOut.BackColor = Color.Transparent;
            SumIN.BackColor = Color.Transparent;

            //bushMasterlist.shafttable.DataSource = null;
            _master.shafttable.DataSource = _raw.GetRawMatProductByType(1);
            _master.shafttable.Columns["Edit"].DisplayIndex = 5;
        }

        private void exitbtn_Click(object sender, EventArgs e) => Application.Exit();
        private void BushMain_Load(object sender, EventArgs e) => _bush.BringToFront();
      
    }
}
