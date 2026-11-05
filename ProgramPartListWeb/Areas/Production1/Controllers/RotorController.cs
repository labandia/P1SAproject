
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

        [HttpPost]
        public async Task<ActionResult> SaveEditEmployeeCode(EfficiencyEmployeeCode model)
        {
            Debug.WriteLine("========== Employee Code ==========");
            Debug.WriteLine($"Model is null: {model == null}");
            Debug.WriteLine($"ID : {model.EmployID}");
            if (model == null)
                return JsonPostError("Request body could not be bound.");


            bool IdCheck = model.EmployID > 0;

            bool results = IdCheck ? await _emp.EditEmployeeCode(model) : await _emp.AdditionalEmployeeCode(model);
            if (!results)
                return JsonPostError("Failed to Add a New Employee Code");

            return JsonSuccess();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEmployeeCode(int EmployID)
        {
            Debug.WriteLine("========== EfficiencyDaily DELETE EMPLOYEE CODE ==========");
            Debug.WriteLine($"EmployID: {EmployID}");

            bool deleted = await _emp.DeleteEmployeeCode(EmployID);
            if (!deleted)
                return JsonPostError("Failed to delete Employee Code");

            return JsonSuccess();
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



        [HttpPost]
        public async Task<ActionResult> SaveEditCycleTime()
        {
            Request.InputStream.Position = 0;
            string raw;
            using (var sr = new System.IO.StreamReader(Request.InputStream))
                raw = await sr.ReadToEndAsync();

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<EfficiencyCycleTime>(raw);
            if (model == null)
                return JsonPostError("Request body could not be bound.");

            bool IdCheck = model.CycleID > 0;

            bool result = IdCheck ? await _emp.UpdateCycleTime(model) : await _emp.AddCycleTime(model);
            return result ? JsonSuccess() : JsonPostError("Failed to Add a New Process");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCycleTime(int CycleID)
        {
            Debug.WriteLine("========== EfficiencyDaily DELETE CYCLE TIME ==========");
            Debug.WriteLine($"CycleID: {CycleID}");

            bool deleted = await _emp.DeleteCycleTime(CycleID);
            if (!deleted)
                return JsonPostError("Failed to delete Cycle Time");

            return JsonSuccess();
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