using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Production1/Employees
        public ActionResult EmployeesForm()
        {
            return View();
        }

        // GET: Production1/Employees
        public ActionResult Index()
        {
            return View();
        }
    }
}