using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Drawing.Color;
using System.Windows.Media;


namespace NCR_system.View.Module
{
    public partial class ShipRejected : UserControl
    {
        private readonly IShipRejected _ship;

        private bool _isInitializing = true;
        private bool _gridConfigured = false;
        private bool _isLoading = false;

        public DataGridView Customgrid => RejectedGrid;

        // Reuse collections to reduce allocations
        private readonly ChartValues<int> _statusValues = new ChartValues<int>();
        private readonly List<string> _statusLabels = new List<string> 
        { "For Circulation", "Close / Completed", "Report OK", "Open" };


        public ShipRejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;
            _isInitializing = true;

            sectionfilter.SelectedIndex = 0;
            filteritems.SelectedIndex = 1;

            EnableDoubleBuffering();
            _isInitializing = false;
        }

        private void EnableDoubleBuffering()
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(RejectedGrid, true, null);

            RejectedGrid.EnableHeadersVisualStyles = false;
            RejectedGrid.RowHeadersVisible = false;
        }



        private void ConfigureGrid()
        {
            if (_gridConfigured) return;

            RejectedGrid.AutoGenerateColumns = false;
            RejectedGrid.SuspendLayout();

            void Setup(string name, int width, int displayIndex,
                DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.None)
            {
                var col = RejectedGrid.Columns[name];
                if (col == null) return;

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.Width = width;
                col.DisplayIndex = displayIndex;
                col.AutoSizeMode = mode;
            }

            Setup("Status", 160, 0);
            Setup("DateCloseReg", 150, 1);
            Setup("RegNo", 200, 2);
            Setup("DateIssued", 150, 3);
            Setup("IssueGroup", 220, 4);
            Setup("SectionID", 150, 5);
            Setup("ModelNo", 180, 6);
            Setup("Quantity", 100, 7);
            Setup("Contents", 150, 8, DataGridViewAutoSizeColumnMode.Fill);
            Setup("Edit", 100, 9);
            Setup("Delete", 100, 10);

            RejectedGrid.ResumeLayout();
            _gridConfigured = true;
        }

  
        public async Task DisplayRejected(int proc)
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
              
                RejectedGrid.SuspendLayout();

                var shipTask = _ship.GetRejectedShipData(
                    sectionfilter.SelectedIndex,
                    filteritems.SelectedIndex, 
                    proc, 0, 0);

                var pieTask = _ship.GetCustomersOpenItem(filteritems.SelectedIndex, proc, sectionfilter.SelectedIndex);

                await Task.WhenAll(shipTask, pieTask);

                var shipList = shipTask.Result;
                var pieData = pieTask.Result;

                if (!_gridConfigured)
                    ConfigureGrid();

                RejectedGrid.DataSource = shipList;

                UpdateStatusChart(shipList);
                UpdatePieChart(pieData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RejectedGrid.ResumeLayout();
                _isLoading = false;
            }
        }

        private void UpdateStatusChart(List<RejectShipmentModel> list)
        {
            if (list == null || list.Count == 0)
            {
                cartesianChart1.Series = new SeriesCollection();
                return;
            }

            int circulation = 0, report = 0, open = 0;

            foreach (var item in list)
            {
                switch (item.Status)
                {
                    case 3: circulation++; break;
                    case 2: report++; break;
                    case 1: open++; break;
                }
            }

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Circulation",
                    Values = new ChartValues<int> { circulation },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Orange,
                    MaxColumnWidth = 200,
                    ColumnPadding = 50
                },
                new ColumnSeries
                {
                    Title = "Report",
                    Values = new ChartValues<int> { report },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Yellow,
                    MaxColumnWidth = 200,
                    ColumnPadding = 50
                },
                new ColumnSeries
                {
                    Title = "Open",
                    Values = new ChartValues<int> { open },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Green,
                    MaxColumnWidth = 200,
                    ColumnPadding = 50
                }
            };

            cartesianChart1.AxisX.Clear();
           

            cartesianChart1.AxisY.Clear();

            cartesianChart1.LegendLocation = LegendLocation.Bottom;
            cartesianChart1.ForeColor = System.Drawing.Color.White;

            cartesianChart1.DisableAnimations = list.Count > 2000;
        }

        private void UpdatePieChart(List<CustomerTotalModel> cc)
        {
            if (cc == null || cc.Count == 0)
            {
                pieChart1.Series = new SeriesCollection();
                return;
            }

            var series = new SeriesCollection();

            foreach (var d in cc)
            {
                if (d.totalOpen <= 0) continue;

                series.Add(new PieSeries
                {
                    Title = d.DepartmentName,
                    Values = new ChartValues<int> { d.totalOpen },
                    DataLabels = true,
                    Fill = GetDepartmentColor(d.DepartmentName),
                    LabelPoint = cp => $"{cp.Y} ({cp.Participation:P0})"
                });
            }

            pieChart1.Series = series;

            pieChart1.LegendLocation = LegendLocation.Right;
            pieChart1.InnerRadius = 70;
            pieChart1.DisableAnimations = cc.Sum(x => x.totalOpen) > 2000;

            pieChart1.Update();
        }

        private Brush GetDepartmentColor(string dept)
        {
            switch (dept.ToLower())
            {
                case "molding":
                    return Brushes.Pink;

                case "press":
                    return Brushes.Blue;

                case "rotor":
                    return Brushes.Yellow;

                case "winding":
                    return Brushes.Green;

                case "circuit":
                    return Brushes.White;

                default:
                    return Brushes.Gray;
            }
        }

        private string GetStatusLabel(int status)
        {
            switch (status)
            {
                case 1: return "Open";
                case 0: return "Close / Completed";
                case 2: return "Report OK";
                case 3: return "For Circulation";
                default: return "Unknown";
            }
        }

        private void RejectedGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            if (RejectedGrid.Columns[e.ColumnIndex].Name == "SectionID")
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "P1SA MOLDING";
                        e.FormattingApplied = true;
                        break;
                    case "2":
                        e.Value = "P1SA PRESS";
                        e.FormattingApplied = true;
                        break;
                    case "3":
                        e.Value = "P1SA ROTOR";
                        e.FormattingApplied = true;
                        break;
                    case "4":
                        e.Value = "P1SA WINDING";
                        e.FormattingApplied = true;
                        break;
                    default:
                        e.Value = "P1SA CIRCUIT";
                        e.FormattingApplied = true;
                        break;
                }
            }
            else if (RejectedGrid.Columns[e.ColumnIndex].Name == "Status")
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(78, 166, 101);
                        e.Value = "Open";
                        break;
                    case "0":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(184, 94, 104);
                        e.Value = "Close / Completed";
                        break;
                    case "2":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(173, 171, 59);
                        e.Value = "Report Ok";
                        break;
                    case "3":
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(219, 137, 37);
                        e.Value = "For Circulation";
                        break;
                    default:
                        e.Value = "Unknown Status";
                        break;
                }

                e.FormattingApplied = true;
            }
        }

  
        private void ShipRejected_Load(object sender, EventArgs e)
        {
        }


        private async void OpenReject_Click(object sender, EventArgs e)
        {
            using (var add = new AddShipment(_ship, 1))
            {
                if (add.ShowDialog(this) == DialogResult.OK)
                {
                    await DisplayRejected(1);
                }
            }
        }

        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
         => await HandleFilterChange(1);

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HandleFilterChange(1);
        }

        private async Task HandleFilterChange(int process)
        {
            if (_isInitializing) return;

            await DisplayRejected(process);
        }



        private void RejectedGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }


        public void DisplayPieChart(List<CustomerTotalModel> cc)
        {
            //resetDisplayText();

            pieChart1.Width = 650;   
            pieChart1.Height = 450;  


            if (cc == null || cc.Count == 0)
            {
                pieChart1.Series = new LiveCharts.SeriesCollection();
                return;
            }

            var seriesCollection = new LiveCharts.SeriesCollection();

            // Department color mapping
            var deptColors = new Dictionary<string, System.Windows.Media.Brush>
            {
                { "Molding", Brushes.DodgerBlue },
                { "Press", Brushes.Orange },
                { "Rotor", Brushes.SeaGreen },
                { "Winding", Brushes.Gold },
                { "Circuit", Brushes.MediumPurple }
            };

            foreach (var d in cc)
            {
                if (d.totalOpen <= 0) continue;

                var pie = new PieSeries
                {
                    Title = d.DepartmentName,
                    Values = new ChartValues<int> { d.totalOpen },
                    DataLabels = true,
                    LabelPoint = chartPoint =>
                        $"{chartPoint.SeriesView.Title}\n{chartPoint.Y} ({chartPoint.Participation:P1})",
                    FontSize = 12
                };

                // Apply custom color if exists
                if (deptColors.ContainsKey(d.DepartmentName))
                    pie.Fill = deptColors[d.DepartmentName];

                seriesCollection.Add(pie);

            }

            pieChart1.Series = seriesCollection;

            // Doughnut Style
            pieChart1.InnerRadius = 70;

            // Animation
            pieChart1.DisableAnimations = false;
            pieChart1.LegendLocation = LegendLocation.Right;
        }

        
    }
}
