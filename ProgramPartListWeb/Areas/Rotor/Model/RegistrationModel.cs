using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Rotor.Model
{
    public class RotorRegistrationModel
    {
        public int RegistrationID { get; set; }
        // New value (from UI)
        public string NewRegistrationNo { get; set; }
        public DateTime DateCreated { get; set; }  
        public string RegistrationNo { get; set; }  
        public string CategoryName { get; set; }    
        public string Desciprtion { get; set; }
        public string Remarks { get; set; }
        public int CategoryID { get; set; }  // 1:   2:   3:
        public int DepartmentID { get; set; }
    }

    public class RotorCatergoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}