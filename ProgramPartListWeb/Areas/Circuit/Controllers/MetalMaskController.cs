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

        [HttpGet]
        public async Task<ActionResult> GetMetalMaskInformation(int Stats)
        {
            var data = await _trans.GetMetalMaskTransaction("", "", 0, 1, 0, 0,  0);

            var result = data.Select(x => new
            {
                x.RecordID,
                x.DateInput,
                x.Shift,
                x.SMTLine,
                x.Partnumber,
                x.AREA,
                x.Blocks,
                SMT_start = x.SMT_start.ToString(@"hh\:mm"),
                SMT_end = x.SMT_end.ToString(@"hh\:mm"),
                TotalTimeHHMM = $"{x.TotalTime / 60:D2}:{x.TotalTime % 60:D2}",
                x.TotalTime,
                x.TotalPrintBoard,
                x.SMT_Operator,
                x.CleanDate,
                x.Pattern,
                x.Frame,
                x.ReadOne,
                x.ReadTwo,
                x.ReadThree,
                x.ReadFour,
                x.Result,
                x.Remarks,
                x.PIC
            });

            if (result == null || !result.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonSuccess(result);
        }


        [HttpPost]
        public async Task<ActionResult> SubmitMetalMaskInfo(MetalMaskTransaction metal)
        {
            metal.Shift = GlobalUtilities.GetTheShiftSchedule() == "DS" ? false : true;
            metal.Status = 1;

            bool result = await _trans.AddMetalMastTransaction(metal);
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(metal, "INSERT new Parts Successfully");
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
        public async Task<ActionResult> FinalSubmitSMTLineinfo(MetalMaskTransaction metal)
        {
            bool result = await _trans.SMTsubmitTransaction(metal);
            //if (data == null || !data.Any()) return JsonNotFound("No Masterlist Data.");
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> StartSMTOperation(int ID)
        {
            bool result = await _trans.StartOperation(ID);
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EndSMTOperation(int ID)
        {
            bool result = await _trans.EndOperation(ID);
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }


        [HttpPost]
        public async Task<ActionResult> SubmitTensionAndCleaning(MetalMaskTransaction metal)
        {
            bool result = await _trans.TensionsubmitTransaction(metal);
            //if (data == null || !data.Any()) return JsonNotFound("No Masterlist Data.");
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(result, "INSERT new Parts Successfully");
        }


        // GET: Circuit/MetalMask
        public ActionResult Index() => View();
        // GET: Circuit/MetalMask/SMTLine
        public ActionResult SMTLine() => View();
        // GET: Circuit/MetalMask/Cleaning
        public ActionResult Cleaning() => View();
    }
}