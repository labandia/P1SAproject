

using System.Collections.Generic;

namespace PMACS_V2.Areas.P1SA.Models
{
    public class UserAccount
    {
        public string Fullname { get; set; }    
    }

    public class SelectionGroup
    {
        private int _Capgroup_ID;
        private string _Cap_name;
        private int _Total_machine;
        private string _Capday;
        private string _Capmonth;
        private string _Totalhour;
        private int _TotalMan;

       

        public int Capgroup_ID
        {
            get => _Capgroup_ID;
            set => _Capgroup_ID = value;
        }
        public string Cap_name
        {
            get => _Cap_name;
            set => _Cap_name = value;
        }
        public int Total_machine
        {
            get => _Total_machine;
            set => _Total_machine = value;
        }
        public string Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public string Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }
        public string Totalhour
        {
            get => _Totalhour;
            set => _Totalhour = value;
        }
        public int TotalMan
        {
            get => _TotalMan;
            set => _TotalMan = value;
        }
    }
    public class PsummaryModel
    {
        private string _ProcessCode;
        private string _ProcessName;
        private int _AvailMachine;
        private int _ActualMachine;
        private int _Machine_Utilization;
        private int _Cap_Per_Machine;
        private string _PerDay;
        private string _Maximum;
        private string _Minimum;
        private int _Capgroup_ID;

        public string ProcessCode
        {
            get => _ProcessCode;
            set => _ProcessCode = value;
        }
        public string ProcessName
        {
            get => _ProcessName;
            set => _ProcessName = value;
        }
        public int AvailMachine
        {
            get => _AvailMachine;
            set => _AvailMachine = value;
        }
        public int ActualMachine
        {
            get => _ActualMachine;
            set => _ActualMachine = value;
        }
        public int Machine_Utilization
        {
            get => _Machine_Utilization;
            set => _Machine_Utilization = value;
        }
        public int Cap_Per_Machine
        {
            get => _Cap_Per_Machine;
            set => _Cap_Per_Machine = value;
        }
        public string PerDay
        {
            get => _PerDay;
            set => _PerDay = value;
        }
        public string Maximum
        {
            get => _Maximum;
            set => _Maximum = value;
        }
        public string Minimum
        {
            get => _Minimum;
            set => _Minimum = value;
        }
        public int Capgroup_ID
        {
            get => _Capgroup_ID;
            set => _Capgroup_ID = value;
        }  
    }
    //public class PsummaryModel
    //{
    //    private string _Process_code;
    //    private string _Process_name;
    //    private int _Avail_Machine;
    //    private int _Acquired;
    //    private double _Machine_Utilization;
    //    private int _Cap_Per_Machine;
    //    private int _PerDay;
    //    private int _Maximum;
    //    private int _Minimum;
    //    private int _Capgroup_ID;

    //    public string Process_code
    //    {
    //        get => _Process_code;
    //        set => _Process_code = value;
    //    }
    //    public string Process_name
    //    {
    //        get => _Process_name;
    //        set => _Process_name = value;
    //    }
    //    public int Avail_Machine
    //    {
    //        get => _Avail_Machine;
    //        set => _Avail_Machine = value;
    //    }
    //    public int Acquired
    //    {
    //        get => _Acquired;
    //        set => _Acquired = value;
    //    }
    //    public double Machine_Utilization
    //    {
    //        get => _Machine_Utilization;
    //        set => _Machine_Utilization = value;
    //    }
    //    public int Cap_Per_Machine
    //    {
    //        get => _Cap_Per_Machine;
    //        set => _Cap_Per_Machine = value;
    //    }
    //    public int PerDay
    //    {
    //        get => _PerDay;
    //        set => _PerDay = value;
    //    }
    //    public int Maximum
    //    {
    //        get => _Maximum;
    //        set => _Maximum = value;
    //    }
    //    public int Minimum
    //    {
    //        get => _Minimum;
    //        set => _Minimum = value;
    //    }
    //    public int Capgroup_ID
    //    {
    //        get => _Capgroup_ID;
    //        set => _Capgroup_ID = value;
    //    }
    //}


   


    public class CapacitySummaryModel
    {
        private string _ProcessCode;
        private string _ProcessName;
        private double _CycleTime;
        private double _OperationTime;
        private double _Days;
        private int _Months;
        private int _Forecast;
        private int _AvailMachine;
        private int _Cap_Per_Machine;
        private int _Totalhours;
        private int _RequiredMan;
        private int _Capday;
        private int _Capmonth;

        public string ProcessCode
        {
            get => _ProcessCode;
            set => _ProcessCode = value;
        }
        public string ProcessName
        {
            get => _ProcessName;
            set => _ProcessName = value;
        }
        public double CycleTime
        {
            get => _CycleTime;
            set => _CycleTime = value;
        }
        public double OperationTime
        {
            get => _OperationTime;
            set => _OperationTime = value;
        }
        public double Days
        {
            get => _Days;
            set => _Days = value;
        }
        public int Months
        {
            get => _Months;
            set => _Months = value;
        }
        public int Forecast
        {
            get => _Forecast;
            set => _Forecast = value;
        }
        public int AvailMachine
        {
            get => _AvailMachine;
            set => _AvailMachine = value;
        }
        public int Cap_Per_Machine
        {
            get => _Cap_Per_Machine;
            set => _Cap_Per_Machine = value;
        }
        public int Totalhours
        {
            get => _Totalhours;
            set => _Totalhours = value;
        }
        public int RequiredMan
        {
            get => _RequiredMan;
            set => _RequiredMan = value;
        }
        public int Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public int Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }
    }
    public class ForecastModel
    {
        public int Forecast_ID { get; set; }    
        public string Model_name { get; set; }
        public double January { get; set; }
        public double February { get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
        public double June { get; set; }
        public double July { get; set; }
        public double August { get; set; }
        public double September { get; set; }
        public double October { get; set; }
        public double November { get; set; }
        public double December { get; set; }

    }






    public class MoldingModel
    {
        private int _Capinfo_ID;
        private double _Cyclepcs;
        private int _DieQty;
        private int _Actual_Cav;
        private double _Days;
        private double _CycleTime;
        private double _Operation_time;
        private string _Model_name;
        private string _Partnum;
        private int _Months;
        private int _foredata;
        private double _Seconds;
        private double _Mins;
        private double _manhour;
        private double _Manpower;
        private double _Daysmin;
        private double _Require;
        private string _Capday;
        private string _Capmonth;

        public int Capinfo_ID
        {
            get => _Capinfo_ID;
            set => _Capinfo_ID = value;
        }
        public double Cyclepcs
        {
            get => _Cyclepcs;
            set => _Cyclepcs = value;
        }
        public int DieQty
        {
            get => _DieQty;
            set => _DieQty = value;
        }
        public int Actual_Cav
        {
            get => _Actual_Cav;
            set => _Actual_Cav = value;
        }
        public double Days
        {
            get => _Days;
            set => _Days = value;
        }
        public double CycleTime
        {
            get => _CycleTime;
            set => _CycleTime = value;
        }
        public double Operation_time
        {
            get => _Operation_time;
            set => _Operation_time = value;
        }
        public string Model_name
        {
            get => _Model_name;
            set => _Model_name = value;
        }
        public string Partnum
        {
            get => _Partnum;
            set => _Partnum = value;
        }
        public int Months
        {
            get => _Months;
            set => _Months = value;
        }
        public int foredata
        {
            get => _foredata;
            set => _foredata = value;
        }
        public double Seconds
        {
            get => _Seconds;
            set => _Seconds = value;
        }
        public double Mins
        {
            get => _Mins;
            set => _Mins = value;
        }
        public double manhour
        {
            get => _manhour;
            set => _manhour = value;
        }
        public double Manpower
        {
            get => _Manpower;
            set => _Manpower = value;
        }
        public double Daysmin
        {
            get => _Daysmin;
            set => _Daysmin = value;
        }
        public double Require
        {
            get => _Require;
            set => _Require = value;
        }
        public string Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public string Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }

    }
    public class RotorModel
    {
        private int _Capinfo_ID;
        private string _Cover;
        private double _CycleTime;
        private string _Impeller;
        private string _Model_name;
        private string _Dream;
        private double _Operation_time;
        private int _foredata;
        private int _Capgroup_ID;
        private string _ProcessCode;
        private double _manhour;
        private int _Avail_Machine;
        private double _Days;
        private int _Months;
        private double _Manpower;
        private double _Require;
        private string _Capday;
        private string _Capmonth;

        public int Capinfo_ID
        {
            get => _Capinfo_ID;
            set => _Capinfo_ID = value;
        }   
        public string Cover
        {
            get => _Cover;
            set => _Cover = value;
        }
        public string Impeller
        {
            get => _Impeller;
            set => _Impeller = value;
        }
        public double Days
        {
            get => _Days;
            set => _Days = value;
        }
        public double CycleTime
        {
            get => _CycleTime;
            set => _CycleTime = value;
        }
        public double Operation_time
        {
            get => _Operation_time;
            set => _Operation_time = value;
        }
        public string Model_name
        {
            get => _Model_name;
            set => _Model_name = value;
        }
        public int foredata
        {
            get => _foredata;
            set => _foredata = value;
        }
        public string Dream
        {
            get => _Dream;
            set => _Dream = value;
        }
        public int Capgroup_ID
        {
            get => _Capgroup_ID;
            set => _Capgroup_ID = value;
        }
        public string ProcessCode
        {
            get => _ProcessCode;
            set => _ProcessCode = value;
        }
        public int Avail_Machine
        {
            get => _Avail_Machine;
            set => _Avail_Machine = value;
        }
        public double manhour
        {
            get => _manhour;
            set => _manhour = value;
        }
        public double Manpower
        {
            get => _Manpower;
            set => _Manpower = value;
        }

        public int Months
        {
            get => _Months;
            set => _Months = value;
        }
        public double Require
        {
            get => _Require;
            set => _Require = value;
        }
        public string Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public string Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }
    }
    public class WindingModel
    {
        private int _Capinfo_ID;
        private string _Product_type;
        private string _Model_name;
        private string _Winding_Assy;
        private double _Cyclepcs;
        private double _CycleTime;
        private double _WireDia;
        private string _WindTurns;
        private int _foredata;
        private string _ProcessCode;
        private int _Capgroup_ID;
        private double _manhour;
        private double _Manpower;
        private double _Require;
        private double _Daysmin;
        private string _Capday;
        private string _Capmonth;
        private double _jig;
        private double _Head_count;
        private int _Avail_Machine;
        private int _Months;
        private double _Operation_time;

        public int Capinfo_ID
        {
            get => _Capinfo_ID;
            set => _Capinfo_ID = value;
        }
        public string Product_type
        {
            get => _Product_type;
            set => _Product_type = value;
        }

        public string Model_name
        {
            get => _Model_name;
            set => _Model_name = value;
        }

        public string Winding_Assy
        {
            get => _Winding_Assy;
            set => _Winding_Assy = value;
        }
        public double Cyclepcs
        {
            get => _Cyclepcs;
            set => _Cyclepcs = value;
        }
        public double CycleTime
        {
            get => _CycleTime;
            set => _CycleTime = value;
        }

        public double WireDia
        {
            get => _WireDia;
            set => _WireDia = value;
        }
        
        public string WindTurns
        {
            get => _WindTurns;
            set => _WindTurns = value;
        }
        public double Operation_time
        {
            get => _Operation_time;
            set => _Operation_time = value;
        }
       
        public int foredata
        {
            get => _foredata;
            set => _foredata = value;
        }
        public double jig
        {
            get => _jig;
            set => _jig = value;
        }
        public double Head_count
        {
            get => _Head_count;
            set => _Head_count = value;
        }
        public int Capgroup_ID
        {
            get => _Capgroup_ID;
            set => _Capgroup_ID = value;
        }
        public string ProcessCode
        {
            get => _ProcessCode;
            set => _ProcessCode = value;
        }
        public int Avail_Machine
        {
            get => _Avail_Machine;
            set => _Avail_Machine = value;
        }
        public double manhour
        {
            get => _manhour;
            set => _manhour = value;
        }
        public double Manpower
        {
            get => _Manpower;
            set => _Manpower = value;
        }
        public double Daysmin
        {
            get => _Daysmin;
            set => _Daysmin = value;
        }
        public int Months
        {
            get => _Months;
            set => _Months = value;
        }
        public double Require
        {
            get => _Require;
            set => _Require = value;
        }
        public string Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public string Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }

    }
    public class PressModel
    {
        private int _Capinfo_ID;
        private string _Product_type;
        private string _Model_name;
        private double _Cyclepcs;
        private double _CycleTime;
        private double _DieQty;
        private double _Operation_time;
        private string _Partnum;
        private string _Cycle_cnc;
        private string _Cycle_drill;
        private string _Apearance_inspect;
        private string _Pre_wash;
        private string _Wash_cycle;
        private string _Bucket;
        private string _Laser;
        private string _TotalCycle;
        private string _CycleTap;
        private string _SPM;
        private string _Row;
        private string _CTLam;
        private string _Lam;
        private int _foredata;
        private int _Capgroup_ID;
        private string _ProcessCode;
        private double _manhour;
        private double _Manpower;
        private double _Require;
        private double _Days;
        private string _Capday;
        private string _Capmonth;
        private int _Months;

        public int Capinfo_ID
        {
            get => _Capinfo_ID;
            set => _Capinfo_ID = value;
        }
        public string Product_type
        {
            get => _Product_type;
            set => _Product_type = value;
        }
        public int foredata
        {
            get => _foredata;
            set => _foredata = value;
        }
        public string Model_name
        {
            get => _Model_name;
            set => _Model_name = value;
        }
        public double DieQty
        {
            get => _DieQty;
            set => _DieQty = value;
        }
        public string Row
        {
            get => _Row;
            set => _Row = value;
        }
        public string Lam
        {
            get => _Lam;
            set => _Lam = value;
        }
        public string SPM
        {
            get => _SPM;
            set => _SPM = value;
        }
        public string CTLam
        {
            get => _CTLam;
            set => _CTLam = value;
        }
        public double CycleTime
        {
            get => _CycleTime;
            set => _CycleTime = value;
        }

        public double Cyclepcs
        {
            get => _Cyclepcs;
            set => _Cyclepcs = value;
        }
        public string ProcessCode
        {
            get => _ProcessCode;
            set => _ProcessCode = value;
        }
        public int Capgroup_ID
        {
            get => _Capgroup_ID;
            set => _Capgroup_ID = value;
        }
        public double Operation_time
        {
            get => _Operation_time;
            set => _Operation_time = value;
        }
        public string Partnum
        {
            get => _Partnum;
            set => _Partnum = value;
        }
        public string Cycle_cnc
        {
            get => _Cycle_cnc;
            set => _Cycle_cnc = value;
        }
        public string Cycle_drill
        {
            get => _Cycle_drill;
            set => _Cycle_drill = value;
        }
        public string CycleTap
        {
            get => _CycleTap;
            set => _CycleTap = value;
        }

        public string Apearance_inspect
        {
            get => _Apearance_inspect;
            set => _Apearance_inspect = value;
        }
        public string Pre_wash
        {
            get => _Pre_wash;
            set => _Pre_wash = value;
        }
        public string Wash_cycle
        {
            get => _Wash_cycle;
            set => _Wash_cycle = value;
        }
        public string Bucket
        {
            get => _Bucket;
            set => _Bucket = value;
        }
        public string Laser
        {
            get => _Laser;
            set => _Laser = value;
        }

        public string TotalCycle
        {
            get => _TotalCycle;
            set => _TotalCycle = value;
        }
     

        public double Days
        {
            get => _Days;
            set => _Days = value;
        }
        public double manhour
        {
            get => _manhour;
            set => _manhour = value;
        }
        public double Manpower
        {
            get => _Manpower;
            set => _Manpower = value;
        }
    
        public int Months
        {
            get => _Months;
            set => _Months = value;
        }
        public double Require
        {
            get => _Require;
            set => _Require = value;
        }
        public string Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public string Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }
    }
    public class CircuitModel
    {
        private int _Capinfo_ID;
        private string _Product_type;
        private string _Model_name;
        private double _CycleTime;
        private double _PWB_TYPE;
        private double _Operation_time;
        private string _Model;
        private string _PCBA;
        private string _PWB;
        private string _HUMISEAL;
        private string _PELGAN_Z;
        private string _SUPER_XL;
        private string _SILICONE;
        private string _Month;
        private int _foredata;
        private int _Capgroup_ID;
        private string _ProcessCode;
        private double _manhour;
        private double _Manpower;
        private double _Require;
        private double _Days;
        private string _Capday;
        private string _Capmonth;
        private int _Months;

        public int Capinfo_ID
        {
            get => _Capinfo_ID;
            set => _Capinfo_ID = value;
        }
        public string Product_type
        {
            get => _Product_type;
            set => _Product_type = value;
        }
        public string Model_name
        {
            get => _Model_name;
            set => _Model_name = value;
        }

        public double CycleTime
        {
            get => _CycleTime;
            set => _CycleTime = value;
        }
        public double PWB_TYPE
        {
            get => _PWB_TYPE;
            set => _PWB_TYPE = value;
        }
        public double Operation_time
        {
            get => _Operation_time;
            set => _Operation_time = value;
        }
        public string Model
        {
            get => _Model;
            set => _Model = value;
        }

        public string PCBA
        {
            get => _PCBA;
            set => _PCBA = value;
        }
        public string PWB
        {
            get => _PWB;
            set => _PWB = value;
        }
        public string HUMISEAL
        {
            get => _HUMISEAL;
            set => _HUMISEAL = value;
        }

        public string PELGAN_Z
        {
            get => _PELGAN_Z;
            set => _PELGAN_Z = value;
        }


        public string SUPER_XL
        {
            get => _SUPER_XL;
            set => _SUPER_XL = value;
        }
        public string SILICONE
        {
            get => _SILICONE;
            set => _SILICONE = value;
        }

        public string Month
        {
            get => _Month;
            set => _Month = value;
        }
        public int foredata
        {
            get => _foredata;
            set => _foredata = value;
        }
        public int Capgroup_ID
        {
            get => _Capgroup_ID;
            set => _Capgroup_ID = value;
        }

        public string ProcessCode
        {
            get => _ProcessCode;
            set => _ProcessCode = value;
        }
        public double manhour
        {
            get => _manhour;
            set => _manhour = value;
        }
        public double Manpower
        {
            get => _Manpower;
            set => _Manpower = value;
        }

        public double Require
        {
            get => _Require;
            set => _Require = value;
        }
        public double Days
        {
            get => _Days;
            set => _Days = value;
        }
        public int Months
        {
            get => _Months;
            set => _Months = value;
        }

        public string Capday
        {
            get => _Capday;
            set => _Capday = value;
        }
        public string Capmonth
        {
            get => _Capmonth;
            set => _Capmonth = value;
        }
    }
}