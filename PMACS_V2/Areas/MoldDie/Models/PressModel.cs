using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Models
{

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