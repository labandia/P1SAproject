using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Module
{
    public partial class Dashboard : UserControl
    {
        private readonly ISummaryNCR _overall;

        public List<SummaryNCRModel> cuslist { get; private set; } = new List<SummaryNCRModel>();

        public Dashboard(ISummaryNCR overall)
        {
            InitializeComponent();
            _overall = overall; 
        }

        public async Task LoadSummaryData()
        {
            cuslist = await _overall.GetCustomerSummary(DateTime.Now);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "http://pmacsweb.sdp.com/P1SA/PMACS/Mainpage",
                UseShellExecute = true
            });
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
           await LoadSummaryData();
        }
    }
}
