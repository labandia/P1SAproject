﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Attendance_Monitoring.Models
{
    public class AttendanceModel
    {
        private DateTime date_today;
        private string employeeID;
        private string fullname;
        private string timein;
        private string shift;
        private string lateTime;


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
    public class SummaryAttendanceModel
    {
        private string date_today;
        private string employee_ID = "";
        private string fullname = "";
        private string timein;
        private string timeout;
        private double regular;
        private double gtotal;
        private double overtime;
        private string lateTime;
        private string shifts;

        public string Date_today
        {
            get => date_today;
            set => date_today = value;
        }
        public string Employee_ID
        {
            get => employee_ID;
            set => employee_ID = value;
        }
        public string Fullname
        {
            get => fullname;
            set => fullname = value;
        }

        public string Timein
        {
            get => timein;
            set => timein = value;
        }

        public string Timeout
        {
            get => timeout;
            set => timeout = value;
        }
        public double Regular
        {
            get => regular;
            set => regular = value;
        }

        public double Overtime
        {
            get => overtime;
            set => overtime = value;
        }

        public string LateTime
        {
            get => lateTime;
            set => lateTime = value;
        }

        public double Gtotal
        {
            get => gtotal;
            set => gtotal = value;
        }

        public string Shifts
        {
            get => shifts;
            set => shifts = value;
        }

    }


    public class P1SA_AttendanceModel
    {
        private int  _RecordID;
        private string _Employee_ID;
        private string _TimeIn;
        private string _fullname;
        private string _Shift;
        private string _LateTime;

        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }

        [Required(ErrorMessage = "Employee ID is required.")]
        public string Employee_ID
        {
            get => _Employee_ID;
            set => _Employee_ID = value;
        }

        public string fullname
        {
            get => _fullname;
            set => _fullname = value;
        }
        public string Fullname
        {
            get => fullname;
            set => fullname = value;
        }

        public string TimeIn
        {
            get => _TimeIn;
            set => _TimeIn = value;
        }

        public string Shifts
        {
            get => _Shift;
            set => _Shift = value;
        }
        public string LateTime
        {
            get => _LateTime;
            set => _LateTime = value;
        }
    }
}
