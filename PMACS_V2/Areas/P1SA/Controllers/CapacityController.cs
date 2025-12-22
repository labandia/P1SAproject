using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using ProgramPartListWeb.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using System.Linq;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities.Security;
using ProgramPartListWeb.Helper;
using PMACS_V2.Utilities;
using System.Web;
using Microsoft.SqlServer.Server;
using System.Diagnostics;
using OfficeOpenXml;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
using System.IO;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [CompressResponse]
    [RateLimiting(300, 1)] // Limits the No of Request
    public class CapacityController : ExtendController
    {
        private readonly ICapacity _cap;

        public CapacityController(ICapacity cap) => _cap = cap;

        // ===========================================================
        // ==================== P1SA Summary  ========================
        // ===========================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetPisaSummaryList()
        {
            var data = await _cap.GetP1SAsummary() ?? new List<PsummaryModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No P1SA Summary data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetCapacitySummaryList(string month, int capid)
        {
            var data = await _cap.GetCapacitySummary(month, capid) ?? new List<CapacitySummaryModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Capacity Summary data found");
            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetForecastTotalData(string month)
        {
            int data = await _cap.GetForecastTotal(month);
            if (data == 0)
                return JsonNotFound("No Total Forecast found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetGroupCapacityList()
        {
            var getdata = await _cap.GetGroupCapacity() ?? new List<SelectionGroup>();
            if (getdata == null || !getdata.Any())
                return JsonNotFound("No Group Capacity data found");

            return JsonSuccess(getdata);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetModelbaseComboBox(int CapID)
        {
            var data = await _cap.GetModelBaseComboxList(CapID);
            if (data == null || !data.Any())
                return JsonNotFound("No Model Base Combobox found");

            return JsonSuccess(data);
        }
        public async Task<ActionResult> GetModelbaseComboBoxDoesntExistList(int CapID)
        {
            var getdata = await _cap.GetModelBaseDoesntExist(CapID);
            if (getdata == null || !getdata.Any())
                return JsonNotFound("No Model Base Combobox found");

            return JsonSuccess(getdata);
        }
        public async Task<ActionResult> GetModelbaseForAddForm(int CapID)
        {
            var getdata = await _cap.GetModelBaseComboxList(CapID);
            var getforest = await _cap.GetForecastChart() ?? new List<ForecastModel>();

            var filtercombo = getdata.Where(f => !getforest.Any(fa => fa.Model_name == f)).ToList();

            if (filtercombo == null || !filtercombo.Any())
                return JsonNotFound("No Add form Model Base Combobox found");

            return JsonSuccess(filtercombo);
        }
        // ===========================================================
        // ==================== CAPACITY PER SECTION =================
        // ===========================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetForecastModelList(string year)
        {
            var data = await _cap.GetForecast(year) ?? new List<ForecastModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No ForecastModel data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetForecastChartList()
        {
            var data = await _cap.GetTotalForecast() ?? new List<TotalForecastModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Forecast Chart data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetMoldingModelData(int CapID, string Month)
        {
            var data = await _cap.GetMoldingModels(Month, CapID) ?? new List<MoldingModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Molding Model data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetRotorModelData(int CapID, string Month)
        {
            var data = await CacheHelper.GetOrSetAsync("Rotor", () => _cap.GetRotorModels(Month, CapID), 15);
            if (data == null || !data.Any())
            {
                return JsonNotFound("No Rotor Model data found");
            }
            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetWindingModelData(int CapID, string Month)
        {
            var data = await _cap.GetWindingModels(Month, CapID) ?? new List<WindingModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Winding Model data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetPressModelData(int CapID, string Month)
        {
            var data = await _cap.GetPressModels(Month, CapID) ?? new List<PressModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Press Model data found");

            return JsonSuccess(data);
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetCircuitModelData(int CapID, string Month)
        {
            var data = await _cap.GetCircuitModels(Month, CapID) ?? new List<CircuitModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Rotor Model data found");

            return JsonSuccess(data);
        }

        [HttpPost]
        public async Task<ActionResult> ImportForecastData()
        {
            var response = new
            {
                isSuccess = false,
                code = 500,
                remarks = "error",
                message = ""
            };
            try
            {
                var postedFile = Request.Files["excelfile"];
                var selected = Request.Form["calendar"];

                if (postedFile == null || postedFile.ContentLength == 0)
                    throw new Exception("No file uploaded.");

                string savePath = Path.Combine(Server.MapPath("~/Content/Excel/"), postedFile.FileName);
                postedFile.SaveAs(savePath);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(savePath)))
                {
                    var worksheet = package.Workbook.Worksheets[3];

                    await _cap.DeleteForecast();

                    int selectedHeaderCol = 0;
                    string[] monthColumns = new string[6];
                    double[] totalForeCastColumns = new double[6];
                    string updateQuery = "";

                    // ===== HEADER SETUP =====
                    if (selected != "month")
                    {
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            if (DateForecast(worksheet.Cells[1, col].Text) == selected)
                            {
                                selectedHeaderCol = col;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            totalForeCastColumns[i] = Convert.ToDouble(worksheet.Cells[2, 3 + i].Text);
                            monthColumns[i] = DateForecast(worksheet.Cells[3, 3 + i].Text);
                        }

                        updateQuery = $@"
                            UPDATE PMACS_Forecast SET
                                {monthColumns[0]}=@{monthColumns[0]},
                                {monthColumns[1]}=@{monthColumns[1]},
                                {monthColumns[2]}=@{monthColumns[2]},
                                {monthColumns[3]}=@{monthColumns[3]},
                                {monthColumns[4]}=@{monthColumns[4]},
                                {monthColumns[5]}=@{monthColumns[5]}
                            WHERE forest_code=@forest_code";

                      
                    }

                    // ===== ROWS UPDATING AND ADDING MODELS =====
                    for (int row = 4; row <= worksheet.Dimension.End.Row - 1; row++)
                    {
                        string codeText = worksheet.Cells[row, 1].Text?.Trim();
                        string modelnames = worksheet.Cells[row, 2].Text?.Trim();

                        int forestCode;

                        if (modelnames == "ﾌｧﾝﾕﾆｯﾄ")
                        {
                            forestCode = 0;
                        }
                        else if (!string.IsNullOrWhiteSpace(codeText) && int.TryParse(codeText, out var fc))
                        {
                            forestCode = fc;
                        }
                        else
                        {
                            continue;
                        }

                        //Debug.WriteLine($"ROW {row} | Model={modelnames} | ForestCode={forestCode}");

                        if (selected == "month")
                        {
                            var forecast = CreateForecastInput(worksheet, row, forestCode);
                            await _cap.UpdateForecast(forecast, updateQuery, monthColumns);
                        }
                        else
                        {
                            double value = GetDouble(worksheet.Cells[row, selectedHeaderCol].Text);
                            await _cap.InsertMonthForeast(selected, value, forestCode);
                        }

                    }

                    await _cap.UpdateTotalForecast(monthColumns, totalForeCastColumns);

                }

                response = new
                {
                    isSuccess = true,
                    code = 200,
                    remarks = "success",
                    message = "File uploaded successfully."
                };
            }
            catch (Exception ex)
            {
                response = new
                {
                    isSuccess = false,
                    code = 403,
                    remarks = "error",
                    message = ex.Message
                };
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private forecastInput CreateForecastInput(ExcelWorksheet ws, int row, int forestCode)
        {
            return new forecastInput
            {
                column2 = GetDouble(ws.Cells[row, 3].Text),
                column3 = GetDouble(ws.Cells[row, 4].Text),
                column4 = GetDouble(ws.Cells[row, 5].Text),
                column5 = GetDouble(ws.Cells[row, 6].Text),
                column6 = GetDouble(ws.Cells[row, 7].Text),
                column7 = GetDouble(ws.Cells[row, 8].Text),
                forest_code = forestCode
            };
        }

        private double GetDouble(string value)
        {
            return double.TryParse(value, out var result) ? result : 0;
        }

        public string DateForecast(string mname)
        {
            var monthMap = new Dictionary<string, string>
            {
                { "1月の合計", "January" },
                { "2月の合計", "February" },
                { "3月の合計", "March" },
                { "4月の合計", "April" },
                { "5月の合計", "May" },
                { "6月の合計", "June" },
                { "7月の合計", "July" },
                { "8月の合計", "August" },
                { "9月の合計", "September" },
                { "10月の合計", "October" },
                { "11月の合計", "November" },
                { "12月の合計", "December" }
            };

            return monthMap.TryGetValue(mname, out var month) ? month : "";
        }

        // GET: Capacity/Winding/capid
        public ActionResult Winding(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 1:
                    ViewBag.processname = "Winding";
                    break;
                case 2:
                    ViewBag.processname = "Pinn";
                    break;
                case 3:
                    ViewBag.processname = "Soldering";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }
        // GET: Capacity/Rotor/capid
        public ActionResult Rotor(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 4:
                    ViewBag.processname = "Balancing";
                    break;
                case 5:
                    ViewBag.processname = "Rotor Caulking";
                    break;
                case 6:
                    ViewBag.processname = "Impeller Assy";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }
        // GET: Capacity/Molding/capi
        public ActionResult Molding(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 7:
                    ViewBag.processname = "Mold Frame";
                    break;
                case 8:
                    ViewBag.processname = "Mold Impeller";
                    break;
                case 9:
                    ViewBag.processname = "Mold Insulator";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }
            
            return View();
        }
        // GET: Capacity/Press/capid
        public ActionResult Press(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 10:
                    ViewBag.processname = "Stator";
                    break;
                case 11:
                    ViewBag.processname = "Rotor Cover";
                    break;
                case 12:
                    ViewBag.processname = "Aluminum Frame";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }
        // GET: Capacity/Press/capid
        public ActionResult Circuit(string capid)
        {
            int secID = Convert.ToInt32(capid);
            switch (secID)
            {
                case 13:
                    ViewBag.processname = "SMT";
                    break;
                case 14:
                    ViewBag.processname = "AOI";
                    break;
                default:
                    return RedirectToAction("Selection", "PMACS", new { area = "P1SA" });
            }

            return View();
        }




        // DYNAMIC USE OF HTML CODE USING PARTIAL VIEW
        [HttpPost]
        public ActionResult CapacitySummaryHeader(List<CapacityPartialViewModel> cap)
        {
            return PartialView("_CapacitySummary", cap);
        }
    }
}