using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.P1SA.Controllers
{
    public class ManpowerController : Controller
    {


        // GET: P1SA/Manpower/ManageEmployee
        public ActionResult ManageEmployee() => View();
        // GET: P1SA/Manpower
        public ActionResult Index() => View();
    }
}