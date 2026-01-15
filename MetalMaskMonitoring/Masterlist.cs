using MetalMaskMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MetalMaskMonitoring
{
    public partial class Masterlist : Form
    {
        private readonly IMaskMasterlist _master;
        private readonly Timer _debounceTimer;

        public Masterlist(IMaskMasterlist master)
        {
            InitializeComponent();
            _master = master;

            _debounceTimer = new Timer();
            _debounceTimer.Interval = 500; // milliseconds
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        private async void Masterlist_Load(object sender, EventArgs e)
        {
            Modelselect.SelectedIndex = 0;

            await DisplayMasterlist();
        }


        private async Task DisplayMasterlist()
        {   
            var data = await _master.GetMasterlist("", 0, Modelselect.SelectedIndex, PartnumText.Text);
            MetalMaskTable.DataSource = data.ToList();
            CountTable.Text = data.Count().ToString();
        }

        private  void PartnumText_TextChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();  
            _debounceTimer.Start();
        }

        private async void Modelselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }


        private async void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();

            await DisplayMasterlist();
        }
    }
}
