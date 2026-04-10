using NCR_system.Interface;
using NCR_system.Models;
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
    public partial class SecondBatchCharts : UserControl
    {
        private readonly ISummaryNCR _overall;
        private readonly INCR _ncr;
        public List<NCRDatamodel> ncrlist { get; private set; } = new List<NCRDatamodel>();
        public List<NCRDatamodel> recurrist { get; private set; } = new List<NCRDatamodel>();
        public List<OverallNCR> summarylist { get; private set; } = new List<OverallNCR>();

        public SecondBatchCharts(ISummaryNCR overall, INCR ncr)
        {
            InitializeComponent();
            _overall = overall;
            _ncr = ncr;
        }


        public async Task DisplayNCR(int procs)
        {
            try
            {
                ncrlist = await  _ncr.GetSummaryNCR(procs);
                recurrist = await _ncr.GetSummaryNCR(0);
                summarylist = await _overall.GetNCRRegistrationSummary();   
                NCRTable.DataSource = ncrlist;
                RecurrenceTable.DataSource = recurrist;
                TotalOverview.DataSource = summarylist;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void SecondBatchCharts_Load(object sender, EventArgs e)
        {
            await DisplayNCR(1);
        }
    }
}
