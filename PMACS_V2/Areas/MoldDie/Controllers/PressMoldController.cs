using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.MoldDie.Models;
using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Controllers;
using ProgramPartListWeb.Helper;

namespace PMACS_V2.Areas.MoldDie.Controllers
{
    public class PressMoldController : ExtendController
    {
        private readonly IDieMold _die;

        public PressMoldController(IDieMold die)
        {
            _die = die;
        }

        // ===========================================================
        // ==================== PRESS MOLD DIE DATA  ==================
        // ===========================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetPressDieRegistryList()
        {
            var data = await _die.GetPressRegistryList() ?? new List<PressDieRegistry>();
            if (data == null || !data.Any())
                return JsonNotFound("No DieMonth input data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetPressDieMainMonitoringList()
        {
            var data = await _die.GetPressMainMonitoring() ?? new List<PressMainMonitor>();
            if (data == null || !data.Any())
                return JsonNotFound("No Monitoring data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetPressDieMonitoringList(string ToolNo)
        {
            var data = await _die.GetPressMonitoring() ?? new List<PressDieMontoring>();
            var filterdata = data.Where(res => res.ToolNo == ToolNo);
            if (filterdata == null || !filterdata.Any())
                return JsonNotFound("No Monitoring data found");

            return JsonSuccess(filterdata);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetPressDieSummaryList()
        {
            var data = await _die.GetPressSummary() ?? new List<PressDieSummary>();

            if (data == null || !data.Any())
                return JsonNotFound("No Monitoring data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
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
            bool update = await _die.AddUpdatePressMonitoring(add);
            if (!update) return JsonValidationError();
            return JsonCreated(add, "Update Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> AddMoldiePressMonitor(PressMonitorInput add)
        {
            bool update = await _die.AddPressMonitorData(add);
            if (!update) return JsonValidationError();

            var data = await _die.GetPressMonitoring() ?? new List<PressDieMontoring>();
            int newTotal = data
                            .Where(res => res.ToolNo == add.ToolNo)
                            .Sum(res => res.PressStamp);


            return JsonCreated(newTotal, "Insert Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> EndofLifeMonitor(string ToolNo)
        {
            bool update = await _die.UpdateEndofLifeMonitorData(ToolNo);
            if (!update) return JsonValidationError();
            return JsonCreated("End of Life Successfully");
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
        public async Task<ActionResult> UpdatePressRegistry(PressDieRegistryEdit edit)
        {
            Debug.WriteLine($@"Tool No : {edit.EditToolNo}");
            var obj = new PressDieRegistry
            {
                ToolNo = edit.EditToolNo,
                Type = edit.EditType,
                Model = edit.EditModel,
                Lines = edit.EditLine,
                Status = edit.EditStatus,
                Operational = edit.EditOpe
            };

            bool update = await _die.EditPressRegistry(obj);
            if (!update) return JsonValidationError();

            return JsonCreated(obj, "Updated Registry Successfully");
        }



        public async Task<ActionResult> DiePressMonitorDetails(int ID)
        {
            var data = await _die.GetPressMainMonitoring() ?? new List<PressMainMonitor>();
            var filterData = data.SingleOrDefault(res => res.MonitorID == ID);
            if (filterData == null)
            {
                return HttpNotFound("Monitor not found.");
            }

            return View(filterData);
        }

        public ActionResult DiePressLife() => View();



        // GET: MoldDie/PressMold
        public ActionResult Index()
        {
            return View();
        }


    }
}