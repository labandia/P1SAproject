using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Areas.Final.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Final.Controllers
{
    public class AssemblyController : ExtendController
    {
        private readonly IManufacturing _manu;
        private readonly IUploadServices _upload;


        public AssemblyController(IManufacturing manu, IUploadServices upload)
        {
            _manu = manu;   
            _upload = upload;
        }

        //======================================================
        //============== DASHBIARD & SHOP ORDER LINE ===========
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetListActiveShopOrders()
        {
            var res = await _manu.GetListofActiveShopOrders();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }
        //=====================================================
        //============== LINE MANAGEMENT  =====================
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> LineShopOrderData(string Linename)
        {
            var res = await _manu.GetListofShopOrdersByLine(Linename);
            if (res == null || !res.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(res);
        }
        //=====================================================
        //============== UPLOAD DATA MANAGEMENT  ==============
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetUploadData()
        {
            var res = await _upload.GetListofUploadedData();
            if (res == null || !res.Any())
                return JsonNotFound("No Uploaded data found");

            return JsonSuccess(res);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUploadApproval(int recordID, bool check)
        {
            var success = await _upload.CheckApprovalForUploadedData(recordID, check);


            if (!success)
                return JsonError("Failed to update approval status.");

            return JsonSuccess("Approval status updated successfully.");
        }


        [HttpPost]
        public async Task<JsonResult> ImportUpload()
        {
            //var file = Request.Form.Files.GetFile("file");

            //System.Diagnostics.Debug.WriteLine("File: " + (file?.FileName ?? "NULL"));

            //if (file == null || file.Length == 0)
            //    return Json(new { Success = false, Message = "No file received." });

            //var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            //if (ext != ".csv" && ext != ".xlsx" && ext != ".xls")
            //    return Json(new { Success = false, Message = "Unsupported file type." });

            try
            {
                //var rows = aswait ParseExcelAsync(file);
                return Json(new
                {
                    Success = true,
                    Message = " rows imported successfully.",
                    data = Array.Empty<UploadRowDto>() // replace with actual data  
                });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }



        private async Task<List<UploadRowDto>> ParseExcelAsync(IFormFile file)
        {
            var rows = new List<UploadRowDto>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = file.OpenReadStream())
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension?.Rows ?? 0;

                if (rowCount < 2)
                    throw new Exception("File is empty or has no data rows.");

                for (int r = 2; r <= rowCount; r++)
                {
                    // skip empty rows
                    if (sheet.Cells[r, 2].Value == null) continue;

                    var row = new UploadRowDto
                    {
                        ShopOrder = GetCell(sheet, r, 2),   // Shop order
                        PartNo = GetCell(sheet, r, 3),   // Part No.
                        Model = GetCell(sheet, r, 4),   // Model
                        Wc = GetCell(sheet, r, 5),   // WC
                        Qty = int.TryParse(GetCell(sheet, r, 6), out var q) ? q : 0,  // QTY
                        PlanStart = GetCell(sheet, r, 7),   // Plan start
                        Result = new RowResult { Success = true, Message = "OK" }
                    };

                    rows.Add(row);
                }
            }

            return await Task.FromResult(rows);
        }

        // safely reads a cell as string
        private string GetCell(ExcelWorksheet sheet, int row, int col)
        {
            return sheet.Cells[row, col].Value?.ToString()?.Trim() ?? "";
        }

        private string GetCellAsDate(ExcelWorksheet sheet, int row, int col)
        {
            var val = sheet.Cells[row, col].Value;
            if (val == null) return "";

            // Excel stores dates as doubles (serial number)
            if (val is double d)
            {
                try
                {
                    return DateTime.FromOADate(d).ToString("yyyy-MM-dd");
                }
                catch
                {
                    return val.ToString();
                }
            }

            return val.ToString().Trim();
        }

        // GET: Final/Assembly
        public ActionResult Dashboard() => View();
        // GET: Final/UploaData
        public ActionResult UploaData() => View();
        // GET: Final/ShopOrderLine/{line}
        public ActionResult ShopOrderLine(string line) => View();
    }
}