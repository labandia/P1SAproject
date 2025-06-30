
namespace ProgramPartListWeb.Areas.PC.Models
{

    public class Employee
    {
        // Data is Encapsulated
        private string employee_ID = "";
        private string fullname = "";
        private string process = "";
        private string affiliation = "";
        private int departmentID = 0;

        public string EmployeeID
        {
            get => employee_ID;
            set => employee_ID = value;
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
    public class PatrolSchedule
    {
        private int _ScheduleID;
        private string _ScheduleDate;
        private string _ProcessName;
        private string _Employee_ID;
        private string _SectionName;
        private string _FullName;

        public int ScheduleID
        {
            get => _ScheduleID;
            set => _ScheduleID = value;
        }
        public string ScheduleDate
        {
            get => _ScheduleDate;
            set => _ScheduleDate = value;
        }
        public string ProcessName
        {
            get => _ProcessName;
            set => _ProcessName = value;
        }

        public string Employee_ID
        {
            get => _Employee_ID;
            set => _Employee_ID = value;
        }
        public string SectionName
        {
            get => _SectionName;
            set => _SectionName = value;
        }

        public string FullName
        {
            get => _FullName;
            set => _FullName = value;
        }
    }
    public class RegistrationModel
    {
        public string RegNo {  get; set; }
        public string DateConduct { get; set; }
        public string Employee_ID { get; set; }
        public string PIC { get; set; }
        public string PIC_Comments { get; set; }
        public string FilePath { get; set; }
        public string Manager { get; set; }
        public string Manager_Comments { get; set; }
    }
    public class InspectorModel
    {
        private int _InspectID;
        private string _Employee_ID;
        private string _FullName;
        private string _DateQualified;
        private string _OJTRegistration;
        private int _Approval;
        private string _Remarks;
        private int _Department_ID;

        public int InspectID
        {
            get => _InspectID;
            set => _InspectID = value;
        }
        public string Employee_ID
        {
            get => _Employee_ID;
            set => _Employee_ID = value;
        }
        public string FullName
        {
            get => _FullName;
            set => _FullName = value;
        }

        public string DateQualified
        {
            get => _DateQualified;
            set => _DateQualified = value;
        }

        public string OJTRegistration
        {
            get => _OJTRegistration;
            set => _OJTRegistration = value;
        }

        public int Approval
        {
            get => _Approval;
            set => _Approval = value;
        }

        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }

        public int Department_ID
        {
            get => _Department_ID;
            set => _Department_ID = value;
        }
    }
    public class PatrolRegistionModel
    {
        private string _RegNo;
        private int _DepartmentID;
        private string _SectionName;
        private string _DateConduct;
        private string _Comments;
        private string _Employee_ID;
        private string _FullName;
        private string _Filepath;
        private string _PIC;
        private string _PIC_Comments;
        private string _Manager;
        private string _Manager_Comments;

        public string RegNo
        {
            get => _RegNo;
            set => _RegNo = value;
        }
        public int DepartmentID
        {
            get => _DepartmentID;
            set => _DepartmentID = value;
        }

        public string Comments
        {
            get => _Comments;
            set => _Comments = value;
        }

        public string DateConduct
        {
            get => _DateConduct;
            set => _DateConduct = value;
        }

        public string SectionName
        {
            get => _SectionName;
            set => _SectionName = value;
        }

        public string Employee_ID
        {
            get => _Employee_ID;
            set => _Employee_ID = value;
        }
        public string FullName
        {
            get => _FullName;
            set => _FullName = value;
        }
        public string Filepath
        {
            get => _Filepath;
            set => _Filepath = value;
        }
        public string PIC
        {
            get => _PIC;
            set => _PIC = value;
        }
        public string PIC_Comments
        {
            get => _PIC_Comments;
            set => _PIC_Comments = value;
        }
        public string Manager
        {
            get => _Manager;
            set => _Manager = value;
        }
        public string Manager_Comments
        {
            get => _Manager_Comments;
            set => _Manager_Comments = value;
        }

    }
    public class FindingModel
    {
        public string RegNo { get; set; }
        public int FindID { get; set; }
        public string FindDescription { get; set; }
        public string Countermeasure { get; set; }
    }
    public class ProccessModel
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int DepartmentID { get; set; }
    }
    public class CalendarSched
    {
        public int ScheduleID { get; set; }
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string ScheduleDate { get; set; }
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int IsActive { get; set; }   
    }
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}