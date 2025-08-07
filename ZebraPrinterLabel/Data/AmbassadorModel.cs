
namespace ZebraPrinterLabel
{
    public class AmbassadorModel
    {
        public string Partnum { get; set; }
        public string WarehouseLocal { get; set; }
        public int Qty { get; set; }
        public string AreaRacks { get; set; }
    }

    public class LabelData
    {
        public string PartNumber { get; set; }
        public string BarcodeText { get; set; }
        public string Quantity { get; set; }
    }

    public class CountToday
    {
        public string DateStart { get; set; }
        public int Count { get; set; }
    }


    public class FinalLabelData
    {
        public string SDP { get; set; }
        public string Warehouse { get; set; }
        public string Quantity { get; set; }
    }

    public class MasterlistData
    {
        public string Partnum { get; set; }
        public string WarehouseLocal { get; set; }
        public int Qty { get; set; }
    }



    public class ReelIDHistory
    {
        public string DateInput { get; set; }
        public string ReelID { get; set; }
        public string Partnum { get; set; }
        public string WarehouseLocal { get; set; }
        public int Qty { get; set; }
    }
}
