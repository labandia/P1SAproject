using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ProgramPartListWeb.Areas.Production1.Model.AttendanceModel;

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
        public async Task<ActionResult> SaveEmployee(EmployeeProfile profile)
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

            if (result == 0) return JsonError("Failed to save employee details.");

            return JsonSuccess("Employee details saved successfully.");
        }


        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Employees/GetEmployeeList
        // Gets the List of Employee Details
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> GetEmployeeList(
            string search, string code, int DepartmentId, int status)
        {
            Debug.WriteLine($@"SearchText {search} - Code : {code} - Department ID : {DepartmentId} - Status :{status}");

            var res = await _emp.GetEmployeeDetails(search, code, DepartmentId, status);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }



        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Employees/AddProcessEmployee
        // Gets the Process Data by Department
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> GetProcessDepartment(int Department)
        {
            var res = await _emp.LoadProcessData(Department);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }
        // ─────────────────────────────────────────────────────────────────
        // POST: /Production1/Employees/AddProcessEmployee
        // Add New Process Task Job Based on Department 
        // ─────────────────────────────────────────────────────────────────

        [HttpPost]
        public async Task<ActionResult> AddProcessEmployee(ProcessEmployee model)
        {
            int added = await _emp.AddProcessData(model);
            if (added == 0)
                return JsonPostError("Failed to Add a New Process");

            return JsonSuccess(added);
        }


        // GET: Production1/Employees
        public ActionResult EmployeeList()
        {
            return View();
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