using PMACS_V2.Areas.P1SA.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.Mvc;
using PMACS_V2.Controllers;
using System.Threading.Tasks;
using PMACS_V2.Areas.P1SA.Models;
using ProgramPartListWeb.Helper;
using System.Diagnostics;

namespace PMACS_V2.Areas.MoldDie.Controllers
{
    public class MoldController : ExtendController
    {
        private readonly IDieMold _die;
        public static string strSender => ConfigurationManager.AppSettings["config:SMTPEmail"];
        public MoldController(IDieMold die) => _die = die;

        // ===========================================================
        // ==================== MOLD DIE DATA  =======================
        // ===========================================================
        public async Task<ActionResult> GlobalMoldDieSummaryList()
        {
            var data = await _die.GetMoldDieSummary() ?? new List<DieMoldSummaryProcess>();

            if (data == null || !data.Any())
                return JsonNotFound("No DieMonth input data found");

            return JsonSuccess(data);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieSummaryList(string ProcessID)
        {
            var data = await _die.GetMoldDieSummary(ProcessID) ?? new List<DieMoldSummaryProcess>();
            if (data == null || !data.Any())
                return JsonNotFound("No DieSummary  data not found");

            // Get the Max no of the data
            int maxNo = data.Max(x => x.No);
            int monitorCount = 0;
            int endLifeCount = 0;

            var groupNo = data.GroupBy(x => x.No)
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
        public async Task<ActionResult> GetMoldDieMonthInputList(int Months, int Year, string ProcessID)
        {
            var data = await _die.GetMoldDieMonthInput(Months, Year, ProcessID) ?? new List<DieMoldTotalPartnum>();

            if (data == null || !data.Any())
                return JsonNotFound("No DieMonth input data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieToolingList()
        {
            var data = await _die.GetMoldToolingData() ?? new List<DieMoldToolingModelDisplay>();

            if (data == null || !data.Any())
                return JsonNotFound("No Mold Die Tooling data found");

            return JsonSuccess(data);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieDailyList(int Months, int Year, string ProcessID, int Days)
        {

            var data = await _die.GetDailyMoldData(Months, Days, Year, ProcessID) ?? new List<DieMoldDaily>();

            if (data == null || !data.Any())
                return JsonNotFound("No Mold Die Daily data found");

            return JsonSuccess(data, "Add Data Succesfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDailyStatus(int RecordID, int Status)
        {
            bool update = await _die.ChangeStatsDaily(RecordID, Status);
            if (!update) return JsonValidationError();
            return JsonCreated(update, "Add Data Successfully");
        }


        public async Task<ActionResult> SearchMoldieDaily(string ProcessID, string SearchInput)
        {

            //Run tasks in parallel for better performance
            var datalistTask = _die.GetDailyMoldHistoryData(SearchInput, ProcessID);
            var detailsTask = _die.GetDailyLastMoldData(SearchInput, ProcessID);

            await Task.WhenAll(datalistTask, detailsTask);


            var tableList = datalistTask.Result ?? new List<DieMoldDaily>();
            var detailList = detailsTask.Result;

            // All in One Display
            var multiData = new Dictionary<string, object>
            {
                { "GetList", tableList },
                { "Details", detailList }
            };

            return JsonMultipleDataV2(multiData);
        }


        [JwtAuthorize]
        public async Task<ActionResult> GetMoldDieLastRecord(string partnum)
        {

            var data = await _die.GetDailyLastMoldData(partnum);

            if (data == null)
                return JsonNotFound("No Mold Die Daily data found");

            return JsonSuccess(data);
        }


        [HttpGet]
        public async Task<ActionResult> GetMoldieDropDownlist()
        {
            try
            {
                var proccessData = await _die.GetMoldProcess() ?? new List<DieMoldProcess>();
                var partsData = await _die.GetMoldPartDescription() ?? new List<DieProcessDescription>();
                var multidata = new Dictionary<string, IEnumerable<object>>
                {
                    { "Process", proccessData },
                    { "MoldParts", partsData }
                };

                return JsonMultipleData(multidata);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }

        }


        [HttpPost]
        public async Task<ActionResult> AddDailyMoldDieMonitor(DieMoldDailyInput add)
        {
            bool update = await _die.AddDailyMoldie(add);
            if (!update) return JsonValidationError();
            return JsonCreated(add, "Add Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDailyMoldDieMonitor(DieMoldDailyInput add)
        {
            bool update = await _die.UpdateDailyMoldie(add);
            if (!update) return JsonValidationError();
            return JsonCreated(add, "Update Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDailyCycleShot(int RecordID, int Cycleshot)
        {
            bool update = await _die.UpdateDailyLastCycle(RecordID, Cycleshot);
            if (!update) return JsonValidationError();
            return JsonCreated(update, "Update Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDailyMoldDieMonitor(int ID)
        {
            bool update = await _die.DeleteDailyMoldie(ID);
            if (!update) return JsonValidationError();
            return JsonCreated(ID, "Delete Data Successfully");
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

        [HttpPost]
        public async Task<ActionResult> UpdateMoldDieTooling(DieMoldToolingModel add)
        {
            bool update = await _die.UpdateMoldieTooling(add);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Update Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> AddMoldDieData(AddDieMoldingDataInput edit)
        {
            bool update = await _die.AddNewMoldDie(edit);
            if (!update) return JsonValidationError();
            return JsonCreated(edit, "Add Data Successfully");
        }


        [HttpPost]
        public async Task<ActionResult> UpdateMoldDieData(DieMoldingDataInput edit)
        {
            bool update = await _die.UpdateMoldDie(edit);
            if (!update) return JsonValidationError();
            return JsonCreated(edit, "Edit Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteMoldDieData(int RecordID, string PartNo)
        {
            bool update = await _die.DeleteMoldDie(RecordID, PartNo);
            if (!update) return JsonValidationError();
            return JsonCreated(update, "Delete Data Successfully");
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


        // GET: P1SA/DieMold
        public ActionResult DieMoldLife() => View();
        public ActionResult DiePressLife() => View();

        public ActionResult AddMonitoringInput() => View();
        // GET: P1SA/DieMold/DiePressMonitorDetails/:ID
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



        // GET: MoldDie/Mold  -- Default Page 
        public ActionResult Index() => View();
    }
}