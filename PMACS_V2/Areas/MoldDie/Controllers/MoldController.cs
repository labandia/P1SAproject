using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Controllers;
using static PMACS_V2.Areas.P1SA.Models.DieMoldMonitoringModel;

namespace PMACS_V2.Areas.MoldDie.Controllers
{
    public class MoldController : ExtendController
    {
        private readonly IMoldDaily _mold;
        private readonly IDieMasterList _master;

        public MoldController(IMoldDaily mold, IDieMasterList master)
        {
            _mold = mold;
            _master = master;
        }
        // ===========================================================
        // MOLD DIE SUMMARY LIST
        // ============================================================
        public async Task<ActionResult> GetMoldDieSummaryList(int Months, int Year, string ProcessID)
        {

            var data = await _mold.GetMoldDieSummary(Months, Year, ProcessID) ?? new List<DieMoldMonitoringModel>();
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



        // ===========================================================
        // MOLD DIE MASTER LIST
        // ============================================================
        [HttpGet]
        public async Task<ActionResult> GetMoldDieMasterList(
          string search = "",
          int page = 1,
          int pageSize = 50)
        {
            Debug.WriteLine("GETMOLD DIE");
            var data = await _master.GetModelDieMasterList(search, page, pageSize);

            if (data == null || !data.Any())
                return JsonNotFound("No Mold Die Tooling data found");

            return JsonSuccess(data);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMoldieMasterlist(MoldieMasterModel model)
        {
            try
            {

                bool result = (model.MoldID == 0) ? await _master.AddMoldieMasterList(model) : await _master.EditMoldieMasterList(model);
                if (!result) return JsonPostError("Insert failed.", 500);


                return JsonCreated(result, "Update Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMoldMasterlist(MoldieMasterModel add)
        {
            bool update = await _master.EditMoldieMasterList(add);
            if (!update) return JsonValidationError();

            return JsonCreated(add, "Update Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMoldMasterlist(string DieSerial)
        {
            //bool update = await _master.EditMoldieMasterList(add);
            //if (!update) return JsonValidationError();

            return JsonCreated(true, "Update Data Successfully");
        }

        // ===========================================================
        // MOLD DIE DAILY FUNCTIONALITY
        // ============================================================
        [HttpGet]
        public async Task<ActionResult> GetMoldDieDailyList(DateTime dateInput, 
            int monthInt = 0,  string process = "")
        {
            var data = await _mold.GetDailyMoldData(dateInput, monthInt, process);

          
            if (data == null || data.Count() == 0)
                return JsonNotFound("No Mold Die Daily data found");

            return JsonSuccess(data, "Get Mold Die Succesfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetSearchMoldieList(string DieSerial, string process = "")
        {
            var data = await _mold.GetThePartnoList(DieSerial, process);
            var finalobj = new 
            {
                Details = data.details,
                Listdata = data.getlist,    
            };
            return JsonSuccess(finalobj, "Get Mold Die Succesfully");
        }

        [HttpGet]
        public async Task<JsonResult> CheckDialyMoldExist(string dieSerial, DateTime DateInput)
        {
            bool result = await _mold.CheckMoldDateInputExist(dieSerial, DateInput);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveDailyMoldDieMonitor(DieMoldDaily model)
        {
            bool success = await _mold.AddDailyInput(model);
            Debug.WriteLine("Result : " + success);
            if (!success)
                return JsonValidationError();

            return JsonCreated(success, "Add Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> UpdateDailyMoldRecord(DieMoldDaily model)
        {
            bool success = await _mold.EditDailyInput(model);
            Debug.WriteLine("Result : " + success);
            if (!success)
                return JsonValidationError();

            return JsonCreated(model, "Add Data Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteDailyMoldRecord(int RecordID, string DieSerial, DateTime DateInput)
        {
            bool success = await _mold.DeleteDailyInput(RecordID, DieSerial, DateInput);
            if (!success)
                return JsonValidationError();
            Debug.WriteLine($"RecordID : {RecordID}");
            Debug.WriteLine($"DieSerial : {DieSerial}");
            Debug.WriteLine($"DateInput : {DateInput:yyyy-MM-dd HH:mm:ss.fff}");

            return JsonCreated(success, "Delete Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> ChangeDieMoldStatusData(int RecordID, int Status)
        {
            bool success = await _mold.ChangeStatusMoldie(RecordID, Status);
            if (!success)
                return JsonValidationError();

            return JsonCreated(success, "Delete Data Successfully");
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


    }
}