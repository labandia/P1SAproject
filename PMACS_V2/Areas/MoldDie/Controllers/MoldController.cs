using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Controllers;
using PMACS_V2.Models;
using ProgramPartListWeb.Helper;

namespace PMACS_V2.Areas.MoldDie.Controllers
{
    public class MoldController : ExtendController
    {
        private readonly IMoldDieModel _dieV2;
        public static string strSender => ConfigurationManager.AppSettings["config:SMTPEmail"];
        public MoldController(IDieMold die, IMoldDieModel dieV2)
        {
            _dieV2 = dieV2;
        }
        // ===========================================================
        // FOR DROPDOWN AND FILTERS
        // ===========================================================
        public async Task<JsonResult> GetYearMoldDie()
        {
            List<string> yearList = new List<string>();
            yearList = await _dieV2.GetMoldDieYear();

            return Json(yearList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetPartDescription()
        {
            List<string> yearList = new List<string>();
            yearList = await _dieV2.GetMoldDieDescription();

            return Json(yearList, JsonRequestBehavior.AllowGet);
        }

        // ===========================================================
        // MOLD DIE SUMMARY AND MONTORING DATA
        // ===========================================================

        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieSummaryList(int Months, int Year,  string ProcessID)
        {
           
            var data = await _dieV2.GetMoldDieSummary(Months, Year, ProcessID) ?? new List<DieMoldMonitoringModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No DieSummary  data not found");

            // Get the Max no of the data
            int maxNo = data.Count;
            int monitorCount = 0;
            int endLifeCount = 0;

            var groupNo = data.GroupBy(x => x.DieSerial)
                .Select(x => new {
                    Remarks = x.First().Remarks
                });



            foreach (var list in groupNo)
            {
                if (list.Remarks == "For Monitoring")
                    monitorCount++;
                else if (list.Remarks == "End of Life")
                    endLifeCount++;
            }

            int maxDieLife = Math.Abs(maxNo - (monitorCount + endLifeCount));

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
        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieMonthlyList(int Months, int Year, string ProcessID)
        {
            var data = await _dieV2.GetMoldDieMonthly(Months, Year, ProcessID) ?? new List<DieMoldMonitoringModel>();

            if (data == null || !data.Any())
                return JsonNotFound("No Mold Die Daily data found");

            return JsonSuccess(data, "Mold Die Month Retrieve");
        }
        // ===========================================================
        // MOLD DIE DAILY FUNCTIONALITY
        // ============================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieDailyList(int Months, int Year, string ProcessID, int Days)
        {
            var data = await _dieV2.GetDailyMoldData(Months, Days, Year, ProcessID) ?? new List<DieMoldMonitoringModel>();

            if (data == null || !data.Any())
                return JsonNotFound("No Mold Die Daily data found");

            return JsonSuccess(data, "Add Data Succesfully");
        }

        [HttpGet]
        public async Task<ActionResult> SearchMoldieDaily(string ProcessID, string SearchInput)
        {
        
            //Run tasks in parallel for better performance
            var datalistTask = await _dieV2.GetDailyMoldHistoryData(SearchInput, ProcessID);
            var detailsTask = await _dieV2.GetMoldieMasterlistParts(SearchInput);
            // All in One Display
            var multiData = new Dictionary<string, object>
            {
                { "GetList", datalistTask },
                { "Details", detailsTask }
            };

            return JsonMultipleDataV2(multiData);
        }

        [HttpGet]
        public async Task<ActionResult> SearchByDieSerialMoldie(string ProcessID, string DieInput)
        {

            ////Run tasks in parallel for better performance
            var datalistTask = await _dieV2.GetDailyMoldHistoryByDieSerialData(DieInput, ProcessID);
            var detailsTask = await _dieV2.GetMoldieDieSerialParts(DieInput);
            //// All in One Display
            var multiData = new Dictionary<string, object>
            {
                { "GetList", datalistTask },
                { "Details", detailsTask }
            };

            return JsonMultipleDataV2(multiData);

            //return JsonSuccess(datalistTask, "Mold Die Month Retrieve");
        }


        [HttpGet]
        public async Task<JsonResult> CheckDialyMoldExist(string srcval, string DateInput)
        {
            bool result = await _dieV2.CheckMoldieExist(srcval, DateInput);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddDailyMoldDieMonitor(DieMoldMonitoringModel add)
        {
            bool update = await _dieV2.AddUpdateDailyMoldie(add, 0);
            if (!update) return JsonValidationError();
            return JsonCreated(add, "Add Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDailyMoldDieMonitor(DieMoldMonitoringModel add)
        {
            bool update = await _dieV2.AddUpdateDailyMoldie(add, 1);
            if (!update) return JsonValidationError();
            return JsonCreated(add, "Update Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteDailyMoldDieMonitor(int ID)
        {
            //bool update = await _die.DeleteDailyMoldie(ID);
            //if (!update) return JsonValidationError();
            return JsonCreated(ID, "Delete Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> UpdateDailyStatus(int RecordID, int Status)
        {
            bool update = await _dieV2.ChangeStatsDaily(RecordID, Status);
            if (!update) return JsonValidationError();
            return JsonCreated(update, "Add Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> UpdateDailyCycleShot(int RecordID, int Cycleshot)
        {
            bool update = await _dieV2.UpdateDailyLastCycle(RecordID, Cycleshot);
            if (!update) return JsonValidationError();
            return JsonCreated(update, "Update Data Successfully");
        }
        // ===========================================================
        // MOLD DIE TOOLING FUNCTIONALITY
        // ===========================================================

        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieToolingList(
            string search = "",
            string filter = "",
            int page = 1,
            int pageSize = 50)
        {
            var data = await _dieV2.GetMoldToolingData(search, filter, page, pageSize);

            if (data == null && data.Items.Any()) return JsonNotFound("No Mold Die Tooling data found");

            return JsonSuccess(data);
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateMoldDieTooling(DieMoldToolingModelDisplay add)
        {
            bool update = await _dieV2.AddUpdateMoldieTooling(add, 0);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Add Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMoldDieTooling(DieMoldToolingModelDisplay add)
        {
            bool update = await _dieV2.AddUpdateMoldieTooling(add, 1);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Update Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMoldDieTooling(int ID)
        {
            bool update = await _dieV2.DeleteMoldieTooling(ID);
            if (!update) return JsonValidationError();

            return JsonCreated(update, "Delete Data Successfully");
        }
        // ===========================================================
        // MOLD DIE MASTERLIST 
        // ===========================================================
        [HttpGet]
        public async Task<JsonResult> CheckMasterlistMoldExist(string PartNo)
        {
            bool result = await _dieV2.CheckMoldieMasterlist(PartNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieMasterList(
            string search = "",
            string filter = "",
            int page = 1,
            int pageSize = 50)
        {
            var data = await _dieV2.GetMoldieMasterlist(search, filter,  page, pageSize);

            if (data == null && data.Items.Any()) return JsonNotFound("No Mold Die Tooling data found");

            return JsonSuccess(data);
        }
        [HttpPost]
        public async Task<ActionResult> AddMoldDieMasterlist(DieMoldMonitoringModel add)
        {
            bool update = await _dieV2.AddUpdateMoldieMasterlist(add, 0);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Add Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMoldMasterlist(DieMoldMonitoringModel add)
        {
            bool update = await _dieV2.AddUpdateMoldieMasterlist(add, 1);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Update Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMoldMasterlist(string partno)
        {
            bool update = await _dieV2.DeleteMoldieMasterlist(partno);
            if (!update) return JsonValidationError();

            return JsonCreated(update, "Update Data Successfully");
        }


        // GET: P1SA/DieMold
        public ActionResult DieMoldLife() => View();
        public ActionResult DieMoldDaily() => View();
        public ActionResult DieMoldSummary() => View();
        public ActionResult DieMoldTooling() => View();
        public ActionResult DieMoldMasterlist() => View();

        public ActionResult AddMonitoringInput() => View();
        // GET: P1SA/DieMold/DiePressMonitorDetails/:ID
       


        // GET: MoldDie/Mold  -- Default Page 
        public ActionResult Index() => View();



        public async Task<ActionResult> LoadMoldDieToolingView()
        {
            //var data = await _dieV2.GetMoldToolingData() ?? new List<DieMoldToolingModelDisplay>();

            //var model = new RequestData<DieMoldToolingModelDisplay>
            //{
            //    Items = data,
            //    Page = 1,
            //    TotalPages = 1,
            //};

            return PartialView("_ToolingTable", null);
        }
    }
}