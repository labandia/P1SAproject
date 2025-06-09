using Attendance_Monitoring.Controller;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace Attendance_Monitoring.View
{
    public partial class EmployeeManage : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmployee _emp;
        private static IEnumerable<Employee> emplist;

        public EmployeeManage(IEmployee emp, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _emp = emp;
            _serviceProvider=serviceProvider;
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<Mainpage>();
            // Show the main form
            mainpage.Show();
            // Hide the login form (optional)
            this.Hide();
            Visible = false;
        }


        public async void Displayemployee()
        {
            IEnumerable<Employee> items = await _emp.GetEmployees();
            emplist = items.AsParallel().ToList();
            Employeetable.DataSource = emplist;
            DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
        }


        private async void PopulateComboBox()
        {
            IEnumerable<Department> items = await _emp.GetDepartments();

            // Convert the items to a list and add a default item
            List<Department> itemList = items.ToList();
            itemList.Insert(0, new Department { Department_ID = 0, Department_name = "All Section" });

            comboBox1.DataSource = itemList;
            comboBox1.DisplayMember = "Department_name";
            comboBox1.ValueMember = "Department_ID";

            // Optionally set the default selected index to the first item
            comboBox1.SelectedIndex = 0;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            Add_Employee form2 = new Add_Employee(this, _emp);
            form2.Show();
        }

        private  void exportbtn_Click(object sender, EventArgs e)
        {
            int dep = comboBox1.SelectedIndex + 1;
            string strquery = "SELECT Employee_ID, FullName, Process, Affiliation, " +
                              "Department_ID FROM Employee_tbl WHERE Department_ID = "+ dep +"";
            //ExportToExcel(await cons.GetData(strquery));
        }


        private void ExportToExcel(DataTable dataTable)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported Data";

                // Adding headers
                for (int i = 1; i < dataTable.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
                }

                //Adding data rows
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                    }
                }

                // Save the excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();
                    MessageBox.Show("Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.ToLower();
            // Filter the list using LINQ

            if (comboBox1.SelectedIndex == 0)
            {
                var filteredList = emplist.Where(p => p.EmployeeID.ToLower().Contains(filterText) ||
                       p.Fullname.ToLower().Contains(filterText))
                       .ToList();

                Employeetable.DataSource =  filteredList;
                DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
            }
            else
            {
                if (filterText != "")
                {
                    var filteredList = emplist.Where(p => p.EmployeeID.ToLower().Contains(filterText) ||
                    p.Fullname.ToLower().Contains(filterText) && p.Department_ID == comboBox1.SelectedIndex)
                    .ToList();
                    Employeetable.DataSource =  filteredList;
                    DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
                }
                else
                {
                    var filteredList = emplist.Where(p => p.Department_ID == comboBox1.SelectedIndex).ToList();
                    Employeetable.DataSource =  filteredList;
                    DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
                }
                      
            }
        }

        private  void Employeetable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a header
            if (e.RowIndex < 0)
            {
                // Do nothing or handle the header click as needed
                return;
            }
            // Proceed with normal cell click processing
            try
            {
                string EmployeeID = Employeetable.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (e.ColumnIndex == 0)
                {
                    EditEmployee ed = new EditEmployee(this, _emp);
                    ed.EmpID.Text = EmployeeID;
                    ed.Fullname.Text = Employeetable.Rows[e.RowIndex].Cells[3].Value.ToString();
                    ed.Process.Text = Employeetable.Rows[e.RowIndex].Cells[4].Value.ToString();
                    ed.TempID.Text = EmployeeID;
                    ed.Afili.Text = Employeetable.Rows[e.RowIndex].Cells[5].Value.ToString();
                    ed.comboBox1.SelectedIndex = Convert.ToInt32(Employeetable.Rows[e.RowIndex].Cells[6].Value.ToString());
                    ed.ShowDialog();
                }
                else if (e.ColumnIndex == 1)
                {
                    DialogResult exit = MessageBox.Show("Are you  sure you want to delete this Employee ID", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (exit == DialogResult.Yes)
                    {
                        //EmployeeID
                        //bool result = await _admin.DeleteEmployee(EmployeeID);

                        //if (result)
                        //{
                        //    MessageBox.Show("Delete data successfully");
                         //   Displayemployee();
                        //}
                    }

                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }   
        }

        private void EmployeeManage_Load(object sender, EventArgs e)
        {
            //SET THE SECTION MOLDING DISPLAY TABLE
            Displayemployee();
            PopulateComboBox();
        }


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
            if (comboBox1.SelectedIndex == 0)
            {
                Displayemployee();
            }
            else
            {
                var filteredList = emplist.Where(p => p.Department_ID == comboBox1.SelectedIndex).ToList();
                Employeetable.DataSource =  filteredList;
                DisplayTotal.Text = "Total Records: " + Employeetable.RowCount;
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UploadsData form2 = new UploadsData(this, _emp);
            form2.Show();
            //openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string filePath = openFileDialog1.FileName;
            //    DataTable dtExcel = ReadExcelFile(filePath);
            //    DialogResult exit = MessageBox.Show("Are you  sure you want to reset this Masterlist", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (exit == DialogResult.Yes)
            //    {
            //        bool result = await _emp.UploadEmployee(dtExcel);

            //        if (result)
            //        {
            //            MessageBox.Show("UPLOAD DATA SUCCESSFULLY :");
            //        }
            //    }
            //    //foreach (DataRow row in dtExcel.Rows)
            //    //{
            //    //    Debug.WriteLine("Part Number: " + row["EmployeeID"]);
            //    //}
            //}
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

        private void Removebtn_Click(object sender, EventArgs e)
        {

        }
    }
}
