using Attendance_Monitoring.Global;
using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.View.V2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class AttendancePage : UserControl
    {
        private readonly IAttendanceMonitor _monitor;
        private readonly IEmployee _emp;

        private readonly IServiceProvider _serviceProvider;
        
        private List<P1SA_AttendanceModel> _attendanceList;
        private List<P1SA_SummaryDataModel> _summaryList;

        private readonly Timer _timer = new Timer();
        private readonly Timer _clockTimer = new Timer();
        public int DepartmentID { get; set; }
        public bool IsFiltered = false;

        private readonly string[] SectionName =
        {
            "Molding", "Press", "Rotor", "Winding", "Circuit", "Process Control"
        };

        public AttendancePage(IAttendanceMonitor monitor, IEmployee emp, int DepID)
        {
            InitializeComponent();

            _monitor = monitor;
            _emp = emp;
            DepartmentID = DepID;

            InitializeTimers();
            InitializeControls();
        }

        private void InitializeTimers()
        {
            // Status reset timer
            _timer.Interval = 1000;
            _timer.Tick += ResetStatus;

            // Real-time clock timer
            _clockTimer.Interval = 1000;
            _clockTimer.Tick += ClockTimer_Tick;
        }

        private void InitializeControls()
        {
            shiftselect.SelectedIndex = 0;

            dstart.Value = DateTime.Today;
            dend.Value = DateTime.Today;

            Timeclock.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }


        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            Timeclock.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public void ConfigGrid()
        {
            attendancetable.AutoGenerateColumns = false;

            attendancetable.Columns["RecordID"].DisplayIndex = 0;

            attendancetable.Columns["Date_today"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["Date_today"].DisplayIndex = 1;

            attendancetable.Columns["Employee_ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["Employee_ID"].DisplayIndex = 2;

            attendancetable.Columns["Fullname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["Fullname"].DisplayIndex = 3;

            attendancetable.Columns["Shifts"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["Shifts"].DisplayIndex = 4;

            attendancetable.Columns["LateTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["LateTime"].DisplayIndex = 5;

            attendancetable.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["Edit"].DisplayIndex = 6;

            attendancetable.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attendancetable.Columns["Delete"].DisplayIndex = 7;
        }

        // ================= DISPLAY ATTENDANCE =================
        public async Task LoadAttendanceAsync()
        {
            try
            {
                var filter = GetFilter();

                var response = await _monitor.GetAttendanceRecordsList(
                    filter.Text,
                    filter.StartDate,
                    filter.EndDate,
                    filter.Shift,
                    selecttime.SelectedIndex, 
                    DepartmentID
                 );

                ConfigGrid();

                _attendanceList = response.Success 
                    ? response.Payload?.ToList() ?? new List<P1SA_AttendanceModel>()
                    : new List<P1SA_AttendanceModel>();

                attendancetable.SuspendLayout();
                attendancetable.DataSource = _attendanceList;
                attendancetable.ResumeLayout();

                DisplayTotal.Text = $"Total Attendance: {attendancetable.RowCount}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Error found at Retreiving Employee Data", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= FILTER =================

        private (string Text, DateTime StartDate, DateTime EndDate, int Shift, int Type) GetFilter()
        {
            bool isTimeIn = selecttime.SelectedIndex == 0;

            int shift = isTimeIn
                ? Timeprocess.TimeIncheckAsIntV2(DateTime.Now)
                : Timeprocess.TimeoutcheckV2(DateTime.Now);

            return (
                textBox2.Text.Trim(),
                dstart.Value,
                dend.Value,
                shift,
                selecttime.SelectedIndex
            );
        }

        //  ####################  PERFORMS THE TIME IN AND OUT  #################### //
        private async void EnterTime(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            try
            {
                string empid = EmployID.Text.Replace("-", "").Trim();
                string shift = Timeprocess.TimeIncheck(DateTime.Now);

                // Filter employee once
                var employee = await _emp.GetEmployeeByID(empid, DepartmentID);

                if (employee == null)
                {
                    ShowStatus("Wrong ID number", Color.Red);
                    return;
                }

                TextName.Text = employee.Fullname;

                if (selecttime.SelectedIndex == 0)
                    await ProcessTimeIn(empid);
                else
                    await ProcessTimeOut(empid);


             
                ////Time In Process
                //if (selecttime.SelectedIndex == 0)
                //{
                //    var res = await _monitor.AttendanceTimeIn(empid, Timeprocess.CalculateLateTime());

                //    if (res.Success)
                //    {
                //        TextName.Text = employee.Fullname;
                //        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                //        Statustext.Text = res.Message;
                //        timer.Start();
                //        await TimeAttendanceDisplay();
                //    }
                //    else
                //    {
                //        MessageBox.Show(res.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }
                //}
                //// Time Out Process
                //else if (selecttime.SelectedIndex == 1)
                //{
                //    var Outresult = await _monitor.AttendanceTimeOut(empid);
                //    if (Outresult.Success)
                //    {
                //        TextName.Text = employee.Fullname;
                //        // Code to execute after the delay
                //        Statustext.BackColor = Color.FromArgb(50, 181, 111);
                //        Statustext.Text = Outresult.Message;
                //        timer.Interval = 1000; // 2000 milliseconds = 2 seconds
                //        timer.Tick += TimerOut_Tick;
                //        timer.Start();
                //        await TimeAttendanceDisplay();
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("SELECT TIME IN / OUT");
                //}
            }
            catch (FormatException)
            {
                MessageBox.Show("Error Encounter During Time IN / OUT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task ProcessTimeIn(string empId)
        {
            var result = await _monitor.AttendanceTimeIn(empId, Timeprocess.CalculateLateTime());

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowStatus(result.Message, Color.FromArgb(50, 181, 111));
            await LoadAttendanceAsync();
        }
        private async Task ProcessTimeOut(string empId)
        {
            var result = await _monitor.AttendanceTimeOut(empId);

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowStatus(result.Message, Color.FromArgb(50, 181, 111));

            await LoadAttendanceAsync();
        }
        // ================= STATUS =================
        private void ShowStatus(string message, Color color)
        {
            Statustext.Text = message;
            Statustext.BackColor = color;

            _timer.Stop();
            _timer.Start();
        }
        private void ResetStatus(object sender, EventArgs e)
        {
            _timer.Stop();

            Statustext.BackColor = Color.FromArgb(26, 36, 59);
            Statustext.Text = "Checking status ...";

            EmployID.Clear();
            TextName.Text = "";

            EmployID.Focus();
        }


    
        private void AttendancePage_Load(object sender, EventArgs e)
        {
            _clockTimer.Start();

            if (DepartmentID >= 0)
            {
                label7.Text = $"Daily Attendance : {SectionName[DepartmentID - 1]}";
            }
        }

        // ================= UI EVENTS =================
        private void Resetbtn_Click(object sender, EventArgs e)
        {
            shiftselect.SelectedIndex = 0;
            dstart.Value = DateTime.Now;
            dend.Value = DateTime.Now;
            textBox2.Text = "";
        }

        private async void Searchinput(object sender, EventArgs e)
        {
            if (IsFiltered == false) return;

            await LoadAttendanceAsync();
            IsFiltered = true;
        }

        private async void shiftselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFiltered == false) return;

            await LoadAttendanceAsync();
            IsFiltered = true;
        }
        private async void Selecttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsFiltered = true;
            await LoadAttendanceAsync();
        }
     
        private async void dstart_ValueChanged(object sender, EventArgs e)
        {
            if (IsFiltered == false) return;

            await LoadAttendanceAsync();
            IsFiltered = true;
        }

        private async void dend_ValueChanged(object sender, EventArgs e)
        {
            if (IsFiltered == false) return;

            await LoadAttendanceAsync();
            IsFiltered = true;
        }

        // ================= DATAGRID =================

        private void Attendancetable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (attendancetable.Columns[e.ColumnIndex].Name != "LateTime") return;

            if (e.Value == null) return;

            string value = e.Value.ToString();

            if (value == "00:00")
                e.Value = "-";
            else
                e.CellStyle.ForeColor = Color.Red;
        }



        // ================= EXPORT =================
        private async void exportbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filter = GetFilter();

                var response = await _monitor.GetAttendanceSummaryList(
                    filter.Text,
                    filter.StartDate,
                    filter.EndDate,
                    filter.Shift,
                    filter.Type,
                    DepartmentID
                );

                _summaryList = response.Payload?.ToList() ?? new List<P1SA_SummaryDataModel>();

                ExportToExcel(_summaryList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ExportToExcel(List<P1SA_SummaryDataModel> data)
        {
            var excel = new Excel.Application();
            var workbook = excel.Workbooks.Add();
            var sheet = (Excel.Worksheet)workbook.ActiveSheet;

            var props = typeof(P1SA_SummaryDataModel).GetProperties();

            for (int i = 0; i < props.Length; i++)
                sheet.Cells[1, i + 1] = props[i].Name;

            object[,] arr = new object[data.Count, props.Length];

            for (int r = 0; r < data.Count; r++)
            {
                for (int c = 0; c < props.Length; c++)
                {
                    var val = props[c].GetValue(data[r]);
                    arr[r, c] = DepartmentID != 2 ? "'" + val?.ToString() : val?.ToString();
                }
            }

            Excel.Range range = sheet.Range[
                sheet.Cells[2, 1],
                sheet.Cells[data.Count + 1, props.Length]
            ];

            range.Value = arr;

            SaveFileDialog save = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx"
            };

            if (save.ShowDialog() != DialogResult.OK) return;

            workbook.SaveAs(save.FileName);

            workbook.Close();
            excel.Quit();

            MessageBox.Show("Export Successful");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = save.FileName,
                UseShellExecute = true
            });
        }

        private async void attendancetable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var product = (P1SA_AttendanceModel)attendancetable.Rows[e.RowIndex].DataBoundItem;

            if (attendancetable.Columns[e.ColumnIndex].Name == "Edit")
            {
                using (var edit = new EditAttandance(product, _monitor))
                {
                    if(DialogResult.OK == edit.ShowDialog()) {
                        await LoadAttendanceAsync();
                    }
                }
            }
            if (attendancetable.Columns[e.ColumnIndex].Name == "Delete")
            {
                DialogResult exit = MessageBox.Show("Are you sure you want to delete this Attendance", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (exit == DialogResult.Yes)
                {
                    await _monitor.DeleteAttandance(product.RecordID);
                    MessageBox.Show("Delete Attendance successful.");
                    await LoadAttendanceAsync();
                }
            }
        }
    }
}
