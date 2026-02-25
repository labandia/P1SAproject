using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;


namespace NCR_system.View.Module
{
    public partial class ShipRejected : UserControl
    {
        private readonly IShipRejected _ship;

        private bool _gridConfigured = false;
        private bool _isLoading = false;

        public int depId { get; set; }
        public int stats { get; set; }

        public DataGridView Customgrid => RejectedGrid;

        // Reuse collections to reduce allocations
        private readonly ChartValues<int> _statusValues = new ChartValues<int>();
        private readonly List<string> _statusLabels = new List<string> { "For Circulation", "Close / Completed", "Report OK", "Open" };


        public ShipRejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;
            EnableDoubleBuffering();
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

            Setup("DateCloseReg", 150, 0);
            Setup("RegNo", 200, 1);
            Setup("DateIssued", 150, 2);
            Setup("IssueGroup", 200, 3);
            Setup("SectionID", 150, 4);
            Setup("ModelNo", 150, 5, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Quantity", 100, 6);
            Setup("Contents", 150, 7, DataGridViewAutoSizeColumnMode.DisplayedCells);
            Setup("Status", 200, 8);
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
                depId = sectionfilter.SelectedIndex > 0 ? sectionfilter.SelectedIndex : 0;
                stats = filteritems.SelectedIndex > 0 ? filteritems.SelectedIndex : 0;

                RejectedGrid.SuspendLayout();

                var shipTask = _ship.GetRejectedShipData(depId, stats, proc, 0, 0);
                var pieTask = _ship.GetCustomersOpenItem(proc, depId);

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

            // Faster than LINQ GroupBy
            int forCirculation = 0, close = 0, reportOk = 0, open = 0;

            foreach (var item in list)
            {
                switch (item.Status)
                {
                    case 3: forCirculation++; break;
                    case 0: close++; break;
                    case 2: reportOk++; break;
                    case 1: open++; break;
                }
            }

            _statusValues.Clear();
            _statusValues.Add(forCirculation);
            _statusValues.Add(close);
            _statusValues.Add(reportOk);
            _statusValues.Add(open);

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = _statusValues,
                    DataLabels = true,
                    Fill = Brushes.SteelBlue
                }
            };

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Labels = _statusLabels,
                LabelsRotation = 20
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis { Title = "Count" });

            cartesianChart1.DisableAnimations = list.Count > 2000; // disable animation for big datasets
        }

        private void UpdatePieChart(List<CustomerTotalModel> cc)
        {
            //resetDisplayText();

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
                    LabelPoint = cp => $"{cp.Y} ({cp.Participation:P0})"
                });

                //DisplayLabelText(d.DepartmentName, d.totalOpen);
            }

            pieChart1.Series = series;
            pieChart1.InnerRadius = 70;
            pieChart1.DisableAnimations = cc.Sum(x => x.totalOpen) > 2000;
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
            sectionfilter.SelectedIndex = 0;
            filteritems.SelectedIndex = 0;
        }


        private void OpenReject_Click(object sender, EventArgs e)
        {
            var rej = new Rejected(_ship);

            using (var add = new AddShipment(_ship, 1, this, rej))
            {
                add.StartPosition = FormStartPosition.CenterParent;
                add.ShowDialog(this);   // <-- modal + always in front of parent
            }
        }

        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
         => await DisplayRejected(1);

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
            => await DisplayRejected(1);

        private void RejectedGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure user clicked on a valid row (not header)
            if (e.RowIndex < 0)
                return;


            // Get the clicked column
            var column = RejectedGrid.Columns[e.ColumnIndex];

            // Get the clicked row
            var row = RejectedGrid.Rows[e.RowIndex];

            var recordID = row.Cells["RecordID"].Value;
            var type = row.Cells["Process"].Value;

            if (column.Name == "Edit")
            {
                using (var openedit = new EditShipments(_ship, this, Convert.ToInt32(recordID), Convert.ToInt32(type)))
                {
                    openedit.StartPosition = FormStartPosition.CenterParent;
                    openedit.ShowDialog(this);   // <-- modal + always in front of parent
                }
            }
            else if (column.Name == "Delete")
            {
                // Handle Delete image click
                DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Remove the row or perform deletion
                   MessageBox.Show($"Delete clicked on row {e.RowIndex} - Record ID selected:  {recordID}");
                }
            }
        }


        public void DisplayPieChart(List<CustomerTotalModel> cc)
        {
            //resetDisplayText();

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

                // Update text labels
                //DisplayLabelText(d.DepartmentName, d.totalOpen);
            }

            pieChart1.Series = seriesCollection;

            // Doughnut Style
            pieChart1.InnerRadius = 70;

            // Animation
            pieChart1.DisableAnimations = false;
            pieChart1.LegendLocation = LegendLocation.Right;
        }


        //private void DisplayLabelText(string dept, int count)
        //{
        //    switch (dept)
        //    {
        //        case "Molding": moldval.Text = count.ToString(); break;
        //        case "Press": Pressval.Text = count.ToString(); break;
        //        case "Rotor": Rotorval.Text = count.ToString(); break;
        //        case "Winding": windingval.Text = count.ToString(); break;
        //        default: Circuitval.Text = count.ToString(); break;
        //    }
        //}

        //private void resetDisplayText()
        //{
        //    moldval.Text = Pressval.Text = Rotorval.Text =
        //    windingval.Text = Circuitval.Text = "0";
        //}

    }
}
