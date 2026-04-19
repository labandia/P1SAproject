using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using Excel = Microsoft.Office.Interop.Excel;


namespace FanTraceableSystem
{
    public partial class FanTraceabilityAutoSearch : Form
    {
        //public int 
        private readonly ITraceable _trac;
        public int isEdit = 0;
        public int sectionID = 0;

        private Timer timer;

        // But this is for the Edit
        private bool _isLoading = false;
        public int isFilter = 0;

      

        private BindingList<TraceableShopOrderModel> data;
        private List<TracePCBModel> _pcbList = new List<TracePCBModel>();
        private List<EditTracePCBModel> _editpcb = new List<EditTracePCBModel>();

        private readonly PagingState _paging = new PagingState();

        public FanTraceabilityAutoSearch(ITraceable trac, int section)
        {
            InitializeComponent();
            _trac = trac;

            shiftText.SelectedIndex = FanTraceabilityCore.GetShift();
            DatePrepared.Value = DateTime.Now;

            sectionID = section;    
            
            label18.Text = FanTraceabilityCore.SectionMap.ContainsKey(section)
                 ? FanTraceabilityCore.SectionMap[section]
                 : "Final Assy Section";

            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeText.Text = DateTime.Now.ToString("hh:mm tt");
            TimeText.ReadOnly = true;
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!FormValidation()) return;

            try
            {
                var obj = new TraceableShopOrderModel
                {
                    FinalShopOrder = Shoptext.Text,
                    PCBA = PCBText.Text,
                    PlanQuan = int.TryParse(PlanQuanText.Text, out var q) ? q : 0,
                    PCBShopOrder = PCBText.Text,
                    Revision = RevText.Text,
                    DatePrepared = DatePrepared.Value,
                    Shift = shiftText.SelectedIndex,
                    TimeInput = TimeText.Text,
                    Customer = CustomerText.Text,
                    LotNo = LotText.Text,
                    CardCaseNo = Cardtext.Text,
                    Remarks = RemarkText.Text,
                    PCBIncharge = PCBtextcharge.Text,
                    PCBIssuer = PCBIssuerText.Text,
                    PreparedBy = PreparedText.Text,
                    DepartmentID = sectionID    
                };

                bool res = await _trac.AddTraceTransactions(obj, _pcbList);

                if (res)
                {
                    MessageBox.Show("Shop Order added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormReset();
                }
                else
                {
                    MessageBox.Show("Failed to add Shop Order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isFilter == 1)
            {
                //using(var edit = new EditPCBShop(_pcbList))
                //{
                //    if (edit.ShowDialog(this) == DialogResult.OK)
                //    {
                //        _pcbList = edit.PCBList;

                //        string isAdd = _pcbList.Count > 0 ? $@"({_pcbList.Count})" : "";

                //        button1.Text = "Add Production Order " + isAdd;
                //    }
                //}
            }
            else
            {
                using (var add = _pcbList.Any()
                   ? new AddPCBShop(_pcbList)   // EDIT MODE
                   : new AddPCBShop())          // ADD MODE
                {
                    if (add.ShowDialog(this) == DialogResult.OK)
                    {
                        _pcbList = add.PCBList;

                        string isAdd = _pcbList.Count > 0 ? $@"({_pcbList.Count})" : "";

                        button1.Text = "Add Production Order " + isAdd;
                    }
                }
            } 
        }

        // Reset the Data
        private async void filterbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchText.Text))
            {
                return;
            }
            await LoadData();
        }

        // ================================================================================
        // ========================== DATA GRIDVIEW & PAGINATION ==========================
        // ================================================================================
        private async void FanTraceabilityAutoSearch_Load(object sender, EventArgs e) => await LoadData();

        private async Task LoadData(bool append = false)
        {
            if (_isLoading) return; // prevent double load
            _isLoading = true;

            try
            {
                var dataTask = _trac.TraceableShopOrder(
                    SearchText.Text,
                    dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                    dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                    isEdit,
                    sectionID,
                    _paging.PageNumber, 
                    _paging.PageSize
                );

                var countTask = _trac.GetTraceableCount(
                    SearchText.Text,
                    dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                    dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                    isEdit,
                    sectionID
                 );

                await Task.WhenAll(dataTask, countTask);

                var result = dataTask.Result;
                var totalCount = countTask.Result;

                BindGrid(result, append);   


                // Compute range
                var (start, end, hasNext) = FanTraceabilityCore.CalculatePage(_paging.PageNumber, _paging.PageSize, result.Count, totalCount);

                // Fix when no data
                if (totalCount == 0)
                {
                    start = 0;
                    end = 0;
                }


                ArrangeColumns();
                UpdatePaginationUI(result.Count, totalCount);

              
                _isLoading = false;
            }
            finally
            {
                _isLoading = false; 
            }
        }
        private void BindGrid(List<TraceableShopOrderModel> result, bool append)
        {
            if (append)
            {
                foreach (var item in result)
                    data.Add(item);
            }
            else
            {
                data = new BindingList<TraceableShopOrderModel>(result);
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
            dataGridView2.Columns["PCBShopOrder"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["PCBShopOrder"].DisplayIndex = 0;
            dataGridView2.Columns["PCBShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["PCBShopOrder"].Width = 100;

            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PCBA", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PreparedQuantity", 200);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "TimeInput", 100);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PreparedBy", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Shift", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Customer", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "CardCaseNo", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Remarks", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PCBIncharge", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PCBIssuer", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "LotNo", 120);
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Shift")
            {
                if (e.Value == null) return;

                e.Value = FanTraceabilityCore.FormatShift(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }
        private async void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_paging.HasNextPage || _isLoading) return;

            int visibleRows = dataGridView2.DisplayedRowCount(false);
            int firstVisibleRow = dataGridView2.FirstDisplayedScrollingRowIndex;

            // If user scrolls near bottom
            if (firstVisibleRow + visibleRows >= dataGridView2.RowCount - 5)
            {
                _paging.PageNumber++;               // next page
                await LoadData(append: true); // append data
            }
        }

        // ================================================================================
        // ========================== FORMS AND VALIATION =================================
        // ================================================================================
        private async void Editbtn_Click(object sender, EventArgs e)
        {
            isFilter = 1;

            if (string.IsNullOrEmpty(Shoptext.Text))
            {
                MessageBox.Show("Required to enter the FinalShop Order");
                return;
            }

            var items = await _trac.GetFinalShopOrderDetails(Shoptext.Text);

            if (items.Count != 0)
            {
                var filterdata = items.FirstOrDefault();

                if (filterdata != null)
                {
                    Debug.WriteLine($@"PCBA {filterdata.PCBA}");
                    PCBText.Text = filterdata.PCBA;
                    RevText.Text = filterdata.Revision;
                    PreparedText.Text = filterdata.PreparedBy;
                    CustomerText.Text = filterdata.Customer;
                    LotText.Text = filterdata.LotNo;
                    Cardtext.Text = filterdata.CardCaseNo;
                    RemarkText.Text = filterdata.Remarks;
                    PCBtextcharge.Text = filterdata.PCBIncharge;
                    PCBIssuerText.Text = filterdata.PCBIssuer;
                }

                foreach (var i in items)
                {
                    _editpcb.Add(new EditTracePCBModel
                    {
                        RecordId = i.RecordId,
                        PCBShopOrder = i.PCBShopOrder,
                        Quantity = i.PreparedQuantity,
                        Rev = i.Rev,
                        isAction = 1
                    });
                }


                string isAdd = _pcbList.Count > 0 ? $@"({_pcbList.Count})" : "";

                button1.Text = "Add Production Order " + isAdd;
            }


        }


        public bool FormValidation()
        {
            if(string.IsNullOrEmpty(Shoptext.Text)) {
                MessageBox.Show("Shop Order is required");
                return false;   
            }

            if (string.IsNullOrEmpty(PCBText.Text))
            {
                MessageBox.Show("PCBA is required ");
                return false;
            }
            return true;
        }
        public void FormReset()
        {
            Shoptext.Text = "";
            PCBText.Text = "";
            RevText.Text = "";
            PreparedText.Text = "";
            CustomerText.Text = "";
            LotText.Text = "";
            Cardtext.Text = "";
            RemarkText.Text = "";
            PCBtextcharge.Text = "";
            PCBIssuerText.Text = "";
            _pcbList.Clear();
            button1.Text = "Add Production Order ";
        }

        // ================================================================================
        // ========================== FILTERS SEARCH FUNCTIONALITY ========================
        // ================================================================================
        private async void btnNext_Click(object sender, EventArgs e)
        {
           
        }
        private async void btnPrev_Click(object sender, EventArgs e)
        {
            
        }
        private async void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            await LoadData();
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            isEdit = 1;
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            isEdit = 1;
        }
        // ================================================================================
        // ========================== EXPORT  DATA  =======================================
        // ================================================================================
        private async void Exportbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchText.Text))
            {
                return;
            }

            string filterText = SearchText.Text;

            var result = await _trac.TraceableShopOrder(
                filterText,
                dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                isEdit,
                sectionID, 
                _paging.PageNumber,
                _paging.PageSize
            );

            ExportToExcel(result, 1);
        }
        private void ExportToExcel(List<TraceableShopOrderModel> data, int section)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported Data";

                var properties = typeof(TraceableShopOrderModel).GetProperties();
                int colCount = properties.Length;
                int rowCount = data.Count();

                // Insert headers
                for (int i = 0; i < colCount; i++)
                {
                    worksheet.Cells[1, i + 1] = properties[i].Name;
                }

                object[,] dataArray = new object[rowCount, colCount];

                // Adding data rows
                int row = 0;
                foreach (var item in data)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        var value = properties[col].GetValue(item, null);
                        dataArray[row, col] = section != 2 ? "'" + value?.ToString() : value?.ToString();
                    }
                    row++;
                }

                // Assign data in one go for better performance
                Excel.Range dataRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[rowCount + 1, colCount]];
                dataRange.Value = dataArray;

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

      


        private void button2_Click(object sender, EventArgs e) => this.Close();

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header double-click
            if (e.RowIndex < 0)
                return;

            var row = dataGridView2.Rows[e.RowIndex];

            // Optional: select full row
            dataGridView2.ClearSelection();
            row.Selected = true;

            // If you're using DataBoundItem (recommended)
            var item = row.DataBoundItem as TraceableShopOrderModel;
            if (item == null) return;


            MessageBox.Show($"Shop Order: {item.PCBShopOrder}\nPCBA: {item.PCBA}\nRevision: {item.Revision}\nPrepared By: {item.PreparedBy}", "Shop Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
   

        private void Shoptext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
        }

      

        private void PlanQuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !PlanQuanText.Text.Contains("."))) ? false : true; // Allow the character
        }

    }
}
