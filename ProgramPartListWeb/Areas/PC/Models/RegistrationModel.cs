using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.PC.Models
{
    public class PatrolRegistrationViewModel
    {
        // 🧾 Basic Info
        public string RegNo { get; set; }
        public string SectionName { get; set; }
        public string DateConduct { get; set; }
        public string Filepath { get; set; }
        public string CounterPath { get; set; }


        // 📋 Approval Info
        public string Manager_ID { get; set; }
        public string Manager_Comments { get; set; }
        public int ReportStatus { get; set; }

        // 🧑 Employee Info Or Inspector
        public string Inspect_ID { get; set; }
        public string InspectName { get; set; }
        public string PICName { get; set; }
        public string Inspect_Comments { get; set; }
        public bool? Inspect_IsAproved { get; set; }
        public bool? Inspect_IsSent { get; set; }

        // 👷 PIC Info
        public string PIC_ID { get; set; }
        public string PIC_Comments { get; set; }
        public bool? PIC_IsSent { get; set; }

        // 👨‍💼 Manager Info
        public bool? Manager_Isedit { get; set; }
        public bool? Manager_IsSent { get; set; }

        // 🏭 Department Manager Info
        public string DepManager_ID { get; set; }
        public bool? DepManager_IsAproved { get; set; }
        public bool? DepManager_IsSent { get; set; }

        // 🏢 Division Manager Info
        public string DivManager_ID { get; set; }
        public bool? DivManager_IsAproved { get; set; }
        public bool? DivManager_IsSent { get; set; }
    }






    public class AddFormRegistrationModel
    {
        public string RegNo { get; set; }
        public string DateConduct { get; set; }
        public int Department_ID { get; set; }

        public string Employee_ID { get; set; }
        public string PIC_ID { get; set; }
        public string Manager_ID { get; set; }
        public string DepManager_ID { get; set; }
        public string DivManager_ID { get; set; }


        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string CounterPath { get; set; }


        public string PIC_Comments { get; set; }
        public int PIC_isedit { get; set; }
        public int PIC_IsSent { get; set; }

        public string Inspect_Comments { get; set; }
        public int Inspect_IsSent { get; set; }
        public int Inspect_IsAproved { get; set; }

        public string Manager_Comments { get; set; }
        public int Manager_Isedit { get; set; }
        public int Manager_IsSent { get; set; }


        public int DepManager_IsSent { get; set; }
        public int DepManager_IsAproved { get; set; }

        public int DivManager_IsSent { get; set; }
        public int DivManager_IsAproved { get; set; }

    }

    public class EmailModelV2
    {
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Department_ID { get; set; }
    }


    public class ProcessOwnerForms
    {
        public string RegNo { get; set; }
        public string Employee_ID { get; set; }
        public string PIC_Comments { get; set; }
        public string CounterPath { get; set; }
        public string Filepath { get; set; }
    }

    public class RegistrationFiles
    {
        public string RegNo { get; set; }
        public string FilePath { get; set; }
        public string CounterPath { get; set; }
    }
}