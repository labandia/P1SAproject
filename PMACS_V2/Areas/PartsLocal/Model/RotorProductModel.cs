
namespace PMACS_V2.Areas.PartsLocal.Model
{
    public class RotorProductModel
    {
        private int _RecordID;
        private string _Partnumber;
        private string _ModelName;
        private int _Area;
        private int _Quantity;
        private string _FrontImage;
        private string _BackImage;

        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }


        public string Partnumber
        {
            get => _Partnumber;
            set => _Partnumber = value;
        }
        public string ModelName
        {
            get => _ModelName;
            set => _ModelName = value;
        }
        public int Area
        {
            get => _Area;
            set => _Area = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }
        public string FrontImage
        {
            get => _FrontImage;
            set => _FrontImage = value;
        }
        public string BackImage
        {
            get => _BackImage;
            set => _BackImage = value;
        }
      
    }
    public class ShopOrderInModel
    {
        private int _TransactionID;
        private int _TransactionType;
        private string _TransactionDate;
        private string _Partnumber;
        private string _ModelName;
        private string _RotorOrder;
        private int _Area;
        private int _PreviousQuantity;
        private int _Quantity;
        private string _Remarks;

        public int TransactionID
        {
            get => _TransactionID;
            set => _TransactionID = value;
        }
        public int TransactionType
        {
            get => _TransactionType;
            set => _TransactionType = value;
        }
        public string TransactionDate
        {
            get => _TransactionDate;
            set => _TransactionDate = value;
        }
        public string Partnumber
        {
            get => _Partnumber;
            set => _Partnumber = value;
        }
        public string ModelName
        {
            get => _ModelName;
            set => _ModelName = value;
        }
        public string RotorOrder
        {
            get => _RotorOrder;
            set => _RotorOrder = value;
        }
        public int Area
        {
            get => _Area;
            set => _Area = value;
        }
         public int PreviousQuantity
        {
            get => _PreviousQuantity;
            set => _PreviousQuantity = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }

        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
    }

    public class ShopOrderOutModel
    {
        private int _TransactionID;
        private string _TransactionDate;
        private string _Partnumber;
        private string _ModelName;
        private string _RotorOrder;
        private string _ShopOrder;
        private int _PlanQuantity;
        private int _Area;
        private int _PreviousQuantity;
        private int _Quantity;
        private string _PlanDate;
        private string _ModelBase;
        private string _Status;
        private int _BushType;
        private string _Remarks;

        public int TransactionID
        {
            get => _TransactionID;
            set => _TransactionID = value;
        }

        public string TransactionDate
        {
            get => _TransactionDate;
            set => _TransactionDate = value;
        }
        public string Partnumber
        {
            get => _Partnumber;
            set => _Partnumber = value;
        }
        public string ModelName
        {
            get => _ModelName;
            set => _ModelName = value;
        }
        public string RotorOrder
        {
            get => _RotorOrder;
            set => _RotorOrder = value;
        }
        public int PreviousQuantity
        {
            get => _PreviousQuantity;
            set => _PreviousQuantity = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }
        public int Area
        {
            get => _Area;
            set => _Area = value;
        }
        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
        public int BushType
        {
            get => _BushType;
            set => _BushType = value;
        }
    }
}