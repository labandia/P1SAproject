using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system
{
    public partial class P1SA_NonComformity : Form
    {
        private readonly Customer_Complaint_user _cc;

        private readonly IServiceProvider _serviceProvider;

        public P1SA_NonComformity(IServiceProvider service, Customer_Complaint_user cc)
        {
            InitializeComponent();
            _cc = cc;

            _cc.Dock = DockStyle.Fill;

            Controls.Add(_cc);

            // IMPORTANT: Add to , not Form
            Sectionpanel.Controls.Add(_cc);
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
        }
    }
}
