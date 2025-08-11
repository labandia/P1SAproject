
using OfficeOpenXml;
using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using ProgramPartListWeb.Utilities.Common;
using ProgramPartListWeb.Utilities.Security;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProgramPartListWeb.Areas.PC.Controllers
{
    [RateLimiting(300, 1)] // Limits the No of Request
    public class PatrolController : ExtendController
    {
        private readonly IInspector _ins;
        public static string strSender => ConfigurationManager.AppSettings["config:SMTPEmail"];
        public PatrolController(IInspector ins) => _ins = ins;

        //-----------------------------------------------------------------------------------------
        //---------------------------- USERS DATA  ------------------------------------------------
        //-----------------------------------------------------------------------------------------

        // GET: GetEmployeeLIST
        public async Task<ActionResult> GetEmployeelist()
        {
            var data = await _ins.GetEmployee() ?? new List<Employee>();
            //var res = CacheHelper.GetOrSet("Pressmasterlist", () => product, 15);
            if (data == null || !data.Any())
                return Json(ResultMessageResponce.JsonError("No Data Found", 404, "No Employee data found"), JsonRequestBehavior.AllowGet);

            return Json(ResultMessageResponce.JsonSuccess(data), JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------------------------------------------------------------
        //---------------------------- DASHBOARD CALENDAR -----------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetScheduleDateList()
        {
            var data = await _ins.GetScheduleDate() ?? new List<PatrolSchedule>();
            if (data == null || !data.Any()) return JsonNotFound("No Patrol Schedule today.");

            return JsonSuccess(data, "Load Schedule");
        }

        //-----------------------------------------------------------------------------------------
        //---------------------------- REGISTRATION NO --------------------------------------------
        //-----------------------------------------------------------------------------------------
        // GET: GetRegistrationNo
        [JwtAuthorize]
        public async Task<ActionResult> GetRegistrationNo()
        {
            var data = await _ins.GetRegistrationData() ?? new List<PatrolRegistionModel>();
            if (data == null || !data.Any()) return JsonNotFound("No registration data found");

            return JsonSuccess(data, "Load Registration No#");
        }
        // GET: GetRegistrationNoByID
        [JwtAuthorize]
        public async Task<ActionResult> GetRegistrationNoByID(string Regno)
        {
            try
            {
                var data = await _ins.GetRegistrationData() ?? new List<PatrolRegistionModel>();
                var filterdata = data.Where(p => p.RegNo == Regno).ToList();
                //var res = CacheHelper.GetOrSet("Pressmasterlist", () => product, 15);
                var find = await _ins.GetPatrolFindings(Regno) ?? new List<FindingModel>();
                var multidata = new Dictionary<string, IEnumerable<object>>
                {
                    { "Register", filterdata },
                    { "Finding", find }
                };

                return JsonMultipleData(multidata);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        //=========================================================================================
        //============================ INSPECTOR PROCESS ==========================================
        //=========================================================================================
        // GET: GetQualifiedInspector
        [JwtAuthorize]
        public async Task<ActionResult> GetQualifiedInspector()
        {
            var data = await _ins.GetInpectorsData() ?? new List<InspectorModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Inspectors data found");

            return JsonSuccess(data, "Load GetQualified Inspectors");
        }
        // GET: GetInspectorsByAdd
        public async Task<ActionResult> GetInspectorsByAdd()
        {
            var data = await _ins.GetUsersInfoSetting() ?? new List<UsersModel>();
            //var removeDuplicateData = data
            //                           .GroupBy(x => x.Employee_ID)
            //                           .Select(g => g.First())
            //                           .ToList();
            //if (removeDuplicateData == null || !removeDuplicateData.Any())
            //    return JsonNotFound("No Inspectors data found");
            if(!data.Any()) return JsonNotFound("No Inspectors data found");
            return JsonSuccess(data, "LoadInspectorData");
        }
        // GET: GetInpectsByApproval
        [JwtAuthorize]
        public async Task<ActionResult> GetInpectsByApproval()
        {
            var data = await _ins.GetInpectorsData() ?? new List<InspectorModel>();
            var filterdata = data.Where(p => p.Approval == 1).ToList();

            if (filterdata == null || !filterdata.Any())  return JsonNotFound("No Patrol data found");
            return JsonSuccess(filterdata, "Loads Inspector By Approval");
        }
        // GET: GetProcesslist
        public async Task<ActionResult> GetProcesslist(int depid)
        {
            var data = await _ins.GetProcessData(depid) ?? new List<ProccessModel>();
            //var res = CacheHelper.GetOrSet("Pressmasterlist", () => product, 15);
            if (data == null || !data.Any()) return JsonNotFound("No Processlist data found");

            return JsonSuccess(data, "Loads ProcessList Data");
        }
        //-----------------------------------------------------------------------------------------
        // POST : AddInspectors
        [HttpPost]
        public async Task<ActionResult> AddInspectors()
        {
            try {
                var obj = new
                {
                    Employee_ID = Request.Form["Employee_ID"],
                    DateQualified = Request.Form["DateQualified"],
                    OJTRegistration = Request.Form["OJTRegistration"],
                    Remarks = Request.Form["Remarks"]
                };
                bool result = await _ins.AddEditInpectors(obj, 0);
                if (!result)
                {
                    CacheHelper.Remove("Inspectors");
                    return JsonPostError("Insert failed.", 500);
                }
                return JsonCreated(obj, "Add Inspector Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
           
        }
        // POST : EditInspectors
        [HttpPost]
        public async Task<ActionResult> EditInspectors()
        {
            try
            {
                var obj = new
                {
                    InspectID = Convert.ToInt32(Request.Form["EditInspectID"]),
                    Employee_ID = Request.Form["EditEmployee_ID"],
                    DateQualified = Request.Form["EditDateQualified"],
                    OJTRegistration = Request.Form["EditOJTRegistration"],
                    Remarks = Request.Form["EditRemarks"]
                };

                bool result = await _ins.AddEditInpectors(obj, 1);
                if (!result)
                {
                    CacheHelper.Remove("Inspectors");
                    return JsonPostError("Edit failed.", 500);
                }
                return JsonCreated(obj, "Edit Inspector Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }
        [HttpPost]
        public async Task<ActionResult> ChangeApprovalInspect()
        {
            try
            {
                int inspectID = Convert.ToInt32(Request.Form["ID"]);
                int Approval = Convert.ToInt32(Request.Form["stats"]);
          
                bool result = await _ins.ApproveAndDisapproveInpectors(inspectID, Approval);

                if (!result) return JsonError("Insert failed.", 500);
                return JsonCreated(inspectID, "Change Status successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }

        //-----------------------------------------------------------------------------------------
        //---------------------------- INSPECTOR DATE SCHEDULE ------------------------------------
        //-----------------------------------------------------------------------------------------
        // GET : GetCalendarEventsMonth
        [JwtAuthorize]
        public async Task<ActionResult> GetCalendarEventsMonth()
        {
            try
            {
                var data = await _ins.GetScheduleDateByMonth() ?? new List<CalendarSched>();
                //var res = CacheHelper.GetOrSet("Pressmasterlist", () => product, 15);
                var events = data.Select(x =>
                {
                    // Parse the string first
                    DateTime parsedDate;
                    if (DateTime.TryParse(x.ScheduleDate, out parsedDate))
                    {
                        return new
                        {
                            id = x.ScheduleID,
                            title = x.ProcessName,
                            start = parsedDate.ToString("yyyy-MM-dd"), // convert to correct format
                            allDay = true,
                            Department_ID = x.Department_ID,
                            Fullname = x.FullName,
                            extendedProps = new
                            {
                                Fullname = x.FullName,
                                Active = x.IsActive,
                                ProcID = x.ProcessID,
                            }
                        };
                    }
                    else
                    {
                        return null; // or handle parsing error
                    }
                })
                  .Where(x => x != null)
                  .ToList();


                if (events == null || !events.Any()) return JsonNotFound("No CalendarEvents data found");

                return JsonSuccess(events, "Load Calendar Events");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        // GET : GetCalendarEvents
        [JwtAuthorize]
        public async Task<ActionResult> GetCalendarEvents(string Employee_ID)
        {
            try
            {
                var data = await _ins.GetCalendarData(Employee_ID) ?? new List<CalendarSched>();
                //var res = CacheHelper.GetOrSet("Pressmasterlist", () => product, 15);
                var events = data.Select(x =>
                {
                    // Parse the string first
                    DateTime parsedDate;
                    if (DateTime.TryParse(x.ScheduleDate, out parsedDate))
                                {
                                    return new
                                    {
                                        id = x.ScheduleID,
                                        title = x.ProcessName,
                                        start = parsedDate.ToString("yyyy-MM-dd"), // convert to correct format
                                        allDay = true,
                                        extendedProps = new
                                        {
                                            Active = x.IsActive,
                                            ProcID = x.ProcessID,    
                                        }
                                    };
                                }
                                else
                                {
                                    return null; // or handle parsing error
                                }
                            })
                  .Where(x => x != null)
                  .ToList();


                if (events == null || !events.Any()) return JsonNotFound("No CalendarEvents data found");

                return JsonSuccess(events, "Load Calendar Events");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        // POST : SETActiveSchedule
        [HttpPost]
        public async Task<ActionResult> SetActiveDateSchedule()
        {
            try
            {
                bool result;
                int modeID = Convert.ToInt32(Request.Form["ModeID"]);
                int mode = modeID > 0 ? 1 : 0;
                int resultcheck = modeID > 0 ? 1 : 2;

                // Zero means if the set date is not yet Inserted
                if (modeID == 0) {
                    var obj = new   
                    {
                        Employee_ID = Request.Form["Employee_ID"],
                        ScheduleDate = Request.Form["ScheduleDate"],
                        ProcessID = Convert.ToInt32(Request.Form["ProcessID"]),
                        TrainerID = 1
                    };

                
                    result = await _ins.SetScheduleCalendar(obj, mode);
                }
                else
                {
                    var obj = new
                    {
                        ScheduleID = Convert.ToInt32(Request.Form["EditScheduleID"]),
                        ProcessID = Convert.ToInt32(Request.Form["EditProcessID"])
                    };
                    //obj.ProcessID = Convert.ToInt32(Request.Form["ProcessID"]);
                    result = await _ins.SetScheduleCalendar(obj, mode);
                }

                if (result == false) return JsonError("Problem update set active Date.", 500);
                return JsonCreated(result, "Change Status successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }
        // POST : DeleteSchedule
        [HttpPost]
        public async Task<ActionResult> RemoveDateSchedule()
        {
            int ID = Convert.ToInt32(Request.Form["ScheduleID"]);
            bool result = await _ins.RemoveScheduleCalendar(ID);
            var formdata = GlobalUtilities.GetMessageResponse(result, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public async Task<ActionResult> AddRegistration(HttpPostedFileBase ExcelFile)
        {
            // Get Department Name
            int departmentID = await _ins.GetEmployeeByDepartment(Request.Form["Employee_ID"]);
            string departmentName = GlobalUtilities.DepartmentName(departmentID);
            bool isSign = !string.IsNullOrEmpty(Request.Form["IsSign"]);

            // Get Employee FullName
            //var data = await _ins.GetEmployee() ?? new List<Employee>();
            //string Fullname = data.FirstOrDefault(p => p.EmployeeID == Request.Form["Employee_ID"])?.Fullname;

            // Set File Name For Registration No. PDF file
            string newFileName = Request.Form["RegNo"] + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            string outputPdfPath = newFileName.Replace(".xlsx", ".pdf");


            // Set File Name for Countermeasures Excel File
            string strExcelname = null;

            // Default template path For Registration PDF
            string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");

            // If user uploaded an Excel file, save it and use as template
            if (ExcelFile != null && ExcelFile.ContentLength > 0)
            {
                string uploadedFileName = Path.GetFileNameWithoutExtension(ExcelFile.FileName);
                string fileExtension = Path.GetExtension(ExcelFile.FileName);
                string savedFileName = $"Countermeasure_{Request.Form["RegNo"]}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";

                strExcelname = savedFileName;

                // Export the file Network Folder
                ExportFiler.SaveFileasExcel(ExcelFile, savedFileName);

            }

            var obj = new RegistrationModel
            {
                RegNo = "P1SA-" + Request.Form["RegNo"],
                DateConduct = Request.Form["DateConduct"],
                Employee_ID = Request.Form["Employee_ID"],
                FullName = Request.Form["EmployeeSearch"],
                PIC = Request.Form["PIC"],
                PIC_Comments = Request.Form["PIC_Comments"],
                FilePath = outputPdfPath,
                Manager = Request.Form["Manager"],
                Manager_Comments = Request.Form["Manager_Comments"],
                CounterPath = strExcelname,
                IsSigned = isSign
            };

            string findJson = Request.Form["FindJson"];

            bool result = await _ins.AddRegistration(obj, findJson);

            if (result)
            {
                CacheHelper.Remove("Registration");

                string creatbody = EmailService.CreateAEmailBody(
                    "JUAN DELA CRUZ",
                    "This is a sample email body used for testing purposes. Please disregard."
                );

                var SendEmail = new SentEmailModel
                {
                    Subject = "Patrol Inspection",
                    Sender = strSender,
                    BCC = "",
                    Body = creatbody,
                    Recipient = "jaye.labandia@sanyodenki.com"
                };

                // GENERATE A PDF FILE AND SAVE TO THE NETWORK FILE
                await ExportFiler.SaveFileasPDF(obj, findJson, departmentName, newFileName, templatePath, isSign);

                // EMAIL SAVE TO THE DATABASE
                await EmailService.SendEmailViaSqlDatabase(SendEmail);

            }
            if (result == false)
                return JsonError("Problem during saving Data.");

            return JsonCreated(result, "Change Status successfully");

        }
        [HttpPost]
        public async Task<ActionResult> EditRegistration()
        {
            var data = await _ins.GetEmployee() ?? new List<Employee>();
            int Department = Convert.ToInt32(data.FirstOrDefault(p => p.EmployeeID == Request.Form["Employee_ID"])?.Department_ID);
            bool isSign = !string.IsNullOrEmpty(Request.Form["IsSign"]);

            string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
            string excelfilepath = Path.Combine(exportFolder, Request.Form["Filepath"]);

            // Set File Name For the Database
            string newFileName = Request.Form["RegNo"] + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            string outputPdfPath = newFileName.Replace(".xlsx", ".pdf");


            var obj = new RegistrationModel
            {
                RegNo = Request.Form["RegNo"],
                DateConduct = Request.Form["DateConduct"],
                Employee_ID = Request.Form["Employee_ID"],
                FullName = Request.Form["EmployeeSearch"],
                PIC = Request.Form["PIC"],
                PIC_Comments = Request.Form["PIC_Comments"],
                FilePath = outputPdfPath,
                Manager = Request.Form["Manager"],
                Manager_Comments = Request.Form["Manager_Comments"],
                IsSigned = isSign
            };

            string findJson = Request.Form["FindJson"];
            string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");
            bool result = await _ins.EditRegistration(obj, findJson);

            if (result)
            {
                if (System.IO.File.Exists(excelfilepath))  System.IO.File.Delete(excelfilepath);
                
                CacheHelper.Remove("Registration");
                await ExportFiler.SaveFileasPDF(obj, findJson, GlobalUtilities.DepartmentName(Department), newFileName, templatePath, isSign);
            }

            if (!result) JsonValidationError();
            return JsonCreated(result, "Updated successfully");

        }
        [HttpPost]
        public async Task<ActionResult> DeleteRegistration()
        {
            string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
            string excelfilepath = Path.Combine(exportFolder, Request.Form["Filepath"]);

            //Debug.WriteLine("PSDsadasd : " + Request.Form["Registration"]);
            bool result = await _ins.DeleteRegistration(Request.Form["Registration"]);

            if(result)
            {
                if (System.IO.File.Exists(excelfilepath))
                {
                    System.IO.File.Delete(excelfilepath);
                }
                CacheHelper.Remove("Registration");
            }

            var formData = GlobalUtilities.GetMessageResponse(result, 3, "Delete successful");
            return Json(formData, JsonRequestBehavior.AllowGet);    
        }



        [HttpPost]
        public ActionResult ExportExcel()
        {
            string templatePath = Server.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");

            // Don't use Server.MapPath here since this is an absolute path
            string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";

            string newFileName = "ExportedFile_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            string exportedFilePath = ExportFiler.ExportExcelTemplate(templatePath, exportFolder, newFileName);

            return Json(new { success = true, filePath = exportedFilePath }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExportToPdf()
        {
            try
            {
                string templatePath = Server.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");
                string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
                string newFileName = "ExportedFile_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string excelfilepath = Path.Combine(exportFolder, newFileName);

                string outputExcelPath = ExportFiler.ExportExcelTemplate(templatePath, exportFolder, newFileName);
                string outputPdfPath = outputExcelPath.Replace(".xlsx", ".pdf");

                // Step 1: Fill Excel with EPPlus
                using (var package = new ExcelPackage(new FileInfo(outputExcelPath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    worksheet.Cells["B2"].Value = "Registration No: ";
                    worksheet.Cells["B4"].Value = "Dept. /Section Inspected: ";
                    worksheet.Cells["C48"].Value = "ManagerComments";
                    worksheet.Cells["C53"].Value = "Manager:";
                    worksheet.Cells["D48"].Value = "Date Conducted: ";
                    worksheet.Cells["D50"].Value = "PIC COMMENTS";
                    package.Save();
                }

                // Force garbage collection to release file lock
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Step 2: Convert Excel to PDF using FreeSpire.XLS
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(outputExcelPath); // ensure the updated file is read
                workbook.SaveToFile(outputPdfPath, Spire.Xls.FileFormat.PDF);

                // Step 3: Delete the Excel file after generating PDF
                if (System.IO.File.Exists(excelfilepath))
                {
                    System.IO.File.Delete(excelfilepath);
                }

                return Json(new
                {
                    success = true,
                    message = "PDF generated successfully.",
                    filePath = outputPdfPath
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred: " + ex.Message
                });
            }
        }


        public ActionResult ViewPDF(string strfilepath)
        {
            // Map your RegNo to a file path or file bytes
            var filePath = $"\\\\SDP010F6C\\Users\\USER\\Pictures\\Access\\Excel\\" + strfilepath;

            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            return File(filePath, "application/pdf");
        }


        //[HttpPost]
        //public JsonResult SendEmail(EmailModel model)
        //{
        //    bool result = EmailService.SendEmail(model.To, model.Subject, model.Body);
        //    return Json(new { success = result, message = result ? "Email sent." : "Failed to send email." });
        //}



        // GET: PC/PatrolReport
        public ActionResult Dashboard() => View();
        // GET: PC/index
        public ActionResult index()
        {
            var viewModel = new UserViewModel
            {
                auth = new AuthModel(),
                reg = new RegisterModel()   
            };

            return View(viewModel); 
        }

        // GET: PC/Inspectors
        public ActionResult Inspectors() => View();
        // GET: PC/PatrolReport
        public ActionResult CheckSheet() => View();
        // GET: PC/PatrolReport
        //[RoleAuthorize("Manager", "SuperAdmin")]
        public ActionResult PatrolReport() => View();
        // GET: PC/PatrolReport
        public ActionResult PatrolSchedule() => View();
        // GET: PC/AddReports
        public ActionResult AddReports() => View();
        // GET: PC/PatrolReport
        public ActionResult PatrolReportDetails(string Regno) => View();
        public ActionResult Settings() => View();
    }
}