using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.AddForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesCollection = LiveCharts.SeriesCollection;
using Axis = LiveCharts.Wpf.Axis;
using NCR_system.View.Details;

namespace NCR_system.View.Module    
{
    public partial class Customer_Complaint_user : UserControl
    {
        private readonly ICustomerComplaint _cust;

        private bool _isInitializing = true;
        private bool _gridConfigured = false;
        private bool _isLoading = false;


        public DataGridView Customgrid => CustomDatagrid;
        public List<CustomerModel> cuslist { get; private set; } = new List<CustomerModel>();
        private readonly Dictionary<string, Label> _departmentLabels;

        public Customer_Complaint_user(ICustomerComplaint cust)
        {
            InitializeComponent();
            _cust = cust;
            _isInitializing = true;

            filteritems.SelectedIndex = 1;
            sectionfilter.SelectedIndex = 0;
            SelectedProcess.SelectedIndex = 0;

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
                ?.SetValue(CustomDatagrid, true, null);

            CustomDatagrid.EnableHeadersVisualStyles = false;
            CustomDatagrid.RowHeadersVisible = false;
        }
        private void ConfigureGrid(int proc)
        {
            if (_gridConfigured) return;

            CustomDatagrid.AutoGenerateColumns = false;
            CustomDatagrid.SuspendLayout();

            void Setup(string name, int width, int displayIndex,
                DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.None)
            {
                var col = CustomDatagrid.Columns[name];
                if (col == null) return;

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.Width = width;
                col.DisplayIndex = displayIndex;
                col.AutoSizeMode = mode;
            }

            CustomDatagrid.Columns["RecordID"].Visible = false;
            CustomDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bool isCCTypeVisible = proc == 0;

            SwitchButton(proc); 

            if (isCCTypeVisible)
            {
                // EXTERNAL CUSTOMER COMPLAINTS 
                CustomDatagrid.Columns["CustomerName"].Visible = true;
                CustomDatagrid.Columns["CCtype"].Visible = false;

                Setup("Status", 60, 0);
                Setup("DateCreated", 100, 1);
                Setup("RegNo", 150, 2);
                Setup("CustomerName", 180, 3);
                Setup("SectionID", 150, 4);
                Setup("ModelNo", 180, 5);

                CustomDatagrid.Columns["LotNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Setup("LotNo", 150, 6);

                Setup("NGQty", 100, 7);
                Setup("Details", 300, 8, DataGridViewAutoSizeColumnMode.Fill);


                
            }
            else
            {
                // SDC CUSTOMER COMPLAINTS 
                CustomDatagrid.Columns["RegNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["RegNo"].Width = 150;
                CustomDatagrid.Columns["RegNo"].Visible = false;

                CustomDatagrid.Columns["CustomerName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                CustomDatagrid.Columns["CustomerName"].Visible = false;
                CustomDatagrid.Columns["CCtype"].Visible = false;


                CustomDatagrid.Columns["DateCreated"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["DateCreated"].DisplayIndex = 1;
                CustomDatagrid.Columns["DateCreated"].Width = 100;

                CustomDatagrid.Columns["SectionID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["SectionID"].Width = 150;
                CustomDatagrid.Columns["SectionID"].DisplayIndex = 2;

                CustomDatagrid.Columns["ModelNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["ModelNo"].Width = 150;
                CustomDatagrid.Columns["ModelNo"].DisplayIndex = 3;

                CustomDatagrid.Columns["LotNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["LotNo"].Width = 150;
                CustomDatagrid.Columns["LotNo"].DisplayIndex = 4;

                CustomDatagrid.Columns["NGQty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["NGQty"].Width = 100;
                CustomDatagrid.Columns["NGQty"].DisplayIndex = 5;

                CustomDatagrid.Columns["Details"].DisplayIndex = 6;

                CustomDatagrid.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["Status"].Width = 100;
                CustomDatagrid.Columns["Status"].DisplayIndex = 7;

           

 
            }



            CustomDatagrid.ResumeLayout();
            _gridConfigured = true;
        }
        public async Task DisplayCustomer(int proc)
        {
            if (_isLoading) return;
            _isLoading = true;

         
            try
            {
                string search = searchText.Text.Trim();
                CustomDatagrid.SuspendLayout();


                var custTask = _cust.GetCustomerData(
                    search,
                    sectionfilter.SelectedIndex, 
                    proc, filteritems.SelectedIndex, 0, 0);

                var pieTask = _cust.GetCustomersOpenItem(proc, sectionfilter.SelectedIndex, filteritems.SelectedIndex);

                await Task.WhenAll(custTask, pieTask);

                var CusList = custTask.Result;
                var pieData = pieTask.Result;

                ConfigureGrid(proc);


                CustomDatagrid.DataSource = CusList;

                UpdateBarChart(pieData);
                DisplaySectionStats(pieData);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
            finally
            {
                CustomDatagrid.ResumeLayout();
                _isLoading = false;
            }
        }
        
        private void CustomDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            if (CustomDatagrid.Columns[e.ColumnIndex].Name == "SectionID")
            {
                var sectionMap = new Dictionary<int, string>
                {
                    {1, "P1SA MOLDING"},
                    {2, "P1SA PRESS"},
                    {3, "P1SA ROTOR"},
                    {4, "P1SA WINDING"},
                    {5, "P1SA CIRCUIT"}
                };

                if (e.Value != null && int.TryParse(e.Value.ToString(), out int sectionID))
                {
                    if (sectionMap.TryGetValue(sectionID, out string sectionName))
                    {
                        e.Value = sectionName;
                        e.FormattingApplied = true;
                    }
                }
            }
            else if (CustomDatagrid.Columns[e.ColumnIndex].Name == "Status")
            {
                string checkstats = e.Value.ToString() == "1" ? "Open" : "Close";
                e.Value = checkstats;

                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.BackColor = (checkstats == "Open")
                    ? Color.FromArgb(78, 166, 101)
                    : Color.FromArgb(184, 94, 104);

                e.FormattingApplied = true;
            }
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

            var seriesCollection = new SeriesCollection();
            var labels = new List<string>();

            foreach (var d in cc)
            {
                if (d.totalOpen <= 0) continue;

                labels.Add(d.DepartmentName);

                // Set color per section
                System.Windows.Media.Brush color = System.Windows.Media.Brushes.Gray;

                switch (d.DepartmentName)
                {
                    case "Molding":
                        color = System.Windows.Media.Brushes.Pink;
                        break;

                    case "Press":
                        color = System.Windows.Media.Brushes.Blue;
                        break;

                    case "Rotor":
                        color = System.Windows.Media.Brushes.Yellow;
                        break;

                    case "Winding":
                        color = System.Windows.Media.Brushes.Green;
                        break;

                    case "Circuit":
                        color = System.Windows.Media.Brushes.White;
                        break;
                }

                seriesCollection.Add(new ColumnSeries
                {
                    Title = d.DepartmentName,
                    Values = new ChartValues<int> { d.totalOpen },
                    DataLabels = true,
                    Fill = color,
                    MaxColumnWidth = 200,     // control bar width
                    ColumnPadding = 50,      // space between bars
                    LabelPoint = point => point.Y.ToString()
                });
            }

            cartesianChart1.Series = seriesCollection;

            // X Axis
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Department",
                Labels = labels,
                Foreground = System.Windows.Media.Brushes.White,
                Separator = new Separator { Step = 1, IsEnabled = false }
            });

            // Y Axis
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Total Open",
                LabelFormatter = value => value.ToString("N0"),
                Foreground = System.Windows.Media.Brushes.White
            });

            cartesianChart1.DisableAnimations = cc.Sum(x => x.totalOpen) > 2000;
        }



        // =========================================================
        // BUTTON ACTION 
        // =========================================================
        private async void OpenCC_Click(object sender, EventArgs e)
        {
            using (var add = new AddCustomerComplaint(_cust, this))
            {
                add.StartPosition = FormStartPosition.CenterParent;
                if (add.ShowDialog(this) == DialogResult.OK)
                {
                    await DisplayCustomer(1);
                }

            }
        }
        private void Externalbtn_Click(object sender, EventArgs e)
        {
            using (var add = new AddExternalCC(_cust, this))
            {
                add.StartPosition = FormStartPosition.CenterParent;
                add.ShowDialog(this);   // <-- modal + always in front of parent
            }
        }
       
      
        private void CustomDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private async void SelectedProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HandleFilterChange();
        }
        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HandleFilterChange();
        }
        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HandleFilterChange();
        }

        private async Task HandleFilterChange()
        {
            if (_isInitializing) return;

            int selectproc = SelectedProcess.SelectedIndex;
            await DisplayCustomer(selectproc);
            SwitchButton(selectproc);
        }

        
        public void SwitchButton(int proc)
        {
            bool isCCTypeVisible = proc == 0;

            OpenCC.Enabled = isCCTypeVisible == true ? false : true;
            OpenCC.BackColor = isCCTypeVisible ? Color.FromArgb(30, 36, 62) : Color.FromArgb(51, 153, 255);
            OpenCC.ForeColor = isCCTypeVisible ? Color.FromArgb(222, 222, 222) : Color.FromArgb(255, 255, 255);
            OpenCC.FlatAppearance.BorderColor = isCCTypeVisible ? Color.FromArgb(30, 36, 62) : Color.FromArgb(51, 153, 255);
            OpenCC.Cursor = isCCTypeVisible == false ? Cursors.Hand : Cursors.Default;


            Externalbtn.Enabled = isCCTypeVisible;
            Externalbtn.BackColor = isCCTypeVisible  ? Color.FromArgb(51, 153, 255) : Color.FromArgb(30, 36, 62);
            Externalbtn.ForeColor = isCCTypeVisible ? Color.FromArgb(255, 255, 255) : Color.FromArgb(222, 222, 222);
            Externalbtn.FlatAppearance.BorderColor = isCCTypeVisible ? Color.FromArgb(51, 153, 255) : Color.FromArgb(30, 36, 62);
            Externalbtn.Cursor = isCCTypeVisible  ? Cursors.Hand : Cursors.Default;

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



        public void DisplayLabelText(string depart, int count)
        {
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

        private void CustomDatagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header double-click
            if (e.RowIndex < 0)
                return;

            var row = CustomDatagrid.Rows[e.RowIndex];

            // Optional: select full row
            CustomDatagrid.ClearSelection();
            row.Selected = true;

            // If you're using DataBoundItem (recommended)
            var item = row.DataBoundItem as CustomerModel;
            if (item == null) return;


            using (var details = new CustomerDetails(item))
            {
                details.ShowDialog(this);
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
