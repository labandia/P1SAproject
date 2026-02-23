using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NCR_system.View.Module    
{
    public partial class Customer_Complaint_user : UserControl
    {
        private readonly ICustomerComplaint _cust;

        public int depId { get; set; } = 0;
        public int stats { get; set; } = 0;


        public List<CustomerModel> cuslist { get; private set; } = new List<CustomerModel>();
        public DataGridView Customgrid { get { return CustomDatagrid; } }

        public Customer_Complaint_user(ICustomerComplaint cust)
        {
            InitializeComponent();
            _cust = cust;
        }


        public async Task DisplayCustomer(int proc)
        {
            string search = searchText.Text.Trim();
            depId = sectionfilter.SelectedIndex > 0 ? sectionfilter.SelectedIndex : 0;
            stats = filteritems.SelectedIndex > 0 ? filteritems.SelectedIndex : 0;

            try
            {

                var filtered = await _cust.GetCustomerData(search, depId, proc, stats, 0, 0);

                // For Displaying Customer
                CustomDatagrid.DataSource = filtered;

                if (proc == 0)
                {
                    //CustomDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                    CustomDatagrid.Columns["RecordID"].Visible = false;
                    CustomDatagrid.Columns["CustomerName"].Visible = true;
                    CustomDatagrid.Columns["CCtype"].Visible = false;

                    CustomDatagrid.Columns["DateCreated"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["DateCreated"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["DateCreated"].DisplayIndex = 0;
                    CustomDatagrid.Columns["DateCreated"].Width = 100;

                    CustomDatagrid.Columns["RegNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["RegNo"].Width = 150;
                    CustomDatagrid.Columns["RegNo"].DisplayIndex = 1;

                    CustomDatagrid.Columns["CustomerName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["CustomerName"].DisplayIndex = 2;

                    CustomDatagrid.Columns["SectionID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["SectionID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["SectionID"].Width = 150;
                    CustomDatagrid.Columns["SectionID"].DisplayIndex = 3;

                    CustomDatagrid.Columns["ModelNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["ModelNo"].Width = 150;
                    CustomDatagrid.Columns["ModelNo"].DisplayIndex = 4;

                    CustomDatagrid.Columns["LotNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["LotNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["LotNo"].Width = 150;
                    CustomDatagrid.Columns["LotNo"].DisplayIndex = 5;

                    CustomDatagrid.Columns["NGQty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["NGQty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["NGQty"].Width = 100;
                    CustomDatagrid.Columns["NGQty"].DisplayIndex = 6;

                    CustomDatagrid.Columns["Details"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["Details"].DisplayIndex = 7;

                    CustomDatagrid.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["Status"].Width = 100;
                    CustomDatagrid.Columns["Status"].DisplayIndex = 8;

                    //CustomDatagrid.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //CustomDatagrid.Columns["Edit"].Width = 100;
                    //CustomDatagrid.Columns["Edit"].DisplayIndex = 9;

                    CustomDatagrid.Columns["Delete"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    CustomDatagrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    CustomDatagrid.Columns["Delete"].Width = 100;
                    CustomDatagrid.Columns["Delete"].DisplayIndex = 9;


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
                    CustomDatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    CustomDatagrid.Columns["RecordID"].Visible = false;

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


                var countItems = await _cust.GetCustomersOpenItem(proc, depId);
                DisplayPieChart(countItems);

          
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
        }

        private void CustomDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.Value == null)
                return;

            if(CustomDatagrid.Columns[e.ColumnIndex].Name == "SectionID")
            {
                switch(e.Value.ToString())
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
            else if (CustomDatagrid.Columns[e.ColumnIndex].Name == "Status")
            {
                string checkstats = e.Value.ToString() == "1" ? "Open" : "Close";
                e.Value = checkstats;

                if (checkstats == "Open")
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(78, 166, 101);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(184, 94, 104);
                }


                e.FormattingApplied = true;
            }
        }

        private void OpenCC_Click(object sender, EventArgs e)
        {
            using (var add = new AddCustomerComplaint(_cust, this))
            {
                add.StartPosition = FormStartPosition.CenterParent;
                add.ShowDialog(this);   // <-- modal + always in front of parent
            }
        }

        private void Customer_Complaint_user_Load(object sender, EventArgs e)
        {
            filteritems.SelectedIndex = 0;
            sectionfilter.SelectedIndex = 0;

            SelectedProcess.SelectedIndex = 0;
            SelectedProcess.DropDownHeight = 41;
          
        }

        private async void SelectedProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
           await DisplayCustomer(SelectedProcess.SelectedIndex);
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
            // Make sure user clicked on a valid row (not header)
            if (e.RowIndex < 0)
                return;

            // Get the clicked column
            var column = CustomDatagrid.Columns[e.ColumnIndex];

            // Get the clicked row
            var row = CustomDatagrid.Rows[e.RowIndex];

            // Assume you have a column named "RecordID"
            var recordID = row.Cells["RecordID"].Value;
            var type = row.Cells["CCtype"].Value;   

            //if (column.Name == "Edit")
            //{
            //    int CCtype = Convert.ToInt32(type);
            //    // You can get the row data like this:
            //    if (CCtype == 0)
            //    {
            //        var openedit = new EditCC_External(_cust, Convert.ToInt32(recordID), Convert.ToInt32(type), this);
            //        openedit.ShowDialog();
            //    }
            //    else
            //    {
            //        var openedit = new EditCC_SDC(_cust, Convert.ToInt32(recordID), Convert.ToInt32(type), this);
            //        openedit.ShowDialog();
            //    }
          
            //}
            if (column.Name == "Delete")
            {
                // Handle Delete image click
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Remove the row or perform deletion
                    MessageBox.Show($"Delete clicked on row {e.RowIndex} - Record ID selected:  {recordID}");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e)
        {
            await DisplayCustomer(SelectedProcess.SelectedIndex);
        }

        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await DisplayCustomer(SelectedProcess.SelectedIndex);
        }

        private void CustomDatagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        public void resetDisplayText()
        {
            moldval.Text = "0";
            Pressval.Text = "0";
            Rotorval.Text = "0";
            windingval.Text = "0";
            Circuitval.Text = "0";
        }

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
    }
}
