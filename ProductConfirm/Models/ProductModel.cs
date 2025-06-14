
namespace ProductConfirm.Models
{
    //FOR RETRIEVING DATA
    public class MasterlistModel
    {
        public int RotorProductID { get; set; } 
        public string RotorAssy {  get; set; }
        public string ProductType { get; set; }
        public string MachinePressureMinMax { get; set; }
        public double RecommendedPressureSetting { get; set; }
        public int ModelType { get; set; }
    }
    public class ProductModel
    {
        private int _RotorProductID;
        private string _RotorAssy;
        private string _ProductType;
        private string _MachinePressureMinMax;
        private string _CaulkingDentTarget;
        private string _CaulkingDentMinMax;
        private string _ShaftLengthMinMax;
        private string _SEA_MinMax;
        private string _ShaftPullingForce;
        private string _BushPullingForce;
        private string _MagnetHeightMinMax;
        private int _ModelType;

        public int RotorProductID
        {
            get => _RotorProductID;
            set => _RotorProductID = value;
        }

        public string RotorAssy
        {
            get => _RotorAssy;
            set => _RotorAssy = value;
        }

        public string ProductType
        {
            get => _ProductType;
            set => _ProductType = value;
        }
        public string MachinePressureMinMax
        {
            get => _MachinePressureMinMax;
            set => _MachinePressureMinMax = value;
        }
        public string CaulkingDentMinMax
        {
            get => _CaulkingDentMinMax;
            set => _CaulkingDentMinMax = value;
        }
        public string CaulkingDentTarget
        {
            get => _CaulkingDentTarget;
            set => _CaulkingDentTarget = value;
        }
        public string ShaftLengthMinMax
        {
            get => _ShaftLengthMinMax;
            set => _ShaftLengthMinMax = value;
        }
        public string SEA_MinMax
        {
            get => _SEA_MinMax;
            set => _SEA_MinMax = value;
        }

        public string ShaftPullingForce
        {
            get => _ShaftPullingForce;
            set => _ShaftPullingForce = value;
        }

        public string BushPullingForce
        {
            get => _BushPullingForce;
            set => _BushPullingForce = value;
        }

        public string MagnetHeightMinMax
        {
            get => _MagnetHeightMinMax;
            set => _MagnetHeightMinMax = value;
        }

        public int ModelType
        {
            get => _ModelType;
            set => _ModelType = value;
        }

    }

    public class ProductOneModel
    {
        public int RotorProductID { get; set; } = 0;
        public string RotorAssy { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string MachinePressureMinMax { get; set; } = string.Empty;
        public double RecommendedPressureSetting { get; set; } = 0.0;
        public int ModelType { get; set; } = 0;

        public decimal CaulkingDentMin { get; set; } = decimal.Zero;
        public decimal CaulkingDentMax { get; set; } = decimal.Zero;
        public decimal ShaftLengthMin { get; set; } = decimal.Zero;
        public decimal ShaftLengthMax { get; set; } = decimal.Zero;
        public decimal SEA_Min { get; set; } = decimal.Zero;
        public decimal SEA_Max { get; set; } = decimal.Zero;
        public decimal MagnetHeightMin { get; set; } = decimal.Zero;
        public decimal MagnetHeightMax { get; set; } = decimal.Zero;
        public int ShaftPullingForce { get; set; } = 0;
        public int BushPullingForce { get; set; } = 0;
    }

    public class ShopOrderModel
    {
        private int _ShoporderID = 0;
        private string _Date_input;
        private string _Date_time;
        private string _Shoporder = string.Empty;
        private string _RotorAssy = string.Empty;
        private string _ProductType = string.Empty;
        private string _Shift = string.Empty;
        private string _Line = string.Empty;
        private string _Inputby = string.Empty;
        private string _MachinePressureMinMax = string.Empty;
        private string _RotorProductID;
        private string _Remarks;
        private string _ConfirmBy;
        private string _Stats;

        public int ShoporderID
        {
            get => _ShoporderID;
            set => _ShoporderID = value;
        }
        public string Date_input
        {
            get => _Date_input;
            set => _Date_input = value;
        }
        public string Date_time
        {
            get => _Date_time;
            set => _Date_time = value;
        }
        public string Shoporder
        {
            get => _Shoporder;
            set => _Shoporder = value;
        }
        public string RotorAssy
        {
            get => _RotorAssy;
            set => _RotorAssy = value;
        }
        public string ProductType
        {
            get => _ProductType;
            set => _ProductType = value;
        }
        public string Shift
        {
            get => _Shift;
            set => _Shift = value;
        }
        public string Line
        {
            get => _Line;
            set => _Line = value;
        }

        public string Inputby
        {
            get => _Inputby;
            set => _Inputby = value;
        }
        public string MachinePressureMinMax
        {
            get => _MachinePressureMinMax;
            set => _MachinePressureMinMax = value;
        }

        public string RotorProductID
        {
            get => _RotorProductID;
            set => _RotorProductID = value;
        }
        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
        public string ConfirmBy
        {
            get => _ConfirmBy;
            set => _ConfirmBy = value;
        }
        public string Stats
        {
            get => _Stats;
            set => _Stats = value;
        }
    }
    public class ProductToolsModel
    {
        private string _Measurements;
        private string _Status;
        private int _ShopProdID;
        public string Measurements
        {
            get => _Measurements;
            set => _Measurements = value;
        }
        public string Status
        {
            get => _Status;
            set => _Status = value;
        }
        public int ShopProdID
        {
            get => _ShopProdID;
            set => _ShopProdID = value;
        }
       
    }
    public class ExportModel
    {
        private int _RotorProductID;
        private string _RotorAssy;
        private string _ProductType;
        private string _MachinePressureMinMax;
        private string _RecommendedPressureSetting;
        private string _CaulkingDent;
        private string _SurfaceEdge;
        private string _ShaftLength;
        private string _ShaftPullingForce;
        private string _BushPullingForce;

        public int RotorProductID
        {
            get => _RotorProductID;
            set => _RotorProductID = value;
        }

        public string RotorAssy
        {
            get => _RotorAssy;
            set => _RotorAssy = value;
        }
        public string ProductType
        {
            get => _ProductType;
            set => _ProductType = value;
        }
        public string MachinePressureMinMax
        {
            get => _MachinePressureMinMax;
            set => _MachinePressureMinMax = value;
        }

        public string RecommendedPressureSetting
        {
            get => _RecommendedPressureSetting;
            set => _RecommendedPressureSetting = value;
        }
        public string CaulkingDent
        {
            get => _CaulkingDent;
            set => _CaulkingDent = value;
        }
        public string SurfaceEdge
        {
            get => _SurfaceEdge;
            set => _SurfaceEdge = value;
        }


        public string ShaftLength
        {
            get => _ShaftLength;
            set => _ShaftLength = value;
        }
        public string ShaftPullingForce
        {
            get => _ShaftPullingForce;
            set => _ShaftPullingForce = value;
        }
        public string BushPullingForce
        {
            get => _BushPullingForce;
            set => _BushPullingForce = value;
        }

    }
    public class SummaryProductModel
    {
        private string _Date_input;
        private string _Shift = "";
        private string _Line = "";
        private string _Shoporder = "";
        private string _Max = "";
        private string _SL_supply = "";
        private string _SL_lot = "";
        private string _SL_first = "";
        private string _SL_second = "";
        private string _SL_third = "";
        private string _SL_fourth = "";
        private string _SL_fifth = "";

        private string _SE_supply = "";
        private string _SE_lot = "";
        private string _SE_first = "";
        private string _SE_second = "";
        private string _SE_third = "";
        private string _SE_fourth = "";
        private string _SE_fifth = "";

        private string _CD_supply = "";
        private string _CD_lot = "";
        private string _CD_first = "";
        private string _CD_second = "";
        private string _CD_third = "";
        private string _CD_fourth = "";
        private string _CD_fifth = "";
        private string _six = "";
        private string _seven = "";
        private string _eight = "";

        private string _SP_supply = "";
        private string _SP_lot = "";
        private string _SP_first = "";
        private string _SP_second = "";
        private string _SP_third = "";
        private string _SP_fourth = "";
        private string _SP_fifth = "";

        private string _BP_supply = "";
        private string _BP_lot = "";
        private string _BP_first = "";
        private string _BP_second = "";
        private string _BP_third = "";
        private string _BP_fourth = "";
        private string _BP_fifth = "";

        private string _MH_supply = "";
        private string _MH_lot = "";
        private string _MH_first_min = "";
        private string _MH_second_min = "";
        private string _MH_third_min = "";
        private string _MH_fourth_min = "";
        private string _MH_fifth_min = "";

        private string _MH_first_max = "";
        private string _MH_second_max = "";
        private string _MH_third_max = "";
        private string _MH_fourth_max = "";
        private string _MH_fifth_max = "";
        private string _Inputby = "";
        private string _ConfirmBy = "";
        private string _Remarks = "";

        public string Date_input
        {
            get => _Date_input;
            set => _Date_input = value;
        }

        public string Shift
        {
            get => _Shift;
            set => _Shift = value;
        }
        public string Line
        {
            get => _Line;
            set => _Line = value;
        }
        public string Shoporder
        {
            get => _Shoporder;
            set => _Shoporder = value;
        }
        public string Max
        {
            get => _Max;
            set => _Max = value;
        }
        public string SL_supply
        {
            get => _SL_supply;
            set => _SL_supply = value;
        }
        public string SL_lot
        {
            get => _SL_lot;
            set => _SL_lot = value;
        }
        public string SL_first
        {
            get => _SL_first;
            set => _SL_first = value;
        }
        public string SL_second
        {
            get => _SL_second;
            set => _SL_second = value;
        }
        public string SL_third
        {
            get => _SL_third;
            set => _SL_third = value;
        }
        public string SL_fourth
        {
            get => _SL_fourth;
            set => _SL_fourth = value;
        }
        public string SL_fifth
        {
            get => _SL_fifth;
            set => _SL_fifth = value;
        }
        public string SE_supply
        {
            get => _SE_supply;
            set => _SE_supply = value;
        }
        public string SE_lot
        {
            get => _SE_lot;
            set => _SE_lot = value;
        }
        public string SE_first
        {
            get => _SE_first;
            set => _SE_first = value;
        }
        public string SE_second
        {
            get => _SE_second;
            set => _SE_second = value;
        }
        public string SE_third
        {
            get => _SE_third;
            set => _SE_third = value;
        }
        public string SE_fourth
        {
            get => _SE_fourth;
            set => _SE_fourth = value;
        }
        public string SE_fifth
        {
            get => _SE_fifth;
            set => _SE_fifth = value;
        }
        public string CD_supply
        {
            get => _CD_supply;
            set => _CD_supply = value;
        }
        public string CD_lot
        {
            get => _CD_lot;
            set => _CD_lot = value;
        }
        public string CD_first
        {
            get => _CD_first;
            set => _CD_first = value;
        }
        public string CD_second
        {
            get => _CD_second;
            set => _CD_second = value;
        }
        public string CD_third
        {
            get => _CD_third;
            set => _CD_third = value;
        }
        public string CD_fourth
        {
            get => _CD_fourth;
            set => _CD_fourth = value;
        }
        public string CD_fifth
        {
            get => _CD_fifth;
            set => _CD_fifth = value;
        }
        public string six
        {
            get => _six;
            set => _six = value;
        }
        public string seven
        {
            get => _seven;
            set => _seven = value;
        }
        public string eight
        {
            get => _eight;
            set => _eight = value;
        }
        public string SP_supply
        {
            get => _SP_supply;
            set => _SP_supply = value;
        }

        public string SP_lot
        {
            get => _SP_lot;
            set => _SP_lot = value;
        }
        public string SP_first
        {
            get => _SP_first;
            set => _SP_first = value;
        }
        public string SP_second
        {
            get => _SP_second;
            set => _SP_second = value;
        }
        public string SP_third
        {
            get => _SP_third;
            set => _SP_third = value;
        }
        public string SP_fourth
        {
            get => _SP_fourth;
            set => _SP_fourth = value;
        }
        public string SP_fifth
        {
            get => _SP_fifth;
            set => _SP_fifth = value;
        }
        public string BP_supply
        {
            get => _BP_supply;
            set => _BP_supply = value;
        }
        public string BP_lot
        {
            get => _BP_lot;
            set => _BP_lot = value;
        }
        public string BP_first
        {
            get => _BP_first;
            set => _BP_first = value;
        }

        public string BP_second
        {
            get => _BP_second;
            set => _BP_second = value;
        }
        public string BP_third
        {
            get => _BP_third;
            set => _BP_third = value;
        }
        public string BP_fourth
        {
            get => _BP_fourth;
            set => _BP_fourth = value;
        }
        public string BP_fifth
        {
            get => _BP_fifth;
            set => _BP_fifth = value;
        }
        public string MH_supply
        {
            get => _MH_supply;
            set => _MH_supply = value;
        }
        public string MH_lot
        {
            get => _MH_lot;
            set => _MH_lot = value;
        }
        public string MH_first_min
        {
            get => _MH_first_min;
            set => _MH_first_min = value;
        }
        public string MH_second_min
        {
            get => _MH_second_min;
            set => _MH_second_min = value;
        }
        public string MH_third_min
        {
            get => _MH_third_min;
            set => _MH_third_min = value;
        }
        public string MH_fourth_min
        {
            get => _MH_fourth_min;
            set => _MH_fourth_min = value;
        }
        public string MH_fifth_min
        {
            get => _MH_fifth_min;
            set => _MH_fifth_min = value;
        }
        public string MH_first_max
        {
            get => _MH_first_max;
            set => _MH_first_max = value;
        }
        public string MH_second_max
        {
            get => _MH_second_max;
            set => _MH_second_max = value;
        }
        public string MH_third_max
        {
            get => _MH_third_max;
            set => _MH_third_max = value;
        }
        public string MH_fourth_max
        {
            get => _MH_fourth_max;
            set => _MH_fourth_max = value;
        }
        public string MH_fifth_max
        {
            get => _MH_fifth_max;
            set => _MH_fifth_max = value;
        }
        public string Inputby
        {
            get => _Inputby;
            set => _Inputby = value;
        }
        public string ConfirmBy
        {
            get => _ConfirmBy;
            set => _ConfirmBy = value;
        }
        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
    }


    //FOR INSERTING DATA
    public class AddProductDetailsModel
    {
        public int RotorProductID { get; set; } = 0;
        public string RotorAssy { get; set; } = "";
        public string ProductType { get; set; } = "";
        public string MachinePressureMinMax { get; set; } = "";
        public decimal RecommendedPressureSetting { get; set; } = decimal.Zero;
        public decimal CaulkingDentMin  { get; set; } = decimal.Zero;
        public decimal CaulkingDentMax { get; set; } = decimal.Zero;
        public decimal ShaftLengthMin { get; set; } = decimal.Zero;
        public decimal ShaftLengthMax  { get; set; } = decimal.Zero;
        public decimal SEA_Min { get; set; } = decimal.Zero;
        public decimal SEA_Max { get; set; } = decimal.Zero;
        public decimal MagnetHeightMin { get; set; } = decimal.Zero;
        public decimal MagnetHeightMax { get; set; } = decimal.Zero;
        public int ShaftPullingForce { get; set; } = 0;
        public int BushPullingForce { get; set; } = 0;
    }
}
