using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string WinnerName { get; set; }
        public string CertificateImage { get; set; }
        public string Subtitle { get; set; }
        public bool IsDisplayed { get; set; }
    }

}