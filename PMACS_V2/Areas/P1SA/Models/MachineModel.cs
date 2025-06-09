namespace PMACS_V2.Areas.P1SA.Models
{
    public class MachineModel
    {
        private int _ID;
        private string _machcode;
        private string _Machname;
        private string _Model;
        private string _Manufact;
        private string _Serial;
        private string _location;
        private string _status;
        private byte[] _Filepath;
        private string _DateAdd;
        private string _Asset;
        private string _Equipment;
        private string _Reasons;
        private string _Date_acquired;
        private string _Tongs;
        private int _IsDelete;
        private int _Section_ID;

        public int ID
        {
            get => _ID;
            set => _ID = value;
        }
        public string machcode
        {
            get => _machcode;
            set => _machcode = value;
        }
        public string Machname
        {
            get => _Machname;
            set => _Machname = value;
        }
        public string Model
        {
            get => _Model;
            set => _Model = value;
        }
        public string Manufact
        {
            get => _Manufact;
            set => _Manufact = value;
        }
        public string Serial
        {
            get => _Serial;
            set => _Serial = value;
        }
        public string location
        {
            get => _location;
            set => _location = value;
        }
        public string status
        {
            get => _status;
            set => _status = value;
        }
        public byte[] Filepath
        {
            get => _Filepath;
            set => _Filepath = value;
        }
        public string DateAdd
        {
            get => _DateAdd;
            set => _DateAdd = value;
        }
        public string Asset
        {
            get => _Asset;
            set => _Asset = value;
        }
        public string Equipment
        {
            get => _Equipment;
            set => _Equipment = value;
        }
        public string Reasons
        {
            get => _Reasons;
            set => _Reasons = value;
        }
        public string Date_acquired
        {
            get => _Date_acquired;
            set => _Date_acquired = value;
        }
        public string Tongs
        {
            get => _Tongs;
            set => _Tongs = value;
        }
        public int IsDelete
        {
            get => _IsDelete;
            set => _IsDelete = value;
        }
        public int Section_ID
        {
            get => _Section_ID;
            set => _Section_ID = value;
        }
    }
    public class EquipmentList
    {
        private string _Machine_code;
        private string _Equipment;
        private int _Section_ID;

        public int Section_ID
        {
            get => _Section_ID;
            set => _Section_ID = value;
        }
        public string Equipment
        {
            get => _Equipment;
            set => _Equipment = value;
        }
        public string Machine_code
        {
            get => _Machine_code;
            set => _Machine_code = value;
        }
    }

    public class CountMachineModel
    {
        public int work { get; set; }
        public int notwork { get; set; }
    }













    public class PostMachineModel
    {
        private string _MACH_CODE;
        private string _Machname;
        private string _Model;
        private string _Manufact;
        private string _Serial;
        private string _Shifts;
        private string _location;
        private string _status;
        private int _Section_ID;
        private byte[] _Filepath;
        private string _DateAdd;
        private string _Asset;
        private string _Equipment;
        private string _Reasons;
        private string _Date_acquired;
        private string _Tongs;
        private int _IsDelete;

       
        public string MACH_CODE
        {
            get => _MACH_CODE;
            set => _MACH_CODE = value;
        }
        public string Machname
        {
            get => _Machname;
            set => _Machname = value;
        }
        public string Model
        {
            get => _Model;
            set => _Model = value;
        }
        public string Manufact
        {
            get => _Manufact;
            set => _Manufact = value;
        }
        public string Serial
        {
            get => _Serial;
            set => _Serial = value;
        }

        public string Shifts
        {
            get => _Shifts;
            set => _Shifts = value;
        }
        public string location
        {
            get => _location;
            set => _location = value;
        }
        public string status
        {
            get => _status;
            set => _status = value;
        }
        public byte[] Filepath
        {
            get => _Filepath;
            set => _Filepath = value;
        }
        public string DateAdd
        {
            get => _DateAdd;
            set => _DateAdd = value;
        }
        public string Asset
        {
            get => _Asset;
            set => _Asset = value;
        }
        public string Equipment
        {
            get => _Equipment;
            set => _Equipment = value;
        }
        public string Reasons
        {
            get => _Reasons;
            set => _Reasons = value;
        }
        public string Date_acquired
        {
            get => _Date_acquired;
            set => _Date_acquired = value;
        }
        public string Tongs
        {
            get => _Tongs;
            set => _Tongs = value;
        }
        public int IsDelete
        {
            get => _IsDelete;
            set => _IsDelete = value;
        }

        public int Section_ID
        {
            get => _Section_ID;
            set => _Section_ID = value;
        }
    }
}