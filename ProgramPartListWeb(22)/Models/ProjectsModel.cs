using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class ProjectsModel
    {
        public string Project_Name { get; set; }
        public int DepartmentID { get; set; }
        public string Links { get; set; }
        public string SystemImage { get; set; }
        public string Version { get; set; }
    }
}