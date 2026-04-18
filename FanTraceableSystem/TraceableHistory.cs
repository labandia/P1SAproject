using FanTraceableSystem.Interface;
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

namespace FanTraceableSystem
{
    public partial class TraceableHistory : Form
    {
        private readonly ISummary _summaryService;
        public int isEditmode = 0;

        private System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();

        public TraceableHistory(ISummary service)
        {
            InitializeComponent();
            _summaryService = service;  
            sectionselect.SelectedIndex = 0;

            searchTimer.Interval = 500; // 500ms delay
            searchTimer.Tick += SearchTimer_Tick;
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            isEditmode = 1; // enable filter mode
            await dispayData(isEditmode);
        }

        private async void TraceableHistory_Load(object sender, EventArgs e)
        {
            await dispayData(isEditmode);
        }

        public async Task dispayData(int isfilter)
        {
            string filterText = string.IsNullOrWhiteSpace(SearchText.Text)
                   ? null
                   : SearchText.Text.Trim();

            DateTime? startDate = dateTimePicker2.Checked
                ? dateTimePicker2.Value.Date
                : (DateTime?)null;

            DateTime? endDate = dateTimePicker3.Checked
                ? dateTimePicker3.Value.Date
                : (DateTime?)null;

            var result = await _summaryService.TraceableShopOrderSummary(
                filterText,
                startDate,
                endDate,
                isfilter,
                sectionselect.SelectedIndex
            );

            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = result;
            ArrangeColumns();
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
            await dispayData(1);
        }

        private async void filterbtn_Click(object sender, EventArgs e)
        {
            isEditmode = 0;

            SearchText.Text = "";
            dateTimePicker2.Checked = false;
            dateTimePicker3.Checked = false;

            await dispayData(isEditmode);
        }

        private  void SearchText_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();   // reset timer
            searchTimer.Start();  // wait again
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
    }
}
