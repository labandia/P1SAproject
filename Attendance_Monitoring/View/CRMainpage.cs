using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.View
{
    public partial class CRMainpage : Form
    {
        public int sectID;
        private readonly IServiceProvider _serviceProvider;
        public CRMainpage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider=serviceProvider;   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<Selection>();
            // Show the main form
            mainpage.Show();
            // Hide the login form (optional)
            this.Hide();
            Visible = false;
           
        }

        private void Moldingbtn_Click(object sender, EventArgs e)
        {
            CRmonitoringform(1);
        }

        private void Pressbtn_Click(object sender, EventArgs e)
        {
            CRmonitoringform(2);
        }

        private void Rotorbtn_Click(object sender, EventArgs e)
        {
            CRmonitoringform(3);
        }

        private void Windingbtn_Click(object sender, EventArgs e)
        {
            CRmonitoringform(4);
        }

        private void Circuitbtn_Click(object sender, EventArgs e)
        {
            CRmonitoringform(5);
        }


        public void CRmonitoringform(int sectID)
        {
            CR_Monitoring at = new CR_Monitoring(sectID, _serviceProvider);
            at.Show();
            Visible = false;
        }

    }
}
