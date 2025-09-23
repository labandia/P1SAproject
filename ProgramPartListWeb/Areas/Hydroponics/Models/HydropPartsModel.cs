

using System.Web.UI.WebControls;

namespace ProgramPartListWeb.Areas.Hydroponics.Models
{
    // ---------------------------
    //  REQUEST CHAMBERS
    // ---------------------------
    public class RequestChambersModel
    {
        private string _OrderID;
        private string _ChamberName;
        private string _OrderDate;
        private string _TargetDate;
        private string _OrderedBy;
        private int _ChambersOrdered;
        private string _RequestStatus;
        private string _PIC;
        private string _MaterialStatus;
        private double _CompletionPercent;


        public string OrderID
        {
            get => _OrderID;
            set => _OrderID = value;
        }
        public string ChamberName
        {
            get => _ChamberName;
            set => _ChamberName = value;
        }
        public string OrderDate
        {
            get => _OrderDate;
            set => _OrderDate = value;
        }
        public string TargetDate
        {
            get => _TargetDate;
            set => _TargetDate = value;
        }
        public string OrderedBy
        {
            get => _OrderedBy;
            set => _OrderedBy = value;
        }
        public int ChambersOrdered
        {
            get => _ChambersOrdered;
            set => _ChambersOrdered = value;
        }
        public string RequestStatus
        {
            get => _RequestStatus;
            set => _RequestStatus = value;
        }
        public string PIC
        {
            get => _PIC;
            set => _PIC = value;
        }

        public string MaterialStatus
        {
            get => _MaterialStatus;
            set => _MaterialStatus = value;
        }
        public double CompletionPercent
        {
            get => _CompletionPercent;
            set => _CompletionPercent = value;
        }
        
    }
    public class RequestChambersDetailsModel
    {
        private int _OrderDetailID;
        private int _PartID;
        private string _PartNo;
        private string _PartName;
        private string _CategoryName;
        private double _QtyUsed;
        private double _RequiredQty;
        private double _RemainQty;
        private double _CurrentQty;
        private string _MaterialStatus;


        public int OrderDetailID
        {
            get => _OrderDetailID;
            set => _OrderDetailID = value;
        }
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
        public string CategoryName
        {
            get => _CategoryName;
            set => _CategoryName = value;
        }
        public double QtyUsed
        {
            get => _QtyUsed;
            set => _QtyUsed = value;
        }
        public double RequiredQty
        {
            get => _RequiredQty;
            set => _RequiredQty = value;
        }
        public double RemainQty
        {
            get => _RemainQty;
            set => _RemainQty = value;
        }

        public double CurrentQty
        {
            get => _CurrentQty;
            set => _CurrentQty = value;
        }
        public string MaterialStatus
        {
            get => _MaterialStatus;
            set => _MaterialStatus = value;
        }

    }

    public class AllocationRequest
    {
        private int _OrderID;
        private int _PartID;
        private int _allocated;

        public int OrderID
        {
            get => _OrderID;
            set => _OrderID = value;
        }

        public int PartID
        {
            get => _PartID;
            set => _PartID = value;
        }

        public int allocated
        {
            get => _allocated;
            set => _allocated = value;
        }
    }
    // ---------------------------
    //  MAIN CHAMBERS (PRODUCTS)
    // ---------------------------
    public class ChamberModel
    {
        private int _ChamberPartID;
        private int _PartID;
        private int _ChamberID;
        private string _ChamberName;
        private string _PartNo;
        private string _PartName;
        private string _CategoryName;
        private string _Supplier;
        private string _RequireQty;
        private double _QuantityPerChamber;
        private double _UnitCost_PHP;
        private double _TotalPHPCost;
        private string _ImageParts;


        public int ChamberPartID
        {
            get => _ChamberPartID;
            set => _ChamberPartID = value;
        }

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
        public double QuantityPerChamber
        {
            get => _QuantityPerChamber;
            set => _QuantityPerChamber = value;
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
    public class ChamberTypeList
    {
        public int ChamberID;
        public string ChamberName;
    }
    public class ChamberTotalPrice
    {
        public int  USDTotal;
        public int PHPTotal;
    }

    public class ChambersProduce
    {
        public int ChamberID;
        public string ChamberName;
        public int MaxBuildableChambers;
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
        private int _StockID;
        private int _PartID;
        private string _PartNo;
        private string _PartName;
        private int _CategoryID;
        private string _CategoryName;
        private string _Supplier;
        private string _Unit;
        private int _CurrentQty;
        private double _ReorderLevel;
        private double _WarningLevel;
        private string _Status;
        private string _ImageParts;

        public int StockID
        {
            get => _StockID;
            set => _StockID = value;
        }
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

        public string ImageParts
        {
            get => _ImageParts;
            set => _ImageParts = value;
        }
    }

    public class StockAlertModel
    {
        public int PartID { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public int CurrentQty { get; set; }
        public string Status { get; set; }


         public string PartUrl { get; set; }
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
    public class RequestItem
    {
        public int ChamberID { get; set; }
        public string PIC { get; set; }
        public string OrderedBy { get; set; } = "Admin";
        public int Quantity { get; set; }
        public string TargetDate { get; set; }
    }


    public class StockAlertItem
    {
        public int PartID { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public int CurrentQty { get; set; }
        public string Status { get; set; }  
    }
}