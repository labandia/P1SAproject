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
    public class PressDieRegistry
    {
        public string ToolNo { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int Lines { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string Operational { get; set; }
    }

    public class PressDieMontoring
    {
        public string DateInput { get; set; }
        public string ToolNo { get; set; }
        public double Upper { get; set; }
        public double Lower { get; set; }
        public double Upper_ActualHeight { get; set; }
        public double Upper_DrawingHeight { get; set; }
        public double Lower_ActualHeight { get; set; }
        public double Lower_DrawingHeight { get; set; }
        public double PressStamp { get; set; }
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
        public int TotalStampPress { get; set; }    
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
        public double Upper { get; set; }
        public double Lower { get; set; }
        public double Upper_ActualHeight { get; set; }
        public double Upper_DrawingHeight { get; set; }
        public double Lower_ActualHeight { get; set; }
        public double Lower_DrawingHeight { get; set; }
        public int PressStamp { get; set; }
    }
}