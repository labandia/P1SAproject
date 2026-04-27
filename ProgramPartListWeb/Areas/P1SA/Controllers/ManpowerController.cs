using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
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
        private readonly IP1SAEmployee _emp;

        public ManpowerController(IP1SAEmployee emp)
        {
            _emp = emp;
        }
        //-----------------------------------------------------------------------------------------
        //---------------------------- OVERALL SUMMARY   ------------------------------------------
        //-----------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------
        //---------------------------- P1SA EMPLOYEEMANAGEMENT ------------------------------------
        //-----------------------------------------------------------------------------------------
        public async Task<ActionResult> GetEmployeelist(
                    string search,
                    int depid = 0,
                    int page = 1,
                    int pageSize = 10)
        {
            var data = await _emp.GetEmployees(search, depid, page, pageSize);

            if (data == null)
                JsonNotFound("No Data found");

            return JsonSuccess(data, "Retrieved data successfully");
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployeeList(P1SAEmployeesInputModel mode)
        {
            bool result = await _emp.CreateEmployee(mode);

            if (!result) JsonValidationError("Input Validation error");

            return JsonCreated(mode, "Add Employee Records Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EditEmployeeList(P1SAEmployeesInputModel mode)
        {
            bool result = await _emp.UpdateEmployee(mode);

            if (!result) JsonValidationError("Input Validation error");

            return JsonCreated(mode, "Data Modified Successfully");
        }






        // GET: P1SA/Manpower/ManageEmployee
        public ActionResult ManageEmployee() => View();
        // GET: P1SA/Manpower
        public ActionResult Index() => View();
    }
}