using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [CompressResponse]
    public class DieMoldController : ExtendController
    {
        private readonly IDieMold _die;
        public DieMoldController(IDieMold die) => _die = die;

        // ===========================================================
        // ==================== MOLD DIE DATA  =======================
        // ===========================================================
        public async Task<ActionResult> GetMoldDieSummaryList(string ProcessID)
        {
            var data = await _die.GetMoldDieSummary(ProcessID);
            if (data == null || !data.Any())
                return JsonNotFound("No DieSummary  data not found");

            // Get the Max no of the data
            int maxNo = data.Max(x => x.No);
            int monitorCount = 0;
            int endLifeCount = 0;

            foreach (var list in data)
            {
                if (list.Remarks == "For Monitoring")
                    monitorCount++;
                else if (list.Remarks == "End of Life")
                    endLifeCount++;
            }

            int maxDieLife = maxNo - (monitorCount + endLifeCount);

            var summaryList = new List<FinalMoldDieSummary>
            {
                new FinalMoldDieSummary { Category = "Max Die life", MoldDie = maxDieLife },
                new FinalMoldDieSummary { Category = "For Monitoring", MoldDie = monitorCount },
                new FinalMoldDieSummary { Category = "End of life", MoldDie = endLifeCount }
            };


            var dataSets = new Dictionary<string, IEnumerable<object>>
            {
                    { "FinalSummary", data },
                    { "MoldDieSummary", summaryList }
            };


            return JsonMultipleData(dataSets);
        }
        public async Task<ActionResult> GetMoldDieMonthInputList(int Months, int Year, string ProcessID)
        {
            var data = await _die.GetMoldDieMonthInput(Months, Year, ProcessID) ?? new List<DieMoldTotalPartnum>();

            if (data == null || !data.Any())
                return JsonNotFound("No DieMonth input data found");

            return JsonSuccess(data);
        }
        public async Task<ActionResult> GetMoldDieToolingList()
        {
            var data = await _die.GetMoldToolingData() ?? new List<DieMoldToolingModel>();

            if (data == null || !data.Any())
                return JsonNotFound("No Mold Die Tooling data found");

            return JsonSuccess(data);
        }


        [HttpPost]
        public async Task<ActionResult> AddUpdateMoldDieMonitor(MoldInputModel add)
        {
            bool update = await _die.AddUpdateMoldie(add);
            if (!update) return JsonValidationError();
            return JsonCreated(add, "Add Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> AddUpdateMoldDieTooling(DieMoldToolingModel add)
        {
            bool update = await _die.AddMoldieTooling(add);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Add Data Successfully");
        }
        // ===========================================================
        // ==================== PRESS MOLD DIE DATA  ==================
        // ===========================================================
        public async Task<ActionResult> GetPressDieRegistryList()
        {
            var data = await _die.GetPressRegistryList() ?? new List<PressDieRegistry>();

            if (data == null || !data.Any())
                return JsonNotFound("No DieMonth input data found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetPressDieMainMonitoringList()
        {
            var data = await _die.GetPressMainMonitoring() ?? new List<PressMainMonitor>();
            if (data == null || !data.Any())
                return JsonNotFound("No Monitoring data found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetPressDieMonitoringList()
        {
            var data = await _die.GetPressMonitoring() ?? new List<PressDieMontoring>();

            if (data == null || !data.Any())
                return JsonNotFound("No Monitoring data found");

            return JsonSuccess(data);
        }
        public async Task<ActionResult> GetPressDieSummaryList()
        {
            var data = await _die.GetPressSummary() ?? new List<PressDieSummary>();

            if (data == null || !data.Any())
                return JsonNotFound("No Monitoring data found");

            return JsonSuccess(data);
        }
        public async Task<ActionResult> GetPressDieControlList()
        {
            var data = await _die.GetPressControl() ?? new List<PressDieControlModel>();

            if (data == null || !data.Any())
                return JsonNotFound("No Control data found");

            return JsonSuccess(data);
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdatePressDieMonitor(PressInputModel add)
        {
            //bool update = await _die.AddUpdatePressMonitoring(add);
            //var formdata = GlobalUtilities.GetMessageResponse(update, 1);
            await Task.Delay(100);
            return Json(add, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddPressRegistry(PressDieRegistry obj)
        {
            bool update = await _die.AddPressRegistry(obj);
            if (!update) return JsonValidationError();

            return JsonCreated(obj, "Add Registry Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> AddPressDieControl(PressDieControlData obj)
        {
           
            bool update = await _die.AddPressDieControl(obj);
            if (!update) return JsonValidationError();

            return JsonCreated(obj, "Updated Registry Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePressRegistry()
        {
            var obj = new PressDieRegistry
            {
                ToolNo = Request.Form["EditToolNo"],
                Type = Request.Form["EditType"],
                Model = Request.Form["EditModel"],
                Lines = Convert.ToInt32(Request.Form["EditLine"]),
                Status = Request.Form["EditStatus"],
                Operational = Convert.ToInt32(Request.Form["EditOpe"])
            };

            bool update = await _die.EditPressRegistry(obj);
            if (!update) return JsonValidationError();

            return JsonCreated(obj, "Updated Registry Successfully");
        }

        // GET: P1SA/DieMold
        public ActionResult DieMoldLife() =>  View();
        public ActionResult DiePressLife() => View();

    }
}