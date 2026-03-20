using NCR_system.Interface;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system
{
    public partial class P1SA_NonComformity : Form
    {
        private Customer_Complaint_user _cc;
        private ShipRejected _ship;
        private Rejected _reg;
        private Inprocess_control _inp;
        private NCR_control _ncr;

        private readonly ICustomerComplaint _cust;
        private readonly IShipRejected _shipV;
        private readonly IInprocess _inps;
        private readonly INCR _ncrinter;

        // COLORS
        public readonly Color ActiveBg = Color.FromArgb(37, 99, 235);
        public readonly Color DefaultBg = Color.FromArgb(37, 42, 71);


        public P1SA_NonComformity(
            ICustomerComplaint cust,
            IShipRejected shipV,
            IInprocess inps, 
            INCR ncrinter)
        {
            InitializeComponent();

            _cust = cust;
            _shipV = shipV;
            _inps = inps;
            _ncrinter = ncrinter;   
        }



        private async void P1SA_NonComformity_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true;

            await LoadCustomerComplaint();
        }

        // ===================== BUTTON EVENTS =====================

        private async void SDCbtn_Click(object sender, EventArgs e) => await LoadCustomerComplaint();
        private async void Shipbtn_Click(object sender, EventArgs e) => await LoadShipRejected(); 
        private async void rejectBtn_Click(object sender, EventArgs e) => await LoadRejected();
        private async void processbtn_Click(object sender, EventArgs e) => await LoadInprocess();
        private async void NCRmenu_Click(object sender, EventArgs e) => await LoadNCR(0);
        private async void button2_Click(object sender, EventArgs e) => await LoadNCR(1);
        // ===================== LOAD METHODS =====================

        private async Task LoadCustomerComplaint()
        {
            SetActiveMenu(SDCbtn);

            if (_cc == null)
            {
                _cc = new Customer_Complaint_user(_cust);
                _cc.Dock = DockStyle.Fill;
                Sectionpanel.Controls.Add(_cc);
            }

            _cc.BringToFront();
            await _cc.DisplayCustomer(0);
        }

        private async Task LoadShipRejected()
        {
            SetActiveMenu(Shipbtn);

            if (_ship == null)
            {
                _ship = new ShipRejected(_shipV);
                _ship.Dock = DockStyle.Fill;
                Sectionpanel.Controls.Add(_ship);
            }

            _ship.BringToFront();
            await _ship.DisplayRejected(1);
        }

        private async Task LoadRejected()
        {
            SetActiveMenu(rejectBtn);

            if (_reg == null)
            {
                _reg = new Rejected(_shipV);
                _reg.Dock = DockStyle.Fill;
                Sectionpanel.Controls.Add(_reg);
            }

            _reg.BringToFront();
            await _reg.DisplayRejected(0);
        }

        private async Task LoadInprocess()
        {
            SetActiveMenu(processbtn);

            if (_inp == null)
            {
                _inp = new Inprocess_control(_inps);
                _inp.Dock = DockStyle.Fill;
                Sectionpanel.Controls.Add(_inp);
            }

            _inp.BringToFront();
            await _inp.DisplayRejected();
        }

        private async Task LoadNCR(int data)
        {
            Button btn = data == 0 ? NCRmenu : button12;
            SetActiveMenu(btn);

            if (_ncr == null)
            {
                _ncr = new NCR_control(_ncrinter);
                _ncr.Dock = DockStyle.Fill;
                Sectionpanel.Controls.Add(_ncr);
            }

            _ncr.BringToFront();
            await _ncr.DisplayNCR(data);
        }

        // ===================== MENU COLOR =====================

        private void SetActiveMenu(Button activeButton)
        {
            var buttons = new List<Button>
            {
                SDCbtn,
                Shipbtn,
                rejectBtn,
                processbtn,
                NCRmenu,
                button2
            };

            foreach (var btn in buttons)
            {
                btn.BackColor = (btn == activeButton)
                    ? ActiveBg
                    : DefaultBg;
            }
        }


        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}
