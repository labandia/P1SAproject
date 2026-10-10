using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class EmployeesController : ExtendController
    {
        private readonly IP1EmployeesService _emp;

        public EmployeesController(IP1EmployeesService emp) => _emp = emp;  

        // ─────────────────────────────────────────────────────────────────
        // POST: /Production1/Employees/SaveEmployee
        // Saves The Employees Details 
        // ─────────────────────────────────────────────────────────────────
        [HttpPost]
        public async Task<JsonResult> SaveEmployee(EmployeeProfile profile)
        {
            if (profile == null)
                 JsonNotFound("No data received.");

            profile.EmployeeCode = (profile.EmployeeCode ?? "").Trim();
            profile.LastName = (profile.LastName ?? "").Trim();
            profile.FirstName = (profile.FirstName ?? "").Trim();
            profile.MiddleName = (profile.MiddleName ?? "").Trim();

           
            string rest = string.Join(" ", new[] { profile.FirstName, profile.MiddleName }
                                       .Where(s => s.Length > 0));
            profile.FullName = profile.LastName + ", " + rest;

            int result = await _emp.SaveEmployeeManage(profile);

            if(result == 0) return JsonError("Failed to save employee details.");

            return JsonSuccess("Employee details saved successfully.");
        }

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