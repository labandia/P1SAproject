using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using ProductConfirm.Data;
using System.Data;
using System.Diagnostics;
using static System.Windows.Forms.AxHost;

namespace ProductConfirm.Modules
{
    public partial class Summary : UserControl
    {
        private readonly IProductRepositoryV2 _prod;
        public int checkedCount = 0;
        private readonly DataTable selectedRowsTable = new DataTable();

        public DataGridView summarygrid { get { return Summarytable; } }
        public Label Countresult { get { return Countrecord; } }

        public Summary(IProductRepositoryV2 prod)
        {
            InitializeComponent();
            _prod=prod; 
        }

        public async void ExportToExcel(string filePath)
        {
            
            // Initialize Excel application
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
            Excel.Worksheet worksheet = workbook.Sheets[1]; // Assuming the data is on the first sheet
            
            DataTable Headertd = new DataTable();
            string sdate = dateTimePicker1.Text;
            string fdate = dateTimePicker2.Text;

            //DateTime startDate = DateTime.ParseExact(sdate, "MM/dd/yyyy", null);
            //DateTime endDate = DateTime.ParseExact(fdate, "MM/dd/yyyy", null);

            Headertd = await Shopordersdata.GetDataAndExportoExcel();


            if (Headertd.Rows.Count > 0)
            {
                DataRow row = Headertd.Rows[0];

                //(ROWS , COLUMNS)
                //Model Name display
                worksheet.Cells[3, 5].Value = row["ProductType"].ToString();
                //Partnumber display
                worksheet.Cells[4, 5].Value = row["RotorAssy"].ToString();
                //Standard Machine pressure display
                worksheet.Cells[5, 5].Value = row["MachinePressureMinMax"].ToString();

                //Shaft Length Measurement
                worksheet.Cells[8, 7].Value = row["ShaftLength"].ToString();

                //Surface Edge Alignment Measurement
                worksheet.Cells[8, 14].Value = row["SurfaceEdge"].ToString();

                // Caulking Dent Height Measurement 
                worksheet.Cells[8, 21].Value = row["CaulkingDent"].ToString();

                //Shaft Pulling Force Measurement
                worksheet.Cells[8, 31].Value = row["ShaftPullingForce"].ToString();

                //Bush Pulling Force Measurement
                worksheet.Cells[8, 38].Value = row["BushPullingForce"].ToString();

                //Magnet Height Measurement
                worksheet.Cells[8, 45].Value = row["ShaftLength"].ToString();
            }

            DataTable dt = new DataTable();

            // Define the range to clear and fill
           // Excel.Range clearRange = worksheet.Range["B12", "BG61"];
            //clearRange.Clear();  // Clear the content in the range (both values and formatting)
            dt = await Products.GetSummaryDataConfirmation(Searchtext.Text.Trim());

            // Fill the worksheet with data
            int startRow = 12;  // Row 12 corresponds to B12
            int startCol = 2;   // Column 2 corresponds to column B

            for(int row = 0; row < dt.Rows.Count && row < (61 - startRow); row++)
            {
                for (int col = 0; col < dt.Columns.Count && col < (60 - startCol); col++)
                {
                    worksheet.Cells[startRow + row, startCol + col].Value = dt.Rows[row][col].ToString();
                }
            }


            // Save the excel file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string newFilePath = saveFileDialog.FileName + ".xlsx";

                // Save the workbook to the new location
                workbook.SaveAs(newFilePath);

                // Show success message
                MessageBox.Show("File saved successfully!");

                // Open the newly saved Excel file
                System.Diagnostics.Process.Start(newFilePath);

            }
            // Close the workbook
            workbook.Close(false);
            Marshal.ReleaseComObject(workbook);

            // Quit Excel application
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = "\\\\SDP010F6C\\Users\\USER\\Pictures\\Sample\\Files\\Records Final Output.xlsx";
            //string filePath = Path.Combine(Application.StartupPath, "Assets", "RecordsFinalOutput.xlsx");
            ExportToExcel(filePath);
           
        }

        private void Summarytable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Summarytable.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                bool isChecked = Convert.ToBoolean(Summarytable.Rows[e.RowIndex].Cells["Checkbox"].Value);

                //UPdates the count selected
                if (isChecked)
                {
                  
                }
                else
                {
                    
                }

      
            }
        }

        private void Summarytable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (Summarytable.IsCurrentCellDirty && Summarytable.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Commit the checkbox value immediately when changed
                Summarytable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            
        }

        private void Summarytable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //Summarytable.Columns["SL_supply"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        

        private async void Filterbtn_Click(object sender, EventArgs e)
        {
            //DateTime startd = DateTime.Parse(dateTimePicker1.Text);
            //DateTime enddate = DateTime.Parse(dateTimePicker2.Text);

            string sdate = dateTimePicker1.Text;
            string fdate = dateTimePicker2.Text;

            DateTime startDate = DateTime.ParseExact(sdate, "MM/dd/yyyy", null);
            DateTime endDate = DateTime.ParseExact(fdate, "MM/dd/yyyy", null);


            if (startDate <= endDate)
            {
                Summarytable.DataSource = await Products.GetSummaryDataConfirmation("");
                Countrecord.Text = "" + Summarytable.RowCount;
            }
            else
            {
                MessageBox.Show("Start date cannot be greater than the end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async  void button1_Click(object sender, EventArgs e)
        {
            Summarytable.DataSource = await Products.GetSummaryDataConfirmation("");
            Countrecord.Text = "" + Summarytable.RowCount;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Summarytable.DataSource = await Products.GetSummaryDataConfirmation(Searchtext.Text.Trim());
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            Searchtext.Text = "";
            Summarytable.DataSource = await Products.GetSummaryDataConfirmation("");
        }
    }
}
