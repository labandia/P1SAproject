

namespace ProgramPartListWeb.Areas.Hydroponics.Models
{
    public class HydropPartsModel
    {
        private int _PartID;
        private string _PartNo;
        private string _PartName;
        private int _CategoryID;
        private string _CategoryName;
        private string _Supplier;
        private string _Unit;
        private double _UnitCost_USD;
        private double _UnitCost_PHP;
        private int _CurrentQty;
        private double _ReorderLevel;
        private double _WarningLevel;
        private string _Status;

        public int PartID
        {
            get => _PartID;
            set => _PartID = value;
        }
        public string PartNo
        {
            get => _PartNo;
            set => _PartNo = value;
        }
        public string PartName
        {
            get => _PartName;
            set => _PartName = value;
        }
        
        public int CategoryID
        {
            get => _CategoryID;
            set => _CategoryID = value;
        }
        public string CategoryName
        {
            get => _CategoryName;
            set => _CategoryName = value;
        }
        public string Supplier
        {
            get => _Supplier;
            set => _Supplier = value;
        }

        public string Unit
        {
            get => _Unit;
            set => _Unit = value;
        }

        public double UnitCost_USD
        {
            get => _UnitCost_USD;
            set => _UnitCost_USD = value;
        }

        public double UnitCost_PHP
        {
            get => _UnitCost_PHP;
            set => _UnitCost_PHP = value;
        }
        public int CurrentQty
        {
            get => _CurrentQty;
            set => _CurrentQty = value;
        }

        public double ReorderLevel
        {
            get => _ReorderLevel;
            set => _ReorderLevel = value;
        }
        public double WarningLevel
        {
            get => _WarningLevel;
            set => _WarningLevel = value;
        }

        public string Status
        {
            get => _Status;
            set => _Status = value;
        }

       
    }

    public class ChamberTypePartsModel
    {
        private int _PartID;
        private string _PartNo;
        private string _PartName;
        private string _Supplier;
        private string _Unit;
        private double _RequireQty;

        public int PartID
        {
            get => _PartID;
            set => _PartID = value;
        }
        public string PartNo
        {
            get => _PartNo;
            set => _PartNo = value;
        }
        public string PartName
        {
            get => _PartName;
            set => _PartName = value;
        }    
        public string Supplier
        {
            get => _Supplier;
            set => _Supplier = value;
        }

        public string Unit
        {
            get => _Unit;
            set => _Unit = value;
        }
        public double RequireQty
        {
            get => _RequireQty;
            set => _RequireQty = value;
        }
    }


    public class ChambersOrdersModel
    {
        private int _OrderID;
        private string _OrderDate;
        private string _OrderedBy;
        private int _ChambersOrdered;
        private string _OrderStatus;
        private string _OrderDetailID;
        private string _PartID;
        private double _PartNo;
        private double _PartName;
        private int _RequiredQty;
        private double _QtyUsed;
        private double _WarningLevel;
        private string _Status;
    }

    public class ChamberTypeList
    {
        public int ChamberID;
        public string ChamberName;
    }
}