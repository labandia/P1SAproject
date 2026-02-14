using NCR_system.Interface;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system
{
    public partial class Mainpage : Form
    {
        private readonly Dashboard _dash;
        private readonly Customer_Complaint_user _cc;
        private readonly Inprocess_control _proc;
        private readonly NCR_control _ncr;
        private readonly Rejected _rej;
        private readonly ShipRejected _ship;
        private readonly IServiceProvider _serviceProvider;

        public Mainpage(IServiceProvider service,
            Customer_Complaint_user cc,
            Dashboard dash, 
            Inprocess_control proc,
            NCR_control ncr,
            Rejected rej,
            ShipRejected ship
            )
        {
            InitializeComponent();
            _serviceProvider = service;
            _cc = cc;
            _proc = proc;
            _ncr = ncr;
            _rej = rej;
            _ship = ship;
            _dash = dash;


            _cc.Dock = DockStyle.Fill;
            _proc.Dock = DockStyle.Fill;
            _ncr.Dock = DockStyle.Fill;
            _rej.Dock = DockStyle.Fill;
            _ship.Dock = DockStyle.Fill;
            _dash.Dock = DockStyle.Fill;

            Controls.Add(_dash);
            Controls.Add(_cc);
            Controls.Add(_proc);
            Controls.Add(_ncr);
            Controls.Add(_rej);
            Controls.Add(_ship);

            // IMPORTANT: Add to panel2, not Form
            panel2.Controls.Add(_dash);
            panel2.Controls.Add(_cc);
            panel2.Controls.Add(_proc);
            panel2.Controls.Add(_ncr);
            panel2.Controls.Add(_rej);
            panel2.Controls.Add(_ship);
        }

        private async void Mainpage_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true; // optional


            _cc.BringToFront();
            await _cc.DisplayCustomer(0);
        }

        private async void Customerbtn_Click(object sender, EventArgs e)
        {
            _cc.BringToFront();
            await _cc.DisplayCustomer(0);
        }

        private async void processbtn_Click(object sender, EventArgs e)
        {
            _proc.BringToFront();
            await _proc.DisplayRejected();
        }

        private async void ncrbtn_Click(object sender, EventArgs e)
        {
            _ncr.BringToFront();
            await _ncr.DisplayNCR(0);
        }

        private async void Rejectedbtn_Click(object sender, EventArgs e)
        {
            _rej.BringToFront();
            await _rej.DisplayRejected(0);
        }

        private async void Shipmentbtn_Click(object sender, EventArgs e)
        {
            _ship.BringToFront();
            await _ship.DisplayRejected(1);
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _dash.BringToFront();
        }

        private async void Customerbtn_Click_1(object sender, EventArgs e)
        {
            _cc.BringToFront();
            await _cc.DisplayCustomer(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}
