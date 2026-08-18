using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Areas.Final.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;

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
        //============== DASHBOARD & SHOP ORDER LINE ===========
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetListActiveShopOrders()
        {
            //await _manu.AutoUpdateShopOrderLine();


            var res = await _manu.GetListofActiveShopOrders();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetCountRecords(string line)
        {
            //await _manu.AutoUpdateShopOrderLine();
            int res = await _manu.GetCountShopOrders(line);
            return JsonSuccess(res);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateQuantityLine(int recordID, int Qty)
        {
            try
            {
                var res = await _manu.AddInputQuantiyPerLine(recordID, Qty);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }
        //=====================================================
        //============== LINE MANAGEMENT  =====================
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> LineShopOrderData(
            string Linename, 
            string searchtext, 
            int orderstatus)
        {
            var res = await _manu.GetListofShopOrdersByLine(Linename, 
                searchtext, orderstatus);

            if (res == null || !res.Any())
                return JsonNotFound("No Manpower data found");

            var finalData = new
            {
                payload = res,
                Total = res.Count
            };

   

            return JsonSuccess(finalData, "Retrieved data successfully");
        }
        [HttpGet]
        public async Task<ActionResult> SelectedShopOrderData(int RecordID)
        {
            try
            {
                var res = await _manu.GetShopderDetails(RecordID);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetTraceabilityData(string shopOrder)
        {
            try
            {
                var res = await _manu.TraceableShopOrderSummary(shopOrder);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> CheckIfAreadyCheckNext(string line)
        {
            int result = await _manu.GetNumberofNextprocess(line);
            return JsonSuccess(result);
        }
        
        [HttpPost]
        public async Task<ActionResult> UpdateStatusLine(int recordID, int orderstats, string line)
        {
            try
            {
                var res = await _manu.UpdateStatusShopOrder(recordID, orderstats, line);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ForCompleteUpdateStatusLine(int recordID, int orderstats, string line)
        {
            try
            {
                var res = await _manu.UpdateCompleteShopOrder(recordID, orderstats, line);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }



        [HttpPost]
        public async Task<ActionResult> CompleteStatusLine(int recordID, string line)
        {
            try
            {
                var updateTask =  _manu.CompletionStatusShopOrder(recordID, 3, "");
                var nextProcessTask =  _manu.NextModelProcess(line);

                await Task.WhenAll(updateTask, nextProcessTask);

                bool updateResult = await updateTask;
                bool nextProcessResult = await nextProcessTask;

                if (!updateResult)
                    return JsonError("Error Updated");


                return JsonSuccess(new
                {
                    Updated = updateResult,
                    NextProcess = nextProcessResult
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateLineshopOrder(int recordID, 
            string Lineman, int process)
        {
            try
            {
                //Debug.WriteLine($@"RecordID : {recordID} - lineman : {Lineman} ");
                var res = await _manu.ChangeLineShopOrder(recordID, Lineman, process);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAssemblyStats(
            int RecordID, string FAStatus, DateTime ShipmentDate, string Mode, bool WithSR, string Remarks)
        {
            try
            {
                var res = await _manu.UpdateAssemblyStatus(RecordID, FAStatus, ShipmentDate, Mode, WithSR, Remarks);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetRegistrationList(string finalshopOrder)
        {

            Debug.WriteLine("ShopOrder " + finalshopOrder);
            try
            {
                var res = await _manu.GetRegistrationMEIG(finalshopOrder);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDownTimeReportList()
        {
            try
            {
                var res = await _manu.GetDowntimeDailyReport();
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }



        [HttpGet]
        public async Task<ActionResult> GetDownTimeTypeList()
        {
            try
            {
                var res = await _manu.GetDownTimeType();
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetDownTimeList(string finalshopOrder)
        {

            try
            {
                var res = await _manu.GetDowntimeMonitor(finalshopOrder);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDownTimeMonitoring(DownTimeModel model)
        {
            try
            {
                var res = await _manu.AddGetTimeMonitor(model);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditDownTimeMonitoring(int ID)
        {
            try
            {
                var res = await _manu.EndTimeMonitor(ID);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetCatergoryPartslist(int cat)
        {
            try
            {
                var res = await _manu.GetCategoryRegistration(cat);
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddRegistrationMEIG(MEIGpartsModel model)
        {
            try
            {
                var res = await _manu.AddRegistrationMEIG(model);
                if (!res) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
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

        [HttpGet]
        public async Task<ActionResult> GetFailedUploadData()
        {
            var res = await _upload.GetListofFailedData();
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
        public async Task<ActionResult> UpdateAllUploadApproval()
        {
            var success = await _upload.CheckApprovalAllUploaded();

            if (!success)
                return JsonError("Failed to update.");

            return JsonSuccess();
        }

        [HttpPost]
        public async Task<ActionResult> FinalProcessUpload()
        {
            bool result = await _upload.TransferDataUploadtoMain();

            if (!result) return JsonError("");

            return JsonSuccess(result);
        }





        [HttpPost]
        public async Task<JsonResult> ImportUpload(HttpPostedFileBase file)
        {        
            if (file == null || file.ContentLength == 0)
                return Json(new { Success = false, Message = "No file uploaded." });

            //const int MaxBytes = 10 * 1024 * 1024; // 10 MB
            //if (file.ContentLength > MaxBytes)
            //    return Json(new { Success = false, Message = "File too large. Maximum allowed size is 10 MB." });


            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (ext != ".csv" && ext != ".xlsx" && ext != ".xls")
                return Json(new { Success = false, Message = "Unsupported file type." });

            try
            {
                string uploadPath = Server.MapPath("~/Content/Excel/");
                string uniqueName = Path.GetFileNameWithoutExtension(file.FileName)
                                  + "_" + Guid.NewGuid() + ext;
                string filePath = Path.Combine(uploadPath, uniqueName);

                await Task.Run(() =>
                {
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    file.SaveAs(filePath);
                });

                string jobId = Guid.NewGuid().ToString();
                var state = new UploadJobState
                {
                    Status = "processing",
                    Total = 0,
                    Current = 0,
                    Success = 0,
                    Failed = 0,
                    LastSent = 0,
                    Rows = new List<UploadRowResult>()
                };

                HttpRuntime.Cache.Insert(
                    "job_" + jobId,
                    state,
                    null,
                    DateTime.Now.AddMinutes(30),  // auto expire after 30 min
                    System.Web.Caching.Cache.NoSlidingExpiration
                );

                // start background processing
                HostingEnvironment.QueueBackgroundWorkItem(ct =>
                {
                    try
                    {
                        Debug.WriteLine("File Process Start");
                        ProcessExcelJob(jobId, filePath);
                    }
                    finally
                    {
                        // Clean up the file whether processing succeeded or failed
                        //if (File.Exists(filePath))
                        //{
                        //    File.Delete(filePath);
                        //}
                    }
                });

                return Json(new { Success = true, JobId = jobId });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult ImportProgress(string jobId)
        {
            var state = HttpRuntime.Cache["job_" + jobId] as UploadJobState;

            if (state == null)
                return Json(new { Success = false, Message = "Job not found." }, JsonRequestBehavior.AllowGet);

            List<UploadRowResult> newRows;

            // ✅ lock to prevent race condition between background thread and poll
            lock (state)
            {
                newRows = state.Rows.Skip(state.LastSent).ToList();
                state.LastSent += newRows.Count;
            }

            return Json(new
            {
                Success = true,
                state.Status,
                state.Total,
                state.Current,
                state.Failed,
                state.Message,
                Rows = newRows
            }, JsonRequestBehavior.AllowGet);
        }

        private async void ProcessExcelJob(string jobId, string filePath)
        {
            var state = HttpRuntime.Cache["job_" + jobId] as UploadJobState;
            if (state == null) return;

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                {
                    var sheet = package.Workbook.Worksheets[0];
                    int rowCount = sheet.Dimension?.Rows ?? 0;
                    // count total non-empty rows first
                    int total = 0;
                    for (int r = 2; r <= rowCount; r++)
                        if (sheet.Cells[r, 2].Value != null) total++;

                    lock (state) { state.Total = total; }

                    for (int r = 2; r <= rowCount; r++)
                    {
                        if (string.IsNullOrWhiteSpace(GetCell(sheet, r, 2)))
                            continue;

                        var rowResult = new UploadRowResult
                        {
                            ShopOrder = GetCell(sheet, r, 2),
                            PartNo = GetCell(sheet, r, 3),
                            Model = GetCell(sheet, r, 4),
                            Wc = GetCell(sheet, r, 5),
                            PlanStart = GetCellAsDate(sheet, r, 7),
                            IsSuccess = false,
                            Message = ""
                        };


                        try
                        {
                            var obj = new ProductionRecord
                            {
                                Model = GetCell(sheet, r, 4),
                                ShopOrder = GetCell(sheet, r, 2),
                                Line = GetCell(sheet, r, 1),
                                PartNo = GetCell(sheet, r, 3),
                                WC = GetCell(sheet, r, 5),
                                Qty = int.TryParse(GetCell(sheet, r, 6), out var qty)
                                ? qty
                                : 0,
                                PlanStart = GetCellAsDate(sheet, r, 7),
                                DispatchDate = GetCell(sheet, r, 8),
                                Note = GetCell(sheet, r, 9),
                                IfsFinish = GetCellAsDate(sheet, r, 10),
                                FaStatus = GetCell(sheet, r, 11),
                                Shipment = GetCellAsDate(sheet, r, 12),
                                Mode = GetCell(sheet, r, 13),
                                WithSr = GetCell(sheet, r, 14) != "",
                                P1SA_C = int.TryParse(GetCell(sheet, r, 15), out var p1sac) ? p1sac : 0,
                                P1SA_W = int.TryParse(GetCell(sheet, r, 16), out var p1saw) ? p1saw : 0,
                                P1SA_M = int.TryParse(GetCell(sheet, r, 17), out var p1sam) ? p1sam : 0,
                                P1SA_P = int.TryParse(GetCell(sheet, r, 18), out var p1sap) ? p1sap : 0,
                                P1SA_R = int.TryParse(GetCell(sheet, r, 19), out var p1sar) ? p1sar : 0,
                                P1FA_FA = int.TryParse(GetCell(sheet, r, 20), out var pifafa) ? pifafa : 0,
                                P1FA_H = int.TryParse(GetCell(sheet, r, 21), out var pifaH) ? pifaH : 0,
                                M1 = int.TryParse(GetCell(sheet, r, 22), out var M1) ? M1 : 0,
                                Operational = int.TryParse(GetCell(sheet, r, 23), out var op) ? op : 0
                            };
                            //Debug.WriteLine($@"
                            //    ========== ProductionRecord (Row {r}) ==========
                            //    Line          : {obj.Line}
                            //    ShopOrder     : {obj.ShopOrder}
                            //    PartNo        : {obj.PartNo}
                            //    Model         : {obj.Model}
                            //    WC            : {obj.WC}
                            //    Qty           : {obj.Qty}
                            //    PlanStart     : {obj.PlanStart:yyyy-MM-dd}
                            //    DispatchDate  : {obj.DispatchDate}
                            //    Note          : {obj.Note}
                            //    IfsFinish     : {obj.IfsFinish:yyyy-MM-dd}
                            //    FaStatus      : {obj.FaStatus}
                            //    Shipment      : {obj.Shipment:yyyy-MM-dd}
                            //    Mode          : {obj.Mode}
                            //    WithSr        : {obj.WithSr}
                            //    P1SA_C        : {obj.P1SA_C}
                            //    P1SA_W        : {obj.P1SA_W}
                            //    P1SA_M        : {obj.P1SA_M}
                            //    P1SA_P        : {obj.P1SA_P}
                            //    P1SA_R        : {obj.P1SA_R}
                            //    P1FA_FA       : {obj.P1FA_FA}
                            //    P1FA_H        : {obj.P1FA_H}
                            //    M1            : {obj.M1}
                            //    Operational   : {obj.Operational}
                            //    ===============================================");
                            // save to DB here if needed
                            bool success = await _upload.UpsertUploadData(obj);

                            rowResult.IsSuccess = success;
                            rowResult.Message = success ? "Imported Successfully" : "Import Failed";

                            if (success)
                            {
                                lock (state)
                                {
                                    state.Success++;
                                }

                            }
                            else
                            {
                                lock (state)
                                {
                                    state.Failed++;
                                }
                            }
                             

                            System.Threading.Thread.Sleep(80);
                        }
                        catch (Exception ex)
                        {
                            rowResult.IsSuccess = false;
                            rowResult.Message = ex.Message;

                            Debug.WriteLine($"Row {r} ERROR");
                            Debug.WriteLine(ex);

                            lock (state)
                            {
                                state.Failed++;
                            }
                        }

                        lock (state)
                        {
                            state.Current++;
                            state.Rows.Add(rowResult);
                        }
                    }
                }

                lock (state) { state.Status = "done"; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                lock (state)
                {
                    state.Status = "error";
                    state.Message = ex.Message;
                }
            }
            finally
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
        }


        private DateTime? CustomCellDate(ExcelWorksheet sheet, int row, int col)
        {
            var value = sheet.Cells[row, col].Value;

            if (value == null)
                return null;

            if (value is DateTime dt)
                return dt;

            if (double.TryParse(value.ToString(), out double oa))
                return DateTime.FromOADate(oa);

            if (DateTime.TryParse(value.ToString(), out dt))
                return dt;

            return null;
        }



        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public async Task ImportUploadStream(string fileName)
        {
          
            string uploadPath = Server.MapPath("~/Content/Excel/");
            string filePath = Path.Combine(uploadPath, fileName);


            if (string.IsNullOrEmpty(fileName)
                || fileName.Contains("..") 
                || !System.IO.File.Exists(filePath))
            {
                Response.StatusCode = 400;
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(new { error = "File not found." }));
                Response.End();
                return;
            }

           
            Response.Clear();
            Response.ContentType = "text/event-stream";
            Response.Charset = "";
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            Response.Headers["X-Accel-Buffering"] = "no";
            Response.Headers["Connection"] = "keep-alive";
            Response.Headers["Content-Encoding"] = "identity";
            Response.Buffer = false;
            Response.BufferOutput = false;
            Response.Flush();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                {
                    var sheet = package.Workbook.Worksheets[0];
                    int rowCount = sheet.Dimension?.Rows ?? 0;
                    int total = rowCount - 1;
                    int current = 0;
                    int success = 0;
                    int failed = 0;

                    for (int r = 2; r <= rowCount; r++)
                    {
                        if (sheet.Cells[r, 2].Value == null) continue;

                        current++;

                        try
                        {
                            var row = new
                            {
                                shopOrder = GetCell(sheet, r, 2),
                                partNo = GetCell(sheet, r, 3),
                                model = GetCell(sheet, r, 4),
                                wc = GetCell(sheet, r, 5),
                                planStart = GetCellAsDate(sheet, r, 7),
                                result = new { success = true, message = "OK" }
                            };

                            success++;

                            Response.Write("data: " + JsonConvert.SerializeObject(new
                            {
                                type = "row",
                                current,
                                total,
                                success,
                                failed,
                                row
                            }) + "\n\n");

                            Response.Flush();
                            await Task.Delay(100);
                        }
                        catch (Exception ex)
                        {
                            failed++;

                            Response.Write("data: " + JsonConvert.SerializeObject(new
                            {
                                type = "row",
                                current,
                                total,
                                success,
                                failed,
                                row = new
                                {
                                    shopOrder = GetCell(sheet, r, 2),
                                    partNo = GetCell(sheet, r, 3),
                                    model = GetCell(sheet, r, 4),
                                    wc = GetCell(sheet, r, 5),
                                    planStart = "",
                                    result = new { success = false, message = ex.Message }
                                }
                            }) + "\n\n");

                            Response.Flush();
                            await Task.Delay(100);
                        }
                    }

                    Response.Write("data: " + JsonConvert.SerializeObject(new
                    {
                        type = "done",
                        current,
                        total,
                        success,
                        failed,
                        message = success + " rows imported successfully."
                    }) + "\n\n");

                    Response.Flush();
                }
            }
            finally
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
        }


        private async Task<List<UploadRowDto>> ParseExcelAsync(string filePath)
        {
            var rows = new List<UploadRowDto>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension?.Rows ?? 0;

                if (rowCount < 2)
                    throw new Exception("File is empty or has no data rows.");

                for (int r = 2; r <= rowCount; r++)
                {
                    if (sheet.Cells[r, 2].Value == null) continue;

                    rows.Add(new UploadRowDto
                    {
                        ShopOrder = GetCell(sheet, r, 2),
                        PartNo = GetCell(sheet, r, 3),
                        Model = GetCell(sheet, r, 4),
                        Wc = GetCell(sheet, r, 5),
                        Qty = int.TryParse(GetCell(sheet, r, 6), out var q) ? q : 0,
                        PlanStart = GetCellAsDate(sheet, r, 7),
                        Result = new RowResult { Success = true, Message = "OK" }
                    });
                }
            }

            return await Task.FromResult(rows);
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

        //private static string GetCellAsDate(ExcelWorksheet sheet, int row, int col)
        //{
        //    var val = sheet.Cells[row, col].Value;
        //    if (val == null) return "";

        //    double serial = Convert.ToDouble(val);          // <-- the trap
        //    return DateTime.FromOADate(serial).ToString("MM/dd/yyyy");
        //}

        private string GetCellAsDate(ExcelWorksheet sheet, int row, int col)
        {
            var value = sheet.Cells[row, col].Value;

            if (value == null)
                return "";

            if (value is DateTime dt)
                return dt.ToString("yyyy-MM-dd");

            if (value is double oa)
            {
                try
                {
                    return DateTime.FromOADate(oa).ToString("yyyy-MM-dd");
                }
                catch
                {
                    return "";
                }
            }

            string text = sheet.Cells[row, col].Text.Trim();

            if (DateTime.TryParse(text, out DateTime parsed))
                return parsed.ToString("yyyy-MM-dd");

            return "";
        }

        //=====================================================
        //============== FINAL ASSEMBLY DATA  =================
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetFinalAssemblyData(string Linename,
            string searchtext,
            int page = 1,
            int pageSize = 10)
        {
            var res = await _manu.GetListofShopOrdersByLine(Linename, searchtext, 0);

            int totalCount = await _manu.GetActualCountOfShopOrders(Linename);

            if (res == null || !res.Any())
                return JsonNotFound("No Manpower data found");

            var finaldata = new
            {
                payload = res,
                Total = totalCount
            };


            return JsonSuccess(finaldata, "Retrieved data successfully");
        }
        //=====================================================
        //============== PARTLY SHORT SUMMARY DATA  ===========
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetLastUpdateAndTotalPartlyShort(int dispatch)
        {
            var res = await _manu.GetLastUpdateAndTotal(dispatch);

            var obj = new
            {
                res.PlanQty,
                res.LastDate,
                res.totalpercent
            };

            return JsonSuccess(obj, "Retrieved data successfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetPartlistReportData(int dispatch)
        {
            var res = await _manu.GetPartlyShortSummary(dispatch);


            if (res == null || !res.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(res, "Retrieved data successfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetDispatchReportData()
        {
            var res = await _manu.GetDispatchShortSummay();


            if (res == null || !res.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(res, "Retrieved data successfully");
        }



        public ActionResult DownloadFile(string fileName)
        {
            // Path to the folder where files are stored
            string filePath = Server.MapPath("~/Content/Uploads/" + fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound("File not found.");
            }

            // Return the file to the user
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileDownloadName = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", fileDownloadName);
        }



        [HttpGet]
        public async Task<ActionResult> GetDailyPlanChart()
        {
            var data = await _manu.GetDailyPlanChart();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //=====================================================
        //============== APPROVAL PROCESS =====================
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetApprovalData(int section)
        {
            var data = await _manu.GetApproverName(section);
            return JsonSuccess(data, "Get Data Successfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetDisposalData(int ID)
        {
            var data = await _manu.GetDisposalDetails(ID);

            var finalobj = new
            {
                materialSummary = data.summary,
                materialList = data.list,
            };

            return JsonSuccess(finalobj, "Get Data Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateApprovalData(string aprovename, int id)
        {
            try
            {

                bool result = await _manu.ApproveDisposal(aprovename, id);
                if (!result)
                {
                    Debug.WriteLine("adsdasda");
                }
                if (!result) return JsonError("Error Updated");
                return JsonSuccess(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CONTROLLER ERROR: {ex.Message}");
                throw;
            }
        }



        // GET: Final/Assembly
        public ActionResult Dashboard() => View();
        public ActionResult DelaySummary() => View();
        public ActionResult DispatchSummary() => View();
        // GET: Final/LineData
        public ActionResult LineData() => View();
        // GET: Final/HistoryDownTime
        public ActionResult HistoryDownTime() => View();
        // GET: Final/LineData
        public ActionResult DailyReport() => View();
        // GET: Final/UploaData
        public ActionResult UploaData() => View();
        // GET: Final/ShopOrderLine/{line}
        public ActionResult ShopOrderLine(string line) => View();
        // GET: Final/ShopOrderDetails/{id}
        public ActionResult ShopOrderDetails(int id) => View();

        public ActionResult DailyPlanChart() => View();




        public async Task<ActionResult> DisposalApproval(int id, int section)
        {
            //var identity = User.Identity as ClaimsIdentity;

            //string email =
            //    identity?.FindFirst("preferred_username")?.Value
            //    ?? identity?.FindFirst(ClaimTypes.Email)?.Value;

            //string name =
            //    identity?.FindFirst("name")?.Value
            //    ?? User.Identity.Name;

            //if (string.IsNullOrEmpty(email))
            //{
            //    return Content("Unable to identify the Microsoft 365 account.");
            //}

            //// TODO:
            //// Check whether this email/name is authorized
            //// to approve this disposal.

            //// TODO:
            //// Save name + email to DisposalControlNumber

            //return Content(
            //    $"Disposal ID: {id}\n" +
            //    $"Approved By: {name}\n" +
            //    $"Email: {email}"
            //);

          

            return View();
        }
    }
}