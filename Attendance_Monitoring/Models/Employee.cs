using System;

namespace Attendance_Monitoring.Models
{
    public class Employee
    {
        public string Employee_ID { get; set; }
        public string Fullname { get; set; }
        public string Process { get; set; }
        public string Affiliation { get; set; }
        public int Department_ID { get; set; }
    }


    public class Department
    {
        public int Department_ID { get; set; }
        public string Department_name { get; set; }
    }
}
