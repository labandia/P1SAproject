using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMACS_V2.Areas.P1SA.Models
{
    // ===========================================================
    // ==================== WINDING SECTION ======================
    // ===========================================================



    // ===========================================================
    // ==================== MOLDING SECTION ======================
    // ===========================================================

    public class AddMoldingModelPost
    {
        public string Model_name { get; set; }
        public string Partnum { get; set; }
        public double CycleTime { get; set; }
        public int Actual_cav { get; set; }
        public int DieQty { get; set; }
        public double Operation_time { get; set; }
        public string ProcessCode { get; set; }
        public int Capgroup_ID { get; set; }
    }
    public class MoldingPostmodel
    {
        public int Capinfo_ID { get; set; } 
        public double CycleTime { get; set; }
        public int Actual_cav { get; set; }
        public double Operation_time { get; set; }
        public int DieQty { get; set; }
        public string Partnum { get; set; }
    }
    // ===========================================================
    // ==================== ROTOR SECTION ========================
    // ===========================================================
    public class AddRotorModelPost
    {
        public string Model_name { get; set; }
        public string Cover { get; set; }
        public double CycleTime { get; set; }
        public int Impeller { get; set; }
        public int Dream { get; set; }
        public double Operation_time { get; set; }
        public string ProcessCode { get; set; }
        public int Capgroup_ID { get; set; }
    }
    public class EditRotorModelPost
    {
        public int Capinfo_ID { get; set; } = 0;
        public string Model_name { get; set; }
        public double CycleTime { get; set; }
        public string Cover { get; set; }
        public string Dream { get; set; }
        public string Impeller { get; set; }
        public double Operation_time { get; set; }
        public string ProcessCode { get; set; }
        public int Capgroup_ID { get; set; }
    }



    // ===========================================================
    // ==================== PRESS SECTION ========================
    // ===========================================================
    public class PressPostStatormodel
    {
        public int Capinfo_ID { get; set; }
        public double Lam { get; set; }
        public double Operation_time { get; set; }
        public double Row { get; set; }
        public double SPM { get; set; }
    }

    // ===========================================================
    // ==================== CIRCUIT SECTION ======================
    // ===========================================================

    public class CapacityGroupPostModel {
        public int Total_machine { get; set; }  
        public int Capday { get; set; } 
        public int Capmonth { get; set; }
        public int TotalMan { get; set; }
        public int Capgroup_ID { get; set; }
    }

    public class ProcessformPostModel
    {
        public string ProcessCode { get; set; }
        public double Days { get; set; }
        public int Months { get; set; }
        public double OperationTime { get; set; }
        public double Cap_Per_Machine { get; set; }
    }
}