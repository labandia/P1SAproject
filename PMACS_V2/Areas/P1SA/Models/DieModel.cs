using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMACS_V2.Areas.P1SA.Models
{

    // Total Sum of Partnumber  Mold die
    public class DieMoldTotalPartnum
    {
        public int RecordID { get; set; }
        public int No { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public string DimensionQuality { get; set; }
        public int DieNumber { get; set; }
        public string DieSerial { get; set; }
        public string Cavity { get; set; }
        public string DateAction { get; set; }
        public int TotalDie { get; set; }
        public int TotalQty { get; set; }
        public int TotalNo { get; set; }
    }
    public class DieMoldSetNotal
    {
        public int No { get; set; }
        public int PreviousCount { get; set; }
        public int DieLife { get; set; }
    }
    public class DieMoldSummaryProcess
    {
        public int No { get; set; }
        public string PartNo { get; set; }
        public int DieNumber { get; set; }
        public string DieSerial { get; set; }
        public int DieLife { get; set; }
        public int PreviousCount { get; set; }
        public int ShotOnwards { get; set; }
        public int totalshoutCount { get; set; }
        //public int ShotAverage { get; set; }
        public int Status { get; set; } 
        public string Remarks { get; set; } 
        public string ProcessID { get; set; }
    }



    public class DieMoldToolingModelDisplay
    {
        private int _RecordID;
        private string _RegNo;
        private string _PartNo;
        private string _DimensionQuality;
        private int _Item;
        private string _DetailsModify;
        private int _ShotRelease;
        private string _DateArrived;
        private string _DateRepair;
        private string _Incharge;
        private string _Remarks;

        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }

        public string RegNo
        {
            get => _RegNo;
            set => _RegNo = value;
        }
        public string PartNo
        {
            get => _PartNo;
            set => _PartNo = value;
        }
        public string DimensionQuality
        {
            get => _DimensionQuality;
            set => _DimensionQuality = value;
        }
        public int Item
        {
            get => _Item;
            set => _Item = value;
        }
        public string DetailsModify
        {
            get => _DetailsModify;
            set => _DetailsModify = value;
        }
        public int ShotRelease
        {
            get => _ShotRelease;
            set => _ShotRelease = value;
        }
        public string DateArrived
        {
            get => _DateArrived;
            set => _DateArrived = value;
        }
        public string DateRepair
        {
            get => _DateRepair;
            set => _DateRepair = value;
        }
        public string Incharge
        {
            get => _Incharge;
            set => _Incharge = value;
        }
        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
    }

    public class DieMoldProcess
    {
        public string ProcessID { get; set; }
        public string ProcessName { get; set; }
    }

    public class DieProcessDescription
    {
        public int PartDescriptionID { get; set; }
        public string Description { get; set; }
    }


    public class DieMoldDaily
    {
        public int RecordID { get; set; }
        public string DimensionQuality { get; set; }
        public string PartNo { get; set; }
        public string DateInput { get; set; }
        public int CycleShot { get; set; }
        public int Total { get; set; }
        public int MachineNo { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string Mincharge { get; set; }
    }

    public class DieMoldDailyInput
    {
        public int RecordID { get; set; }
        public string dailypartno { get; set; }
        public string DateInput { get; set; }
        public int CycleShot { get; set; }
        public int MachineNo { get; set; }
        public string Remarks { get; set; }
        public string Mincharge { get; set; }
    }



    public class DieMoldToolingModel
    {
        private int _RecordID;
        private string _RegNo = string.Empty;
        private string _ParNoSearch = string.Empty;
        private string _DimensionQuality = string.Empty;
        private int _Item = 0;
        private string _DetailsModify = string.Empty;
        private int _Item2 = 0;
        private string _DetailsModify2 = string.Empty;
        private int _ShotRelease = 0;
        private string _DateArrived = string.Empty;
        private string _DateRepair = string.Empty;
        private string _Incharge = string.Empty;
        private string _Remarks = string.Empty;


        public int RecordID { get => _RecordID; set => _RecordID = value; }
        public string RegNo { get => _RegNo; set => _RegNo = value ?? string.Empty; }
        public string ParNoSearch { get => _ParNoSearch; set => _ParNoSearch = value ?? string.Empty; }
        public string DimensionQuality { get => _DimensionQuality; set => _DimensionQuality = value ?? string.Empty; }
        public int Item { get => _Item; set => _Item = value; }
        public string DetailsModify { get => _DetailsModify; set => _DetailsModify = value ?? string.Empty; }
        public int Item2 { get => _Item2; set => _Item2 = value; }
        public string DetailsModify2 { get => _DetailsModify2; set => _DetailsModify2 = value ?? string.Empty; }
        public int ShotRelease { get => _ShotRelease; set => _ShotRelease = value; }
        public string DateArrived { get => _DateArrived; set => _DateArrived = value ?? string.Empty; }
        public string DateRepair { get => _DateRepair; set => _DateRepair = value ?? string.Empty; }
        public string Incharge { get => _Incharge; set => _Incharge = value ?? string.Empty; }
        public string Remarks { get => _Remarks; set => _Remarks = value ?? string.Empty; }
    }


    public class AddDieMoldingDataInput
    {
        public int AddNo { get; set; }
        public string AddPartno { get; set; }
        public string DimensionQual { get; set; }
        public int AddDienum { get; set; }
        public string AddSerial { get; set; }
        public int AddCavity { get; set; }
        public string AddSelectProcess { get; set; }
        public int PartDescriptionID { get; set; }
    }


    public class DieMoldingDataInput
    {
        public int RecordID { get; set; }
        public int EditNo { get; set; }
        public string EditPartNo { get; set; }  
        public int EditDie {  get; set; }   
        public string EditSerial { get; set; }  
        public int EditCavity   { get; set; }
    }


    public class FinalMoldDieSummary
    {
        public string Category { get; set; }
        public int MoldDie { get; set; }
    }




    // FOR DISPLAY DATA
    public class DieMoldMonth
    {
        private int _RecordID;
        private int _No;
        private string _PartNo;
        private string _Description;
        private string _DimensionQuality;
        private int _DieNumber;
        private string _DieSerial;
        private int _Cavity;
        private DateTime _DateAction;
        private int _TotalDie;

        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }

        public string PartNo
        {
            get => _PartNo;
            set => _PartNo = value;
        }
        public int No
        {
            get => _No;
            set => _No = value;
        }
        public string Description
        {
            get => _Description;
            set => _Description = value;
        }
        public string DimensionQuality
        {
            get => _DimensionQuality;
            set => _DimensionQuality = value;
        }
        public int DieNumber
        {
            get => _DieNumber;
            set => _DieNumber = value;
        }
        public string DieSerial
        {
            get => _DieSerial;
            set => _DieSerial = value;
        }
        public int Cavity
        {
            get => _Cavity;
            set => _Cavity = value;
        }

        public DateTime DateAction
        {
            get => _DateAction;
            set => _DateAction = value;
        }
        public int TotalDie
        {
            get => _TotalDie;
            set => _TotalDie = value;
        }

    }
    public class DieMoldSummary
    {
        private int _No;
        private string _PartNo;
        private string _Description;
        private string _DimensionQuality;
        private int _DieNumber;
        private string _DieSerial;
        private int _Cavity;
        private int _DieLife;
        private int _PreviousCount;
        private int _ShotOnwards;
        private int _ShotAverage;

        public string PartNo
        {
            get => _PartNo;
            set => _PartNo = value;
        }
        public int No
        {
            get => _No;
            set => _No = value;
        }
        public string Description
        {
            get => _Description;
            set => _Description = value;
        }
        public string DimensionQuality
        {
            get => _DimensionQuality;
            set => _DimensionQuality = value;
        }
        public int DieNumber
        {
            get => _DieNumber;
            set => _DieNumber = value;
        }
        public string DieSerial
        {
            get => _DieSerial;
            set => _DieSerial = value;
        }
        public int Cavity
        {
            get => _Cavity;
            set => _Cavity = value;
        }

        public int DieLife
        {
            get => _DieLife;
            set => _DieLife = value;
        }
        public int PreviousCount
        {
            get => _PreviousCount;
            set => _PreviousCount = value;
        }
        public int ShotOnwards
        {
            get => _ShotOnwards;
            set => _ShotOnwards = value;
        }
        public int ShotAverage
        {
            get => _ShotAverage;
            set => _ShotAverage = value;
        }
    }

    public class DieModel
    {
        private int _RecordID;
        private string _PartNo;
        private int _No;
        private string _Description;
        private string _DimensionQuality;
        private int _DieNumber;
        private string _DieSerial;
        private int _Cavity;

        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }
        public string PartNo
        {
            get => _PartNo;
            set => _PartNo = value;
        }
        public int No
        {
            get => _No;
            set => _No = value;
        }
        public string Description
        {
            get => _Description;
            set => _Description = value;
        }
        public string DimensionQuality
        {
            get => _DimensionQuality;
            set => _DimensionQuality = value;
        }
        public int DieNumber
        {
            get => _DieNumber;
            set => _DieNumber = value;
        }
        public string DieSerial
        {
            get => _DieSerial;
            set => _DieSerial = value;
        }
        public int Cavity
        {
            get => _Cavity;
            set => _Cavity = value;
        }
    }




    // INPUT DATA
    public class MoldInputModel
    {
        public int No { get; set; }
        public string PartNo { get; set; }
        public string MoldInput { get; set; }
        public int DateAction { get; set; }
    }





    //############################# PRESS MOLD DIE =======================================
    public class PressMainMonitor
    {
        public int MonitorID { get; set; }  
        public string ToolNo { get; set; }
        public string Type { get; set; }
        public int Line { get; set; }
        public int Up { get; set; }
        public int low { get; set; }
        public double MinUpper { get; set; }
        public double MinLower { get; set; }
        public int TotalPressStamp { get; set; }
        public string Operational { get; set; }
    }

    public class PressDieRegistry
    {
        public string ToolNo { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int Lines { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public int Operational { get; set; }
    }

    public class PressDieRegistryEdit
    {
        public string EditToolNo { get; set; }
        public string EditType { get; set; }
        public string EditModel { get; set; }
        public int EditLine { get; set; }
        public string EditStatus { get; set; }
        public int EditOpe { get; set; }
    }

    public class PressDieMontoring
    {
        public int RecordID { get; set; }
        public string DateInput { get; set; }
        public string ToolNo { get; set; }
        public double Up { get; set; }
        public double Low { get; set; }
        public double GrindUpper { get; set; }
        public double GrindLower { get; set; }
        public double Upper_ActualHeight { get; set; }
        public double Upper_DrawingHeight { get; set; }
        public double Lower_ActualHeight { get; set; }
        public double Lower_DrawingHeight { get; set; }
        public int PressStamp { get; set; }
        public int DieStatus { get; set; }
    }



    public class PressDieSummary
    {
        public string Model { get; set; }
        public string ToolNo { get; set; }
        public string Type { get; set; }
        public string DiePart { get; set; }
        public double DieHeight { get; set; }
        public double StdGrind { get; set; }
        public int StampGrind { get; set; }
        public int Line { get; set; }
        public int Avg { get; set; }

        public int Remaining { get; set; }
        public string Status { get; set; }
        public int TotalPressStamp { get; set; }    
    }

    public class PressDieControlModel
    {
        public string DateInput { get; set; }
        public string ToolNo { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Stamp { get; set; }
        public int Machine { get; set; }
        public int DieCondition { get; set; }
        public string Operator { get; set; }
        public double DieHeight { get; set; }
        public string LeaderCom { get; set; }

        public string Gear { get; set; }
        public string Pitch { get; set; }
        public string GearCom { get; set; }
        public string ReasonDetach { get; set; }
    }



    public class PressDieLowerUpper
    {
        public string ToolNo { get; set; }
        public double UpperDieHeight { get; set; }
        public double LowerDieHeight { get; set; }
    }





    public class PressInputModel
    {
        public string ToolNo { get; set; }
        public int Up { get; set; }
        public int low { get; set; }
        public int Line { get; set; }
    }

    public class PressMonitorInput
    {
        public int MonitorID { get; set; }
        public string ToolNo { get; set; }
        public double Upper_ActualHeight { get; set; }
        public double Upper_DrawingHeight { get; set; }
        public double Lower_ActualHeight { get; set; }
        public double Lower_DrawingHeight { get; set; }
        public int PressStamp { get; set; }
    }



    public class PressDieControlData
    {
        public string ToolNo { get; set; }
        public int Stamp { get; set; }
        public int Machine { get; set; }
        public int DieCondition { get; set; }
        public string Operator { get; set; }
        public double DieHeight { get; set; }
        public string LeaderCom { get; set; }

        public string Gear { get; set; }
        public string Pitch { get; set; }
    }
}