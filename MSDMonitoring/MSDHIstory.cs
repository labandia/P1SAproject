using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring
{
    public partial class MSDHIstory : Form
    {
        private readonly IMSD _msd;

        public MSDHIstory(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        public async Task LoadData()
        {
            MonitorTable.DataSource = await _msd.GetMSDHistoryList();
        }

        private async void MSDHIstory_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable; // keeps title bar

            await LoadData();
        }
    }
}
