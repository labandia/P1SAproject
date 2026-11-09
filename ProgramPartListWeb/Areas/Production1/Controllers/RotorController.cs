
using DocumentFormat.OpenXml.EMMA;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
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
            bool isEdit = model.EfficiencyID > 0;
            //Debug.WriteLine($"Action : {IdCheck}");
            int results = isEdit
                    ? await _emp.EditEfficiencyRecord(model)
                    : await _emp.AddEfficiencyRecord(model);
            if (results == 0)
                return JsonPostError(isEdit ? "Failed to update the record." : "Failed to add the record.");

            return JsonSuccess();
        }
        [HttpPost]
        public async Task<ActionResult> DeleteEfficiencyRecord(int ID)
        {
            Debug.WriteLine("========== EfficiencyDailyMonitor ==========");
            Debug.WriteLine($"Record ID: {ID}");

            //if (model == null)
            //    return JsonPostError("Request body could not be bound.");
            ////Debug.WriteLine($"WorkDate: {model.WorkDate:yyyy-MM-dd}");
            //bool isEdit = model.EfficiencyID > 0;
            ////Debug.WriteLine($"Action : {IdCheck}");
            bool results = await _emp.DeleteEfficiencyRecord(ID);
            if (!results)
                return JsonPostError("Failed to Delete the record.");

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

        // ─────────────────────────────────────────────────────────────────
        // POST: IMPORT SHOP ORDERS
        // ─────────────────────────────────────────────────────────────────
        [HttpPost]
        public async Task<ActionResult> ImportShopOrders(List<EfficiencyShopOrderStaging> rows)
        {
            if (rows == null || rows.Count == 0)
                return JsonPostError("No rows were received.");

            var bad = rows.FirstOrDefault(r =>
                string.IsNullOrWhiteSpace(r.ShopOrder) || r.ShopOrder.Length != 12 || !r.ShopOrder.All(char.IsDigit) ||
                string.IsNullOrWhiteSpace(r.Partnumber) || r.Partnumber.Length > 50 ||
                (r.Model != null && r.Model.Length > 50));
            if (bad != null)
                return JsonPostError($"Invalid row for shop order '{bad.ShopOrder}'.");

            try
            {
                var result = await _emp.UploadShopOrderCSV(rows);
                return Json(new { Success = true, Inserted = result.Inserted, Updated = result.Updated });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return JsonPostError("Import failed: " + ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // POST: IMPORT EMPLOYEE CODES
        // ─────────────────────────────────────────────────────────────────
        [HttpPost]
        public async Task<ActionResult> ImportEmployeeCode(
            List<EfficiencyEmployeeCode> rows)
        {
            if (rows == null || rows.Count == 0)
                return JsonPostError("No rows were received.");

            try
            {
                var result = await _emp.UploadEmployeeCodes(rows);

                // Return success even if some employee codes are missing.
                return Json(new
                {
                    Success = true,

                    TotalUploaded = rows.Count,
                    Inserted = result.Inserted,
                    Updated = result.Updated,

                    Missing = result.Missing,
                    MissingCount = result.Missing.Count,

                    Message = result.Missing.Count > 0
                        ? "Import completed with skipped employees."
                        : "Import completed successfully."
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return JsonPostError("Import failed: " + ex.Message);
            }
        }


        // ─────────────────────────────────────────────────────────────────
        // POST: IMPORT CYCLE TIME
        // ─────────────────────────────────────────────────────────────────
        [HttpPost]
        public async Task<ActionResult> ImportCycleTime(
            List<EfficiencyCycleTime> rows)
        {
            if (rows == null || rows.Count == 0)
                return JsonPostError("No rows were received.");

            try
            {
                // 1. Normalize incoming data.
                foreach (var row in rows)
                {
                    if (row == null)
                        return JsonPostError("Invalid empty row detected.");

                    row.Partnumber = row.Partnumber?.Trim();
                    row.Model = row.Model?.Trim();
                }

                // 2. Validate required fields.
                var bad = rows.FirstOrDefault(r =>
                    string.IsNullOrWhiteSpace(r.Partnumber) ||
                    r.Partnumber.Length > 50 ||
                    (r.Model != null && r.Model.Length > 100) ||
                    r.ProcessId < 1 || r.ProcessId > 6 ||
                    r.CycleTimeSec <= 0
                );

                if (bad != null)
                {
                    return JsonPostError(
                        $"Invalid cycle time record. " +
                        $"Partnumber: '{bad.Partnumber}', " +
                        $"ProcessId: {bad.ProcessId}, " +
                        $"CycleTimeSec: {bad.CycleTimeSec}."
                    );
                }

                // 3. Check duplicate Partnumber + ProcessId.
                var dup = rows
                    .GroupBy(r => new
                    {
                        P = r.Partnumber.ToLowerInvariant(),
                        r.ProcessId
                    })
                    .FirstOrDefault(g => g.Count() > 1);

                if (dup != null)
                {
                    return JsonPostError(
                        $"Duplicate Partnumber / ProcessId: " +
                        $"'{dup.Key.P}' / Process ID {dup.Key.ProcessId}."
                    );
                }

                // 4. Execute database import.
                var result = await _emp.UploadCycleTimes(rows);

                // 5. Return successful import result.
                return Json(new
                {
                    Success = true,
                    TotalUploaded = rows.Count,
                    Inserted = result.Inserted,
                    Updated = result.Updated,
                    Message = "Cycle time import completed successfully."
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return JsonPostError(
                    "Cycle time import failed: " + ex.Message
                );
            }
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