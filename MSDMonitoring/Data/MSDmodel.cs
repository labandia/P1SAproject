using System.ComponentModel.DataAnnotations;

namespace MSDMonitoring.Data
{
    public class PrintLabelModel
    {
        public string ReelID { get; set; }
        public string Partnumber { get; set; }
        public int FloorLife { get; set; }
        public string Level { get; set; }
        public string LotNo { get; set; }
        public string Date_IN { get; set; }
        public int Quantity_IN { get; set; }
        public int RemainLife { get; set; }
    }

 
    public class MSDCardModel
    {
        public int RecordID { get; set; }
        public string ReelID { get; set; }
        public int QuantityIN { get; set; }
        public int Line { get; set; }
        public string DateIn { get; set; }
        public string TimeIn { get; set; }
        public string DateCheck { get; set; }
        public double RemainFloor { get; set; } 
        public string InputIn { get; set; } 
    }


    public class MSDReelID
    {
        public string ReelID { get; set; }
        public int FloorLife { get; set; }
        public int Level { get; set; }
        public int RemainQuantity { get; set; }
        public bool IsDone { get; set; }
    }

    public class MSDMasterlistodel
    {
        [Key]
        public string AmbassadorPartnum { get; set; }
        public string Partname { get; set; }
        public string SupplyPartName { get; set; }
        public string SupplyName { get; set; }
        public int Level { get; set; }
        public int FloorLife { get; set; }
    }


    public class MSDmodel
    {
        [Key]
        public int RecordID { get; set; }   
        public string ReelID { get; set; }
        public string Partnumber { get; set; }
        public int Line { get; set;}
        public int FloorLife { get; set; }
        public string Level { get; set; }
        public string LotNo { get; set; }
        public string DateIn { get; set; }
        public string TimeIn { get; set; }
        public int Reel_Quantity { get; set; }
        public string InputName { get; set; }
        public string DateOut { get; set; }
        public string TimeOut { get; set; }
        public int Quantity_IN { get; set; }
        public string Input_Name { get; set; }
        public double Exphours { get; set;  }
        public double TotalHours { get; set; }
        public string RemainLife { get; set; }
        public int PlanQty { get; set; }
    }

    public class InputMSD
    {
        public string ReelID { get; set; }
        public string AmbassadorPartnum { get; set; }
        public string DateIn { get; set; }
        public string DateOut { get; set; }
        public string InputIn { get; set; }
        public string INputOut { get; set; }
        public double RemainFloor { get; set; }
        public int QuantityIN { get; set; }
        public int QuantityOut { get; set; }
    }




    public class InputIN_MSD
    {
        public string ReelID { get; set; }
        public string AmbassadorPartnum { get; set; }
        public string DateIn { get; set; }
        public string InputIn { get; set; }
        public int QuantityIN { get; set; }
        public double RemainFloor { get; set; }
        public int Line { get; set; }
        public string LotNo { get; set; }   
    }


    public class InputOUT_MSD
    {
        public int RecordID { get; set; }
        public string DateOut { get; set; }
        public string INputOut { get; set; }
        public int QuantityOut { get; set; }
        public double RemainFloor { get; set; } 
        public int PlanQty { get; set; }
        public int IsStats { get; set; }    
    }
}
