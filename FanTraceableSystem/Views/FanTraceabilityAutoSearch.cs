using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using static System.Collections.Specialized.BitVector32;
using Excel = Microsoft.Office.Interop.Excel;


namespace FanTraceableSystem
{
    public partial class FanTraceabilityAutoSearch : Form
    {
        //public int 
        private readonly ITraceable _trac;
        private readonly ISubassy _sub;

        private BindingList<TraceableOverAllSummaryModel> data 
            = new BindingList<TraceableOverAllSummaryModel>();

        private readonly PagingState _paging = new PagingState();

        private bool _isLoading = false;

        private BindingList<TraceableSubAssyModel> subassyform = new BindingList<TraceableSubAssyModel>();
        private BindingList<TraceableSubAssyModel> _editpcb = new BindingList<TraceableSubAssyModel>();

        public string CurrentShopOrder = "";    
        public int isEdit = 0;
        public int sectionID = 0;
        public int shiftToday = 0;
        public int isEditMode = 0; // By Default is in Add Mode, when click the Edit Button it will change to Edit Mode
        // But this is for the Edit
        public int isFilter = 0;
        public int finalId = 0;
        private Timer timer;
        private Timer _filterTimer;

        public FanTraceabilityAutoSearch(ITraceable trac, ISubassy sub,  int section)
        {
            InitializeComponent();
            _trac = trac;
            _sub = sub;

            shopfilter.SelectedIndex = 0;
            shiftToday = FanTraceabilityCore.GetShift();
            ShiftLabel.Text = FanTraceabilityCore.GetShift() == 0 ? "DAYSHIFT" : "NIGHTSHIFT";

            DatePrepared.Value = DateTime.Now;
            sectionID = section;    
            
            label18.Text = FanTraceabilityCore.SectionMap.ContainsKey(section)
                 ? FanTraceabilityCore.SectionMap[section]
                 : "Final Assy Section";

            // ✅ Bind ONCE
            dataGridView2.DataSource = data;

            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();

            _filterTimer = new Timer();
            _filterTimer.Interval = 400; // 400ms delay
            _filterTimer.Tick += async (s, e) =>
            {
                _filterTimer.Stop();

                _paging.PageNumber = 1;
                await LoadData();
            };

            // ✅ Smooth UI (reduce flicker)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dataGridView2, new object[] { true });
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeText.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
           

            if (!FormValidation()) return;

            ToggleUI(false);

            try
            {
                if (isEditMode == 1)
                {
                    await EditTraceAbility();
                    return;
                }

                var finaobj = new FinalTraceabilityModel
                {
                    FinalShopOrder = Shoptext.Text,
                    Revision = RevText.Text,
                    ItemNo = PCBText.Text,
                    PlanQuan = int.TryParse(PlanQuanText.Text, out var q) ? q : 0,
                    DatePrepared = DatePrepared.Value,
                    PreparedBy = PreparedText.Text,
                    Shift = shiftToday,
                    TimeInput = TimeText.Text,
                    Customer = CustomerText.Text,
                    Modeltype = Cardtext.Text,
                    Remarks = RemarkText.Text,
                    Incharge = PCBtextcharge.Text,
                    FinalIssuedby = PCBtextcharge.Text,
                    DepartmentID = sectionID
                };


                bool res = await _trac.AddTraceTransactions(finaobj, subassyform);

                if (res)
                {
                    MessageBox.Show("Shop Order added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SearchText.Text = Shoptext.Text;
                    FormReset();
                    await LoadData();

                }
                else
                {
                    MessageBox.Show("Failed to add Shop Order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            finally
            {
                ToggleUI(true);
            }
        }


        public async Task EditTraceAbility()
        {
            try
            {
                var finaobj = BuildFinalObject();


                bool res = await _trac.EditTraceTransaction(finaobj, _editpcb, CurrentShopOrder);

                if (res)
                {
                    MessageBox.Show("Edit Data successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SearchText.Text = Shoptext.Text;
                    Shoptext.Text = "";
                    PCBText.Text = "";
                    RevText.Text = "";
                    PreparedText.Text = "";
                    CustomerText.Text = "";
                    PlanQuanText.Text = "";
                    Cardtext.Text = "";
                    RemarkText.Text = "";
                    PCBtextcharge.Text = "";
                    textBox1.Text = "";
                    subassyform.Clear();

                    isEditMode = 0;
                    button3.Text = "Edit ";
                    button3.Visible = false;
                    button1.Visible = true;
                    button1.BringToFront();
                    await LoadData();

                }
                else
                {
                    MessageBox.Show("Failed to add Shop Order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
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
                using (var add = subassyform.Any()
                   ? new AddPCBShop(subassyform)   // EDIT MODE
                   : new AddPCBShop(_sub))          // ADD MODE
                {
                    if (add.ShowDialog(this) == DialogResult.OK)
                    {
                        subassyform = add.subassy;

                        string isAdd = subassyform.Count > 0 ? $@"({subassyform.Count})" : "";

                        button1.Text = "Add Production Order " + isAdd;
                    }
                }
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var edit = new EditPCBShop(_editpcb, _sub))
            {
                if (edit.ShowDialog(this) == DialogResult.OK)
                {
                    _editpcb = edit.subassy;

                    Debug.WriteLine("============ Edited Sub Assy List: ==============");   

                    //foreach (var items in _editpcb)
                    //{
                    //    Debug.WriteLine($@"FinalShop : {items.ShopOrder} - Lot No : {items.LotNo} - {items.PreparedQuantity} - {items.Line} - Action : {items.isAction}");
                    //}

                    string isAdd = _editpcb.Count > 0 ? $@"({_editpcb.Count})" : "";

                    button3.Text = "Edit" + isAdd;
                }
            }
        }

        // Reset the Data
        private async void filterbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchText.Text)) return;

            _paging.PageNumber = 1;   // ✅ Reset page
            data.Clear();             // optional cleanup

            await LoadData();         // replace data
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
                var dataTask =  _trac.TraceableShopOrder(
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
                UpdatePaginationUI(result.Count, totalCount);
                ArrangeColumns();
            }
            finally
            {
                _isLoading = false; 
            }
        }

        private async Task ShopOrderLoadData(bool append = false)
        {
            if (_isLoading) return; // prevent double load
            _isLoading = true;

            try
            {
                var result = await _trac.TraceSearchByShopOrder(
                    SearchText.Text, 
                    shopfilter.SelectedIndex    
                );
                BindGrid(result, append);
                //UpdatePaginationUI(result.Count, totalCount);
                ArrangeColumns();
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void BindGrid(List<TraceableOverAllSummaryModel> result, bool append)
        {
            if (!append)
                data.Clear();

            foreach (var item in result)
                data.Add(item);
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

            Prevbtn.Enabled = _paging.PageNumber > 1;
            nextbtn.Enabled = _paging.HasNextPage;
        }
     
        public void ArrangeColumns()
        {
            dataGridView2.Columns["FinalShopOrder"].DisplayIndex = 0;

            dataGridView2.Columns["ShopOrder"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["ShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView2.Columns["ShopOrder"].Width = 120;

            FanTraceabilityCore.ConfigureColumn(dataGridView2, "ItemNo", 180);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Revision", 150);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PreparedQuantity", 150);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "TimeInput", 100);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "PreparedBy", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Shift", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Customer", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Modeltype", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Remarks", 150);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Incharge", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "FinalIssuedby", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "SubAssyIssued", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "Line", 120);
            FanTraceabilityCore.ConfigureColumn(dataGridView2, "LotNo", 120);
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
        }
      

        // ================================================================================
        // ========================== FORMS AND VALIATION =================================
        // ================================================================================
      
        public bool FormValidation()
        {
            if(string.IsNullOrEmpty(Shoptext.Text) || string.IsNullOrEmpty(PCBText.Text)) {
                MessageBox.Show("Shop Order is required");
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
            PlanQuanText.Text = "";
            Cardtext.Text = "";
            RemarkText.Text = "";
            PCBtextcharge.Text = "";
            textBox1.Text = "";
            subassyform.Clear();

            button1.Text = "Add Production Order ";
            button1.Visible = true;
            button1.BringToFront();
            button4.Visible = false;
            button3.Visible = false;
        }

        // ================================================================================
        // ========================== FILTERS SEARCH FUNCTIONALITY ========================
        // ================================================================================
   
        private async void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            
            _paging.PageNumber = 1;

            if (string.IsNullOrEmpty(SearchText.Text)){
               shopfilter.SelectedIndex = 0; // reset to default if search is cleared
            }

            if (shopfilter.SelectedIndex == 0)
            {
                isEdit = 1;
            }
            else
            {
                isEdit = 2;
            }

            await LoadData();
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            TriggerDateFilter();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            TriggerDateFilter();
        }

        private void TriggerDateFilter()
        {
            // Only apply filter if at least one is checked
            if (!dateTimePicker2.Checked && !dateTimePicker3.Checked)
            {
                isEdit = 0;
            }
            else
            {
                isEdit = 1;
            }

            // Restart debounce timer
            _filterTimer.Stop();
            _filterTimer.Start();
        }
        // ================================================================================
        // ========================== EXPORT  DATA  =======================================
        // ================================================================================
        private async void Exportbtn_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(SearchText.Text)) return;

            //ToggleUI(false);

            try
            {
                var result = await _trac.TraceableShopOrder(
                     SearchText.Text,
                     dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                     dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                     isEdit,
                     sectionID,
                     _paging.PageNumber,
                     _paging.PageSize
                 );

                var exportData = result.Select(items => new ExportTraceableShopOrderModel
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
                }).ToList();

                ExportToExcel(exportData);
            }
            finally
            {
                ToggleUI(true);
            }
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

      


        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner?.Show(); // bring parent back
            this.Close();       // close child
        }

        private async void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header double-click
            if (e.RowIndex < 0)
                return;

            var row = dataGridView2.Rows[e.RowIndex];

            // Optional: select full row
            dataGridView2.ClearSelection();
            row.Selected = true;

            // If you're using DataBoundItem (recommended)
            var item = row.DataBoundItem as TraceableOverAllSummaryModel;
            if (item == null) return;


            var finalassyDetails = await _trac.TraceAbilityFinalAssy(item.FinalShopOrder, sectionID);

            if (finalassyDetails == null)
            {
                MessageBox.Show($@"Customer Shop Order Doesnt Exist or Wrong Input ");
                return;
            }

            finalId = finalassyDetails.RecordId;
            Shoptext.Text = finalassyDetails.FinalShopOrder;
            PCBText.Text = finalassyDetails.ItemNo;
            PlanQuanText.Text = finalassyDetails.PlanQuan.ToString();
            RevText.Text = finalassyDetails.Revision;
            CustomerText.Text = finalassyDetails.Customer;

            Cardtext.Text = finalassyDetails.Modeltype;
            RemarkText.Text = finalassyDetails.Remarks;
            PCBtextcharge.Text = finalassyDetails.Incharge;
            PreparedText.Text = finalassyDetails.PreparedBy;
            textBox1.Text = finalassyDetails.FinalIssuedby;

            var subassydata = await _sub.GetSubAssyDatalist(item.FinalShopOrder);

            if (subassydata != null)
            {
                _editpcb = new BindingList<TraceableSubAssyModel>(
                    subassydata.Select(i => new TraceableSubAssyModel
                    {
                        SubAssyID = i.SubAssyID,
                        ShopOrder = i.ShopOrder,
                        PreparedQuantity = i.PreparedQuantity,
                        Rev = i.Rev,
                        LotNo = i.LotNo,
                        Line = i.Line,
                        SubAssyIssued = i.SubAssyIssued,
                        isAction = 0
                    }).ToList()
                 );

                string isAdd = _editpcb.Count > 0 ? $@"({_editpcb.Count})" : "";

                isEditMode = 1;
                button1.Visible = false;
                button3.Visible = true;
                button4.Visible = true;
                button3.Text = "Edit " + isAdd;
                button3.BringToFront();
            }




        }


        private async void Shoptext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            var finalassyDetails = await _trac.TraceAbilityFinalAssy(Shoptext.Text, sectionID);

            if(finalassyDetails == null)
            {
                MessageBox.Show(
                      "Customer Shop Order doesn't exist or input is incorrect.",
                      "Error",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error
                  );
                return;
            }
            CurrentShopOrder = Shoptext.Text;   
            finalId = finalassyDetails.RecordId;
            PCBText.Text = finalassyDetails.ItemNo;
            PlanQuanText.Text = finalassyDetails.PlanQuan.ToString();
            RevText.Text = finalassyDetails.Revision;
            CustomerText.Text = finalassyDetails.Customer;

            Cardtext.Text = finalassyDetails.Modeltype;
            RemarkText.Text = finalassyDetails.Remarks;
            PCBtextcharge.Text = finalassyDetails.Incharge;
            PreparedText.Text = finalassyDetails.PreparedBy;
            textBox1.Text = finalassyDetails.FinalIssuedby;

            var subassydata = await _sub.GetSubAssyDatalist(Shoptext.Text);

            if(subassydata != null)
            {
                _editpcb = new BindingList<TraceableSubAssyModel>(
                    subassydata.Select(i => new TraceableSubAssyModel
                    {
                        SubAssyID = i.SubAssyID,
                        ShopOrder = i.ShopOrder,
                        PreparedQuantity = i.PreparedQuantity,
                        Rev = i.Rev,
                        LotNo = i.LotNo,
                        Line = i.Line,
                        SubAssyIssued = i.SubAssyIssued,    
                        isAction = 0
                    }).ToList()
                 );

                string isAdd = _editpcb.Count > 0 ? $@"({_editpcb.Count})" : "";

                isEditMode = 1;
                button1.Visible = false;
                button3.Visible = true;
                button4.Visible = true;
                button3.Text = "Edit " + isAdd;
                button3.BringToFront();
            }

        }

        private void PlanQuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !PlanQuanText.Text.Contains("."))) ? false : true; // Allow the character
        }

        private async void nextbtn_Click(object sender, EventArgs e)
        {
            if (!_paging.HasNextPage) return;

            _paging.PageNumber++;
            await LoadData(false);
        }

        private async void Prevbtn_Click(object sender, EventArgs e)
        {
            if (_paging.PageNumber <= 1) return;

            _paging.PageNumber--;
            await LoadData(false);     // ❗ replace
        }

        private FinalTraceabilityModel BuildFinalObject()
        {
            return new FinalTraceabilityModel
            {
                RecordId = finalId,
                FinalShopOrder = Shoptext.Text,
                Revision = RevText.Text,
                ItemNo = PCBText.Text,
                PlanQuan = int.TryParse(PlanQuanText.Text, out var q) ? q : 0,
                DatePrepared = DatePrepared.Value,
                PreparedBy = PreparedText.Text,
                Shift = shiftToday,
                TimeInput = TimeText.Text,
                Customer = CustomerText.Text,
                Modeltype = Cardtext.Text,
                Remarks = RemarkText.Text,
                Incharge = PCBtextcharge.Text,
                FinalIssuedby = PCBtextcharge.Text,
                DepartmentID = sectionID
            };
        }


        private void ToggleUI(bool enabled)
        {
            Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;

            SaveBtn.Enabled = enabled;
            nextbtn.Enabled = enabled && _paging.HasNextPage;
            Prevbtn.Enabled = enabled && _paging.PageNumber > 1;
            filterbtn.Enabled = enabled;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReset();
        }
    }
}
