using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [GlobalErrorException]
    public class PMACSController : ExtendController
    {
        private readonly IManpower _man;

        public PMACSController(IManpower man) => _man = man;


        public async Task<ActionResult> GetFullnameList()
        {
            var data = await CacheHelper.GetOrSetAsync("username", () =>  _man.GetUserFullname(), 15);
            if (data == null || !data.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetLastUpdatedData()
        {
            var data = await CacheHelper.GetOrSetAsync("Updatestats", () => _man.GetUpdatedData(), 15);
            if (data == null || !data.Any())
                return JsonNotFound("No Updatestats data found");

            return JsonSuccess(data);
        }


        // ===========================================================
        // ==================== Manpower Data ========================
        // ===========================================================
        public async Task<ActionResult> GetManpowerData()
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("manpower", () => _man.GetManpower(), 15);
                if (data == null || !data.Any())
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetTotalmanpower(string months)
        {
            try
            {
               
                var data = await CacheHelper.GetOrSetAsync("requiredManpower", () => _man.GetTotalManpower(months), 15);
                if (data == null || !data.Any())
                {
                    return JsonNotFound("No Total Manpower found");
                }
                   
                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        // ============================================================
        // ==================== Edit Manpower =========================
        // ============================================================
        [HttpPost]
        public async Task<ActionResult> EditManpower()
        {
            var obj = new
            {
                SDP = Convert.ToInt32(Request.Form["SDP"]), 
                SubCon = Convert.ToInt32(Request.Form["Sub/Con"]),
                Remarks = Request.Form["Remarks"],
                Manpower_ID = Convert.ToInt32(Request.Form["Manpower_ID"])
            };

            CacheHelper.Remove("manpower");

            var formdata = GlobalUtilities.GetMessageResponse(await _man.EditRequireManpower(obj), 0);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> EdiEditRequiredManpower()
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
            }
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        // ============================================================




        // ======================= RENDER THE PAGE  ==================
        // GET: P1SA/Mainpage
        public ActionResult Mainpage() => View();
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
        public ActionResult Sample() => View();
    }
}