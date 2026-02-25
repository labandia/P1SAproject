using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Windows.Forms;
using NCR_system.Models;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using Color = System.Drawing.Color;
using System.Diagnostics;
using System.Linq;
using System.Windows.Markup;


namespace NCR_system.View.Module
{
    public partial class ShipRejected : UserControl
    {
        private readonly IShipRejected _ship;

        private bool _gridConfigured = false;
        private bool _isLoading = false;

        public int depId { get; set; }
        public int stats { get; set; }

        bool isSelectSection = false;
        bool isSelectStatus = false;
        bool isChart = false;



        public DataGridView Customgrid { get { return RejectedGrid; } }
        List<int> outputData = new List<int>();


        public ShipRejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;

            EnableDoubleBuffering();
            InitializeCharts();
        }

        private void EnableDoubleBuffering()
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(RejectedGrid, true, null);

            typeof(Chart)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(chart1, true, null);

            typeof(Chart)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(chartStatus, true, null);
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

        private void InitializeCharts()
        {
            if (chart1.ChartAreas.Count == 0)
            {
                ChartArea area = new ChartArea();
                area.BackColor = Color.Transparent;
                chart1.ChartAreas.Add(area);
            }

            if (chartStatus.ChartAreas.Count == 0)
            {
                ChartArea area = new ChartArea();
                area.AxisX.Interval = 1;
                area.AxisX.LabelStyle.Angle = -90;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartStatus.ChartAreas.Add(area);
            }
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
                chart1.SuspendLayout();
                chartStatus.SuspendLayout();

                // For Displaying Customer
                var ShipList = await _ship.GetRejectedShipData(depId, stats, proc, 0, 0);
                DisplayStatusChartFromList(ShipList);

                if (!_gridConfigured)
                    ConfigureGrid();

                DisplayStatusChart(ShipList);

                var countItems = await _ship.GetCustomersOpenItem(proc, depId);
                DisplayPieChart(countItems);


                RejectedGrid.DataSource = ShipList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RejectedGrid.ResumeLayout();
                chart1.ResumeLayout();
                chartStatus.ResumeLayout();
                _isLoading = false;
            }
        }

        private void DisplayStatusChart(List<RejectShipmentModel> list)
        {
            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach (var item in list)
            {
                if (!counts.ContainsKey(item.Status))
                    counts[item.Status] = 0;

                counts[item.Status]++;
            }

            chartStatus.Series.Clear();
            var series = new Series("Status")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            int[] allStatuses = { 3, 0, 2, 1 };

            foreach (var status in allStatuses)
            {
                int count = counts.ContainsKey(status) ? counts[status] : 0;
                int index = series.Points.AddXY(GetStatusLabel(status), count);

                switch (status)
                {
                    case 3: series.Points[index].Color = Color.SteelBlue; break;
                    case 0: series.Points[index].Color = Color.DarkGray; break;
                    case 2: series.Points[index].Color = Color.SeaGreen; break;
                    case 1: series.Points[index].Color = Color.OrangeRed; break;
                }
            }

            chartStatus.Series.Add(series);
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
        {
            await DisplayRejected(1);
        }

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            await DisplayRejected(1);
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
            resetDisplayText();

            chart1.Series.Clear();

            Series series = new Series("Open Items")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = false
            };

            series["DoughnutRadius"] = "65";

            Dictionary<string, Color> deptColors = new Dictionary<string, Color>
            {
                { "Molding", Color.DodgerBlue },
                { "Press", Color.Orange },
                { "Rotor", Color.Green },
                { "Winding", Color.Gold },
                { "Circuit", Color.Aqua }
            };

            foreach (var d in cc)
            {
                if (d.totalOpen <= 0) continue;

                int index = series.Points.AddY(d.totalOpen);
                series.Points[index].LegendText = d.DepartmentName;

                if (deptColors.ContainsKey(d.DepartmentName))
                    series.Points[index].Color = deptColors[d.DepartmentName];

                DisplayLabelText(d.DepartmentName, d.totalOpen);
            }

            chart1.Series.Add(series);
        }


        private void DisplayLabelText(string dept, int count)
        {
            switch (dept)
            {
                case "Molding": moldval.Text = count.ToString(); break;
                case "Press": Pressval.Text = count.ToString(); break;
                case "Rotor": Rotorval.Text = count.ToString(); break;
                case "Winding": windingval.Text = count.ToString(); break;
                default: Circuitval.Text = count.ToString(); break;
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
            Series series = new Series("Status");
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
    }
}
