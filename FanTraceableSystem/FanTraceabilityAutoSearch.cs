using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        List<TraceableShopOrderModel> data = new List<TraceableShopOrderModel>();
        private List<TracePCBModel> _pcbList = new List<TracePCBModel>();
        public FanTraceabilityAutoSearch(ITraceable trac, int section)
        {
            InitializeComponent();
            _trac = trac;

            shiftText.SelectedIndex = GetShift();
            DatePrepared.Value = DateTime.Now;

            sectionID = section;    
            var sectionMap = new Dictionary<int, string>
            {
                {1, "Molding Section"},
                {2, "Press Section"},
                {3, "Rotor Section"},
                {4, "Winding Section"},
                {5, "Circuit Section"}
            };

            label18.Text = sectionMap.ContainsKey(section)
                 ? sectionMap[section]
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var add = _pcbList.Any()
               ? new AddPCBShop(_pcbList)   // EDIT MODE
               : new AddPCBShop())          // ADD MODE
            {
                if (add.ShowDialog(this) == DialogResult.OK)
                {
                    _pcbList = add.PCBList;

                    string isAdd = _pcbList.Count > 0 ? $@"({_pcbList.Count})" : ""; 

                    button1.Text = "Products Order " + isAdd;  
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

            await dispayData(isEdit);
        }

        public async Task dispayData(int isfilter)
        {
            string filterText = SearchText.Text;

            var result = await _trac.TraceableShopOrder(
                filterText,
                dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null,
                isfilter, 
                sectionID
            );

            dataGridView2.AutoGenerateColumns = true;

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = result;


            ArrangeColumns();
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

            if (string.IsNullOrEmpty(RevText.Text))
            {
                MessageBox.Show("Revision is required ");
                return false;
            }

            return true;
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

        private void button10_Click(object sender, EventArgs e)
        {
            //var openHistory = new TraceableHistory();
            //openHistory.ShowDialog();   

        }

        public int GetShift()
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            TimeSpan dayStart = new TimeSpan(6, 30, 0);    // 6:30 AM
            TimeSpan nightStart = new TimeSpan(18, 30, 0); // 6:30 PM

            // Day shift: 6:30 AM to before 6:30 PM
            if (time >= dayStart && time < nightStart)
            {
                return 0; // Day shift
            }

            // Night shift
            return 1; // Night shift
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Shift")
            {
                string checkstats = e.Value.ToString() == "0" ? "DAYSHIFT" : "NIGHTSHIFT";
                e.Value = checkstats;

                e.FormattingApplied = true;
            }
        }

        private async void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            await dispayData(isEdit);
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            isEdit = 1;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            isEdit = 1;
        }

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
                sectionID   
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
            button1.Text = "Products Order ";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private async void FanTraceabilityAutoSearch_Load(object sender, EventArgs e)
        {
            await dispayData(isEdit);
        }
    }
}
