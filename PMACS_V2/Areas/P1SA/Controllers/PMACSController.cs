using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Areas.P1SA.Repository;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using PMACS_V2.Utilities.Security;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [CompressResponse]
    [RateLimiting(300, 1)] // Limits the No of Request
    public class PMACSController : ExtendController
    {
        private readonly IManpower _man;
        public PMACSController(IManpower man) => _man = man;


        public async Task<ActionResult> GetUpdateLogs(int Module)
        {
            var data = await UpdateRepository.GetUserLogs(Module) ?? new List<UserLogs>();
            if (data == null || !data.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetFullnameList()
        {
            var data = await _man.GetUserFullname() ?? new List<UserAccount>();
            if (data == null || !data.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetLastUpdatedData()
        {
            var data = await _man.GetUpdatedData() ?? new List<UpdateStatusModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Updatestats data found");

            return JsonSuccess(data);
        }


        // ===========================================================
        // ==================== Manpower Data ========================
        // ===========================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetManpowerData()
        {
            var data = await _man.GetManpower() ?? new List<ManpowerModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetTotalmanpower(string months)
        {
            var data = await _man.GetTotalManpower(months) ?? new List<TotalManpowerSection>();
            if (data == null || !data.Any())
                return JsonNotFound("No Total Manpower found");

            return JsonSuccess(data);
        }
        // ============================================================
        // ==================== Edit Manpower =========================
        // ============================================================
        [HttpPost]
        public async Task<ActionResult> EditManpower(manpowerform man)
        {

            bool result = await _man.EditRequireManpower(man);
            if (result)
            {
                CacheHelper.Remove("manpower");
                await UpdateRepository.UpdateUserLogs(1, 1, "Edit");
            }

            return JsonCreated(result, "Edit Manpower Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> EditRequiredManpower()
        {
            var obj = new
            {
                Section_ID = Convert.ToInt32(Request.Form["sectionID"]),
                Required = Convert.ToInt32(Request.Form["requiredQty"])
            };

            bool result = await _man.EditRequireManpower(obj);
            var formdata = GlobalUtilities.GetMessageResponse(result, 0);
            if (result)
            {
                CacheHelper.Remove("requiredManpower");
                await UpdateRepository.UpdateUserLogs(1, 1, "Edit");
            }
            return JsonCreated(result, "Edit Required Manpower Successfully");
        }
        // ============================================================




        // ======================= RENDER THE PAGE  ==================
        // GET: P1SA/Mainpage
        public ActionResult Mainpage()
        {
            ViewData["Version"] = "2.0.10";
            return View();
        }
        // GET: P1SA/ManpowerProduction
        public ActionResult ManpowerProduction() => View();
        // GET: P1SA/FanMajor
        public ActionResult FanMajor() => View();
        // GET: P1SA/P1SASummary
        public ActionResult P1SASummary() => View();
        // GET: P1SA/Selection
        public ActionResult Selection() => View();
        // GET: P1SA/ForecastData
        public ActionResult ForecastData() => View();
        // GET: P1SA/FanMajor
    }
}