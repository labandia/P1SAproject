using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using ProgramPartListWeb.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using System.Linq;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using Microsoft.AspNet.SignalR;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [CompressResponse]
    public class CapacityController : ExtendController
    {
        private readonly ICapacity _cap;

        public CapacityController(ICapacity cap) => _cap = cap;

        // ===========================================================
        // ==================== P1SA Summary  ========================
        // ===========================================================

        public async Task<ActionResult> GetPisaSummaryList()
        {
            try
            {
                var data = await _cap.GetP1SAsummary();
                if (data == null || !data.Any())
                    return JsonNotFound("No P1SA Summary data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetCapacitySummaryList(string month, int capid)
        {
            try
            {
                var data = await _cap.GetCapacitySummary(month, capid);
                if (data == null || !data.Any())
                    return JsonNotFound("No Capacity Summary data found");
                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetForecastTotalData(string month)
        {
            try
            {
                int data = await _cap.GetForecastTotal(month);
                if (data == 0)
                    return JsonNotFound("No Total Forecast found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetGroupCapacityList()
        {
            try
            {
                var getdata = await _cap.GetGroupCapacity() ?? new List<SelectionGroup>();
                //var data = CacheHelper.GetOrSet("ProcessGroup", () => getdata, 15);
                if (getdata == null || !getdata.Any())
                    return JsonNotFound("No Group Capacity data found");

                return JsonSuccess(getdata);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetModelbaseComboBox(int CapID)
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("DisplayModelbase", () => _cap.GetModelBaseComboxList(CapID), 15);
                if (data == null || !data.Any())
                    return JsonNotFound("No Model Base Combobox found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetModelbaseComboBoxDoesntExistList(int CapID)
        {
            try
            {
                var getdata = await _cap.GetModelBaseDoesntExist(CapID);
                //var data = CacheHelper.GetOrSet("NoExistModelbase", () => getdata, 15);
                if (getdata == null || !getdata.Any())
                    return JsonNotFound("No Model Base Combobox found");

                return JsonSuccess(getdata);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetModelbaseForAddForm(int CapID)
        {
            try
            {
                var getdata = await _cap.GetModelBaseComboxList(CapID);
                var getforest = await _cap.GetForecastChart();

                var filtercombo = getdata.Where(f => !getforest.Any(fa => fa.Model_name == f)).ToList();
                //var filtercombo = getforest.ExceptBy(getdata.Select(p => p), p => p.Model_name)
                //                .ToList();


                //var data = CacheHelper.GetOrSet("DisplayModelbase", () => getdata, 15);
                if (filtercombo == null || !filtercombo.Any())
                    return JsonNotFound("No Add form Model Base Combobox found");

                return JsonSuccess(filtercombo);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        // ===========================================================
        // ==================== CAPACITY PER SECTION =================
        // ===========================================================
        public async Task<ActionResult> GetForecastModelList(string year)
        {
            try
            {
                var data = await _cap.GetForecast(year);
                if (data == null || !data.Any())
                    return JsonNotFound("No ForecastModel data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetForecastChartList()
        {
            try
            {
                var data = await _cap.GetForecastChart();
                if (data == null || !data.Any())
                    return JsonNotFound("No Forecast Chart data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }



        public async Task<ActionResult> GetMoldingModelData(int CapID, string Month)
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("Molding", () => _cap.GetMoldingModels(Month, CapID), 15);
                if (data == null || !data.Any())
                    return JsonNotFound("No Molding Model data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetRotorModelData(int CapID, string Month)
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("Rotor", () => _cap.GetRotorModels(Month, CapID), 15);
                if (data == null || !data.Any())
                {
                    return JsonNotFound("No Rotor Model data found");
                }
                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetWindingModelData(int CapID, string Month)
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("Winding", () => _cap.GetWindingModels(Month, CapID), 10);
                if (data == null || !data.Any())
                    return JsonNotFound("No Winding Model data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetPressModelData(int CapID, string Month)
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("Press", () => _cap.GetPressModels(Month, CapID), 15);
                if (data == null || !data.Any())
                    return JsonNotFound("No Press Model data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetCircuitModelData(int CapID, string Month)
        {
            var data = await CacheHelper.GetOrSetAsync("Circuit", () => _cap.GetCircuitModels(Month, CapID), 15);
            if (data == null || !data.Any())
            {
                return JsonNotFound("No Rotor Model data found");
            }
            return JsonSuccess(data);
        }


        // GET: Capacity/Winding/capid
        public ActionResult Winding(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 1:
                    ViewBag.processname = "Winding";
                    break;
                case 2:
                    ViewBag.processname = "Pinn";
                    break;
                case 3:
                    ViewBag.processname = "Soldering";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }
        // GET: Capacity/Rotor/capid
        public ActionResult Rotor(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 4:
                    ViewBag.processname = "Balancing";
                    break;
                case 5:
                    ViewBag.processname = "Rotor Caulking";
                    break;
                case 6:
                    ViewBag.processname = "Impeller Assy";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }
        // GET: Capacity/Molding/capi
        public ActionResult Molding(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 7:
                    ViewBag.processname = "Mold Frame";
                    break;
                case 8:
                    ViewBag.processname = "Mold Impeller";
                    break;
                case 9:
                    ViewBag.processname = "Mold Insulator";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }
            
            return View();
        }
        // GET: Capacity/Press/capid
        public ActionResult Press(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 10:
                    ViewBag.processname = "Stator";
                    break;
                case 11:
                    ViewBag.processname = "Rotor Cover";
                    break;
                case 12:
                    ViewBag.processname = "Aluminum Frame";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }
        // GET: Capacity/Press/capid
        public ActionResult Circuit(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 13:
                    ViewBag.processname = "SMT";
                    break;
                case 14:
                    ViewBag.processname = "AOI";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }




        // DYNAMIC USE OF HTML CODE USING PARTIAL VIEW
        [HttpPost]
        public ActionResult CapacitySummaryHeader(List<CapacityPartialViewModel> cap)
        {
            return PartialView("_CapacitySummary", cap);
        }
    }
}