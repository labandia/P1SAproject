using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Attendance_Monitoring.View
{
    public partial class Selection : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminController _admin;
        public Selection(IServiceProvider serviceProvider)
        {
            InitializeComponent();  
            _serviceProvider = serviceProvider;
            _admin = new AdminController();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<CRMainpage>();
            // Show the main form
            mainpage.Show();
            // Hide the login form (optional)
            this.Hide();
            Visible = false;
        }

        private void Moldingbtn_Click(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<Mainpage>();
            // Show the main form
            mainpage.Show();
            // Hide the login form (optional)
            this.Hide();
            Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private async void Selection_Load(object sender, EventArgs e)
        {
            string hostName = Dns.GetHostName();
            string ipAddress = Dns.GetHostAddresses(hostName)
                                  .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                                  .ToString();


            IEnumerable<CRaccess> emp = await _admin.GetCRaccess();

            var employee = emp.FirstOrDefault(p => p.IPaddress.Equals(ipAddress, StringComparison.OrdinalIgnoreCase));

            if (employee != null)
            {
                if (employee.Active == 1)
                {
                    Moldingbtn.Enabled = true;
                }
                else
                {
                    Moldingbtn.Enabled = false;
                }

                if (employee.CRactive == 1)
                {
                    button10.Enabled = true;
                }
                else
                {
                    button10.Enabled = false;
                }
            }
            else
            {
                button10.Enabled = false;
            }
        }
    }
}
