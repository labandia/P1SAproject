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
        public async Task<ActionResult> GetMoldDieDailyList()
        {
            Debug.WriteLine("adadads");
            var data = await _mold.GetDailyMoldData(DateTime.Now, 6, "M002");

            foreach (var items in data)
            {
                Debug.WriteLine($@"PartNo {items.PartNo} - DateInput {items.DateInput}");
            }


            if (data == null || data.Count() == 0)
                return JsonNotFound("No Mold Die Daily data found");

            return JsonSuccess(data, "Add Data Succesfully");
        }




        [HttpGet]
        public async Task<JsonResult> CheckDailyMoldExist(string dieSerial, DateTime DateInput)
        {
            bool result = await _mold.CheckMoldDateInputExist(dieSerial, DateInput);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveDailyMoldDieMonitor(DieMoldDaily model)
        {
            bool success = await _mold.AddDailyInput(model);

            if (!success)
                return JsonValidationError();

            return JsonCreated(model, "Add Data Successfully");
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