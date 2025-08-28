using System;

namespace Attendance_Monitoring.Models
{
    public class Employee
    {
        // Data is Encapsulated
        private string employee_ID = "";
        private string fullname = "";
        private string process = "";
        private string affiliation = "";
        private int departmentID = 0;


        //public string EmpTemp { get; set; } = "";

        public string Employee_ID
        {
            get => employee_ID;
            set => employee_ID = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Employee ID cannot be empty.");
        }

        public string Fullname
        {
            get => fullname;
            set => fullname = value;
        }

        public string Process
        {
            get => process;
            set => process = value;
        }

        public string Affiliation
        {
            get => affiliation; 
            set => affiliation = value;
        }

        public int Department_ID
        {
            get => departmentID; 
            set => departmentID = value;
        }
    }


    public class Department
    {
        public int Department_ID { get; set; }
        public string Department_name { get; set; }
    }
}
