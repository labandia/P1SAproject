using Parts_locator.Interface;
using System;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Parts_locator.View.Moldingbush.Modules
{
    public partial class BushSummary_in : UserControl
    {
        private readonly IRawMats _raw;
        public DataGridView shoptable {  get { return MoldShopordertable; } }

        public BushSummary_in(IRawMats raw)
        {
            InitializeComponent();
            _raw = raw;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            GlobalDb connect = new GlobalDb();
            DateTime startd = DateTime.Parse(dstart.Text, CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.Parse(dend.Text, CultureInfo.InvariantCulture);


            string sdate = startd.ToString("yyyy-MM-dd");
            string fdate = enddate.ToString("yyyy-MM-dd");

            string strsql = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput, " +
                           "FORMAT(DateInput, 'HH:mm:ss tt') as TimeInput, " +
                           "ShopOrder,PartNumber, " +
                           "Quantity,Inputby " +
                           "FROM Part_transaction_BushMold_shoporder " +
                           "WHERE CAST(DateInput AS DATE) between '"+ sdate +"' AND " +
                           "'"+ fdate +"' AND Action = 0  ORDER BY DateInput DESC";
            DataTable dt = connect.GetData(strsql);
            ExportExcel(dt);
        }


        public void ExportExcel(DataTable dt)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported Data";

                // Adding headers
                for (int i = 1; i < dt.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dt.Columns[i - 1].ColumnName;
                }

                // Adding data rows
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // Precede the value with an apostrophe to treat it as text
                        worksheet.Cells[i + 2, j + 1] = "'" + dt.Rows[i][j].ToString();
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

                    workbook.SaveAs(saveFileDialog.FileName);
                    // Show success message
                    MessageBox.Show("File exported successfully!");

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalDb connect = new GlobalDb();
            DateTime startd = DateTime.Parse(dstart.Text, CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.Parse(dend.Text, CultureInfo.InvariantCulture);


            string sdate = startd.ToString("yyyy-MM-dd");
            string fdate = enddate.ToString("yyyy-MM-dd");

            string strsql = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput, " +
                           "FORMAT(DateInput, 'HH:mm:ss tt') as TimeInput, " +
                           "ShopOrder,PartNumber, " +
                           "Quantity,Inputby " +
                           "FROM Part_transaction_BushMold_shoporder " +
                           "WHERE CAST(DateInput AS DATE) between '"+ sdate +"' AND " +
                           "'"+ fdate +"' AND Action = 0  ORDER BY DateInput DESC";
            DataTable dt = connect.GetData(strsql);

            MoldShopordertable.DataSource = dt;
        }
    }
}
