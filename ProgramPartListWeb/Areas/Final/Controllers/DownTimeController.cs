using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Areas.Final.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Final.Controllers
{
    public class DownTimeController : ExtendController
    {
        private readonly IDownTime _downTimeService;    

        public DownTimeController() => _downTimeService = new Services.DownTimeServices();  

        [HttpGet]
        public async Task<ActionResult> GetDownTimeReportList(
            string searchtext, string Linename)
        {
            try
            {
                var res = await _downTimeService.GetDowntimeMonitor(searchtext, Linename);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }



        [HttpGet]
        public async Task<ActionResult> GetDownTimeTypeList()
        {
            try
            {
                var res = await _downTimeService.GetDownTimeType();
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetDownTimeList(string finalshopOrder)
        {

            try
            {
                var res = await _downTimeService.GetDowntimeMonitor(finalshopOrder);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDownTimeMonitoring(DownTimeModel model)
        {
            try
            {
                var res = await _downTimeService.AddGetTimeMonitor(model);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditDownTimeMonitoring(int ID)
        {
            try
            {
                var res = await _downTimeService.EndTimeMonitor(ID);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }


    }
}