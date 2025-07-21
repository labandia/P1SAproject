using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class SignatureModel
    {
        public HttpPostedFileBase SignatureImage { get; set; }  
        public int UserID { get; set; }
    }
}