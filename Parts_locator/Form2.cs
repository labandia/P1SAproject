using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        List<AttendanceModel> attendanceList = new List<AttendanceModel>
{
    new AttendanceModel
    {
        RecordID = 1,
        Date_today = new DateTime(2026, 3, 15),
        Employee_ID = "EMP001",
        Fullname = "Juan Dela Cruz",
        TimeIn = "06:35",
        Shifts = "6:30 AM",
        LateTime = "00:05"
    },
    new AttendanceModel
    {
        RecordID = 2,
        Date_today = new DateTime(2026, 3, 15),
        Employee_ID = "EMP002",
        Fullname = "Maria Santos",
        TimeIn = "07:40",
        Shifts = "7:30 AM",
        LateTime = "00:10"
    },
    new AttendanceModel
    {
        RecordID = 3,
        Date_today = new DateTime(2026, 3, 15),
        Employee_ID = "EMP003",
        Fullname = "Pedro Reyes",
        TimeIn = "08:25",
        Shifts = "8:30 AM",
        LateTime = "00:00"
    },
    new AttendanceModel
    {
        RecordID = 4,
        Date_today = new DateTime(2026, 3, 15),
        Employee_ID = "EMP004",
        Fullname = "Ana Lopez",
        TimeIn = "08:45",
        Shifts = "8:30 AM",
        LateTime = "00:15"
    }
};

        public class AttendanceModel
        {
            private int _RecordID;
            private DateTime date_today;
            private string employeeID;
            private string fullname;
            private string timein;
            private string shift;
            private string lateTime;

            // Combined display for DataGridView
            public string Employee
            {
                get { return Fullname + Environment.NewLine + Employee_ID; }
            }
            public int RecordID
            {
                get => _RecordID;
                set => _RecordID = value;
            }

            public DateTime Date_today
            {
                get => date_today;
                set => date_today = value;
            }

            public string Employee_ID
            {
                get => employeeID;
                set => employeeID = value;
            }
            public string Fullname
            {
                get => fullname;
                set => fullname = value;
            }

            public string TimeIn
            {
                get => timein;
                set => timein = value;
            }

            public string Shifts
            {
                get => shift;
                set => shift = value;
            }
            public string LateTime
            {
                get => lateTime;
                set => lateTime = value;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            attendancetable.AutoGenerateColumns = false;

            // Create Employee column
            DataGridViewTextBoxColumn empCol = new DataGridViewTextBoxColumn();
            empCol.Name = "Employee";
            empCol.HeaderText = "Employee";
            empCol.DataPropertyName = "Employee"; // binds to AttendanceModel.Employee
            empCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            empCol.DefaultCellStyle.Padding = new Padding(10, 5, 5, 5);
            empCol.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            attendancetable.Columns.Add(empCol);

            // Other columns
            attendancetable.Columns.Add("Date_today", "Date");
            attendancetable.Columns["Date_today"].DataPropertyName = "Date_today";

            attendancetable.Columns.Add("TimeIn", "Time In");
            attendancetable.Columns["TimeIn"].DataPropertyName = "TimeIn";

            attendancetable.Columns.Add("Shifts", "Shift");
            attendancetable.Columns["Shifts"].DataPropertyName = "Shifts";

            attendancetable.Columns.Add("LateTime", "Late");
            attendancetable.Columns["LateTime"].DataPropertyName = "LateTime";

            attendancetable.DataSource = attendanceList;

            attendancetable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
