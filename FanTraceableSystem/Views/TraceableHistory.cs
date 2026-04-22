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
using static System.Collections.Specialized.BitVector32;
using Excel = Microsoft.Office.Interop.Excel;


namespace FanTraceableSystem
{
    public partial class TraceableHistory : Form
    {
        private readonly ISummary _summaryService;
        public int isEditmode = 0;

       


        private bool _isLoading = false;
        public int isFilter = 0;


        private System.Windows.Forms.Timer _searchTimer;
        private const int _debounceDelay = 500; // milliseconds (adjust if needed)
        private Timer timer;

        private BindingList<SummaryraceableShopOrderModel> data;

        private readonly PagingState _paging = new PagingState();


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

                _paging.PageNumber = 1;          // reset pagination
                await loadData();        // reload with new search
            };

            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeText.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        // ================================================================================
        // ========================== DATA GRIDVIEW & PAGINATION ==========================
        // ================================================================================

        private async void TraceableHistory_Load(object sender, EventArgs e) => await loadData();
        public async Task loadData(bool append = false)
        {
            if (_isLoading) return; // prevent double load
            _isLoading = true;

            try
            {
                var dataTask = _summaryService.TraceableShopOrderSummary(
                      SearchText.Text,
                      dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                      dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                      isEditmode,
                      sectionselect.SelectedIndex,
                      _paging.PageNumber,
                      _paging.PageSize
                );

                var countTask = _summaryService.GetSummaryCount(
                    SearchText.Text,
                    dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                    dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                    isEditmode,
                    sectionselect.SelectedIndex
                );


                await Task.WhenAll(dataTask, countTask);

                var result = dataTask.Result;
                var totalCount = countTask.Result;

                BindGrid(result, append);
                UpdatePaginationUI(result.Count, totalCount);
                ArrangeColumns();

            }
            finally
            {
                _isLoading = false;
            }   

        }
        private void BindGrid(List<SummaryraceableShopOrderModel> result, bool append)
        {
            if (append)
            {
                foreach (var item in result)
                    data.Add(item);
            }
            else
            {
                data = new BindingList<SummaryraceableShopOrderModel>(result);
                dataGridView2.DataSource = data;
            }
        }
        private void UpdatePaginationUI(int returnedCount, int totalCount)
        {
            int start = ((_paging.PageNumber - 1) * _paging.PageSize) + 1;
            int end = start + returnedCount - 1;

            if (totalCount == 0)
            {
                start = 0;
                end = 0;
            }

            lblEntries.Text = $"Showing {start} to {end} of {totalCount} entries";

            _paging.HasNextPage = end < totalCount;

            btnPrev.Enabled = _paging.PageNumber > 1;
            btnNext.Enabled = _paging.HasNextPage;
        }
        public void ArrangeColumns()
        {
            dataGridView2.Columns["FinalShopOrder"].DisplayIndex = 0;


            dataGridView2.Columns["ShopOrder"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["ShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["ShopOrder"].Width = 120;

            FanTraceabilityCore.ConfigureColumn(dataGridView2, "ItemNo", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PreparedQuantity", 200);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "TimeInput", 100);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PreparedBy", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Shift", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Customer", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Modeltype", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Remarks", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Incharge", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "FinalIssuedby", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "LotNo", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "DepartmentID", 120);
        }
        private async void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_paging.HasNextPage || _isLoading) return;

            int visibleRows = dataGridView2.DisplayedRowCount(false);
            int firstVisibleRow = dataGridView2.FirstDisplayedScrollingRowIndex;

            if (firstVisibleRow + visibleRows >= dataGridView2.RowCount - 5)
            {
                _paging.PageNumber++;
                await loadData(append: true); // ✅ append ONLY here
            }
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = "--";
                e.FormattingApplied = true;
            }


            if (dataGridView2.Columns[e.ColumnIndex].Name == "Shift")
            {
                if (e.Value == null) return;

                e.Value = FanTraceabilityCore.FormatShift(e.Value.ToString());
                e.FormattingApplied = true;
            }


            if (dataGridView2.Columns[e.ColumnIndex].Name == "DepartmentID")
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int sectionID))
                {
                    e.Value = FanTraceabilityCore.SectionMap.ContainsKey(sectionID)
                    ? FanTraceabilityCore.SectionMap[sectionID]
                    : "Final Assy Section";

                    e.FormattingApplied = true;
                }
            }
        }

        // ================================================================================
        // ========================== FILTERS SEARCH FUNCTIONALITY ========================
        // ================================================================================
        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (!_paging.HasNextPage) return;

            _paging.PageNumber++;
            await loadData(false);
        }
        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_paging.PageNumber <= 1) return;

            _paging.PageNumber--;
            await loadData(false);     // ❗ replace
        }


        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void sectionselect_SelectedIndexChanged(object sender, EventArgs e) => await loadData();

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



        // ================================================================================
        // ========================== EXPORT  DATA  =======================================
        // ================================================================================
        private async void Exportbtn_Click(object sender, EventArgs e)
        {
            List<ExportTraceableShopOrderModel> Exportdata = new List<ExportTraceableShopOrderModel>();

            var result = await _summaryService.TraceableShopOrderSummary(
                     SearchText.Text,
                     dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                     dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                     isEditmode,
                     sectionselect.SelectedIndex,
                     _paging.PageNumber,
                     _paging.PageSize
               );

            foreach (var items in result)
            {
                Exportdata.Add(new ExportTraceableShopOrderModel
                {
                    FinalShopOrder = items.FinalShopOrder,
                    ShopOrder = items.ShopOrder,
                    ItemNo = items.ItemNo,
                    Revision = items.Revision,
                    PlanQuan = items.PlanQuan,
                    DatePrepared = items.DatePrepared.ToString("yyyy-MM-dd"),
                    TimeInput = items.TimeInput,
                    PreparedBy = items.PreparedBy,
                    PreparedQuantity = items.PreparedQuantity,
                    Shift = FanTraceabilityCore.FormatShift(items.Shift?.ToString() ?? ""),
                    Rev = items.Rev,
                    Customer = items.Customer,
                    Modeltype = items.Modeltype,
                    Remarks = items.Remarks,
                    Incharge = items.Incharge,
                    FinalIssuedby = items.FinalIssuedby,
                    LotNo = items.LotNo,
                    DepartmentID = FanTraceabilityCore.SectionMap.ContainsKey(items.DepartmentID)
                                 ? FanTraceabilityCore.SectionMap[items.DepartmentID]
                                 : "Final Assy Section"
                });
            }


            ExportToExcel(Exportdata);
        }


        private void ExportToExcel(List<ExportTraceableShopOrderModel> data)
        {
            var headerMap = new Dictionary<string, string>
            {
                { "FinalShopOrder", "Final ShopOrder" },
                { "ShopOrder", "ShopOrder" },
                { "PreparedBy", "Prepared By" },
                { "Revision", "Revision" },
                { "ItemNo", "Item No." },
                { "PlanQuan", "Plan Quantity" },
                { "DatePrepared", "Date Prepared" },
                { "TimeInput", "Time" },
                { "PreparedQuantity", "Prepared Quantity" },
                { "Rev", "Rev" },
                { "Customer", "Customer" },
                { "Modeltype", "Model Type" },
                { "Remarks", "Remarks" },
                { "Incharge", "Incharge" },
                { "FinalIssuedby", "Issuer" },
                { "LotNo", "Lot No" },
                { "DepartmentID", "Section" }
            };

            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Sub Assy FanTraceability";

                var properties = typeof(ExportTraceableShopOrderModel).GetProperties();
                int colCount = properties.Length;
                int rowCount = data.Count();

                // ✅ Set column width (adjust as needed)
                worksheet.Columns.ColumnWidth = 20;

                // Insert headers
                for (int i = 0; i < colCount; i++)
                {
                    string propName = properties[i].Name;
                    worksheet.Cells[1, i + 1] = headerMap.ContainsKey(propName)
                        ? headerMap[propName]
                        : propName;
                }

                // ✅ FORCE TEXT FORMAT for specific columns
                for (int i = 0; i < colCount; i++)
                {
                    string propName = properties[i].Name;

                    if (propName == "FinalShopOrder" || propName == "ShopOrder")
                    {
                        Excel.Range colRange = worksheet.Columns[i + 1];
                        colRange.NumberFormat = "@"; // TEXT
                    }
                }

                // ✅ Header Styling
                Excel.Range headerRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[1, colCount]
                ];

                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSkyBlue);
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                headerRange.RowHeight = 20;
                headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Freeze header row
                worksheet.Application.ActiveWindow.SplitRow = 1;
                worksheet.Application.ActiveWindow.FreezePanes = true;

                object[,] dataArray = new object[rowCount, colCount];

                // Adding data rows
                int row = 0;
                foreach (var item in data)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        var value = properties[col].GetValue(item, null);
                        dataArray[row, col] = value?.ToString();
                    }
                    row++;
                }

                // ✅ Insert data
                Excel.Range dataRange = worksheet.Range[
                    worksheet.Cells[2, 1],
                    worksheet.Cells[rowCount + 1, colCount]
                ];
                dataRange.Value = dataArray;

                // Optional: add borders to data
                dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                dataRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                dataRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savedFilePath = saveFileDialog.FileName;
                    workbook.SaveAs(savedFilePath);
                    workbook.Close();
                    excelApp.Quit();

                    MessageBox.Show("Export Successful");

                    // Reopen the saved file
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = savedFilePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
