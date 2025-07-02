
namespace ProgramPartListWeb.Models
{
    public class EmployeeModel
    {
        private string _Employee_ID;
        private string _FullName;
        private string _Process;
        private string _Affiliation;
        private int _Department_ID;

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
        public string Process
        {
            get => _Process;
            set => _Process = value;
        }
        public string Affiliation
        {
            get => _Affiliation;
            set => _Affiliation = value;
        }
        public int Department_ID
        {
            get => _Department_ID;
            set => _Department_ID = value;
        }


    }

    public class UserEmployee
    {
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }   

    }

}