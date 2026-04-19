using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;

namespace FanTraceableSystem
{
    public partial class TraceableHistory : Form
    {
        private readonly ISummary _summaryService;
        public int isEditmode = 0;

        private BindingList<SummaryraceableShopOrderModel> data;


        private bool _isLoading = false;
        public int isFilter = 0;

        private int pageNumber = 1;   // current page
        private int pageSize = 50;    // rows per page
        private bool _hasNextPage;     // controls Next button

        private System.Windows.Forms.Timer _searchTimer;
        private const int _debounceDelay = 500; // milliseconds (adjust if needed)


        public TraceableHistory(ISummary service)
        {
            InitializeComponent();
            _summaryService = service;  
            sectionselect.SelectedIndex = 0;

            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = _debounceDelay;

            _searchTimer.Tick += async (s, ev) =>
            {
                _searchTimer.Stop();

                pageNumber = 1;          // reset pagination
                await loadData();        // reload with new search
            };
        }

     

        private async void TraceableHistory_Load(object sender, EventArgs e)
        {
            await loadData();
        }

        public async Task loadData(bool append = false)
        {
            if (_isLoading) return; // prevent double load
            _isLoading = true;

            var result = await _summaryService.TraceableShopOrderSummary(
                SearchText.Text,
                dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                isEditmode,
                sectionselect.SelectedIndex, 
                pageNumber,
                pageSize
            );

            int totalCount = await _summaryService.GetSummaryCount(
                SearchText.Text,
                dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                isEditmode,
                sectionselect.SelectedIndex
            );


            if (append && dataGridView2.DataSource is List<SummaryraceableShopOrderModel> existingData)
            {
                existingData.AddRange(result);
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = existingData;
            }
            else
            {
                dataGridView2.DataSource = result;
            }

            // Compute range
            int start = ((pageNumber - 1) * pageSize) + 1;
            int end = start + result.Count - 1;

            // Fix when no data
            if (totalCount == 0)
            {
                start = 0;
                end = 0;
            }

            ArrangeColumns();

            // Pagination buttons
            lblEntries.Text = $"Showing {start} to {end} of {totalCount} entries";

            _hasNextPage = end < totalCount;

            btnPrev.Enabled = pageNumber > 1;
            btnNext.Enabled = _hasNextPage;
        }


        public void ArrangeColumns()
        {
            dataGridView2.Columns["PCBShopOrder"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PCBShopOrder"].DisplayIndex = 0;
            dataGridView2.Columns["PCBShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PCBShopOrder"].Width = 100;

            dataGridView2.Columns["PCBA"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PCBA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PCBA"].Width = 120;

            dataGridView2.Columns["PreparedQuantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PreparedQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PreparedQuantity"].Width = 200;



            dataGridView2.Columns["TimeInput"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["TimeInput"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["TimeInput"].Width = 100;



            dataGridView2.Columns["PreparedBy"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PreparedBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PreparedBy"].Width = 120;

            dataGridView2.Columns["Shift"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Shift"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["Shift"].Width = 120;

            dataGridView2.Columns["Customer"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["Customer"].Width = 120;

            dataGridView2.Columns["CardCaseNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["CardCaseNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["CardCaseNo"].Width = 120;

            dataGridView2.Columns["Remarks"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["Remarks"].Width = 120;

            dataGridView2.Columns["PCBIncharge"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PCBIncharge"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PCBIncharge"].Width = 120;

            dataGridView2.Columns["PCBIssuer"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PCBIssuer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PCBIssuer"].Width = 120;

            dataGridView2.Columns["LotNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["LotNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["LotNo"].Width = 120;
        }

        private void button12_Click(object sender, EventArgs e)
        {
        
            this.Close();
        }

        private async void sectionselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            await loadData();
        }

        private async void filterbtn_Click(object sender, EventArgs e)
        {
            isEditmode = 0;

            SearchText.Text = "";
            dateTimePicker2.Checked = false;
            dateTimePicker3.Checked = false;

            await loadData();
        }

        private  void SearchText_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();  // reset timer
            _searchTimer.Start(); // start countdown again
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "DepartmentID")
            {
                var sectionMap = new Dictionary<int, string>
                {
                    {1, "Molding"},
                    {2, "Press"},
                    {3, "Rotor"},
                    {4, "Winding"},
                    {5, "Circuit"},
                    {6, "OilProof"},
                    {7, "Harness"}
                };

                if (e.Value != null && int.TryParse(e.Value.ToString(), out int sectionID))
                {
                    if (sectionMap.TryGetValue(sectionID, out string sectionName))
                    {
                        e.Value = sectionName;
                    }
                    else
                    {
                        // 🔹 Default fallback
                        e.Value = "Final Assy";
                    }

                    e.FormattingApplied = true;
                }
                else
                {
                    // 🔹 If null or invalid, also default
                    e.Value = "Final Assy";
                    e.FormattingApplied = true;
                }
            }
        }

        private void Exportbtn_Click(object sender, EventArgs e)
        {

        }
    }
}
