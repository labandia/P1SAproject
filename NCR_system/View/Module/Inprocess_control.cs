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
using System.IO;
using NCR_system.View.Details;

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
            
            Setup("Status", 60, 0);
            Setup("RecordID", 150, 1);
            Setup("DateEncounter", 140, 2);
            Setup("ProcEncounter", 150, 3);
            Setup("TitleEmail", 150, 4, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Invest", 150, 5, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("cause", 150, 6, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Model", 150, 7, DataGridViewAutoSizeColumnMode.AllCells);
            Setup("Defect", 150, 8, DataGridViewAutoSizeColumnMode.AllCells);

            Setup("P1saStatus", 120, 9);
            Setup("Shift", 50, 10);
            Setup("Line", 70, 11);
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

                //UpdateBarChart(pieData);
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
        //private void UpdateBarChart(List<CustomerTotalModel> cc)
        //{
        //    if (cc == null || cc.Count == 0)
        //    {
        //        cartesianChart1.Series = new SeriesCollection();
        //        return;
        //    }

        //    var seriesCollection = new SeriesCollection();
        //    var labels = new List<string>();

        //    foreach (var d in cc)
        //    {
        //        if (d.totalOpen <= 0) continue;

        //        labels.Add(d.DepartmentName);

        //        // Set color per section
        //        System.Windows.Media.Brush color = System.Windows.Media.Brushes.Gray;

        //        switch (d.DepartmentName)
        //        {
        //            case "Molding":
        //                color = System.Windows.Media.Brushes.Pink;
        //                break;

        //            case "Press":
        //                color = System.Windows.Media.Brushes.Blue;
        //                break;

        //            case "Rotor":
        //                color = System.Windows.Media.Brushes.Yellow;
        //                break;

        //            case "Winding":
        //                color = System.Windows.Media.Brushes.Green;
        //                break;

        //            case "Circuit":
        //                color = System.Windows.Media.Brushes.White;
        //                break;
        //        }

        //        seriesCollection.Add(new ColumnSeries
        //        {
        //            Title = d.DepartmentName,
        //            Values = new ChartValues<int> { d.totalOpen },
        //            DataLabels = true,
        //            Fill = color,
        //            MaxColumnWidth = 200,     // control bar width
        //            ColumnPadding = 50,      // space between bars
        //            LabelPoint = point => point.Y.ToString()
        //        });
        //    }

        //    cartesianChart1.Series = seriesCollection;

        //    // X Axis
        //    cartesianChart1.AxisX.Clear();
        //    cartesianChart1.AxisX.Add(new Axis
        //    {
        //        Title = "Department",
        //        Labels = labels,
        //        Foreground = System.Windows.Media.Brushes.White,
        //        Separator = new Separator { Step = 1, IsEnabled = false }
        //    });

        //    // Y Axis
        //    cartesianChart1.AxisY.Clear();
        //    cartesianChart1.AxisY.Add(new Axis
        //    {
        //        Title = "Total Open",
        //        LabelFormatter = value => value.ToString("N0"),
        //        Foreground = System.Windows.Media.Brushes.White
        //    });

        //    cartesianChart1.DisableAnimations = cc.Sum(x => x.totalOpen) > 2000;
        //}



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
                    await DisplayRejected();
                }

            }
        }

        private void InprocessGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (InprocessGrid.Columns[e.ColumnIndex].Name == "Invest" && e.RowIndex >= 0)
            {
                string path = InprocessGrid.Rows[e.RowIndex].Cells["Invest"].Value?.ToString();

                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                }
                else
                {
                    MessageBox.Show("File not found.");
                }
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void InprocessGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var product = (InprocessModel)InprocessGrid.Rows[e.RowIndex].DataBoundItem;

            using (var edit = new InprocessDetails(product, _inp))
            {
                if(edit.ShowDialog(this) == DialogResult.OK)
                {
                    await DisplayRejected();
                }
            }
        }

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = (filteritems.SelectedIndex == 1) ? "Open Item Per Section" : "Close Item Per Section";

            await HandleFilterChange();
        }
    }
}
