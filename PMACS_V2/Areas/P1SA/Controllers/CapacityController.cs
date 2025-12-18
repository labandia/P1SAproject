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
            var data = await _cap.GetForecastChart() ?? new List<ForecastModel>();
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
            bool msgsuccess = false;
            int httpcode = 0;
            string rmk = "";
            string msg = "";

            HttpPostedFileBase postedFile = Request.Files["excelfile"];
            string selected = Request.Form["calendar"];

            // Store column headers
            string column2 = "";
            string column3 = "";
            string column4 = "";
            string column5 = "";
            string column6 = "";
            string column7 = "";

            int columnselectedHeader = 0;

            object formdata;
            string CombineString = "";

            try
            {
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    Debug.WriteLine("HERE");
                    // Save file
                    string savepath = Server.MapPath("~/Content/Excel/") + postedFile.FileName;
                    postedFile.SaveAs(savepath);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(new System.IO.FileInfo(savepath)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        for (int row = 3; row <= worksheet.Dimension.End.Row - 1; row++)
                        {
                            if (row == 3)
                            {
                                // ================= HEADER SETUP =================
                                if (selected != "month")
                                {
                                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                                    {
                                        if (DateForecast(worksheet.Cells[1, col].Text) == selected)
                                        {
                                            columnselectedHeader = col;
                                        }
                                    }
                                }
                                else
                                {
                                    column2 = DateForecast(worksheet.Cells[3, 3].Text);
                                    column3 = DateForecast(worksheet.Cells[3, 4].Text);
                                    column4 = DateForecast(worksheet.Cells[3, 5].Text);
                                    column5 = DateForecast(worksheet.Cells[3, 6].Text);
                                    column6 = DateForecast(worksheet.Cells[3, 7].Text);
                                    column7 = DateForecast(worksheet.Cells[3, 8].Text);

                                    CombineString =
                                        $"UPDATE Forecast_tbl SET " +
                                        $"{column2}=@{column2}, {column3}=@{column3}, " +
                                        $"{column4}=@{column4}, {column5}=@{column5}, " +
                                        $"{column6}=@{column6}, {column7}=@{column7} " +
                                        "WHERE forest_code=@forest_code";
                                }
                            }
                            else
                            {
                                // ================= DATA PROCESSING =================
                                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                {
                                    if (worksheet.Cells[row, 2].Text == "ﾌｧﾝﾕﾆｯﾄ")
                                    {
                                        var obj = new forecastInput
                                        {
                                            column2 = column2,
                                            column3 = column3,
                                            column4 = column4,      
                                            column5 = column5,
                                            column6 = column6,
                                            column7 = column7,
                                            forest_code = 0
                                        };


                                        await _cap.UpdateForecast(obj, CombineString);  
                                        //using (SqlConnection con = new SqlConnection(cons.DbConnection()))
                                        //{
                                        //    con.Open();
                                        //    using (SqlCommand cmd = new SqlCommand(CombineString, con))
                                        //    {
                                        //        cmd.Parameters.AddWithValue("@forest_code", 0);
                                        //        cmd.Parameters.AddWithValue("@" + column2, Convert.ToDouble(worksheet.Cells[row, 3].Text));
                                        //        cmd.Parameters.AddWithValue("@" + column3, Convert.ToDouble(worksheet.Cells[row, 4].Text));
                                        //        cmd.Parameters.AddWithValue("@" + column4, Convert.ToDouble(worksheet.Cells[row, 5].Text));
                                        //        cmd.Parameters.AddWithValue("@" + column5, Convert.ToDouble(worksheet.Cells[row, 6].Text));
                                        //        cmd.Parameters.AddWithValue("@" + column6, Convert.ToDouble(worksheet.Cells[row, 7].Text));
                                        //        cmd.Parameters.AddWithValue("@" + column7, Convert.ToDouble(worksheet.Cells[row, 8].Text));
                                        //        cmd.ExecuteNonQuery();
                                        //    }
                                        //}
                                    }
                                    else
                                    {
                                        int forecode = Convert.ToInt32(worksheet.Cells[row, 1].Text);
                                        string checksql =
                                            $"SELECT Model_name FROM Forecast_tbl WHERE forest_code = {forecode}";


                                    }


                                }


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                msgsuccess = false;
                httpcode = 403;
                rmk = "error";
            }

            formdata = new
            {
                isSuccess = msgsuccess,
                code = httpcode,
                remarks = rmk,
                message = msg
            };

            return Json(formdata, JsonRequestBehavior.AllowGet);

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