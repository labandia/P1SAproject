using NCR_system.Interface;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using NCR_system.View.AddForms;

namespace NCR_system.View.Module
{
    public partial class NCR_control : UserControl
    {
        private readonly INCR _ncr;

        private bool _isInitializing = true;
        private bool _gridConfigured = false;
        private bool _isLoading = false;

        public int depId { get; set; } = 0;
        public int stats { get; set; } = 0;

        private readonly Dictionary<string, Label> _departmentLabels;

        public NCR_control(INCR ncr)
        {
            InitializeComponent();
            _ncr = ncr;
            _isInitializing = true;

            sectionfilter.SelectedIndex = 0;
            filteritems.SelectedIndex = 0;
            //_departmentLabels = new Dictionary<string, Label>(StringComparer.OrdinalIgnoreCase)
            //{
            //    { "Molding", moldval },
            //    { "Press", Pressval },
            //    { "Rotor", Rotorval },
            //    { "Winding", windingval },
            //    { "Circuit", Circuitval }
            //};

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
                ?.SetValue(NCRTable, true, null);

            NCRTable.EnableHeadersVisualStyles = false;
            NCRTable.RowHeadersVisible = false;
        }

        private void ConfigureGrid()
        {
            if (_gridConfigured) return;

            NCRGrid.AutoGenerateColumns = false;
            NCRGrid.SuspendLayout();

            void Setup(string name, int width, int displayIndex,
               DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.None)
            {
                var col = NCRGrid.Columns[name];
                if (col == null) return;

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.Width = width;
                col.DisplayIndex = displayIndex;
                col.AutoSizeMode = mode;
            }

            //Setup("RecordID", 150, 0);
            NCRGrid.Columns["RecordID"].Visible = false;
            NCRGrid.Columns["Category"].Visible = false;
            //InprocessGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Setup("Status", 60, 0);
            Setup("RegNo", 150, 1, DataGridViewAutoSizeColumnMode.None);
            Setup("DateIssued", 120, 2);
            Setup("IssueGroup", 100, 3);
            Setup("SectionID", 120, 4);
            Setup("ModelNo", 100, 5);
            Setup("Quantity", 150, 6);
            Setup("DateRegist", 150, 7);

            Setup("DateCloseReg", 100, 8);
            Setup("Contents", 150, 9, DataGridViewAutoSizeColumnMode.Fill);
            Setup("FilePath", 120, 10, DataGridViewAutoSizeColumnMode.Fill);


            NCRGrid.Columns["DateRegist"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            NCRGrid.Columns["DateIssued"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //NCRGrid.Columns["cause"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //NCRGrid.Columns["Line"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //NCRGrid.Columns["NGQty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //NCRGrid.Columns["SectionDep"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            NCRGrid.ResumeLayout();
            _gridConfigured = true;
        }
        private async Task HandleFilterChange()
        {
            if (_isInitializing) return;
            await DisplayNCR(0);
        }

        public async Task DisplayNCR(int procs)
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                string search = searchText.Text.Trim();

                NCRTable.SuspendLayout();

                var Summarydata = _ncr.GetSummaryNCR(procs);
                var tabledata = _ncr.GetNCRData(
                    search,
                    catselection.SelectedText,
                    sectionfilter.SelectedIndex,
                    filteritems.SelectedIndex,
                    procs);

                await Task.WhenAll(Summarydata, tabledata);

                var Summarylist = Summarydata.Result;
                var Tablelist = tabledata.Result;

                if (!_gridConfigured)
                    ConfigureGrid();

                NCRTable.DataSource = Summarylist;
                NCRGrid.DataSource = Tablelist;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
            finally
            {
                NCRTable.ResumeLayout();
                _isLoading = false;
            }
        }

        private void NCRGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (NCRGrid.Columns[e.ColumnIndex].Name == "Status")
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

            if (NCRGrid.Columns[e.ColumnIndex].Name == "SectionID")
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
        }

        private void NCR_control_Load(object sender, EventArgs e)
        {
           
        }

       

        private void NCRGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

      
        }

      
        // ============================================================
        // ==================== FILTER ACTION =========================
        // ============================================================
        private async void sectionfilter_SelectedIndexChanged(object sender, EventArgs e) => await HandleFilterChange();
        private async void filteritems_SelectedIndexChanged(object sender, EventArgs e) => await HandleFilterChange();
        private async void catselection_SelectedIndexChanged(object sender, EventArgs e) => await HandleFilterChange();

        private async void button1_Click(object sender, EventArgs e)
        {     
            using (var form = new AddMainRegistration())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await DisplayNCR(0);
                }

            }
        }
    }
}
