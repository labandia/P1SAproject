using ProgramPartListWeb.Data;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProgramPartListWeb.Interfaces;
using OfficeOpenXml;
using System.IO;
using ProgramPartListWeb.Controllers;
using System.Diagnostics;


namespace ProgramPartListWeb.Areas.Circuit.Controllers
{
    [CompressResponse]
    public class ProgramPartListController : ExtendController
    {
        private readonly WarehouseRepository _ware;
        private readonly SeriesRepository _series;
        private readonly ISeriesRepository _series2;

        public ProgramPartListController(ISeriesRepository series)
        {
            _series = new SeriesRepository();
            _ware = new WarehouseRepository();
            _series2 = series;
        }

        //------------------------------------------------------------------------------
        //--------------------- PLAN SCHEDULE PULL DATA --------------------------------
        //------------------------------------------------------------------------------
        //[JwtAuthorize]
        public async Task<ActionResult> GetScheduleSeries()
        {
            try
            {
                var data = await _series2.GetSeriesData();
                if (data == null || !data.Any())
                {
                    return JsonNotFound("No Plan Schedule data found");
                }
                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        public async Task<ActionResult> GetSummmaryComponentlist(int intval)
        {
            try
            {
                // Get the list of seriesData
                var datalist = await _series2.GetSeriesData() ?? new List<SeriesviewModel>();
                // Filter the data in only row data
                var seriesdata = datalist.FirstOrDefault(p => p.Series_ID == intval);
                var data = await _series2.GetComponentsSummmary(seriesdata.Series_no) ?? new List<SummaryComponentModel>();

                if (data == null || !data.Any())
                    return JsonNotFound("No Components Summary data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        public async Task<ActionResult> GetparlistData(int intval)
        {
            try
            {
                // Get the list of seriesData
                var datalist = await _series2.GetSeriesData() ?? new List<SeriesviewModel>();
                // Filter the data in only row data
                var seriesdata = datalist.FirstOrDefault(p => p.Series_ID == intval);
                var data = await _series2.Getpartlist(seriesdata.Series_no) ?? new List<PartlistModel>();
                if (data == null || !data.Any())
                    return JsonNotFound("No Components Partlist data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetPreparedWarehouseList(int intval)
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("PreparedWarehouse", () => _series2.GetWarehousePreparedData(intval), 10);
                if (data == null || !data.Any())
                    return JsonNotFound("No Warehouse data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetSupplierDatalist(string partText)
        {
            try
            {
                var data = await _series.GetSuppliersData(partText);
                if (data == null || !data.Any())
                    return JsonNotFound("No Supplier data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }

        }
        public async Task<ActionResult> GetSeriesDetails(int seriesID)
        {
            try
            {
                var datalist = await _series2.GetSeriesData() ?? new List<SeriesviewModel>();
                var data = datalist.FirstOrDefault(p => p.Series_ID == seriesID);
                if (data == null)
                    return JsonNotFound("No Plan Schedule Details data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        // ###################### POST METHOD =========================================
        [HttpPost]
        public async Task<ActionResult> AddSummaryComponents()
        {

            int totalcount = await _series2.GetTotalQuantity(Request.Form["Partnum"], Convert.ToInt32(Request.Form["SeriesID"]));
            int gtotal = totalcount +  Convert.ToInt32(Request.Form["QuantityInput"]);

            int Status = gtotal < Convert.ToInt32(Request.Form["PreparedQuan"]) ? 0 : 1;

            //string strLotID = Request.Form["LotIDText"];
            string strReelID = Request.Form["LotID"];
            string strLotNo = Request.Form["LotNo"];
            string strpartnum = Request.Form["Partnum"];
            int seriesID = Convert.ToInt32(Request.Form["SeriesID"]);


            var setnewval = new
            {
                SeriesID = seriesID,
                NeedQuan = Convert.ToInt32(Request.Form["PreparedQuan"]),
                ReelID = strReelID.Trim(),
                AbassadorPartnum = strpartnum,
                CompIN = Convert.ToInt32(Request.Form["QuantityInput"]),
                SetNo = Convert.ToInt32(Request.Form["SetNo"]),
                SupID = Convert.ToInt32(Request.Form["Supplierselect"]),
                LotNo = strLotNo,
                Stats = Status
            };


            var updateset = new
            {
                Series_ID = Convert.ToInt32(Request.Form["SeriesID"]),
                AbassadorPartnum = strpartnum,
                Stats = Status
            };

            bool result = await _series2.SaveSummaryComponents(setnewval);
            if (result)
            {
                // Updates the the Summary input
                // Updates the prepared Quantity input
                await _series2.UpdatePreparedQuantity(updateset);
                await _series2.UpdatePartsSummary(seriesID, strpartnum, gtotal);

            }

            var formdata = GlobalUtilities.GetMessageResponse(result, 0);
            return Json(formdata, JsonRequestBehavior.AllowGet);

        }

        //------------------------------------------------------------------------------
        //--------------------- PLAN SCHEDULE MANAGE DATA-------------------------------
        //------------------------------------------------------------------------------
        //[JwtAuthorize]
        public async Task<ActionResult> GetSeriesDataList()
        {
            try
            {
                var data = await _series.GetSeriesData();
                if (data == null || !data.Any())
                    return JsonNotFound("No  Plan Schedule data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        

        // ######################### POST METHOD ######################################
        [HttpPost]
        public async Task<ActionResult> Addnewseries()
        {
            try
            {
                bool result = false;
                string filepath = "";
                string seriesnum = Request.Form["Series_add"].Replace("_", "-");

                var setnewval = new
                {
                    Series_no = seriesnum,
                    Line = Convert.ToInt32(Request.Form["Line_add"]),
                    Timetarget = Convert.ToDecimal(Request.Form["Time_add"]),
                    CreatedBy = Request.Form["Created_add"],
                    Shift = GlobalUtilities.GetTheShiftSchedule(),
                    Remarks = Request.Form["Remark_add"],
                    SetupNavi = Request.Form["Navi_add"],
                    VisualManage = Request.Form["Visual_add"],
                    Status = "N",
                    MachineSerial = Request.Form["Machine_add"],
                    Modelno = Request.Form["Model_add"],
                    SetGroup = Request.Form["Group_add"]
                };


                 //Get the series Data Value
                var getseries = await _series.GetSeriesData();
                var seriesData = getseries.FirstOrDefault(res => res.Series_no == seriesnum);
                if (seriesData == null)
                {
                    int seriesadd = 0;
                    List<Dictionary<string, string>> allRows = new List<Dictionary<string, string>>();
                    Debug.WriteLine("Files Count : " + Request.Files.Count);
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            // Set The file path and Filepath and save to the folder
                            filepath = Path.Combine(Server.MapPath("~/Content/Uploads"), Path.GetFileName(file.FileName));
                            file.SaveAs(filepath);

                            // Get Extension File Name
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string strSeries = SeriesText(fileName);


                            int numinput;
                            if (!int.TryParse(fileName.Split('_').Last(), out numinput))
                            {
                                throw new Exception("Invalid Machine number format in file name.");
                            }

                            // Store All Cell value to the Dictionay collection
                            var headers = new List<string>();
                            var rows = new List<Dictionary<string, string>>();
                            using (var reader = new StreamReader(file.InputStream))
                            {
                                for (int j = 0; j < 3; j++) reader.ReadLine(); // Skip first 3 lines
                                var headerLine = reader.ReadLine();
                                if (headerLine != null)
                                {
                                    
                                     headers = headerLine
                                            .Split(',')
                                            .Select((h, index) => index == 5 && string.IsNullOrWhiteSpace(h.Trim()) ? "PitchIndex" : h.Trim())
                                            .ToList();

                                    for (int q = 0; q < headers.Count; q++)
                                    {
                                        if(q == 4) {
                                            headers[q] = "PitchIndex";
                                        }
                                    }
                                }

                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    var values = line.Split(',');
                                    var row = new Dictionary<string, string>();

                                    for (int j = 0; j < headers.Count && j < values.Length; j++)
                                    {
                                        row[headers[j]] = values[j].Trim();
                                    }

                                    rows.Add(row);
                                }
                            }

                            // Checks the Series if the input is the same as the File name series
                            if (seriesnum == strSeries)
                            {
                                result = true;
                                // Prevents from inserting a series data
                                if (seriesadd == 0)
                                {
                                    await _series.AddSeriesData(setnewval);
                                    seriesadd = 1;
                                }


                                await _series.SaveImportData(rows, strSeries, numinput);
                            }
                            else
                            {
                                // If the series is not the same procceed to the next iteration
                                //continue;
                                result = false;
                                //return new HttpStatusCodeResult(400, "Series no " + strSeries + "is not the same as the excel file input");           
                            }


                        }
                    }
                }
                else
                {
                    // If the Series is already Exist in the database 
                    result = false;
                }


                var formdata = GlobalUtilities.GetMessageResponse(result, 2);
                return Json(formdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CustomLogger.LogError(ex);
                return new HttpStatusCodeResult(500, "Internal Server Error: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Editseries()
        {
            var setnewval = new
            {
                Series_ID = Convert.ToInt32(Request.Form["SeriesID"]),
                Series_no = Request.Form["Edit_Series_add"],
                Line = Convert.ToInt32(Request.Form["Edit_Line_add"]),
                Timetarget = Convert.ToDecimal(Request.Form["Edit_Time_add"]),
                CreatedBy = Request.Form["Edit_Created_add"],
                Remarks = Request.Form["Edit_Remark_add"],
                SetupNavi = Request.Form["Edit_Navi_add"],
                Status = Request.Form["Edit_Status_add"],
                VisualManage = Request.Form["Edit_Visual_add"],
                MachineSerial = Request.Form["Edit_Machine_add"],
                Modelno = Request.Form["Edit_Model_add"],
                SetGroup = Request.Form["Edit_Group_add"]
            };

            bool result = await _series.UpdateSeriesData(setnewval);

            if (result) CacheHelper.Remove("ManagePlan");

            var formdata = GlobalUtilities.GetMessageResponse(result, 1);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SetActiveSeries()
        {
            var serieID = Request.Form["SeriesID"];
            var status = Request.Form["Selected"];
            bool result = await _series2.SeriesChangeStatus(Convert.ToInt32(serieID), Convert.ToInt32(status));
            if (result)
            {
                CacheHelper.Remove("serieslist");
                CacheHelper.Remove("ManagePlan");
            }

            var formdata = GlobalUtilities.GetMessageResponse(result, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> LateUploadData()
        {
            bool result = false;
            string filepath = "";

            var file = Request.Files["filetoupload"];
            int series = Convert.ToInt32(Request.Form["SeriesID"]);


            if (file != null && file.ContentLength > 0)
            {
                filepath = Path.Combine(Server.MapPath("~/Content/Uploads"), Path.GetFileName(file.FileName));
                file.SaveAs(filepath);

                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string strSeries = SeriesText(fileName);
                int numinput = Int32.Parse(fileName.Split('_').Last());

                var extension = Path.GetExtension(file.FileName).ToLower();
                var headers = new List<string>();
                var rows = new List<Dictionary<string, string>>();

                // Handle CSV files
                if (extension == ".csv")
                {
                    using (var reader = new StreamReader(file.InputStream))
                    {
                        // Skip the first 3 lines (because your data starts at A4)
                        for (int i = 0; i < 3; i++)
                        {
                            reader.ReadLine();
                        }

                        // Read the headers (this should now be the 4th line)
                        var headerLine = reader.ReadLine();
                        if (headerLine != null)
                        {
                            headers = headerLine.Split(',').Select(h => h.Trim()).ToList();
                        }

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var values = line.Split(',');
                            var row = new Dictionary<string, string>();

                            for (int i = 0; i < headers.Count && i < values.Length; i++)
                            {
                                row[headers[i]] = values[i].Trim();
                            }
                            rows.Add(row);
                        }
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Unsupported file format." });
                }

                bool queryresult = await _series.LateSaveImportData(rows, series, numinput);

                if (queryresult)
                {
                    result = true;
                }
            }
            else
            {
                result = false;
            }

            var formdata = GlobalUtilities.GetMessageResponse(result, 2);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }


        //------------------------------------------------------------------------------
        //--------------------- COMPONENTS PARTLIST OUT --------------------------------
        //------------------------------------------------------------------------------
        public async Task<ActionResult> GetComponentOutList()
        {
            try
            {
                var data = await CacheHelper.GetOrSetAsync("ComponentsOut", () => _series2.GetComponentsOutData(), 15);
                if (data == null || !data.Any())
                    return JsonNotFound("No Components OUT data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        //########################### POST METHOD ######################################
        [HttpPost]
        public async Task<ActionResult> UploadComponentsOut()
        {
            string filepath = "";
            var formData = new Object();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    filepath = Path.Combine(Server.MapPath("~/Content/Uploads"), Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);

                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string seriesText = fileName.Replace("_", "-").ToUpper();

                    var headers = new List<string>();
                    var rows = new List<Dictionary<string, string>>();

                    using (var reader = new StreamReader(file.InputStream))
                    {
                        // Read the header from the first row
                        var headerLine = reader.ReadLine();
                        if (!string.IsNullOrEmpty(headerLine))
                        {
                            headers = headerLine.Split(',').Select(h => h.Trim()).ToList();
                        }

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var values = line.Split(',');
                            var row = new Dictionary<string, string>();

                            for (int j = 0; j < headers.Count && j < values.Length; j++)
                            {
                                row[headers[j]] = values[j].Trim();
                            }

                            rows.Add(row);
                        }
                    }

                    int seriesID = await _series.GetSeriesID(seriesText);

                    if (seriesID != 0)
                    {
                        // CHECKS IF THE FILE IS ALREADY IMPORT 
                        if (await _series.CheckComponentsOutData(seriesID))
                        {
                            formData = GlobalUtilities.GetMessageResponse(false, 0, "Series no is already exist in the database");
                        }
                        else
                        {
                            bool result = await _series.UploadComponentsPartlistOut(rows, seriesID);
                            formData = GlobalUtilities.GetMessageResponse(result, 1);
                        }
                    }
                    else
                    {
                        formData = GlobalUtilities.GetMessageResponse(false, 0, "Series no doesnt match to the file name");
                        return Json(formData, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            CacheHelper.Remove("ComponentsOut");

            return Json(formData, JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------
        //--------------------- HISTORY TRANSACTION  -----------------------------------
        //------------------------------------------------------------------------------
        public async Task<ActionResult> GetHistoryTransactionList()
        {
            var data = await CacheHelper.GetOrSetAsync("HistoryTransact", () => _series2.GetHistoryTransactionData(), 15);
            if (data == null || !data.Any())
                return JsonNotFound("No History Transaction data found");

            return JsonSuccess(data);
        }
        //######################### POST METHOD ########################################
        [HttpPost]
        public ActionResult ExportToExcel(List<SummaryHistory> data)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exported Data");

                    // Add Header
                    worksheet.Cells[1, 1].Value = "Series No";
                    worksheet.Cells[1, 2].Value = "Product Name";
                    worksheet.Cells[1, 3].Value = "Abassador Part Number";
                    worksheet.Cells[1, 4].Value = "Item Code";
                    worksheet.Cells[1, 5].Value = "Need Quantity";
                    worksheet.Cells[1, 6].Value = "Comp IN";
                    worksheet.Cells[1, 7].Value = "Comp Out";
                    worksheet.Cells[1, 8].Value = "Total Production";
                    worksheet.Cells[1, 9].Value = "Difference";

                    // Add Data
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].Series_no;
                        worksheet.Cells[i + 2, 2].Value = data[i].ProductName;
                        worksheet.Cells[i + 2, 3].Value = data[i].AbassadorPartnum;
                        worksheet.Cells[i + 2, 4].Value = data[i].ItemCode;
                        worksheet.Cells[i + 2, 5].Value = data[i].NeedQuan;
                        worksheet.Cells[i + 2, 6].Value = data[i].CompIN;
                        worksheet.Cells[i + 2, 7].Value = data[i].CompOut;
                        worksheet.Cells[i + 2, 8].Value = data[i].Totalprod;
                        worksheet.Cells[i + 2, 9].Value = data[i].Diff;
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Save to MemoryStream
                    using (MemoryStream stream = new MemoryStream())
                    {
                        package.SaveAs(stream);
                        byte[] byteArray = stream.ToArray();

                        return File(byteArray,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "ExportedData.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error: " + ex.Message);
            }


        }

        //------------------------------------------------------------------------------
        //--------------------- REGISTER SUPPLIERS  -----------------------------------
        //------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetSupplierList()
        {
            try
            {
                var res = await _series2.GetSupplierData();
                if (res == null || !res.Any())
                    return JsonNotFound("No Supplier data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        //######################### POST METHOD ########################################
        [HttpPost]
        public async Task<ActionResult> AddSuppliersData(SupplierModel sup)
        {
            bool result = await _series2.AddEditSuppliers(sup);

            if (result)
            {
                CacheHelper.Remove("Suppliers");
            }
            var formdata = GlobalUtilities.GetMessageResponse(result, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        //#######################################################################################//
        public string SeriesText(string seriesName)
        {
            var parts = seriesName.Split('_').Take(3).ToList();

            // Remove leading zeros from the last part
            parts[2] = parts[2].TrimStart('0');

            // Join the parts back together with underscores
            string strjoin = string.Join("_", parts);
            string result = strjoin.Replace("_", "-");

            return result;
        }
        //------------------------------------------------------------------------------
        //--------------------- WAREHOUSE PULL DATA ------------------------------------
        //------------------------------------------------------------------------------
        public async Task<ActionResult> GetWarePartnumberDetails(string partnum)
        {
            try
            {
                var data = await _ware.Warehousepartnumber(partnum);
                if (data == null || !data.Any())
                    return JsonNotFound("No Warehouse Partnumber data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }  
        }
        public async Task<ActionResult> GetPartlistDatabase()
        {
            try
            {
                var data = await _ware.Warehousepartsmasterlist();
                if (data == null || !data.Any())
                    return JsonNotFound("No Warehouse Masterlist data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetPartlistRequestSummary()
        {
            try
            {
                var data = await _ware.PartsRequestSummary();
                if (data == null || !data.Any())
                    return JsonNotFound("No Partlist Request data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        //###################### POST METHOD ###########################################
        [HttpPost]
        public async Task<ActionResult> SaveRequestSummaryList(List<WarehouseSummaryModel> data)
        {
            try
            {
                if (data == null || !data.Any())
                {
                    return Json(new { success = false, message = "No data provided!" });
                }

                bool result = await _ware.SaveSummaryRequestWarehouse(data);
                return Json(new { success = result, message = result ? "Data saved successfully!" : "Failed to save data." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }







        //------------------------------------------------------------------------------
        //--------------------- DISPLAY PAGE  ------------------------------------------
        //------------------------------------------------------------------------------
        public ActionResult HistoryTransaction() => View();
        public ActionResult ComponentsOut() => View();
        public ActionResult RegisterSupplier() => View();
        public ActionResult LogMainpage()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            ///    return RedirectToAction("LogMainpage", "Series"); // Redirect logged-in users
            //}
            return View();
        }
       



        public ActionResult PlanSchedule() => View();
        public ActionResult PlanScheduleDetails(string series)
        {
            try
            {
                //Redirect to Main series data if no data exist
                if (string.IsNullOrEmpty(series))
                {
                    return RedirectToAction("PlanSchedule");
                }

                // Decode Base64
                byte[] data = Convert.FromBase64String(series);
                string decodedSeries = System.Text.Encoding.UTF8.GetString(data);
                ViewBag.SeriesNo = decodedSeries;
                return View();
            }
            catch (Exception ex)
            {
                CustomLogger.LogError(ex);
                return RedirectToAction("Error");
            }
        }



        public ActionResult ManagePlanSchedule() => View();
        public ActionResult PlanDetails(string series)
        {
            try
            {
                //Redirect to Main series data if no data exist
                if (string.IsNullOrEmpty(series))
                {
                    return RedirectToAction("ManagePlanSchedule");
                }

                // Decode Base64
                byte[] data = Convert.FromBase64String(series);
                string decodedSeries = System.Text.Encoding.UTF8.GetString(data);
                ViewBag.SeriesNo = decodedSeries;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult ManageWarehouse() => View();
        public ActionResult FeederType() => View();
    }
}