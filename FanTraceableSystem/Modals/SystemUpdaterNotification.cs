using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem.Modals
{
    public partial class SystemUpdaterNotification : Form
    {
        public SystemUpdaterNotification()
        {
            InitializeComponent();
        }

        private void filterbtn_Click(object sender, EventArgs e)
        {
            string appPath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\Sub Assy Traceability Auto Search System.application";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = appPath,
                UseShellExecute = true // REQUIRED for ClickOnce
            };

            Process.Start(psi);
        }

        private void SystemUpdaterNotification_Load(object sender, EventArgs e)
        {
            versionText.Text = GetPublishedVersion();
        }

        public string GetPublishedVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return version.ToString();
            }

            return "Not a ClickOnce deployed application";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
