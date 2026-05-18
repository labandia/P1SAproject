using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Areas.P1SA.Interface;
using ProgramPartListWeb.Areas.P1SA.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.P1SA.Controllers
{
    public class ManpowerController : ExtendController
    {
        private readonly IP1SAEmployeeRepository _emp;

        public ManpowerController(IP1SAEmployeeRepository emp)
        {
            _emp = emp;
        }
        //-----------------------------------------------------------------------------------------
        //---------------------------- OVERALL SUMMARY   ------------------------------------------
        //-----------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------
        //---------------------------- P1SA EMPLOYEEMANAGEMENT ------------------------------------
        //-----------------------------------------------------------------------------------------
        // List of All Employees with Inluding Production Operators 
        [HttpGet]   
        public async Task<ActionResult> GetEmployeelist(
                    string search,
                    int depid = 0,
                    int gender = 0,
                    int pos = 0,
                    int agency = 0,
                    int status = 0,
                    int page = 1,
                    int pageSize = 10)
        {
            var data = await _emp.GetEmployees(search, depid, gender,pos, agency, status, page, pageSize);

            if (data == null)
                JsonNotFound("No Data found");

            return JsonSuccess(data, "Retrieved data successfully");
        }
        // List of Employee for Production Operators
        [HttpGet]
        public async Task<ActionResult> GetProductionEmployeelist(
                    string search,
                    int depid = 0,
                    int gender = 0,
                    int pos = 0,
                    int agency = 0,
                    int status = 0,
                    int page = 1,
                    int pageSize = 10)
        {
            var data = await _emp.GetProductionEmployees(search, gender, agency, status, page, pageSize);

            if (data == null)
                JsonNotFound("No Data found");

            return JsonSuccess(data, "Retrieved data successfully");
        }

        // Get the Employees Details    
        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails(int employeeID)
        {
            var data = await _emp.GetEmployees(employeeID);

            if (data == null)
                JsonNotFound("No Data found");

            return JsonSuccess(data, "Retrieved data successfully");
        }




        //[HttpPost]
        //public async Task<ActionResult> AddEmployeeList(P1SAEmployeesInputModel mode)
        //{
        //    bool result = await _emp.CreateEmployee(mode);

        //    if (!result) JsonValidationError("Input Validation error");

        //    return JsonCreated(mode, "Add Employee Records Successfully");
        //}

        //[HttpPost]
        //public async Task<ActionResult> EditEmployeeList(P1SAEmployeesInputModel mode)
        //{
        //    bool result = await _emp.UpdateEmployee(mode);

        //    if (!result) JsonValidationError("Input Validation error");

        //    return JsonCreated(mode, "Data Modified Successfully");
        //}




        // GET: P1SA/Manpower/EmployeeDetails/24050006
        public ActionResult EmployeeDetails(int EmployeeId) => View();

        // GET: P1SA/Manpower/EmployeeDetails/24050006
        public ActionResult EmployeeProductionDetails(int EmployeeId) => View();
        // GET: P1SA/Manpower/ManageEmployee
        public ActionResult ManageEmployee() => View();

        // GET: P1SA/Manpower/Productions
        public ActionResult Productions() => View();

        // GET: P1SA/LoginPage
        public ActionResult LoginPage() => View();

        // GET: P1SA/Selection
        public ActionResult Index() => View();
    }
}