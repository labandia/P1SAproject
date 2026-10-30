
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class RotorController : ExtendController
    {
        private readonly IEfficiencyProcessRepository _emp;

        public RotorController(IEfficiencyProcessRepository emp) => _emp = emp;

        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Rotor/GetEmployeeList
        // Gets the List of Employee Details
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> GetProcessList()
        {

            var res = await _emp.GetProcessFilter();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }

        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Rotor/GetEfficiencyList?from=2026-10-29&to=2026-10-29
        // Gets the efficiency daily monitor records for a date range
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> GetEfficiencyList(DateTime? from, DateTime? to)
        {
            var res = await _emp.GetEfficiencyDailyMonitors(from, to);
            return JsonSuccess(res ?? Enumerable.Empty<object>());   // empty range is not an error
        }


        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Rotor/SearchByShopOrderFromCSV
        // Gets the List of Employee Details
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> SearchByShopOrderFromCSV(string ShopOrders)
        {

            var res = await _emp.GetEfficientShopOrders(ShopOrders);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }



        // ─────────────────────────────────────────────────────────────────
        // POST: /Production1/Rotor/AddRecordForMonitoring
        // Add New Process Task Job Based on Department 
        // ─────────────────────────────────────────────────────────────────

        [HttpPost]
        public async Task<ActionResult> AddRecordForMonitoring(EfficiencyDailyMonitor model)
        {
            Debug.WriteLine("========== EfficiencyDailyMonitor ==========");
            Debug.WriteLine($"Model is null: {model == null}");

            if (model == null)
                return JsonPostError("Request body could not be bound.");

           

            //Debug.WriteLine($"WorkDate: {model.WorkDate:yyyy-MM-dd}");

            int added = await _emp.AddEfficiencyRecord(model);
            if (added == 0)
                return JsonPostError("Failed to Add a New Process");

            return JsonSuccess();
        }



        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Employees/GetEmployeeList
        // Gets the List of Employee Details
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> GetEmployeeList()
        {

            var res = await _emp.GetEmployeeName("");
            if (res == null)
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }

        // ─────────────────────────────────────────────────────────────────
        // GET: /Production1/Employees/GetModelList
        // Gets the List of Employee Details
        // ─────────────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<ActionResult> GetModelList()
        {

            var res = await _emp.GetEfficiencyCycleTime();
            if (res == null)
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }


        // GET: Production1/Rotor
        public ActionResult Encoder()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}