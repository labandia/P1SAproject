using NCR_system.View.Module;
using System;
using System.Windows.Forms;

namespace NCR_system
{
    public partial class P1SA_NonComformity : Form
    {
        private readonly Customer_Complaint_user _cc;
        private readonly ShipRejected _ship;

        private readonly IServiceProvider _serviceProvider;

        public P1SA_NonComformity(IServiceProvider service, 
            Customer_Complaint_user cc,
            ShipRejected ship)
        {
            InitializeComponent();
            _cc = cc;
            _ship = ship;

            _cc.Dock = DockStyle.Fill;
            _ship.Dock = DockStyle.Fill;

            Controls.Add(_cc);

            // IMPORTANT: Add to , not Form
            Sectionpanel.Controls.Add(_cc);
            Sectionpanel.Controls.Add(_ship);
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void P1SA_NonComformity_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true; // optional

            _cc.BringToFront(); 
        }

        private async void Shipbtn_Click(object sender, EventArgs e)
        {
            _ship.BringToFront();

            await _ship.DisplayRejected(1);
        }
    }
}
