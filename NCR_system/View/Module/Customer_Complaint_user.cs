using LiveCharts.Wpf;
using LiveCharts;
using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.AddForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SeriesCollection = LiveCharts.SeriesCollection;
using Axis = LiveCharts.Wpf.Axis;

namespace NCR_system.View.Module    
{
    public partial class Customer_Complaint_user : UserControl
    {
        private readonly ICustomerComplaint _cust;

        private bool _gridConfigured = false;
        private bool _isLoading = false;
        private bool _isfilter = false;

        public int depId { get; set; } = 0;
        public int stats { get; set; } = 0;


        public DataGridView Customgrid => CustomDatagrid;
        public List<CustomerModel> cuslist { get; private set; } = new List<CustomerModel>();


        public Customer_Complaint_user(ICustomerComplaint cust)
        {
            InitializeComponent();
            _cust = cust;
            filteritems.SelectedIndex = 0;
            sectionfilter.SelectedIndex = 0;

            SelectedProcess.SelectedIndex = 0;
            SelectedProcess.DropDownHeight = 41;

            EnableDoubleBuffering();
        }

        private void Customer_Complaint_user_Load(object sender, EventArgs e)
        {
          

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


            if (proc == 0)
            {
                CustomDatagrid.Columns["CustomerName"].Visible = true;
                CustomDatagrid.Columns["CCtype"].Visible = false;

                Setup("DateCreated", 100, 0);
                Setup("RegNo", 150, 1);
                Setup("CustomerName", 150, 2);
                Setup("SectionID", 150, 3);
                Setup("ModelNo", 150, 4);

                CustomDatagrid.Columns["LotNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Setup("LotNo", 150, 5);

                Setup("NGQQty", 100, 6);
                Setup("Details", 150, 7);
                Setup("Status", 200, 8);
                Setup("Delete", 100, 9);


                OpenCC.Enabled = false;
                OpenCC.BackColor = Color.FromArgb(230, 230, 230);
                OpenCC.ForeColor = Color.FromArgb(194, 194, 194);
                OpenCC.FlatAppearance.BorderColor = Color.FromArgb(172, 172, 172);

                Externalbtn.Enabled = true;
                Externalbtn.BackColor = Color.FromArgb(95, 34, 200);
                Externalbtn.ForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {

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

                //CustomDatagrid.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //CustomDatagrid.Columns["Edit"].Width = 100;
                //CustomDatagrid.Columns["Edit"].DisplayIndex = 8;

                CustomDatagrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CustomDatagrid.Columns["Delete"].Width = 100;
                CustomDatagrid.Columns["Delete"].DisplayIndex = 8;



                Externalbtn.Enabled = false;
                Externalbtn.BackColor = Color.FromArgb(230, 230, 230);
                Externalbtn.ForeColor = Color.FromArgb(194, 194, 194);
                Externalbtn.FlatAppearance.BorderColor = Color.FromArgb(172, 172, 172);

                OpenCC.Enabled = true;
                OpenCC.BackColor = Color.FromArgb(95, 34, 200);
                OpenCC.ForeColor = Color.FromArgb(255, 255, 255);

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
                depId = sectionfilter.SelectedIndex > 0 ? sectionfilter.SelectedIndex : 0;
                stats = filteritems.SelectedIndex > 0 ? filteritems.SelectedIndex - 1 : 0;
                CustomDatagrid.SuspendLayout();

                var custTask = _cust.GetCustomerData(search, depId, proc, stats, 0, 0);
                var pieTask = _cust.GetCustomersOpenItem(proc, depId);

                await Task.WhenAll(custTask, pieTask);

                var CusList = custTask.Result;
                var pieData = pieTask.Result;

                if (!_gridConfigured)
                    ConfigureGrid(proc);

                CustomDatagrid.DataSource = CusList;

                UpdateBarChart(pieData);
                //DisplayPieChart(pieData);

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



        // =========================================================
        // BUTTON ACTION 
        // =========================================================
        private void OpenCC_Click(object sender, EventArgs e)
        {
            using (var add = new AddCustomerComplaint(_cust, this))
            {
                add.StartPosition = FormStartPosition.CenterParent;
                add.ShowDialog(this);   // <-- modal + always in front of parent
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
        private async void SelectedProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ApplyFilter(); 
        }
      
        private void CustomDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var column = CustomDatagrid.Columns[e.ColumnIndex];

            var row = CustomDatagrid.Rows[e.RowIndex];

            var recordID = row.Cells["RecordID"].Value;
            var type = row.Cells["CCtype"].Value;   

            if (column.Name == "Delete")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show($"Delete clicked on row {e.RowIndex} - Record ID selected:  {recordID}");
                }
            }
        }

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ApplyFilter();
        }
        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ApplyFilter();
        }

        public async Task ApplyFilter()
        {
            int selectproc = SelectedProcess.SelectedIndex == -1 ? 0 : SelectedProcess.SelectedIndex;
            await DisplayCustomer(selectproc);
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

      
    }
}
