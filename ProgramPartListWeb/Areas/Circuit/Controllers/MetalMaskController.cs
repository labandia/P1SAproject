using OfficeOpenXml;
using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            
            var data = await _trans.GetMetalMaskTransaction("", "", 0, Stats, 0, 0,  0);

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

        [HttpPost]
        public async Task<ActionResult> ExportMetalMask(List<int> recordIds)
        {
            if (recordIds == null || !recordIds.Any())
                return new HttpStatusCodeResult(400, "No record IDs");

            var getData = new List<MetalMaskTransaction>();

            foreach (var id in recordIds)
            {
                var details = await _trans.GetMetalMaskTransacDetails(id);
                if (details != null)
                    getData.Add(details);
            }
            Debug.WriteLine("Done Getting Data ... ");

            if (!getData.Any())
                return new HttpStatusCodeResult(204);

            //string templatePath = Server.MapPath("~/Content/Uploads/PCFY-81013Form1L.xlsx");
            string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");

            if (!System.IO.File.Exists(templatePath))
                throw new Exception("Template not found: " + templatePath);

            Debug.WriteLine("Get the Excel Template  ");

            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                Debug.WriteLine("Inside the Files");
                Debug.WriteLine("Count:  " + getData.Count());
                //var ws = package.Workbook.Worksheets["TensionMonitor"];
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    Debug.WriteLine("Sheet name: [" + sheet.Name + "]");
                }

                int row = 6;

                foreach (var item in getData)
                {
                    Debug.WriteLine($@"Partnumber : {item.Partnumber} - Shift {item.Shift}");

                    //ws.Cells[row, 1].Value = item.RecordID;
                    ////ws.Cells[row, 2].Value = item.DateInput.ToString("yyyy-MM-dd");
                    //ws.Cells[row, 3].Value = item.Shift;
                    //ws.Cells[row, 4].Value = item.SMTLine;
                    //ws.Cells[row, 5].Value = item.Partnumber;
                    //ws.Cells[row, 6].Value = item.AREA;
                    //ws.Cells[row, 7].Value = item.Blocks;
                    //ws.Cells[row, 8].Value = item.SMT_start.ToString();
                    //ws.Cells[row, 9].Value = item.SMT_end.ToString();
                    //ws.Cells[row, 10].Value = item.TotalTime;
                    //ws.Cells[row, 11].Value = item.TotalPrintBoard;
                    //ws.Cells[row, 12].Value = item.SMT_Operator;
                    //ws.Cells[row, 13].Value = item.CleanDate.ToString("yyyy-MM-dd");
                    //ws.Cells[row, 14].Value = item.Pattern;
                    //ws.Cells[row, 15].Value = item.Frame;
                    //ws.Cells[row, 16].Value = item.RevisionNo;
                    //ws.Cells[row, 17].Value = item.ReadOne;
                    //ws.Cells[row, 18].Value = item.ReadTwo;
                    //ws.Cells[row, 19].Value = item.ReadThree;
                    //ws.Cells[row, 20].Value = item.ReadFour;
                    //ws.Cells[row, 21].Value = item.Result;
                    //ws.Cells[row, 22].Value = item.Remarks;
                    row++;
                }

                return File(
                    package.GetAsByteArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"MetalMask_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                );
            }

        }


        // GET: Circuit/MetalMask
        public ActionResult Index() => View();
        // GET: Circuit/MetalMask/SMTLine
        public ActionResult SMTLine() => View();
        // GET: Circuit/MetalMask/Cleaning
        public ActionResult Cleaning() => View();
        // GET: Circuit/MetalMask/History
        public ActionResult History() => View();
    }
}