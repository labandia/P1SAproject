
using Parts_locator.Models;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace Parts_locator
{
    public partial class Form1 : Form
    {
        Products p = new Products();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                // Path to the .application file
                string clickOnceApp = @"\\SDP04003C\PC_system\DesktopApp\Attendance_Monitoring.application"; // Update with actual path

                // Start the ClickOnce application
                Process.Start(clickOnceApp);
                this.Close();
            }
            catch (Exception ex)
            {
                // Handle any errors that occur when trying to start the application
                MessageBox.Show($"Error running ClickOnce application: {ex.Message}");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string trxFilePath = @"D:\PC_system\New folder\6. Rotor\1Jan2017.trx"; // Replace with your trx file path
            string excelFilePath = @"C:\Users\jaye-labandia\Desktop\output.xlsx"; // Replace with your desired Excel file path

            ConvertTrxToExcel(trxFilePath, excelFilePath);

        }


        public void GetFileAttachment(string accessFilePath, string query, string fileNameColumn, string filePathToSave)
        {
            // Connection string to your MS Access database
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={accessFilePath};Persist Security Info=False;";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Prepare query to retrieve the file data from MS Access
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the file name
                                string fileName = reader[fileNameColumn].ToString();

                                // Get the file data as a byte array
                                byte[] fileData = (byte[])reader["FileDataColumn"]; // Replace with your file data column

                                // Combine the file path and name to save the file
                                string fullPath = Path.Combine(filePathToSave, fileName);

                                // Write the file to disk
                                File.WriteAllBytes(fullPath, fileData);

                                MessageBox.Show($"File saved to {fullPath}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ConvertTrxToExcel(string trxFilePath, string excelFilePath)
        {
            DataTable trxDataTable = ReadTrxFile(trxFilePath);
            ExportTrxToExcel(trxDataTable, excelFilePath);
        }

        private void ExportTrxToExcel(DataTable trxDataTable, string filePath)
        {
            // Create an instance of Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // Create a new Workbook
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            // Add headers
            for (int i = 0; i < trxDataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = trxDataTable.Columns[i].ColumnName;
            }

            // Add data rows
            for (int i = 0; i < trxDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < trxDataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = trxDataTable.Rows[i][j].ToString();
                }
            }

            // Save the workbook
            workbook.SaveAs(filePath);
            workbook.Close();
            excelApp.Quit();

            // Release COM objects
            ReleaseObject(worksheet);
            ReleaseObject(workbook);
            ReleaseObject(excelApp);
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error releasing object: " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }


        private DataTable ReadTrxFile(string trxFilePath)
        {
            DataTable dataTable = new DataTable();

            using (var reader = new StreamReader(trxFilePath))
            {
                string headerLine = reader.ReadLine();
                if (headerLine != null)
                {
                    // Assuming the first line is the header
                    string[] headers = headerLine.Split('\t'); // Change '\t' to your delimiter
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            string[] rows = line.Split('\t'); // Change '\t' to your delimiter
                            dataTable.Rows.Add(rows);
                        }
                    }
                }
            }

            return dataTable;
        }

    }
}
