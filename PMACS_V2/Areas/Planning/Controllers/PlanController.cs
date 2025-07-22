using PMACS_V2.Areas.Planning.Interface;
using PMACS_V2.Controllers;
using PMACS_V2.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Areas.Planning.Model;
using System.Linq;
using PMACS_V2.Utilities;
using System.Data;
using System.IO;
using System.Web;
using System.Globalization;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using ProgramPartListWeb.Helper;
using System.Diagnostics;

namespace PMACS_V2.Areas.Planning.Controllers
{
    [CompressResponse]
    public class PlanController : ExtendController
    {
        private readonly IPlanning _pl;

        public PlanController(IPlanning pl) => _pl = pl;


        // ####################### FETCH DATA LIST ####################################
        public async Task<ActionResult> GetFirstAndLastDateList()
        {
            var data = await _pl.GetsDatelist() ?? new List<DateModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Dates Found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetPlanningData()
        {
            var data = await _pl.GetPCDataList(10, 1);
            if (data == null || !data.Any())
                return JsonNotFound("No M1 Planning data found");

            return JsonSuccess(data);
        }

        public async Task<ActionResult> GetBranchData()
        {
            var data = await _pl.GetBranchSummary() ?? new List<BranchModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Branch data found");

            return JsonSuccess(data);
        }
        public async Task<ActionResult> GetSummaryPartnum(string stardate, string endDate)
        {
            var dates = new List<string>();
            DataTable data = await _pl.GetDailyPartnumberSummary(stardate, endDate);

            if (data == null || data.Rows.Count == 0)
            {
                var errorObj = new MessageResponse<object>
                {
                    StatusCode = 404,
                    Message = "Data was not found"
                };
                return Json(errorObj, JsonRequestBehavior.AllowGet);
            }

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column]);
                }
                dataList.Add(rowDict);
            }

            foreach (DataColumn col in data.Columns)
            {
                dates.Add(col.ColumnName);
            }

            var successObj = new Messagepartnum<object>
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Header = dates,
                Data = dataList
            };

            return Json(successObj, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetSelectedPartnum(string stardate)
        {
            var dates = new List<string>();
            DataTable data = await _pl.GetSelectedPartnumberSummary(stardate);
            await Task.Delay(500);

            if (data == null || data.Rows.Count == 0)
            {
                var errorObj = new MessageResponse<object>
                {
                    StatusCode = 404,
                    Message = "Data was not found"
                };
                return Json(errorObj, JsonRequestBehavior.AllowGet);
            }

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column]);
                }
                dataList.Add(rowDict);
            }

            foreach (DataColumn col in data.Columns)
            {
                dates.Add(col.ColumnName);
            }

            var successObj = new Messagepartnum<object>
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Header = dates,
                Data = dataList
            };

            return Json(successObj, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAdditionalResult(string stardate, string endDate)
        {
            var dates = new List<string>();
            DataTable data = await _pl.AdditionalSummary(stardate, endDate);
            await Task.Delay(500);

            if (data == null || data.Rows.Count == 0)
            {
                var errorObj = new MessageResponse<object>
                {
                    StatusCode = 404,
                    Message = "Data was not found"
                };
                return Json(errorObj, JsonRequestBehavior.AllowGet);
            }

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column]);
                }
                dataList.Add(rowDict);
            }

            foreach (DataColumn col in data.Columns)
            {
                dates.Add(col.ColumnName);
            }

            var successObj = new Messagepartnum<object>
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Header = dates,
                Data = dataList
            };

            return Json(successObj, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DownloadFile(string fileName)
        {
            // Path to the folder where files are stored
            string filePath = Server.MapPath("~/Content/Template/" + fileName);

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


        public async Task<ActionResult> GetLackingResults(string stardate, string endDate)
        {
            var dates = new List<string>();
            var data = await _pl.LackingResultSummary(stardate, endDate);
            await Task.Delay(200);

            if (data == null || data.Rows.Count == 0)
            {
                var errorObj = new MessageResponse<object>
                {
                    StatusCode = 404,
                    Message = "Data was not found"
                };
                return Json(errorObj, JsonRequestBehavior.AllowGet);
            }

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column]);
                }
                dataList.Add(rowDict);
            }

            foreach (DataColumn col in data.Columns)
            {
                dates.Add(col.ColumnName);
            }

            var successObj = new Messagepartnum<object>
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Header = dates,
                Data = dataList
            };

            return Json(successObj, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetShopOrderResult(string stardate, string endDate)
        {
            var dates = new List<string>();
            var data = await _pl.ShopOrderSummary(stardate, endDate);
            await Task.Delay(500);

            if (data == null || data.Rows.Count == 0)
            {
                var errorObj = new MessageResponse<object>
                {
                    StatusCode = 404,
                    Message = "Data was not found"
                };
                return Json(errorObj, JsonRequestBehavior.AllowGet);
            }

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column]);
                }
                dataList.Add(rowDict);
            }

            foreach (DataColumn col in data.Columns)
            {
                dates.Add(col.ColumnName);
            }

            var successObj = new Messagepartnum<object>
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Header = dates,
                Data = dataList
            };

            return Json(successObj, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetRequestResults()
        {
            var dates = new List<string>();
            var data = await _pl.SalesResultSummary();

            if (data == null || data.Rows.Count == 0)
            {
                var errorObj = new MessageResponse<object>
                {
                    StatusCode = 404,
                    Message = "Data was not found"
                };
                return Json(errorObj, JsonRequestBehavior.AllowGet);
            }

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    rowDict.Add(column.ColumnName, row[column]);
                }
                dataList.Add(rowDict);
            }

            foreach (DataColumn col in data.Columns)
            {
                dates.Add(col.ColumnName);
            }

            var successObj = new Messagepartnum<object>
            {
                StatusCode = 200,
                Message = "Data retrieved successfully",
                Header = dates,
                Data = dataList
            };

            return Json(successObj, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetEndCurrentdata()
        {
            string currentYear = DateTime.Now.Year.ToString();
            var data = await _pl.GetEndMonthlist(currentYear);
            var formdata = GlobalUtilities.DataGetMessageResponse(data);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetSelectedPartnumDetails(string strdate, int impID)
        {
            var data = await _pl.GetSelectedDetailsSummary(strdate, impID);
            var formdata = GlobalUtilities.DataGetMessageResponse(data);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetLackSelectedPartnumDetails(string strdate, int impID)
        {
            var data = await _pl.GetSelectedlackDetailsSummary(strdate, impID);
            var formdata = GlobalUtilities.DataGetMessageResponse(data);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetSelectedShopOrderDetails(string strdate, int impID)
        {
            var data = await _pl.GetSelectedShopOrderDetailsSummary(strdate, impID);
            var formdata = GlobalUtilities.DataGetMessageResponse(data);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetRequestedResultDetails(int monthcol, int monthrow, int intyear)
        {
           
            var data = await _pl.GetSelectedRequestsDetailsSummary(monthcol, monthrow, intyear);
            var formdata = GlobalUtilities.DataGetMessageResponse(data);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public async Task<ActionResult> EditEndMonthResult(PostMontlyEndResult end)
        {
            bool result = await _pl.UpdateEndMonthData(end);
            if (!result) JsonValidationError();
            return JsonCreated(null, "Update data successfully!");   
        }

        [HttpPost]
        public async Task<ActionResult> AdditionalEndMonthResult(string Current_Remains,
                                                                 string Current_TotalOrders,
                                                                 string EndRemain_Orders,
                                                                 string EndTotal_Orders)
        {
            string currentMonthName = DateTime.Now.ToString("MMMM");
            int currentYear = DateTime.Now.Year;

            var newinput = new
            {
                DateMonth = currentMonthName,
                EndTotalOrders = EndTotal_Orders,
                EndRemainOrders = EndRemain_Orders,
                CurrentTotalOrders = Current_TotalOrders,
                CurrentRemains = Current_Remains,
                EndYear = currentYear
            };

            if (await _pl.CheckEndMonthExist(currentMonthName))
            {
                return Json(new { status = false, Message = $"{currentMonthName} is already uploaded" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool result = await _pl.AddEndMonthData(newinput);
                if (result)
                    return Json(new { status = true, Message = "Insert new data" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = false, Message = "Error occurred during Insert" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEndMonthResult(int RecordID)
        {
            Debug.WriteLine("ID" + RecordID);
            bool result = await _pl.DeleteEndMonthData(RecordID);
            if (!result) JsonValidationError();
            return JsonCreated(null, "Delete data successfully!");
        }



        [HttpPost]
        public async Task<JsonResult> UploadFiledetails(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                string checkDatenow = DateTime.Now.ToString("yyyy-MM-dd");
                string uploadPath = Server.MapPath("~/Content/Excel/");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                try
                {
                    string uniqueFileName = Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "_" +
                                            Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName);
                    string filePath = Path.Combine(uploadPath, uniqueFileName);
                    uploadedFile.SaveAs(filePath);

                    string sheetName = "Mat'l Prob Sum (over 8wks)";
                    var datalist = await ImportExcelByList(filePath, sheetName);
                    bool result = await _pl.InsertDataExcelFile(datalist, checkDatenow);

                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    return Json(new { status = result });
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, TextMessage = ex.Message });
                }
            }
            else
            {
                return Json(new { status = false, Message = "No file uploaded." });
            }
        }
        public async Task<DataTable> ImportExcelToDataTable(string filePath, string sheetName)
        {
            var dt = new DataTable();
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[sheetName];
                range = worksheet.Range["A1", worksheet.Cells[worksheet.UsedRange.Rows.Count, worksheet.UsedRange.Columns.Count]];

                int numberCol = 0;
                for (int col = 1; col <= range.Columns.Count; col++)
                {
                    var headerValue = ((Excel.Range)range.Cells[1, col]).Value2;
                    if (col > 25 || headerValue == null) break;

                    numberCol++;
                    dt.Columns.Add(Convert.ToString(headerValue));
                }

                int totalRows = range.Rows.Count;

                for (int row = 2; row <= totalRows; row++)
                {
                    var firstValue = ((Excel.Range)range.Cells[row, 1]).Value;
                    if (firstValue == null || string.IsNullOrWhiteSpace(firstValue.ToString())) break;

                    var dataRow = dt.NewRow();
                    for (int col = 1; col <= numberCol; col++)
                    {
                        var cellValue = ((Excel.Range)range.Cells[row, col]).Value;
                        dataRow[col - 1] = cellValue;
                    }
                    dt.Rows.Add(dataRow);
                    await Task.Delay(10);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during Excel import: " + ex.Message, ex);
            }
            finally
            {
                if (range != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) workbook.Close(false); System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) excelApp.Quit(); System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return dt;
        }

        public async Task<List<M1ExcelData>> ImportExcelByList(string filePath, string sheetName)
        {
            return await Task.Run(() =>
            {
                var resultList = new List<M1ExcelData>();
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[sheetName];
                    if (worksheet == null)
                        throw new Exception("Worksheet not found: " + sheetName);

                    int lastRow = worksheet.Dimension.End.Row - 1;
                    int counter = 0;    

                    for (int row = 2; row <= lastRow; row++)
                    {
                        var firstCell = worksheet.Cells[row, 1].Text;
                        if (string.IsNullOrWhiteSpace(firstCell))
                            break; // Stop loop if first cell is blank

                        string format = "MM/dd/yy";
                        CultureInfo culture = CultureInfo.InvariantCulture;

                        DateTime UploadDate = DateTime.ParseExact(worksheet.Cells[row, 1].Text, format, culture);
                        DateTime salesDate = DateTime.ParseExact(worksheet.Cells[row, 10].Text, format, culture);

                        var data = new M1ExcelData();
                        data.DateUpload = UploadDate;
                        data.No = int.TryParse(worksheet.Cells[row, 2].Text, out int noVal) ? noVal : 0;
                        data.DOItoM1 = worksheet.Cells[row, 3].Text;
                        data.CreatedDate = worksheet.Cells[row, 4].Text;
                        data.MDBasedOnSalesRequest = worksheet.Cells[row, 5].Text;
                        data.Branch = worksheet.Cells[row, 6].Text;
                        data.SDPOrderNo = worksheet.Cells[row, 7].Text;
                        data.SDPSalesPartNo = worksheet.Cells[row, 8].Text;
                        data.SDPSalesQty = int.TryParse(worksheet.Cells[row, 9].Text, out int salesQty) ? salesQty : 0;

                        data.SalesRequestedShipDate = salesDate;
                        data.PreviousUpdate = worksheet.Cells[row, 11].Text;
                        data.PC1ProposedShipDate = worksheet.Cells[row, 12].Text;
                        data.M1LatestUpdate = worksheet.Cells[row, 13].Text;

                        //if (DateTime.TryParseExact(worksheet.Cells[row, 14].Text, "M/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        //{
                        //    data.PC1LatestProposedShipDate = parsedDate;
                        //}
                        //else
                        //{
                        //    data.PC1LatestProposedShipDate = DateTime.MinValue;
                        //}
                        //DateTime PC1latestDate = DateTime.ParseExact(orginalText, format, culture);

                        data.PC1LatestProposedShipDate = worksheet.Cells[row, 14].Text;
                        data.Negativenum = int.TryParse(worksheet.Cells[row, 15].Text, out int negNum) ? negNum : 0;
                        data.AdjustDueDate = worksheet.Cells[row, 16].Text;
                        data.ReplyStatus = worksheet.Cells[row, 17].Text;
                        data.Days = int.TryParse(worksheet.Cells[row, 18].Text, out int daysVal) ? daysVal : 0;
                        data.MDDate = worksheet.Cells[row, 19].Text;
                        data.LeadTime = int.TryParse(worksheet.Cells[row, 20].Text, out int leadTimeVal) ? leadTimeVal : 0;
                        data.PartNo = worksheet.Cells[row, 21].Text;
                        data.PartName = worksheet.Cells[row, 22].Text;
                        data.Usage = double.TryParse(worksheet.Cells[row, 23].Text, out double usageVal) ? usageVal : 0.0;
                        data.NeedQty = double.TryParse(worksheet.Cells[row, 24].Text, out double needQtyVal) ? needQtyVal : 0.0;
                        data.Judgement = worksheet.Cells[row, 25].Text;

                        counter++;

                        resultList.Add(data);
                    }
                }

                return resultList;
            });
        }



        // GET: Planning/Plan
        public ActionResult LiveViewMonitoring() => View();
        public ActionResult MonitoringDashboard() => View();





        [HttpPost]
        public async Task<JsonResult> SampleUpload(HttpPostedFileBase file1)
        {
            if (file1 != null && file1.ContentLength > 0)
            {
                string checkDatenow = DateTime.Now.ToString("yyyy-MM-dd");
                string uploadPath = Server.MapPath("~/Content/Excel/");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);


                try
                {
                    string uniqueFileName = Path.GetFileNameWithoutExtension(file1.FileName) + "_" +
                                            Guid.NewGuid() + Path.GetExtension(file1.FileName);
                    string filePath = Path.Combine(uploadPath, uniqueFileName);
                    file1.SaveAs(filePath);

                    string sheetName = "Mat'l Prob Sum (over 8wks)";
                    var datalist = await ImportExcelByList(filePath, sheetName);

                    //foreach (var item in datalist)
                    //{
                    //    Debug.WriteLine("No " + item.Branch + " PC1 Latest Proposed Ship Date: " + item.SalesRequestedShipDate);
                    //}

                    // OPTIONAL: Save to DB
                    await _pl.InsertDataExcelFile(datalist, checkDatenow);

                    // Delete file after reading
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    return Json(new { status = true, rows = datalist });
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, TextMessage = ex.Message });
                }
            }
            else
            {
                return Json(new { status = false, Message = "No file uploaded." });
            }
        }
    }
}