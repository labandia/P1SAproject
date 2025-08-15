using MSDMonitoring.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring
{
    public partial class MSDMasterlist : Form
    {
        private readonly IMSD _msd;

        public MSDMasterlist(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;
        }

        public async Task DisplayData()
        {
            MonitorTable.DataSource = await _msd.GetMSDMasterlist();    
        }

        private async void MSDMasterlist_Load(object sender, EventArgs e)
        {
            await DisplayData();
        }
    }
}
