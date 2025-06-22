using Attendance_Monitoring.Controller;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

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

        private void CRpage(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<CRMainpage>();
            mainpage.Show();
            this.Hide();
            Visible = false;
        }

        private void Moldingbtn_Click(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<Mainpage>();
            mainpage.Show();
            this.Hide();
            Visible = false;
        }

        private async void Selection_Load(object sender, EventArgs e)
        {
            try
            {
                //string hostName = Dns.GetHostName();
                //string ipAddress = Dns.GetHostAddresses(hostName)
                //                      .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                //                      .ToString();


                //var emp = await _admin.GetCRaccess();

                //var employee = emp.FirstOrDefault(p => p.IPaddress.Equals(ipAddress, StringComparison.OrdinalIgnoreCase));

                //if (employee != null)
                //{
                //    if (employee.Active == 1)
                //    {
                //        Moldingbtn.Enabled = true;
                //    }
                //    else
                //    {
                //        Moldingbtn.Enabled = false;
                //    }

                //    if (employee.CRactive == 1)
                //    {
                //        button10.Enabled = true;
                //    }
                //    else
                //    {
                //        button10.Enabled = false;
                //    }
                //}
                //else
                //{
                //    button10.Enabled = false;
                //}
            }
            catch (FormatException)
            {
                MessageBox.Show("Can Detect local ip address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitApp(object sender, EventArgs e) => Application.Exit();

       
    }
}
