using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ProgramPartListWeb.Areas.Production1.Model.AttendanceModel;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class AttendanceController : ExtendController
    {
        private readonly INCRDashboardRepository _manu;

        public AttendanceController(INCRDashboardRepository manu) => _manu = manu;


        [HttpGet]
        public async Task<ActionResult> GetAttendanceBreakDown(DateTime? filterDate, int isfilter)
        {

            var res = await _manu.AttendanceBreakDown(filterDate, isfilter);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }
        [HttpGet]
        public async Task<ActionResult> GetAttendanceSummary(DateTime? filterDate, int isfilter)
        {

            var (res, today) = await _manu.GetAttendanceSummary(filterDate, isfilter);
            if (res == null)
                return JsonNotFound("No Active Lines found");

            var result = new
            {
                Listdata = res,
                today = today,
            };

            return JsonSuccess(result);
        }


        [HttpGet]
        public async Task<ActionResult> GetAttendanceTrends(DateTime? filterDate)
        {

            var data = await _manu.GetLatestTrendsAttendance(filterDate);


            var chartData = data.Select(d => new {
                DateToday = d.DateToday.ToString("yyyy-MM-dd"),  // plain ISO string, no /Date()/ wrapper
                d.AttendRate,
                d.AbsentRate
            });

            if (chartData == null)
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(chartData);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAttendanceBreakDown(UpdateAttendanceBreakDownRequest model)
        {
            if (model == null || model.DashID <= 0)
                return JsonNotFound("Invalid request");

            var updated = await _manu.UpdateAttandanceSummary(model);
            if (!updated)
                return JsonNotFound("Update failed for DashID " + model.DashID);

            return JsonSuccess(updated);
        }



        // GET: Production1/Attendance
        public ActionResult Dashboard() => View();

    }
}