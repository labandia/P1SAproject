using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Policy;

namespace ProgramPartListWeb.Areas.Production1.Model
{
    public class FourMSummaryModel
    {
        public string NCRType { get; set; }

        public int Man { get; set; }
        public int Machine { get; set; }
        public int Material { get; set; }
        public int Method { get; set; }

        public int Qty { get; set; }

        public decimal Percentage { get; set; }
    }

    public class GroupSummaryModel
    {
        public string NCRType { get; set; }
        public decimal Group1 { get; set; }
        public decimal Group2 { get; set; }
        public decimal Group3 { get; set; }
        public decimal Matprep { get; set; }
        public decimal Oiloof { get; set; }
        public decimal Qty { get; set; }
        public decimal Percentage { get; set; }
    }


    public class RegistrationFinalModel
    {
        public int NCRID { get; set; }
        public string RegistrationNo { get; set; }
        public string CreatedDate { get; set; }
        public string ModelShopOrder { get; set; }
        public string OriginID { get; set; }
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int FourMID { get; set; }
        public string FourMName { get; set; }
        public int NCRTypeID { get; set; }
        public string NCRTypeName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }


    public class LineTopsModel
    {
        public string NCRtype { get; set; }
        public string BestLine { get; set; }
        public int Qty { get; set; }
        public decimal Percentage { get; set; }
    }

    public class AwardDto
    {
        public int AwardID { get; set; }
        public string Months { get; set; }
        public string EmployeeID { get; set; }
        public string WinnerName { get; set; }
        public string CertificateImage { get; set; }
        public string Subtitle { get; set; }
        public string AssignLine { get; set; }
        public string DefectDetect { get; set; }
        public bool IsDisplayed { get; set; }
    }


    public class LineTopNCRModel
    {
        public string Line { get; set; }
        public string NCRType { get; set; }
        public int Qty { get; set; }
        public decimal Percentage { get; set; }
    }


    public class ProcessGroupsModel
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int ProcessGroups { get; set; }
    }


    public class Monthyear
    {
        public string months { get; set; }
        public int years { get; set; }
    }


    public class TotalOutputChartModel
    {
        public int TotalOutput { get; set; }
        public int AverageOutput { get; set; }
        public int TargetOutput { get; set; }
        public int AverageEfficiency { get; set; }

        public List<TotalGroupOutputModel> totalGroup { get; set;}

        public List<DailyOutputModel> daily { get; set; }

    }

    public class TotalGroupOutputModel
    {
        public string GroupName { get; set; }
        public int TotalOutput { get; set; }
        public int AvgOutput { get; set; }
        public int TargetOutput { get; set; }
        public int Efficiency { get; set; }
    }

    public class DailyOutputModel
    {
        public string date { get; set; }
        public int value { get; set; }
    }


    public class AuditInfoModel
    {
        public int AuditInfoID { get; set; }
        public string Customer { get; set; }
        public string AuditDate { get; set; }
        public string AuditTime { get; set; }
        public string CoverageArea { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ProductionGroupModel
    {
        public string GroupDate { get; set; }
        public int Group1 { get; set; }
        public int Group2 { get; set; }
        public int Group3 { get; set; }
        public int OP { get; set; }
        public int Total { get; set; }  
    }


    public class AttendanceSummaryModel
    {
        public int TotalHeadCount { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalAbsent { get; set; }
        public double AttendRate { get; set; }
        public int AbsentRate { get ; set; }
    }



    public class AttendanceModel
    {
        public int DashID { get; set; }

        public int DepartmentId { get; set; }
        public int DayShiftCount { get; set; }
        public int NightShiftCount { get; set; }

        public int TotalHeadCount { get; set; }
        public int PresentCount { get; set; }
        public int Absent { get; set; }
        public double AttendanceRate { get; set; }
        public string DepartmentName
        {
            get
            {
                switch (DepartmentId)
                {
                    case 1:
                        return "P1SA-M";
                    case 2:
                        return "P1SA-P";
                    case 3:
                        return "P1SA-R";
                    case 4:
                        return "P1SA-W";
                    case 5:
                        return "P1SA-C";
                    case 6:
                        return "P1SA-PC";
                    case 8:
                        return "P1SA-FA";
                    default:
                        return "-- Select FourM --";
                }
            }
        }

        public class UpdateAttendanceBreakDownRequest
        {
            public int DashID { get; set; }
            public int? DayShiftCount { get; set; }
            public int? NightShiftCount { get; set; }
        }
        public class AttendanceTrendModel
        {
            public DateTime DateToday { get; set; }
            public double AttendRate { get; set; }
            public double AbsentRate { get; set; }
        }

    }

}