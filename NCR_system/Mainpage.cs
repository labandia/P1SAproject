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

        public readonly Color primaryGray = Color.FromArgb(37, 99, 235);
        public readonly Color whiteback = Color.FromArgb(94, 111, 126);

        public readonly Color textColor = Color.FromArgb(255, 255, 255);

        public readonly Color TransClr = Color.Transparent;

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

        private  void Mainpage_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true; // optional

            DefaultMenuHighligted();


        }

      
        private async void processbtn_Click(object sender, EventArgs e)
        {
            _proc.BringToFront();

            // Set Active of the Current Button
            processbtn.BackColor = primaryGray;
            processbtn.ForeColor = textColor;



            // Set tit Default Color of other button
            SDCbtn.BackColor = Color.Transparent;
            SDCbtn.ForeColor = Color.FromArgb(94, 111, 126);
            rejectBtn.BackColor = Color.Transparent;
            rejectBtn.ForeColor = Color.FromArgb(94, 111, 126);

            Shipbtn.BackColor = Color.Transparent;
            Shipbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Mainbtn.BackColor = Color.Transparent;
            Mainbtn.ForeColor = Color.FromArgb(94, 111, 126);

            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            //Externalbtn.BackColor = Color.Transparent;
            //Externalbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Recurrencebtn.BackColor = Color.Transparent;
            Recurrencebtn.ForeColor = Color.FromArgb(94, 111, 126);

            await _proc.DisplayRejected();
        }

       
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

     
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private async void SDCClickbtn(object sender, EventArgs e)
        {
            _cc.BringToFront();
            // Set Active of the Current Button
            SDCbtn.BackColor = primaryGray;
            SDCbtn.ForeColor = textColor;

            // Set tit Default Color of other button
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            rejectBtn.BackColor = Color.Transparent;
            rejectBtn.ForeColor = Color.FromArgb(94, 111, 126);

            Shipbtn.BackColor = Color.Transparent;
            Shipbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Mainbtn.BackColor = Color.Transparent;
            Mainbtn.ForeColor = Color.FromArgb(94, 111, 126);

            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            //Externalbtn.BackColor = Color.Transparent;
            //Externalbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Recurrencebtn.BackColor = Color.Transparent;
            Recurrencebtn.ForeColor = Color.FromArgb(94, 111, 126);

            await _cc.DisplayCustomer(0);
        }

        private async void rejectBtn_Click(object sender, EventArgs e)
        {
            _ncr.BringToFront();

            // Set Active of the Current Button

            rejectBtn.BackColor = primaryGray;
            rejectBtn.ForeColor = textColor;


            // Set tit Default Color of other button
            SDCbtn.BackColor = Color.Transparent;
            SDCbtn.ForeColor = Color.FromArgb(94, 111, 126);
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);

            Shipbtn.BackColor = Color.Transparent;
            Shipbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Mainbtn.BackColor = Color.Transparent;
            Mainbtn.ForeColor = Color.FromArgb(94, 111, 126);

            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            //Externalbtn.BackColor = Color.Transparent;
            //Externalbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Recurrencebtn.BackColor = Color.Transparent;
            Recurrencebtn.ForeColor = Color.FromArgb(94, 111, 126);

            await _ncr.DisplayNCR(0);
        }

        private async void Shipbtn_Click(object sender, EventArgs e)
        {
            _rej.BringToFront();

            // Set Active of the Current Button       
            Shipbtn.BackColor = primaryGray;
            Shipbtn.ForeColor = textColor;


            // Set tit Default Color of other button
            SDCbtn.BackColor = Color.Transparent;
            SDCbtn.ForeColor = Color.FromArgb(94, 111, 126);
            rejectBtn.BackColor = Color.Transparent;
            rejectBtn.ForeColor = Color.FromArgb(94, 111, 126);

            
            Mainbtn.BackColor = Color.Transparent;
            Mainbtn.ForeColor = Color.FromArgb(94, 111, 126);
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            //Externalbtn.BackColor = Color.Transparent;
            //Externalbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Recurrencebtn.BackColor = Color.Transparent;
            Recurrencebtn.ForeColor = Color.FromArgb(94, 111, 126);

            await _rej.DisplayRejected(0);
        }

        private async void Mainbtn_Click(object sender, EventArgs e)
        {
            _ship.BringToFront();

            // Set Active of the Current Button       
            Mainbtn.BackColor = primaryGray;
            Mainbtn.ForeColor = textColor;

            // Set tit Default Color of other button
            SDCbtn.BackColor = Color.Transparent;
            SDCbtn.ForeColor = Color.FromArgb(94, 111, 126);
            rejectBtn.BackColor = Color.Transparent;
            rejectBtn.ForeColor = Color.FromArgb(94, 111, 126);
            Shipbtn.BackColor = Color.Transparent;
            Shipbtn.ForeColor = Color.FromArgb(94, 111, 126);


            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            //Externalbtn.BackColor = Color.Transparent;
            //Externalbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Recurrencebtn.BackColor = Color.Transparent;
            Recurrencebtn.ForeColor = Color.FromArgb(94, 111, 126);

            await _ship.DisplayRejected(1);
        }

        private void Recurrencebtn_Click(object sender, EventArgs e)
        {
            // Set Active of the Current Button       
            Recurrencebtn.BackColor = primaryGray;
            Recurrencebtn.ForeColor = textColor;

            // Set tit Default Color of other button
            SDCbtn.BackColor = Color.Transparent;
            SDCbtn.ForeColor = Color.FromArgb(94, 111, 126);
            rejectBtn.BackColor = Color.Transparent;
            rejectBtn.ForeColor = Color.FromArgb(94, 111, 126);
            Shipbtn.BackColor = Color.Transparent;
            Shipbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Mainbtn.BackColor = Color.Transparent;
            Mainbtn.ForeColor = Color.FromArgb(94, 111, 126);

            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            //Externalbtn.BackColor = Color.Transparent;
            //Externalbtn.ForeColor = Color.FromArgb(94, 111, 126);
        }

        private void Externalbtn_Click(object sender, EventArgs e)
        {
           
        }

        private void Dashbtn_Click(object sender, EventArgs e)
        {
            DefaultMenuHighligted();
        }


        public void DefaultMenuHighligted()
        {
            // Set Active of the Current Button       
            Dashbtn.BackColor = primaryGray;
            Dashbtn.ForeColor = textColor;

            // Set tit Default Color of other button
            SDCbtn.BackColor = Color.Transparent;
            SDCbtn.ForeColor = Color.FromArgb(94, 111, 126);
            rejectBtn.BackColor = Color.Transparent;
            rejectBtn.ForeColor = Color.FromArgb(94, 111, 126);
            Shipbtn.BackColor = Color.Transparent;
            Shipbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Mainbtn.BackColor = Color.Transparent;
            Mainbtn.ForeColor = Color.FromArgb(94, 111, 126);

            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            processbtn.BackColor = Color.Transparent;
            processbtn.ForeColor = Color.FromArgb(94, 111, 126);
            Recurrencebtn.BackColor = Color.Transparent;
            Recurrencebtn.ForeColor = Color.FromArgb(94, 111, 126);
        }
    }
}
