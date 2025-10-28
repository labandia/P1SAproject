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
        public string Employee_ID { get; set; }
        public string FullName { get; set; }
        public string Filepath { get; set; }

        // 📋 Approval Info
        public string Manager_ID { get; set; }
        public string Manager_Comments { get; set; }
        public bool? ReportStatus { get; set; }

        // 🧑 Employee Info
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public bool? Inspect_IsAproved { get; set; }
        public bool? Inspect_IsSent { get; set; }

        // 👷 PIC Info
        public string PICName { get; set; }
        public string PICEmail { get; set; }
        public bool? PIC_IsEdit { get; set; }
        public bool? PIC_IsSent { get; set; }

        // 👨‍💼 Manager Info
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public bool? Manager_IsEdit { get; set; }
        public bool? Manager_IsSent { get; set; }

        // 🏭 Department Manager Info
        public string DepManagerName { get; set; }
        public string DepManagerEmail { get; set; }
        public bool? DepManager_IsSent { get; set; }
        public bool? DepManager_IsAproved { get; set; }

        // 🏢 Division Manager Info
        public string DivManagerName { get; set; }
        public string DivManagerEmail { get; set; }
        public bool? DivManager_IsSent { get; set; }
        public bool? DivManager_IsAproved { get; set; }
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


}