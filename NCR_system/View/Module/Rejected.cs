using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using NCR_system.Models;
using System.Windows.Forms.DataVisualization.Charting;
using Brushes = System.Windows.Media.Brushes;
using SeriesCollection = LiveCharts.SeriesCollection;
using System.Linq;
using Axis = LiveCharts.Wpf.Axis;

namespace NCR_system.View.Module
{
    public partial class Rejected : UserControl
    {
        private readonly IShipRejected _ship;

        private bool _gridConfigured = false;
        private bool _isLoading = false;

        public int depId { get; set; } = 0;
        public int stats { get; set; } = 0;

        bool isSelectSection = false;
        bool isSelectStatus = false;
        bool isChart = false;

        public DataGridView Customgrid => RejectedGrid;

        List<int> outputData = new List<int>();
        // Reuse collections to reduce allocations
        private readonly ChartValues<int> _statusValues = new ChartValues<int>();


        public Rejected(IShipRejected ship)
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

                UpdateBarChart(pieData);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                RejectedGrid.ResumeLayout();
                _isLoading = false;
            }
        }

        private void UpdateBarChart(List<CustomerTotalModel> cc)
        {
            if (cc == null || cc.Count == 0)
            {
                cartesianChart1.Series = new SeriesCollection();
                return;
            }

            var values = new ChartValues<int>();
            var labels = new List<string>();

            foreach (var d in cc)
            {
                if (d.totalOpen <= 0) continue;

                values.Add(d.totalOpen);
                labels.Add(d.DepartmentName);
            }

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Open Total",
                    Values = values,
                    DataLabels = true,
                    LabelPoint = point => point.Y.ToString()
                }
            };

            // X Axis (Department Names)
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Department",
                Labels = labels,
                Foreground = System.Windows.Media.Brushes.White
            });

            // Y Axis (Values)
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Total Open",
                LabelFormatter = value => value.ToString("N0"),
                Foreground = System.Windows.Media.Brushes.White
            });

            //cartesianChart1.LegendLocation = LegendLocation.Right;

            cartesianChart1.DisableAnimations = cc.Sum(x => x.totalOpen) > 2000;
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
                        e.Value = "Open";
                        break;
                    case "0":
                        e.Value = "Close / Completed";
                        break;
                    case "2":
                        e.Value = "Report Ok";
                        break;
                    case "3":
                        e.Value = "For Circulation";
                        break;
                    default:
                        e.Value = "Unknown Status";
                        break;
                }

                e.FormattingApplied = true;
            }
        }

        private void OpenReject_Click(object sender, EventArgs e)
        {
            var _shipcontrol = new ShipRejected(_ship);

            using (var add = new AddShipment(_ship, 0, _shipcontrol, this))
            {
                add.StartPosition = FormStartPosition.CenterParent;
                add.ShowDialog(this);   // <-- modal + always in front of parent
            }
        }

        private async void RejectedGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure user clicked on a valid row (not header)
            if (e.RowIndex < 0)
                return;

            var RejectData = (RejectShipmentModel)RejectedGrid.Rows[e.RowIndex].DataBoundItem;

            // Get the clicked column
            var column = RejectedGrid.Columns[e.ColumnIndex];

            // Get the clicked row
            var row = RejectedGrid.Rows[e.RowIndex];

            var recordID = row.Cells["RecordID"].Value;
            var type = row.Cells["Process"].Value;


            if (column.Name == "Edit")
            {
               
                using (var openedit = new EditRejected(_ship, this, RejectData))
                {
                    openedit.StartPosition = FormStartPosition.CenterParent;
                  
                    if (openedit.ShowDialog(this) == DialogResult.OK)
                    {
                        MessageBox.Show("Update successful.");
                        await DisplayRejected(0);

                    }

                }

            }
            else if (column.Name == "Delete")
            {
                // Handle Delete image click
                DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Remove the row or perform deletion
                    System.Windows.Forms.MessageBox.Show($"Delete clicked on row {e.RowIndex} - Record ID selected:  {recordID}");
                }
            }
        }

        private void Rejected_Load(object sender, EventArgs e)
        {
            sectionfilter.SelectedIndex = 0;
            filteritems.SelectedIndex = 0;
        }

        private void RejectedGrid_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
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
                string checkstats = e.Value.ToString() == "1" ? "Open" : "Close";
                e.Value = checkstats;

                if (checkstats == "Open")
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.White;
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(78, 166, 101);
                }
                else
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.White;
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(184, 94, 104);
                }


                e.FormattingApplied = true;
            }
        }






        public void resetDisplayText()
        {
            moldval.Text = "0";
            Pressval.Text = "0";
            Rotorval.Text = "0";
            windingval.Text = "0";
            Circuitval.Text = "0";
        }

        public void DisplayLabelText(string depart, int count)
        {
            Debug.WriteLine($" - {depart}: {count}");
            switch (depart)
            {
                case "Molding":
                    moldval.Text = count.ToString();
                    break;
                case "Press":
                    Pressval.Text = count.ToString();
                    break;
                case "Rotor":
                    Rotorval.Text = count.ToString();
                    break;
                case "Winding":
                    windingval.Text = count.ToString();
                    break;
                default:
                    Circuitval.Text = count.ToString();
                    break;
            }
        }



        private string GetStatusLabel(int status)
        {
            switch (status)
            {
                case 1:
                    return "Open";
                case 0:
                    return "Close / Completed";
                case 2:
                    return "Report OK";
                case 3:
                    return "For Circulation";
                default:
                    return "Unknown";
            }


        }

        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e) => await DisplayRejected(0);
        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e) => await DisplayRejected(0);
    }
}