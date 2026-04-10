using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Axis = LiveCharts.Wpf.Axis;
using System.Linq;
using NCR_system.View.Module;
using System.Drawing;

namespace NCR_system.View
{
    public partial class OverallSummary : Form
    {
        private FirstBatchCharts _first;
        private SecondBatchCharts _second;  

        private readonly ISummaryNCR _overall;
        private readonly INCR _ncr;


        //public List<SummaryInprocessModel> NCRlist { get; private set; } = new List<SummaryInprocessModel>();

        public OverallSummary(ISummaryNCR overall, INCR ncr)
        {
            InitializeComponent();
            _overall = overall;
            _ncr = ncr;


        }

      
        private void OverallSummary_Load(object sender, EventArgs e)
        {


            if (_first == null)
            {
                _first = new FirstBatchCharts(_overall);
                _first.Dock = DockStyle.Fill;
                chartContent.Controls.Add(_first);
            }

            _first.BringToFront();
        }

        private void overbtn_Click(object sender, EventArgs e)
        {

            if (_first == null)
            {
                _first = new FirstBatchCharts(_overall);
                _first.Dock = DockStyle.Fill;
                chartContent.Controls.Add(_first);
            }

            _first.BringToFront();
        }

        private void NCRMenu_Click(object sender, EventArgs e)
        {

            if (_second == null)
            {
                _second = new SecondBatchCharts(_overall, _ncr);
                _second.Dock = DockStyle.Fill;
                chartContent.Controls.Add(_second);
            }

            _second.BringToFront();
        }
    }
}
