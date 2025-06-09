using System;


namespace PMACS_V2.Areas.Attendance.Model
{
    public class CRmodel
    {
        private DateTime date_today;
        private string employeeID;
        private string fullname;
        private string affiliation;
        private string process;
        private string timein;
        private string timeOut;
        private string duration;


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

        public string Affiliation
        {
            get => affiliation;
            set => affiliation = value;
        }

        public string Process
        {
            get => process;
            set => process = value;
        }

        public string TimeIn
        {
            get => timein;
            set => timein = value;
        }

        public string TimeOut
        {
            get => timeOut;
            set => timeOut = value;
        }

        public string Duration
        {
            get => duration;
            set => duration = value;
        }


    }
    public class CRaccess
    {
        public string IPaddress { get; set; }
        public int Active { get; set; }
        public int CRactive { get; set; }
    }
}