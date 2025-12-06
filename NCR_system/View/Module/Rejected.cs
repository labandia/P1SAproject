using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows;

namespace NCR_system.View.Module
{
    public partial class Rejected : UserControl
    {
        private readonly IShipRejected _ship;

        public DataGridView Customgrid { get { return RejectedGrid; } }
     

        public Rejected(IShipRejected ship)
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
                var rejectlist = (await _ship.GetRejectedShipData(proc)).ToList();
                RejectedGrid.DataSource = rejectlist;


                RejectedGrid.Columns["RegNo"].DisplayIndex = 0;
                RejectedGrid.Columns["RegNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["RegNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                RejectedGrid.Columns["DateIssued"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["DateIssued"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["DateIssued"].DisplayIndex = 1;
                RejectedGrid.Columns["DateIssued"].Width = 120;

                RejectedGrid.Columns["IssueGroup"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["IssueGroup"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["IssueGroup"].Width = 150;
                RejectedGrid.Columns["IssueGroup"].DisplayIndex = 2;


                RejectedGrid.Columns["SectionID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["SectionID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["SectionID"].DisplayIndex = 3;
                RejectedGrid.Columns["SectionID"].Width = 150;


                RejectedGrid.Columns["ModelNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["ModelNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["ModelNo"].DisplayIndex = 4;
                RejectedGrid.Columns["ModelNo"].Width = 150;

                RejectedGrid.Columns["Quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Quantity"].DisplayIndex = 5;
                RejectedGrid.Columns["Quantity"].Width = 100;

                RejectedGrid.Columns["Contents"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Contents"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Contents"].DisplayIndex = 6;
                RejectedGrid.Columns["Contents"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                RejectedGrid.Columns["DateCloseReg"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["DateCloseReg"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["DateCloseReg"].DisplayIndex = 7;
                RejectedGrid.Columns["DateCloseReg"].Width = 150;

                RejectedGrid.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Status"].DisplayIndex = 8;
                RejectedGrid.Columns["Status"].Width = 100;

                RejectedGrid.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Edit"].DisplayIndex = 9;
                RejectedGrid.Columns["Edit"].Width = 100;


                RejectedGrid.Columns["Delete"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RejectedGrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                RejectedGrid.Columns["Delete"].DisplayIndex = 10;
                RejectedGrid.Columns["Delete"].Width = 100;



                // 🔹 Define all known sections
                var sections = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Molding"),
                    new KeyValuePair<int, string>(2, "Press"),
                    new KeyValuePair<int, string>(3, "Rotor"),
                    new KeyValuePair<int, string>(4, "Winding"),
                    new KeyValuePair<int, string>(5, "Circuit")
                };


                var countItems = await _ship.GetCustomersOpenItem(proc);
                int kk = 0;

                foreach (var items in countItems)
                {
                    kk++;
                    outputData.Add(items.totalOpen);
                    outputData2.Add(items.TotalClosed);
                    Debug.WriteLine(kk);
                }


                //foreach(var number in outputData2)
                //{
                //    Debug.WriteLine(number);
                //}



                DisplayCharts(outputData, outputData2);
                //cartesianChart1.Series = new SeriesCollection();
                //cartesianChart1.AxisX.Clear();
                //cartesianChart1.AxisY.Clear();


                //cartesianChart1.Series = new SeriesCollection
                //{
                //    new ColumnSeries
                //    {
                //        Title = "Open Items",
                //        Values = new ChartValues<int>(outputData),
                //         MaxColumnWidth = 150,   // 🔥 Bar size increased
                //    },
                //     new ColumnSeries
                //    {
                //        Title = "Production B",
                //        Values = new ChartValues<int>(outputData2),
                //         MaxColumnWidth = 150,          // optional color
                //    }
                //};

                //    cartesianChart1.AxisX.Add(new Axis
                //    {
                //        Title = "",
                //        Labels = new[] { "Molding", "Press", "Rotor", "Winding", "Circuit" },
                //        Foreground = System.Windows.Media.Brushes.Black,              // label color
                //        FontFamily = new System.Windows.Media.FontFamily("Segoe UI"), // label font
                //        FontSize = 11,
                //        FontWeight = FontWeights.Bold,      // <-- Added
                //        Separator = new Separator
                //        {
                //            Step = 1,
                //            Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 180, 180, 180)),
                //            StrokeThickness = 1
                //        }
                //    });

                //    cartesianChart1.AxisY.Add(new Axis
                //    {
                //        Title = "Total Open Items",
                //        LabelFormatter = value => value.ToString("N0"),
                //        Foreground = System.Windows.Media.Brushes.Black,              // label color
                //        FontFamily = new System.Windows.Media.FontFamily("Segoe UI"), // label font
                //        FontSize = 9,
                //        FontWeight = FontWeights.Bold,      // <-- Added
                //        Separator = new Separator
                //        {
                //            Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 180, 180, 180)),         // line color
                //            StrokeThickness = 1,                 // line thickness
                //            StrokeDashArray = new DoubleCollection { 2 }  // <-- FIXED
                //        }
                //    });

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


            var addmodel = new AddShipment(_ship, 0,  _shipcontrol, this);
            addmodel.ShowDialog();
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
                // Handle Edit image click
                var openedit = new EditRejected(_ship, this, Convert.ToInt32(recordID), processtype);
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

        private void Rejected_Load(object sender, EventArgs e)
        {
          
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


        public void DisplayCharts(List<int> outputData, List<int> outputData2)
        {
            // RESET the chart fully
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

            // ----------------- SERIES (DOUBLE BAR) -----------------
            cartesianChart1.Series = new SeriesCollection
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
            cartesianChart1.AxisX.Add(new Axis
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
            cartesianChart1.AxisY.Add(new Axis
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
