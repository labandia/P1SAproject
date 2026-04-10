using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Module
{
    public partial class FirstBatchCharts : UserControl
    {
        private readonly ISummaryNCR _overall;

        public List<SummaryNCRModel> cuslist { get; private set; } = new List<SummaryNCRModel>();
        public List<SummaryInprocessModel> inprocessList { get; private set; } = new List<SummaryInprocessModel>();
        public List<SummaryInprocessModel> rejectloct { get; private set; } = new List<SummaryInprocessModel>();
        public List<SummaryInprocessModel> shipmentlist { get; private set; } = new List<SummaryInprocessModel>();

        public FirstBatchCharts(ISummaryNCR overall)
        {
            InitializeComponent();
            _overall = overall;
        }

        public async Task LoadSummaryData()
        {
            cuslist = await _overall.GetCustomerSummary(DateTime.Now);
            inprocessList = await _overall.GetInprocessSummary();
            rejectloct = await _overall.GetRejectedSummary();
            shipmentlist = await _overall.GetShipmentSummary();
        }

        private async void FirstBatchCharts_Load(object sender, EventArgs e)
        {
            await LoadSummaryData();
            CustomerChart();
            LoadInprocessData();
            LoadRejecteData();
            LoadShipmentData();
        }

        private void CustomerChart()
        {
            if (cuslist == null || cuslist.Count == 0)
                return;

            // Extract data from cuslist
            var departments = cuslist.Select(x => x.DepartmentName).ToArray();

            var sdcValues = new LiveCharts.ChartValues<int>(
                cuslist.Select(x => x.SDC)
            );

            var extValues = new LiveCharts.ChartValues<int>(
                cuslist.Select(x => x.External)
            );

            // Series
            customerChart.Series = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "SDC",
                    Values = sdcValues,
                    Fill = System.Windows.Media.Brushes.Green,
                    DataLabels = false
                },
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "External CC",
                    Values = extValues,
                    Fill = System.Windows.Media.Brushes.Blue,
                    DataLabels = false
                }
            };

            // X Axis
            customerChart.AxisX.Clear();
            customerChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "",
                Labels = departments
            });

            // Y Axis
            customerChart.AxisY.Clear();
            customerChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "",
                MinValue = 0,
                LabelFormatter = value => value.ToString("N0"), // ✅ no decimals
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = 1 // ✅ show only whole numbers
                }
            });

            // Legend
            customerChart.LegendLocation = LiveCharts.LegendLocation.Right;
        }


        public void LoadInprocessData()
        {
            if (inprocessList == null || inprocessList.Count == 0)
                return;

            var departments = inprocessList.Select(x => x.DepartmentName).ToArray();

            var openValues = new LiveCharts.ChartValues<int>(
                inprocessList.Select(x => x.OpenCase)
            );

            inprocessChart.Series = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = openValues,
                    Fill = System.Windows.Media.Brushes.Orange, // 🔶 different color
                    DataLabels = false
                }
            };

            // X Axis
            inprocessChart.AxisX.Clear();
            inprocessChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "",
                Labels = departments
            });

            // Y Axis (integer only)
            //inprocessChart.AxisY.Clear();
            //inprocessChart.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Open Cases",
            //    MinValue = 0
            //});

            inprocessChart.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        public void LoadRejecteData()
        {
            if (rejectloct == null || rejectloct.Count == 0)
                return;

            var departments = rejectloct.Select(x => x.DepartmentName).ToArray();

            var openValues = new LiveCharts.ChartValues<int>(
                rejectloct.Select(x => x.OpenCase)
            );

            rejectedChart.Series = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = openValues,
                    Fill = System.Windows.Media.Brushes.Orange, // 🔶 different color
                    DataLabels = false
                }
            };

            // X Axis
            rejectedChart.AxisX.Clear();
            rejectedChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "",
                Labels = departments
            });

            // Y Axis (integer only)
            rejectedChart.AxisY.Clear();
            rejectedChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Open Cases",
                MinValue = 0
            });

            rejectedChart.LegendLocation = LiveCharts.LegendLocation.None;
        }

        public void LoadShipmentData()
        {
            if (shipmentlist == null || shipmentlist.Count == 0)
                return;

            var departments = shipmentlist.Select(x => x.DepartmentName).ToArray();

            var openValues = new LiveCharts.ChartValues<int>(
                shipmentlist.Select(x => x.OpenCase)
            );

            ShipmentChart.Series = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = openValues,
                    Fill = System.Windows.Media.Brushes.Orange, // 🔶 different color
                    DataLabels = false
                }
            };

            // X Axis
            ShipmentChart.AxisX.Clear();
            ShipmentChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "",
                Labels = departments
            });

            // Y Axis (integer only)
            ShipmentChart.AxisY.Clear();
            ShipmentChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Open Cases",
                MinValue = 0
            });

            ShipmentChart.LegendLocation = LiveCharts.LegendLocation.None;
        }
    }
}
