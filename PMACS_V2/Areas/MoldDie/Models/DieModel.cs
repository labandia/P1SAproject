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
        private string _Dimension_Quality;
        private int _Item;
        private string _DetailsModify;
        private int _ShotRelease;
        private DateTime _DateArrived;
        private DateTime _DateRepair;
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
        public string Dimension_Quality
        {
            get => _Dimension_Quality;
            set => _Dimension_Quality = value;
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
        public DateTime DateArrived
        {
            get => _DateArrived;
            set => _DateArrived = value;
        }
        public DateTime DateRepair
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
        public string ProcessID { get; set; }
    }

    public class DieMoldDailyInput
    {
        public int RecordID { get; set; }
        public string dailypartno { get; set; }
        //public string DateInput { get; set; }
        public DateTime DateInput { get; set; }
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




  


    // INPUT DATA
    public class MoldInputModel
    {
        public int No { get; set; }
        public string PartNo { get; set; }
        public string MoldInput { get; set; }
        public int DateAction { get; set; }
    }




    public class DieMoldMonitoringModel
    {
        /* =======================
           DAILY (DieMold_Daily)
           ======================= */
        public int RecordID { get; set; }
        public string DateInput { get; set; }
        public int CycleShot { get; set; }
        public int Total { get; set; }
        public int MachineNo { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string Mincharge { get; set; }

        /* =======================
           MAIN PARTS
           (DieMold_MoldingMainParts)
           ======================= */
        public string PartNo { get; set; }
        public string PartDescription { get; set; }
        public string Dimension_Quality { get; set; }
        public string DieSerial { get; set; }
        public int? DieNumber { get; set; }
        public int? PreviousCount { get; set; }

        /* =======================
           PROCESS
           (DieMold_MoldingProcess)
           ======================= */
        public string ProcessID { get; set; }
        public string ProcessName { get; set; }

        /* =======================
          SEARCH CONTROL
          ======================= */
        public int SearchMode { get; set; }
        // 0 = PartNo
        // 1 = DieSerial

        /* =======================
           CTE / COMPUTED VALUES
           ======================= */
        public int TotalDie { get; set; }
        public int TotalQty { get; set; }
        public int TotalNo { get; set; } = 0;
        public int ShotOnwards { get; set; }
        public long TotalShotCount { get; set; }


    }





    public class DieMoldDieSerialInput
    {
        public string DieSerial { get; set; }
        public string DateInput { get; set; }
        public int CycleShot { get; set; }
        public int MachineNo { get; set; }
        public string Remarks { get; set; }
        public string Mincharge { get; set; }
    }
}