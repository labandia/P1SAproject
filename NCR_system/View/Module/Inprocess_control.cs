using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using NCR_system.View.AddForms;

namespace NCR_system.View.Module
{
    public partial class Inprocess_control : UserControl
    {
        private readonly IInprocess _inp;

        private bool _isInitializing = true;
        private bool _gridConfigured = false;
        private bool _isLoading = false;

        public int depId { get; set; } = 0;
        public int stats { get; set; } = 0;

        public DataGridView InprocessgridV2 => InprocessGrid;
        public List<CustomerModel> cuslist { get; private set; } = new List<CustomerModel>();
        private readonly Dictionary<string, Label> _departmentLabels;

        public Inprocess_control(IInprocess inp)
        {
            InitializeComponent();
            _inp = inp;
            
            _isInitializing = true;

            sectionfilter.SelectedIndex = 0;
            filteritems.SelectedIndex = 1;

            _departmentLabels = new Dictionary<string, Label>(StringComparer.OrdinalIgnoreCase)
            {
                { "Molding", moldval },
                { "Press", Pressval },
                { "Rotor", Rotorval },
                { "Winding", windingval },
                { "Circuit", Circuitval }
            };

            EnableDoubleBuffering();

            _isInitializing = false;
        }

        // =========================================================
        // LOADS DATA PAGE
        // =========================================================
        private void EnableDoubleBuffering()
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(InprocessGrid, true, null);

            InprocessGrid.EnableHeadersVisualStyles = false;
            InprocessGrid.RowHeadersVisible = false;
        }

        private void ConfigureGrid()
        {
            if (_gridConfigured) return;

            InprocessGrid.AutoGenerateColumns = false;
            InprocessGrid.SuspendLayout();

            void Setup(string name, int width, int displayIndex,
               DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.None)
            {
                var col = InprocessGrid.Columns[name];
                if (col == null) return;

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.Width = width;
                col.DisplayIndex = displayIndex;
                col.AutoSizeMode = mode;
            }

            //Setup("RecordID", 150, 0);
            //InprocessGrid.Columns["RecordID"].Visible = false;
            //InprocessGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            Setup("Status", 150, 0);
            Setup("RecordID", 150, 1);
            Setup("DateEncounter", 150, 2);
            Setup("ProcEncounter", 150, 3);
            Setup("TitleEmail", 150, 4, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Invest", 150, 5, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("cause", 150, 6, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Model", 150, 7, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Defect", 150, 8, DataGridViewAutoSizeColumnMode.AllCells);

            Setup("P1saStatus", 150, 9);
            Setup("Shift", 150, 10);
            Setup("Line", 150, 11);
            Setup("NGQty", 150, 12);
            Setup("SectionDep", 150, 13);
            Setup("ShopOrder", 150, 14, DataGridViewAutoSizeColumnMode.AllCells);

            InprocessGrid.Columns["P1saStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InprocessGrid.Columns["Shift"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InprocessGrid.Columns["cause"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InprocessGrid.Columns["Line"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InprocessGrid.Columns["NGQty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InprocessGrid.Columns["SectionDep"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            InprocessGrid.ResumeLayout();
            _gridConfigured = true;
        }


        public async Task DisplayRejected()
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                string search = searchText.Text.Trim();
            
                InprocessGrid.SuspendLayout();
                var inprocesslist = _inp.GetInprocessData(
                    search, 
                    sectionfilter.SelectedIndex, 
                    filteritems.SelectedIndex, 0, 0);

                var pieTask = _inp.GetCustomersOpenItem(
                    filteritems.SelectedIndex, 
                    sectionfilter.SelectedIndex);

                await Task.WhenAll(inprocesslist, pieTask);

                var CusList = inprocesslist.Result;
                var pieData = pieTask.Result;

                if (!_gridConfigured)
                    ConfigureGrid();

                InprocessGrid.DataSource = CusList;

                UpdateBarChart(pieData);
                DisplaySectionStats(pieData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
            finally
            {
                InprocessGrid.ResumeLayout();
                _isLoading = false;
            }
        }

        private async Task HandleFilterChange()
        {
            if (_isInitializing) return;
            await DisplayRejected();
        }

        private  void Inprocess_control_Load(object sender, EventArgs e)
        {
            
        }

        private void InprocessGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (InprocessGrid.Columns[e.ColumnIndex].Name == "Status")
            {
                int checkstatus = (int)e.Value;


                e.Value = (checkstatus == 0) ? "Close" : "Open";   
                e.FormattingApplied = true;

                if (checkstatus == 1)
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(78, 166, 101);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(184, 94, 104);
                }
            }
            else if (InprocessGrid.Columns[e.ColumnIndex].Name == "Shift")
            {
                int checkshift = (int)e.Value;

                e.Value = (checkshift == 0) ? "DS" : "NS";
            }
        }
        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HandleFilterChange();
        }

        // =========================================================
        // CHARTS DISPLAY
        // =========================================================
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



        public void DisplaySectionStats(List<CustomerTotalModel> cc)
        {
            moldval.Text = "0";
            Pressval.Text = "0";
            Rotorval.Text = "0";
            windingval.Text = "0";
            Circuitval.Text = "0";
            if (cc == null || cc.Count == 0)
                return;
            if (_departmentLabels == null)
                return;

            foreach (var d in cc)
            {
                if (d == null)
                    continue;

                if (string.IsNullOrWhiteSpace(d.DepartmentName))
                    continue;

                if (_departmentLabels.TryGetValue(d.DepartmentName, out var label) && label != null)
                {
                    label.Text = d.totalOpen.ToString();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (var form = new AddInprocess(_inp))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Update successful.");
                    await DisplayRejected();
                }

            }
        }
    }
}
