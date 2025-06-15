
using DocumentFormat.OpenXml.Vml;
using System.Data.SqlClient;

namespace Parts_locator.Models
{
    public class RawMatModel
    {
        private string _PartNumber;
        private int _Racks;
        private int _Quantity;
        private string _RotorBush;
        private string _ShaftPartnum;
        private string _ModelName;
        private int _Type;
        private string _Sample_img;

        public string PartNumber
        {
            get => _PartNumber;
            set => _PartNumber = value;
        }

        public int Racks
        {
            get => _Racks;
            set => _Racks = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }

        public string RotorBush
        {
            get => _RotorBush;
            set => _RotorBush = value;
        }

        public string ShaftPartnum
        {
            get => _ShaftPartnum;
            set => _ShaftPartnum = value;
        }
        public string ModelName
        {
            get => _ModelName;
            set => _ModelName = value;
        }
        public int Type
        {
            get => _Type;
            set => _Type = value;
        }

        public string Sample_img
        {
            get => _Sample_img;
            set => _Sample_img = value;
        }
    }
    public class RawMatSummaryModel
    {
        private string _DateInput;
        private string _TimeInput;
        private string _PartNumber;
        private int _Quantity;
        private string _Inputby;

        public string DateInput
        {
            get => _DateInput;
            set => _DateInput = value;
        }
        public string TimeInput
        {
            get => _TimeInput;
            set => _TimeInput = value;
        }
        public string PartNumber
        {
            get => _PartNumber;
            set => _PartNumber = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }
        public string Inputby
        {
            get => _Inputby;
            set => _Inputby = value;
        }
    }


    public class RawMatInputModel
    {
        private string _PartNumber;
        private int _Quantity;
        private string _Inputby;
        private int _Action;

        public string PartNumber
        {
            get => _PartNumber;
            set => _PartNumber = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }
        public string Inputby
        {
            get => _Inputby;
            set => _Inputby = value;
        }
        public int Action
        {
            get => _Action;
            set => _Action = value;
        }
    }
}
