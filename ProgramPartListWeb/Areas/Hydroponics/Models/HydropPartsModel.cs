

using System.Web.UI.WebControls;

namespace ProgramPartListWeb.Areas.Hydroponics.Models
{
    // ---------------------------
    //  MAIN CHAMBERS (PRODUCTS)
    // ---------------------------
    public class ChamberModel
    {
        private int _PartID;
        private int _ChamberID;
        private string _ChamberName;
        private string _PartNo;
        private string _PartName;
        private string _CategoryName;
        private string _Supplier;
        private string _RequireQty;
        private double _UnitCost_PHP;
        private double _TotalPHPCost;


        public int PartID
        {
            get => _PartID;
            set => _PartID = value;
        }
        public int ChamberID
        {
            get => _ChamberID;
            set => _ChamberID = value;
        }
        public string ChamberName
        {
            get => _ChamberName;
            set => _ChamberName = value;
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

        public string RequireQty
        {
            get => _RequireQty;
            set => _RequireQty = value;
        }
        public double UnitCost_PHP
        {
            get => _UnitCost_PHP;
            set => _UnitCost_PHP = value;
        }

        public double TotalPHPCost
        {
            get => _TotalPHPCost;
            set => _TotalPHPCost = value;
        }
    }
    public class ChamberTypeList
    {
        public int ChamberID;
        public string ChamberName;
    }
    // ---------------------------
    //  PART LISTS (EQUIPMENTS)
    // ---------------------------
    public class MasterlistPartsModel
    {
        private int _PartID;
        private string _PartNo;
        private string _PartName;
        private int _CategoryID;
        private string _CategoryName;
        private string _Supplier;
        private double _UnitCost_PHP;
        private double _TotalPHPCost;
        private string _ImageParts;


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
        public double UnitCost_PHP
        {
            get => _UnitCost_PHP;
            set => _UnitCost_PHP = value;
        }

        public double TotalPHPCost
        {
            get => _TotalPHPCost;
            set => _TotalPHPCost = value;
        }
        public string ImageParts
        {
            get => _ImageParts;
            set => _ImageParts = value;
        }
    }




    // ---------------------------
    //  STOCKS MANAGEMENT MODELS
    // ---------------------------
    public class StockPartsModel
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




    public class StockItem
    {
        public int PartID { get; set; }
        public string PartName { get; set; }
        public string PartNo { get; set; }
        public string CategoryName { get; set; }
        public string Supplier { get; set; }
        public decimal UnitCost_PHP { get; set; }
        public double Quantity { get; set; }
    }

}