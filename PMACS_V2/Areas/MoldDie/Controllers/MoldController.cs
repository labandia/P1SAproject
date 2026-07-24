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
using PMACS_V2.Helper;

namespace PMACS_V2.Areas.MoldDie.Controllers
{
    public class MoldController : ExtendController
    {
        private readonly IMoldDaily _mold;
        public MoldController(IMoldDaily mold) => _mold = mold;


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