using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Areas.Final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Areas.Hydroponics.Interface;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class FinalAssemblyController : ExtendController
    {
        private readonly INCRDashboardRepository _manu;


        public FinalAssemblyController(INCRDashboardRepository manu)
        {
            _manu = manu;
        }
        //======================================================
        //============== DASHBOARD  ===========
        //=====================================================

        [HttpGet]
        public async Task<ActionResult> GetListof4ManFactor()
        {
            //await _manu.AutoUpdateShopOrderLine();


            var res = await _manu.GetFourMSummary();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetListofGroupProcess()
        {
            //await _manu.AutoUpdateShopOrderLine();


            var res = await _manu.GetGroupSummary();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }
        [HttpGet]
        public async Task<ActionResult> GetBestLineList()
        {
            //await _manu.AutoUpdateShopOrderLine();


            var res = await _manu.GetBestLines();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }
        //======================================================
        //============== MANAGE DATA  ===========
        //====================================================
        [HttpGet]
        public async Task<ActionResult> GetRegistrationList(string search, int month = 0)
        {
            // 0 (or any value outside 1-12) falls back to the current month
            if (month < 1 || month > 12)
                month = DateTime.Today.Month;

            //await _manu.AutoUpdateShopOrderLine();
            var res = await _manu.GetRegistrationData(search, month);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }

        [HttpPost]
        public async Task<ActionResult> SaveRegistration(RegistrationFinalModel model)
        {
            try
            {
      
                bool result = (model.NCRID == 0) ? await _manu.AddRegistrationData(model) : await _manu.EditRegistrationData(model);
                if (!result) return JsonPostError("Insert failed.", 500);


                return JsonCreated(result, "Update Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }

       


        // GET: Production1/FinalAssembly
        public ActionResult Dashboard()
        {
            return View();
        }


        // GET: Production1/FinalAssembly
        public ActionResult ManagementData()
        {
            return View();
        }
    }
}