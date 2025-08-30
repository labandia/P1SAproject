using Attendance_Monitoring.Repositories;
using System;
using OfficeOpenXml;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Attendance_Monitoring.View
{
    public partial class UploadsData : Form
    {
        private readonly EmployeeManage _emp;
        private readonly IEmployee _admin;


        public UploadsData(EmployeeManage emp, IEmployee admin)
        {
            InitializeComponent();
            _emp=emp;
            _admin = admin;
        }

        public async void SaveUpload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(filepathText.Text) && comboBox1.SelectedIndex != 0)
            {
          
                //DialogResult exit = MessageBox.Show("Are you  sure you want to reset this Masterlist", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //// DELETES THE WHOLE MASTERLIST DEPENDS ON THE SECTION 
                //if (exit == DialogResult.Yes)
                //{
                //    string strfilePath = openFileDialog1.FileName;
                //    DataTable dtExcel = ReadExcelFile(strfilePath);
                //    bool result = await _admin.UploadEmployee(dtExcel, comboBox1.SelectedIndex, 0);

                //    if (result)
                //    {
                //        _emp.Displayemployee(comboBox1.SelectedIndex);
                //        MessageBox.Show("UPLOAD DATA SUCCESSFULLY :");
                //        Visible = false;
                //    }
                //}
                //else
                //{
                //    // Updates only the list 
                //    DialogResult updates = MessageBox.Show("Do you want only to Update the Masterlist ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //    if (updates == DialogResult.Yes)
                //    {
                //        string strfilePath = openFileDialog1.FileName;
                //        DataTable dtExcel = ReadExcelFile(strfilePath);
                //        bool result = await _admin.UploadEmployee(dtExcel, comboBox1.SelectedIndex, 1);

                //        if (result)
                //        {
                //            _emp.Displayemployee(comboBox1.SelectedIndex);
                //            MessageBox.Show("UPLOAD DATA SUCCESSFULLY :");
                //            Visible = false;
                //        }
                //    }
                //}
              
            }
            else
            {
                MessageBox.Show("No no fill all the Required inputs");
            }
        }

        private void UploadsData_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void FileSelected_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                filepathText.Text = filePath;
            }
        }


        private DataTable ReadExcelFile(string filePath)
        {
            var dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[1]; // First worksheet
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // Get headers
                for (int col = 1; col <= colCount; col++)
                    dt.Columns.Add(worksheet.Cells[1, col].Text);

                // Get rows
                for (int row = 2; row <= rowCount; row++)
                {
                    bool isRowEmpty = true;
                    var newRow = dt.NewRow();

                    for (int col = 1; col <= colCount; col++)
                    {
                        string cellValue = worksheet.Cells[row, col].Text?.Trim();
                        if (!string.IsNullOrEmpty(cellValue))
                            isRowEmpty = false;

                        newRow[col - 1] = cellValue;
                    }

                    // Skip empty rows
                    if (!isRowEmpty)
                        dt.Rows.Add(newRow);
                }
            }

            return dt;
        }
    }
}
