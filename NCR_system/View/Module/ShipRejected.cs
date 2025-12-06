using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;   // Yes, WPF namespace is normal for LiveCharts WinForms
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;


namespace NCR_system.View.Module
{
    public partial class ShipRejected : UserControl
    {
        private readonly IShipRejected _ship;

        public DataGridView Customgrid { get { return RejectedGrid; } }
        List<int> outputData = new List<int>();


        public ShipRejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;
        }

        public async Task DisplayRejected(int proc)
        {
            try
            {
                List<int> outputData = new List<int> { };
                List<int> outputData2 = new List<int> { };

                // For Displaying Customer
                var ShipList = (await _ship.GetRejectedShipData(proc)).ToList();
                RejectedGrid.DataSource = ShipList;


                RejectedGrid.Columns["RegNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["RegNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["RegNo"].DisplayIndex = 0;
                RejectedGrid.Columns["RegNo"].Width = 200;

                RejectedGrid.Columns["DateIssued"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["DateIssued"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["DateIssued"].Width = 150;
                RejectedGrid.Columns["DateIssued"].DisplayIndex = 1;

                RejectedGrid.Columns["IssueGroup"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["IssueGroup"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["IssueGroup"].Width = 200;
                RejectedGrid.Columns["IssueGroup"].DisplayIndex = 2;


                RejectedGrid.Columns["SectionID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["SectionID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["SectionID"].Width = 150;
                RejectedGrid.Columns["SectionID"].DisplayIndex = 3;

                RejectedGrid.Columns["ModelNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["ModelNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                RejectedGrid.Columns["ModelNo"].DisplayIndex = 4;

                RejectedGrid.Columns["Quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Quantity"].DisplayIndex = 5;


                RejectedGrid.Columns["Contents"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Contents"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                RejectedGrid.Columns["Contents"].DisplayIndex = 6;

                RejectedGrid.Columns["DateCloseReg"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["DateCloseReg"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["DateCloseReg"].Width = 150;
                RejectedGrid.Columns["DateCloseReg"].DisplayIndex = 7;

                RejectedGrid.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Status"].Width = 200;
                RejectedGrid.Columns["Status"].DisplayIndex = 8;

                RejectedGrid.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Edit"].Width = 100;
                RejectedGrid.Columns["Edit"].DisplayIndex = 9;

                RejectedGrid.Columns["Delete"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Delete"].Width = 100;
                RejectedGrid.Columns["Delete"].DisplayIndex = 10;

                var countItems = await _ship.GetCustomersOpenItem(proc);

                foreach (var items in countItems)
                {
                    outputData.Add(items.totalOpen);
                    outputData2.Add(items.TotalClosed);
                }


                DisplayCharts(outputData, outputData2);

                //var countItems = await _ship.GetCustomersOpenItem(proc);


                //foreach (var items in countItems)
                //{
                //    outputData.Add(items.totalOpen);

                //    switch (items.DepartmentName)
                //    {
                //        case "Molding":
                //            MoldText.Text = items.totalOpen.ToString();
                //            break;
                //        case "Press":
                //            PressText.Text = items.totalOpen.ToString();
                //            break;
                //        case "Rotor":
                //            RotorText.Text = items.totalOpen.ToString();
                //            break;
                //        case "Winding":
                //            WindText.Text = items.totalOpen.ToString();
                //            break;
                //        default:
                //            CircuitText.Text = items.totalOpen.ToString();
                //            break;
                //    }

                //}


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
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

        private void OpenShip_Click(object sender, EventArgs e)
        {
            var rej = new Rejected(_ship);

            var openmodel = new AddShipment(_ship, 1,  this, rej);
            openmodel.ShowDialog();
        }

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
                int processtype = Convert.ToInt32(type);
                // You can get the row data like this:
                var openedit = new EditShipments(_ship, this, Convert.ToInt32(recordID), processtype);
                openedit.ShowDialog();

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

        private void ShipRejected_Load(object sender, EventArgs e)
        {
            //cartesianChart1.Series = new SeriesCollection
            //{
            //    new ColumnSeries
            //    {
            //        Title = "Production",
            //        Values =  new ChartValues<int>(outputData),
            //         MaxColumnWidth = 150,   // 🔥 Bar size increased
            //    }
            //};

            //cartesianChart1.AxisX.Add(new Axis
            //{
            //    Title = "",
            //    Labels = new[] { "Press", "Molding", "Rotor", "Winding", "Circuit" }
            //});

            //cartesianChart1.AxisY.Add(new Axis
            //{
            //    Title = "Total Open Items",
            //    LabelFormatter = value => value.ToString("N0")
            //});
        }

        public void DisplayCharts(List<int> outputData, List<int> outputData2)
        {
            // RESET the chart fully
            ShipmentChart.Series.Clear();
            ShipmentChart.AxisX.Clear();
            ShipmentChart.AxisY.Clear();

            // ----------------- SERIES (DOUBLE BAR) -----------------
            ShipmentChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Open Items",
                    Values = new ChartValues<int>(outputData),
                    MaxColumnWidth = 150,
                    DataLabels = true,                            // <--- VALUE LABELS
                    LabelPoint = p => p.Y.ToString("N0"),

                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(66, 133, 244)), // Blue
                    ColumnPadding = 20
                },
                new ColumnSeries
                {
                    Title = "Closed Items",
                    Values = new ChartValues<int>(outputData2),
                    MaxColumnWidth = 150,

                     DataLabels = true,                            // <--- VALUE LABELS
                    LabelPoint = p => p.Y.ToString("N0"),

                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 128, 0)),  // Orange
                    ColumnPadding = 20                               // <--- spacing inside the group
                }
            };

            // ----------------- AXIS X (CATEGORIES) -----------------
            ShipmentChart.AxisX.Add(new Axis
            {
                Title = "",
                Labels = new[] { "Molding", "Press", "Rotor", "Winding", "Circuit" },

                Foreground = System.Windows.Media.Brushes.Black,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                FontSize = 11,
                FontWeight = FontWeights.Bold,

                Separator = new Separator
                {
                    Step = 1,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 180, 180, 180)),
                    StrokeThickness = 1
                }
            });

            // ----------------- AXIS Y (VALUES) -----------------
            ShipmentChart.AxisY.Add(new Axis
            {
                Title = "",
                LabelFormatter = value => value.ToString("N0"),

                Foreground = System.Windows.Media.Brushes.Black,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                FontSize = 9,
                FontWeight = FontWeights.Bold,

                Separator = new Separator
                {
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 180, 180, 180)),
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection { 1 }
                }
            });

        }





    }
}
