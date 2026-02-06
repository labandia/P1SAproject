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
using ClosedXML.Excel;

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

        //-----------------------------------------------------------------------------------------
        //---------------------------- MASTERLIST METHODS  ----------------------------------------
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetMastetlistData(
            string search,
            int ModelType)
        {
            var data = await _mas.GetMetalMaskMasterlist(search, 0, ModelType, 0, 0);
            if (data == null || !data.Items.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(data);
        }

        [HttpGet]
        public async Task<ActionResult> SearchMetalMaskPartnum(string Partnumber)
        {
            var data = await _mas.SearchMetalMaskData(Partnumber);
            if (data == null || !data.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(data);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertMasterlist(MetalMaskModel model)
        {
            bool result = await _mas.AddMasterlist(model);
            if (!result) return JsonPostError("failed to add a new Masterlist.", 500);
            return JsonCreated(result, "Add Partnum Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult>EditMasterlist(MetalMaskModel model)
        {
            bool result = await _mas.EditMasterlist(model);
            if (!result) return JsonPostError("failed to add a new Masterlist.", 500);
            return JsonCreated(result, "Add Partnum Data Successfully");
        }


        //-----------------------------------------------------------------------------------------
        //---------------------------- TRANSACTION OR SUMMARY METHODS  ----------------------------
        //-----------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<ActionResult> GetMetalMaskInformation(
            string search, 
            int SMTLine,  
            int Stats, 
            int ModelType)
        {
            
            var data = await _trans.GetMetalMaskTransaction(search, "", SMTLine, Stats, ModelType, 0,  0);

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


        [HttpGet]
        public async Task<ActionResult> GetMetalMaskINCOMPLETE(string partnum)
        {
            var data = await _trans.GetTransactINComplete(partnum, 0);

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

            if (result == null || !result.Any()) return JsonNotFound("No Data Found.");
            return JsonSuccess(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetMetalMaskCOMPLETE(string partnum)
        {
            var data = await _trans.GetTransactINComplete(partnum, 1);

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

            if (result == null || !result.Any()) return JsonNotFound("No Data Found.");
            return JsonSuccess(result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateIncompleteData(MetalMaskTransaction metal)
        {
            bool result = await _trans.UpdateMetalMaskIncomplete(metal);
            if (!result) return JsonPostError("Updated failed.", 500);
            return JsonCreated(result, "Update Metal Mask Data Successfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetMetalMaskCurrentCount()
        {
            var data = await _trans.GetTheTotalCount();

            if (data == null) return JsonNotFound("No Count Data.");
            return JsonSuccess(data);
        }


        [HttpPost]
        public async Task<ActionResult> SubmitMetalMaskInfo(MetalMaskTransaction metal)
        {
            metal.Shift = GlobalUtilities.GetTheShiftSchedule() == "DS" ? false : true;
            metal.Status = 0;

            bool result = await _trans.AddMetalMastTransaction(metal);
            if (!result) return JsonPostError("INSERT failed.", 500);
            return JsonCreated(metal, "INSERT new Parts Successfully");
        }



        [HttpPost]
        public async Task<ActionResult> SubmitSMTLineinfo(MetalMaskTransaction metal)
        {
            bool result = await _trans.AddMetalMastTransaction(metal);
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

            if (!getData.Any())
                return new HttpStatusCodeResult(204);

            string templatePath = Server.MapPath("~/Content/Uploads/PCFY-81013Form1L.xlsx");
            //string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");

            if (!System.IO.File.Exists(templatePath))
                throw new Exception("Template not found: " + templatePath);


            using (var workbook = new XLWorkbook(templatePath))
            {
                var ws = workbook.Worksheet(1); // first sheet
                int row = 6;

                foreach (var item in getData)
                {
                    ws.Cell(3, 22).Value = item.Partnumber;

                    ws.Cell(row, 1).Value = item.DateInput.ToString("yyyy-MM-dd");
                    ws.Cell(row, 2).Value = item.Shift == false ? "DS" : "NS";
                    ws.Cell(row, 3).Value = item.SMTLine.ToString();
                    ws.Cell(row, 4).Value = item.AREA.ToString();
                    ws.Cell(row, 5).Value = item.Partnumber;
                    ws.Cell(row, 6).Value = item.Blocks.ToString();
                    ws.Cell(row, 7).Value = item.SMT_start.ToString();
                    ws.Cell(row, 8).Value = item.SMT_end.ToString();
                    ws.Cell(row, 9).Value = item.TotalTime.ToString();
                    ws.Cell(row, 10).Value = item.TotalPrintBoard.ToString();
                    ws.Cell(row, 11).Value = item.SMT_Operator;
                    ws.Cell(row, 12).Value = item.CleanDate.ToString("yyyy-MM-dd");
                    ws.Cell(row, 13).Value = item.CleanDate.ToString("HH:mm");
                    ws.Cell(row, 14).Value = item.Pattern;
                    ws.Cell(row, 15).Value = item.Frame;
                    ws.Cell(row, 16).Value = item.ReadOne.ToString();
                    ws.Cell(row, 17).Value = item.ReadTwo.ToString();
                    ws.Cell(row, 18).Value = item.ReadThree.ToString();
                    ws.Cell(row, 19).Value = item.ReadFour.ToString();
                    ws.Cell(row, 20).Value = item.Result;
                    ws.Cell(row, 21).Value = item.Remarks;
                    ws.Cell(row, 22).Value = item.PIC;

                  
                    row++;
                }

                // 4️⃣ Return file
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    string fileName = $"MetalMask_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName
                    );
                }
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
        // GET: Circuit/MetalMask/Masterlist
        public ActionResult Masterlist() => View();
        // GET: Circuit/MetalMask/History
        public ActionResult IncompleteMaskInfo(string partnum)
        {
            if (partnum == "")
            {
                return RedirectToAction("IncompleteMaskInfo", "MetalMask", new { area = "Circuit" });
            }
            return View();
        }
    }
}