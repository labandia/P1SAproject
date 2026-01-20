using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Circuit.Controllers
{
    public class MetalMaskController : ExtendController
    {
        private readonly IMaskMasterlist _mas;
        private readonly IMetalMast_Transaction _trans;


        public MetalMaskController(IMaskMasterlist mas, IMetalMast_Transaction trans)
        {
            _mas = mas;
            _trans = trans;
        }


        [HttpGet]
        public async Task<ActionResult> SearchMetalMaskPartnum(string Partnumber)
        {
            var data = await _mas.SearchMetalMaskData(Partnumber);
            if (data == null || !data.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(data);
        }


        [HttpPost]
        public async Task<ActionResult> SubmitMetalMaskInfo(MetalMaskTransaction metal)
        {
            metal.Shift = GlobalUtilities.GetTheShiftSchedule() == "DS" ? false : true;
            metal.Status = 1;

            bool result = await _trans.AddMetalMastTransaction(metal);
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }



        [HttpPost]
        public async Task<ActionResult> SubmitSMTLineinfo(MetalMaskTransaction metal)
        {
            bool result = await _trans.AddMetalMastTransaction(metal);
            //if (data == null || !data.Any()) return JsonNotFound("No Masterlist Data.");
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }


        [HttpPost]
        public async Task<ActionResult> SubmitTensionAndCleaning(MetalMaskTransaction metal)
        {
            bool result = await _trans.AddMetalMastTransaction(metal);
            //if (data == null || !data.Any()) return JsonNotFound("No Masterlist Data.");
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }


        // GET: Circuit/MetalMask
        public ActionResult Index()
        {
            return View();
        }
    }
}