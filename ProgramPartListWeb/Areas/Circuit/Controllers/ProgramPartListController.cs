using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;


namespace ProgramPartListWeb.Areas.Circuit.Controllers
{
    public class ProgramPartListController : ExtendController
    {
        private readonly IPlanSchedule _plan;
        public ProgramPartListController(IPlanSchedule plan)
        {
            _plan = plan;   
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetPlanScheduleList()
        {
            var data = await _plan.GetPlanSchedules() ?? new List<PlanScheduleMode>();
            if (data == null) return JsonNotFound("No Plan Schedule Data.");

            return JsonSuccess(data, "Load Plan Schedule Data");
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetPlanScheduleDetails(string plan)
        {
            var data = await _plan.GetPlanSchedulesByID(plan) ?? new PlanScheduleMode { };
            if (data == null) return JsonNotFound("No Plan Schedule Data.");

            return JsonSuccess(data, "Load Plan Schedule Data");
        }

        //------------------------------------------------------------------------------
        //--------------------- DISPLAY PAGE  ------------------------------------------
        //------------------------------------------------------------------------------
        public ActionResult HistoryTransaction() => View();
        public ActionResult ComponentsOut() => View();
        public ActionResult RegisterSupplier() => View();
        public ActionResult LogMainpage() =>  View();    



        public ActionResult PlanSchedule() => View();
        public ActionResult PDAView() => View();
        public ActionResult PlanScheduleDetails(string series)
        {
            try
            {
                //Redirect to Main series data if no data exist
                if (string.IsNullOrEmpty(series)) return RedirectToAction("PlanSchedule");
               
                // Decode Base64
                byte[] data = Convert.FromBase64String(series);
                string decodedSeries = System.Text.Encoding.UTF8.GetString(data);
                ViewBag.SeriesNo = decodedSeries;
                return View();
            }
            catch (Exception ex)
            {
                CustomLogger.LogError(ex);
                return RedirectToAction("Error");
            }
        }

        public ActionResult ManagePlanSchedule() => View();
        public ActionResult PlanDetails(string series)
        {
            try
            {
                //Redirect to Main series data if no data exist
                if (string.IsNullOrEmpty(series)) return RedirectToAction("ManagePlanSchedule");
                
                // Decode Base64
                byte[] data = Convert.FromBase64String(series);
                string decodedSeries = System.Text.Encoding.UTF8.GetString(data);
                ViewBag.SeriesNo = decodedSeries;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult ManageWarehouse() => View();
        public ActionResult FeederType() => View();
    }
}