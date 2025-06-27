

using System;

namespace PMACS_V2.Areas.P1SA.Models
{
    public class ManpowerModel
    {
        private int _Manpower_ID;
        private int _Section_ID;
        private string _Process;
        private int _SDP;
        private int _SubCon;
        private string _Remarks;
        private string _Section_name;

        public int Manpower_ID
        {
            get => _Manpower_ID;
            set => _Manpower_ID = value;
        }
        public int Section_ID
        {
            get => _Section_ID;
            set => _Section_ID = value;
        }
        public string Process
        {
            get => _Process;
            set => _Process = value;
        }
        public int SDP
        {
            get => _SDP;
            set => _SDP = value;
        }
        public int SubCon
        {
            get => _SubCon;
            set => _SubCon = value;
        }
        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
        public string Section_name
        {
            get => _Section_name;
            set => _Section_name = value;
        }
    }
    public class TotalManpowerSection
    {
        private int _Section_ID;
        private string _SectionName;
        private int _Actual;
        private int _Required;
        private int _Lacking;

        public int Section_ID
        {
            get => _Section_ID;
            set => _Section_ID = value;
        }
        public string SectionName
        {
            get => _SectionName;
            set => _SectionName = value;
        }
        public int Actual
        {
            get => _Actual;
            set => _Actual = value;
        }
        public int Required
        {
            get => _Required;
            set => _Required = value;
        }
        public int Lacking
        {
            get => _Lacking;
            set => _Lacking = value;
        }
    }

    public class UpdateStatusModel
    {
        public string Module { get; set; }
        public DateTime LastUpdated { get; set; }   
        public string UpdatedBy { get; set; }
    }



    public class UserLogs
    {
        public int ModuleID { get; set; }   
        public string Fullname {  get; set; }   
        public string Action { get; set; }
        public string LastUpdated {  get; set; }   
    }
}