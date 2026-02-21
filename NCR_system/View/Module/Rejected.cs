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
using NCR_system.Models;
using System.Windows.Forms.DataVisualization.Charting;
using Color = System.Drawing.Color;

namespace NCR_system.View.Module
{
    public partial class Rejected : UserControl
    {
        private readonly IShipRejected _ship;

        public int depId { get; set; } = 0;
        public int stats { get; set; } = 0;

        bool isSelectSection = false;
        bool isSelectStatus = false;
        bool isChart = false;

        public DataGridView Customgrid { get { return RejectedGrid; } }
        List<int> outputData = new List<int>();

        public Rejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;
        }

        public async Task DisplayRejected(int proc)
        {
            depId = sectionfilter.SelectedIndex > 0 ? sectionfilter.SelectedIndex : 0;
            stats = filteritems.SelectedIndex > 0 ? filteritems.SelectedIndex : 0;

            try
            {
                List<int> outputData = new List<int> { };
                List<int> outputData2 = new List<int> { };

                // For Displaying Customer
                var rejectlist = await _ship.GetRejectedShipData(depId, stats, proc, 0, 0);
                RejectedGrid.DataSource = rejectlist;

                DisplayStatusChartFromList(rejectlist);
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



                var countItems = await _ship.GetCustomersOpenItem(proc, depId);
                DisplayPieChart(countItems);

                foreach (var items in countItems)
                {
                    outputData.Add(items.totalOpen);
                    outputData2.Add(items.TotalClosed);
                }


                //foreach(var number in outputData2)
                //{
                //    Debug.WriteLine(number);
                //}



                //DisplayCharts(outputData, outputData2);
                
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


        //public void DisplayCharts(List<int> outputData, List<int> outputData2)
        //{
        //    // RESET the chart fully
        //    cartesianChart1.Series.Clear();
        //    cartesianChart1.AxisX.Clear();
        //    cartesianChart1.AxisY.Clear();

        //    // ----------------- SERIES (DOUBLE BAR) -----------------
        //    cartesianChart1.Series = new SeriesCollection
        //    {
        //        new ColumnSeries
        //        {
        //            Title = "Open Items",
        //            Values = new ChartValues<int>(outputData),
        //            MaxColumnWidth = 150,
        //            DataLabels = true,                            // <--- VALUE LABELS
        //            LabelPoint = p => p.Y.ToString("N0"),

        //            Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(84, 130, 53)), // Green
        //            ColumnPadding = 20
        //        },
        //        new ColumnSeries
        //        {
        //            Title = "Closed Items",
        //            Values = new ChartValues<int>(outputData2),
        //            MaxColumnWidth = 150,

        //             DataLabels = true,                            // <--- VALUE LABELS
        //            LabelPoint = p => p.Y.ToString("N0"),

        //            Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(192, 0, 0)),  // Red
        //            ColumnPadding = 20                               // <--- spacing inside the group
        //        }
        //    };

        //    // ----------------- AXIS X (CATEGORIES) -----------------
        //    cartesianChart1.AxisX.Add(new Axis
        //    {
        //        Title = "",
        //        Labels = new[] { "Molding", "Press", "Rotor", "Winding", "Circuit" },

        //        Foreground = System.Windows.Media.Brushes.Black,
        //        FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
        //        FontSize = 11,
        //        FontWeight = FontWeights.Bold,

        //        Separator = new Separator
        //        {
        //            Step = 1,
        //            Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 180, 180, 180)),
        //            StrokeThickness = 1
        //        }
        //    });

        //    // ----------------- AXIS Y (VALUES) -----------------
        //    cartesianChart1.AxisY.Add(new Axis
        //    {
        //        Title = "",
        //        LabelFormatter = value => value.ToString("N0"),

        //        Foreground = System.Windows.Media.Brushes.Black,
        //        FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
        //        FontSize = 9,
        //        FontWeight = FontWeights.Bold,

        //        Separator = new Separator
        //        {
        //            Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 180, 180, 180)),
        //            StrokeThickness = 1,
        //            StrokeDashArray = new DoubleCollection { 1 }
        //        }
        //    });

        //}

        public void DisplayPieChart(List<CustomerTotalModel> cc)
        {
            resetDisplayText();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            // Chart Area
            ChartArea area = new ChartArea
            {
                BackColor = System.Drawing.Color.Transparent
            };
            chart1.ChartAreas.Add(area);

            // Doughnut Series
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Open Items")
            {
                ChartType = SeriesChartType.Doughnut, // ✅ CHANGE HERE
                IsValueShownAsLabel = false           // ✅ No labels on ring
            };

            // Doughnut hole size (0–99)
            series["DoughnutRadius"] = "65"; // ⭐ adjust thickness

            // Legend
            //Legend legend = new Legend
            //{
            //    Docking = Docking.Right,
            //     Alignment = StringAlignment.Center, // Top aligned
            //    LegendStyle = LegendStyle.Column, // Vertical
            //    BackColor = System.Drawing.Color.Transparent
            //};
            //chart1.Legends.Add(legend);
            // Department color map
            Dictionary<string, Color> deptColors = new Dictionary<string, Color>
            {
                { "Molding", Color.DodgerBlue },
                { "Press", Color.Orange },
                { "Rotor", Color.Green },
                { "Winding", Color.Yellow },
                { "Circuit", Color.Aqua }
            };

            // Add data
            foreach (var d in cc)
            {
                if (d.totalOpen > 0)
                {
                    int index = series.Points.AddY(d.totalOpen);

                    series.Points[index].LegendText = d.DepartmentName;

                    // ✅ Apply color
                    if (deptColors.ContainsKey(d.DepartmentName))
                        series.Points[index].Color = deptColors[d.DepartmentName];

                    DisplayLabelText(d.DepartmentName, d.totalOpen);
                }
            }

            chart1.Series.Add(series);

            // Layout
            chart1.Width = 420;
            chart1.Height = 320;

            area.Position.Auto = false;
            area.Position = new ElementPosition(5, 5, 65, 90);
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


        private void DisplayStatusChartFromList(List<RejectShipmentModel> shipList)
        {
            // Group data
            Dictionary<int, int> statusCounts = shipList
                .GroupBy(x => x.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            chartStatus.Series.Clear();
            chartStatus.ChartAreas.Clear();
            chartStatus.Legends.Clear();

            // Chart Area
            ChartArea area = new ChartArea();

            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -90;     // ← THIS is the vertical position
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            area.AxisX.MajorGrid.Enabled = false;

            area.AxisY.Title = "Count";
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // Axis label colors
            area.AxisX.LabelStyle.ForeColor = Color.White;
            area.AxisY.LabelStyle.ForeColor = Color.White;

            // Axis title color
            area.AxisX.TitleForeColor = Color.White;
            area.AxisY.TitleForeColor = Color.White;

            // Grid color (optional – subtle white)
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(60, Color.White);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(60, Color.White);

            chartStatus.ChartAreas.Add(area);

            // Series (Vertical Column Chart)
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Status");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            // Ensure ALL statuses appear
            int[] allStatuses = new int[] { 3, 0, 2, 1 };

            foreach (int status in allStatuses)
            {
                int count = statusCounts.ContainsKey(status)
                    ? statusCounts[status]
                    : 0;

                int index = series.Points.AddXY(GetStatusLabel(status), count);

                // ✔ OLD-SCHOOL switch (Framework safe)
                switch (status)
                {
                    case 3:
                        series.Points[index].Color = Color.SteelBlue;   // For Circulation
                        break;

                    case 0:
                        series.Points[index].Color = Color.DarkGray;    // Close
                        break;

                    case 2:
                        series.Points[index].Color = Color.SeaGreen;    // Report OK
                        break;

                    case 1:
                        series.Points[index].Color = Color.OrangeRed;   // Open
                        break;

                    default:
                        series.Points[index].Color = Color.LightGray;
                        break;
                }
            }

            chartStatus.Series.Add(series);
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

        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSelectSection = true;
            await DisplayRejected(0);
        }

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSelectStatus = true;
            await DisplayRejected(0);
        }
    }
}
