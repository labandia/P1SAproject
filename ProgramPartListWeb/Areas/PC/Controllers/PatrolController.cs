
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Areas.PC.Repository;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using ProgramPartListWeb.Utilities.Common;
using ProgramPartListWeb.Utilities.Security;
using Spire.Xls;

namespace ProgramPartListWeb.Areas.PC.Controllers
{
    [RateLimiting(300, 1)] // Limits the No of Request
    public class PatrolController : ExtendController
    {
        private readonly IInspector _ins;
        private readonly IRegistration _reg;

        public static string strSender => ConfigurationManager.AppSettings["config:SMTPEmail"];
        // Default template path for PDF generation 
        string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");


        public PatrolController(IInspector ins, IRegistration reg)
        {
            _ins = ins;
            _reg = reg;
        }

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

        // GET: GetEmaiRegistration
        public async Task<ActionResult> GetEmaiRegistration()
        {
            var data = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();

            if (data == null || !data.Any())
                if (data == null || !data.Any()) return JsonNotFound("No Patrol Schedule today.");

            return JsonSuccess(data, "Load Emails");
        }

        // GET: GetEmailList
        public async Task<ActionResult> GetEmailList()
        {
            var data = await _ins.GetEmailsList() ?? new List<EmailRecepients>();

            if (data == null || !data.Any())
                if (data == null || !data.Any()) return JsonNotFound("No Patrol Schedule today.");

            return JsonSuccess(data, "Load Emails");
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
        public async Task<ActionResult> GetRegistrationNo()
        {
            //var data = await _ins.GetRegistrationData() ?? new List<PatrolRegistionModel>();
            var data = await _reg.GetRegistrationData(GetPrefix()) ?? new List<PatrolRegistrationViewModel>();
            if (data == null || !data.Any()) return JsonNotFound("No registration data found");

            return JsonSuccess(data, "Load Registration No#");
        }

        // GET: GetRegistrationNo
        public async Task<ActionResult> GetRegistrationNoDetails(string Regno)
        {
            // ===================== STEP 1: Retrieve Require Data =====================
            var data = await _reg.GetRegistrationData(GetPrefix()) ?? new List<PatrolRegistrationViewModel>();
            // ===================== STEP 2: Filter Only One Data =====================
            var filterdata = data.SingleOrDefault(res => res.RegNo == Regno);

            if (filterdata == null) return JsonNotFound("No registration data found");

            return JsonSuccess(filterdata, "Load Registration No#");
        }

        // GET: GetRegistrationNoByID
        public async Task<ActionResult> GetRegistrationFindings(string Regno)
        {
            //var data = await _ins.GetRegistrationData() ?? new List<PatrolRegistionModel>();
            var data = await _reg.GetRegisterFindings(Regno) ?? new List<FindingModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Findings data found");

            return JsonSuccess(data, "Load Registration No#");
        }



        // GET: GetRegistrationNoByID
        public async Task<ActionResult> GetRegistrationAllFiles(string Regno)
        {
            var data = await _reg.GetRegisterFiles(Regno) ?? new List<RegistrationFiles>();
            if (data == null || !data.Any()) return JsonNotFound("No Files data found");

            return JsonSuccess(data, "Load Registration Files #");
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
                var multidata = new Dictionary<string, object>
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
            if (!data.Any()) return JsonNotFound("No Inspectors data found");
            return JsonSuccess(data, "LoadInspectorData");
        }
        // GET: GetInpectsByApproval
        [JwtAuthorize]
        public async Task<ActionResult> GetInpectsByApproval()
        {
            var data = await _ins.GetInpectorsData() ?? new List<InspectorModel>();
            var filterdata = data.Where(p => p.Approval == 1).ToList();

            if (filterdata == null || !filterdata.Any()) return JsonNotFound("No Patrol data found");
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

                if (!result) return JsonPostError("Insert failed.", 500);

                CacheHelper.Remove("Inspectors");
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

                if (!result) return JsonPostError("Edit failed.", 500);

                CacheHelper.Remove("Inspectors");
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
        public async Task<ActionResult> AddRegistration()
        {
            try
            {
                // ===================== STEP 1: Extract Request Data =====================
                string regNo = Request.Form["RegNo"];
                string employeeId = Request.Form["Employee_ID"];
                string inspectId = Request.Form["SelectedInspector"];
                int departmentId = Convert.ToInt32(Request.Form["DepartmentID"]);
                string findJson = Request.Form["FindJson"];
                string getprefix = GetPrefix();
                string finalPrefix = $"{getprefix}-{regNo}";

                if (string.IsNullOrWhiteSpace(regNo) || string.IsNullOrWhiteSpace(employeeId))
                    return JsonValidationError("Missing required fields: RegNo or Employee_ID.");


                // ===================== STEP 2: Retrieve Required Data =====================
                var employeeEmail = await _reg.GetEmployeeEmailDetails(employeeId, 5, getprefix);


                // ===================== STEP 3: Prepare File Paths =====================
                string timestampFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string excelFileName = $"RN_{finalPrefix}_{timestampFile}.xlsx";
                string pdfOutputPath = excelFileName.Replace(".xlsx", ".pdf");


                // ===================== STEP 4: Create Process Object =====================
                var obj = new AddFormRegistrationModel
                {
                    RegNo = finalPrefix,
                    Department_ID = departmentId,
                    FilePath = pdfOutputPath,
                    PIC_ID = employeeId,
                    InspectorID = inspectId
                };


                bool isUpdated = await _reg.AddRegistration(obj, findJson);

                if (!isUpdated)
                    return JsonValidationError("Failed to update process owner information.");


                CacheHelper.Remove("Registration");

                // ===================== STEP 5: Generate Updated PDF =====================
                string departmentName = GlobalUtilities.DepartmentName(departmentId);

                await ExportFiler.SaveFileasPDFV2(
                    obj, findJson,
                    employeeEmail.FullName,
                    departmentName, pdfOutputPath,
                    templatePath, false
                );

                // ===================== STEP 6: Send Notification Email =====================
                string processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/ProcessOwner?Regno={finalPrefix}&mode=1";
                string strSubject = $@"[FOLLOW UP - REGISTRATION REPORT] 'For Review/Submit CounterMeasure' - {finalPrefix}";
                //string emailBody = EmailService.RegistrationEmailBody(employeeEmail.FullName, finalPrefix, processLink);

                var getPatrolView = await GetRegistrationDetailList(finalPrefix);
                var getFindings = await GetFindings(finalPrefix);


                string countermeasureEmail = PatrolEmailService.CreatePatrolProductionBody(
                    getPatrolView,
                    getFindings,
                    employeeEmail.FullName,
                    strSubject,
                    processLink,
                    "processowner"
                 );


                var sendEmail = new SentEmailModel
                {
                    Subject = strSubject,
                    Sender = strSender,
                    BCC = "",
                    Body = countermeasureEmail,
                    Recipient = employeeEmail.Email
                };

                await EmailService.SendEmailViaSqlDatabase(sendEmail);





                return JsonCreated(inspectId, "Updated successfully.");
            }
            catch (Exception ex)
            {
                return JsonValidationError("An unexpected error occurred while processing the request." + ex.Message);
            }
        }


      


        [HttpPost]
        public async Task<ActionResult> EditRegistration(HttpPostedFileBase[] Attachments)
        {
            var data = await _ins.GetEmployee() ?? new List<Employee>();
            int Department = Convert.ToInt32(data.FirstOrDefault(p => p.EmployeeID == Request.Form["Employee_ID"])?.Department_ID);
            bool isSign = !string.IsNullOrEmpty(Request.Form["IsSign"]);


            // Step 1:  Get existing record to get old Countermeasures Excel file path if needed
            var regData = await _ins.GetRegistrationData();
            string oldPatrolPath = regData.SingleOrDefault(res => res.RegNo == Request.Form["RegNo"]).PatrolPath;

            // Prepare new file names
            string newFileName = $"RN_{Request.Form["RegNo"]}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string outputPdfPath = newFileName.Replace(".xlsx", ".pdf");





            // Step 2: Checks If a new Excel file is uploaded
            List<string> newAttachements = new List<string>();
            if (Attachments != null && Attachments.Length > 0)
            {
                foreach (var file in Attachments)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = $"CM_{Request.Form["RegNo"]}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(file.FileName)}";
                        ExportFiler.SaveFileasExcel(file, fileName);
                        newAttachements.Add(fileName);
                    }
                }
            }

            // if there is a new File upload, append them to the old/current PatrolPaths 
            string newpatrolPaths = oldPatrolPath;
            // If there is a file
            if (newAttachements.Count > 0)
            {
                if (!string.IsNullOrEmpty(newpatrolPaths))
                    newpatrolPaths += ";" + string.Join(";", newAttachements);
                else
                    newpatrolPaths = string.Join(";", newAttachements);
            }


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
                PatrolPath = newpatrolPaths,
                IsSigned = isSign
            };

            string findJson = Request.Form["FindJson"];


            // Step 3: UPDATES the Information and PDF FILE
            bool result = await _ins.EditRegistration(obj, findJson);

            if (result)
            {
                CacheHelper.Remove("Registration");
                await ExportFiler.SaveFileasPDF(obj, findJson, GlobalUtilities.DepartmentName(Department), newFileName, templatePath, isSign);


            }

            if (!result) JsonValidationError();


            return JsonCreated(result, "Updated successfully");

        }

        [HttpPost]
        public async Task<ActionResult> ReturnEmailSubmit(string Regno, string Message, int ReportStatus)
        {
            string regNo = Request.Form["RegNo"];
            string Employee_ID = Request.Form["Employee_ID"];
            string getprefix = GetPrefix();
            string finalPrefix = $"{getprefix}-{regNo}";

            var emailList = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();
            var employeeEmail = new EmailModelV2();
            string processLink = "";


            if (ReportStatus == 2)
            {
                processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/InspectorsReview?Regno={finalPrefix}&mode=1";
                employeeEmail = emailList.FirstOrDefault(p => p.Employee_ID == Employee_ID);
            }
            else if (ReportStatus == 3)
            {
                employeeEmail = emailList.FirstOrDefault(p => p.Employee_ID == Employee_ID);
            }


            // ===================== STEP 8: Send Notification Email =====================

            string emailBody = EmailService.RegistrationEmailBody(employeeEmail.FullName, finalPrefix, processLink);

            var SendEmail = new SentEmailModel
            {
                Subject = "Patrol Inspection",
                Sender = strSender,
                BCC = "",
                Body = emailBody,
                Recipient = employeeEmail.Email
            };

            await EmailService.SendEmailViaSqlDatabase(SendEmail);

            return JsonCreated(true, "Updated successfully.");
        }

        // =======================================================================
        // =================  SAVE AND SUBMIT BY PROCESS OWNER ===================
        // =======================================================================
      
        [HttpPost]
        public async Task<ActionResult> ProcessOwnerSubmission(HttpPostedFileBase[] Attachments)
        {
            try
            {
                // ===================== STEP 1: Extract Request Data =====================
                string finalPrefix = Request.Form["RegNo"];
                string findJson = Request.Form["FindJson"];
                string getprefix = GetPrefix();
                string employeeId = Request.Form["Employee_ID"];

                // ===================== STEP 2: Retrieve Required Data =====================
                var emailList = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();
                var employeeEmail = await _reg.GetEmployeeEmailDetails(employeeId, 4, getprefix);    

                if (employeeEmail == null)
                    return JsonValidationError("Employee email not found.");

                var registrationData = await _reg.GetRegistrationData(GetPrefix());
                var existingReg = registrationData.SingleOrDefault(r => r.RegNo == finalPrefix);

                if (existingReg == null)
                    return JsonValidationError($"Registration record not found for RegNo {finalPrefix}.");

                string oldAttachmentPaths = existingReg.CounterPath;
                string previousPdfPath = existingReg.Filepath;


                // ===================== STEP 3: Handle Attachments =====================
                var newAttachments = new List<string>();

                if (Attachments != null && Attachments.Any())
                {
                    foreach (var file in Attachments)
                    {
                        if (file?.ContentLength > 0)
                        {
                            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            string fileName = $"CM_{finalPrefix}_{timestamp}{Path.GetExtension(file.FileName)}";

                            ExportFiler.SaveFileasExcel(file, fileName);
                            newAttachments.Add(fileName);
                        }
                    }
                }

                // if there is a new File upload, append them to the old/current PatrolPaths 
                string combinedAttachmentPaths = oldAttachmentPaths;

                // If there is a file
                if (newAttachments.Count > 0)
                {
                    combinedAttachmentPaths = string.IsNullOrEmpty(oldAttachmentPaths)
                        ? string.Join(";", newAttachments)
                        : $"{oldAttachmentPaths};{string.Join(";", newAttachments)}";
                }

                // ===================== STEP 4: Prepare File Paths =====================
                string timestampFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string excelFileName = $"RN_{finalPrefix}_{timestampFile}.xlsx";
                string pdfOutputPath = excelFileName.Replace(".xlsx", ".pdf");


                // ===================== STEP 5: Create Process Object =====================
                var processObj = new ProcessOwnerForms
                {
                    RegNo = Request.Form["RegNo"],
                    Employee_ID = Request.Form["Employee_ID"],
                    PIC_Comments = Request.Form["PIC_Comments"],
                    CounterPath = combinedAttachmentPaths,
                    Filepath = pdfOutputPath
                };
                // ===================== STEP 6: Update Records =====================
                bool isUpdated = await _reg.EditReg_ProcessOwner(processObj, findJson);
                if (!isUpdated)
                    return JsonValidationError("Failed to update process owner information.");



                // ===================== STEP 7: Generate Updated PDF =====================
                var updatedRegData = await _reg.GetRegistrationData(GetPrefix());
                var updatedReg = updatedRegData.SingleOrDefault(r => r.RegNo == finalPrefix);
                var updatedFindings = await _reg.GetRegisterFindings(finalPrefix);

                await ExportFiler.UpdatePDFRegistration(
                   updatedReg,
                   updatedFindings,
                   emailList,
                   previousPdfPath,
                   excelFileName,
                   templatePath
               );

                // ===================== STEP 8: Send Notification Email =====================
                string processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/InspectorsReview?Regno={finalPrefix}&mode=1";
                string strSubject = $@"[PATROL INSPECTION] 'For Review/Verification' - {finalPrefix}";
               
                var getPatrolView = await GetRegistrationDetailList(finalPrefix);
                var getFindings = await GetFindings(finalPrefix);

                string InspectorsEmail = PatrolEmailService.CreatePatrolProductionBody(
                    getPatrolView,
                    getFindings,
                    employeeEmail.FullName,
                    strSubject,
                    processLink,
                    "processowner"
                 );


                var SendEmail = new SentEmailModel
                {
                    Subject = strSubject,
                    Sender = strSender,
                    BCC = "",
                    Body = InspectorsEmail,
                    Recipient = employeeEmail.Email
                };


                await EmailService.SendEmailViaSqlDatabase(SendEmail);

                return JsonCreated(true, "Updated successfully.");
            }
            catch (Exception ex)
            {
                return JsonValidationError("An unexpected error occurred while processing the request." + ex.Message);
            }
        }
        // =======================================================================
        // ==============  INSPECTOR PROCESS BY APPROVED AND REVISE ==============
        // =======================================================================
        [HttpPost]
        public async Task<ActionResult> InspectorAproveSubmit(string Regno, string DateConduct)
        {
            try
            {
                string getprefix = GetPrefix();

                // ===================== STEP 1: Retrieve Required Data =====================
                var emailList = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();

                // SAFE: Use FirstOrDefault to avoid "more than one matching element"
                var employeeEmail = emailList
                    .FirstOrDefault(e => e.Position == 3 && e.DepPrefix == getprefix);

                if (employeeEmail == null)
                    return JsonValidationError("Employee email not found.");

                // SAFE: Get registration list
                var registrationData = await _reg.GetRegistrationData(getprefix);

                var regItem = registrationData
                    .FirstOrDefault(res => res.RegNo == Regno);

                if (regItem == null)
                    return JsonValidationError("Registration record not found.");

                string previousPdfPath = regItem.Filepath;

                // ===================== STEP 2: Prepare File Paths =====================
                string timestampFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string excelFileName = $"RN_{Regno}_{timestampFile}.xlsx";
                string pdfOutputPath = excelFileName.Replace(".xlsx", ".pdf");

                bool isUpdated = await _reg.ApproveByInspector(
                    Regno,
                    DateConduct,
                    pdfOutputPath,
                    employeeEmail.Employee_ID
                );

                if (!isUpdated)
                    return JsonValidationError("Failed to update process owner information.");

                CacheHelper.Remove("Registration");

                // ===================== STEP 3: Generate Updated PDF =====================
                var updatedRegData = await _reg.GetRegistrationData(getprefix);

                // SAFE: Get updated registration safely
                var updatedReg = updatedRegData
                    .FirstOrDefault(r => r.RegNo == Regno);

                if (updatedReg == null)
                    return JsonValidationError("Updated registration not found.");

                var updatedFindings = await _reg.GetRegisterFindings(Regno);

                
                await ExportFiler.UpdatePDFRegistration(
                    updatedReg,
                    updatedFindings,
                    emailList,
                    previousPdfPath,
                    excelFileName,
                    templatePath
                );
                

                // ===================== STEP 4: Send Notification Email =====================
                string processLink =
                    $"http://p1saportalweb.sdp.com/PC/Patrol/ManagerView?Regno={Regno}&mode=1";
   
                string strSubject = $@"[PATROL INSPECTION] 'For Review/Verification' - {Regno}";

                var getPatrolView = await GetRegistrationDetailList(Regno);
                var getFindings = await GetFindings(Regno);

                string ManagersEmail = PatrolEmailService.CreatePatrolProductionBody(
                    getPatrolView,
                    getFindings,
                    employeeEmail.FullName,
                    strSubject,
                    processLink,
                    "sendtomanager"
                 );

                var sendEmail = new SentEmailModel
                {
                    Subject = strSubject,
                    Sender = strSender,
                    BCC = "",
                    Body = ManagersEmail,
                    Recipient = employeeEmail.Email
                };
                await EmailService.SendEmailViaSqlDatabase(sendEmail);
                // ===================== Another Send Notification Email For the Process Owner  =====================



                return JsonCreated(true, "Updated successfully.");
            }
            catch (Exception ex)
            {
                return JsonValidationError(
                    "An unexpected error occurred while processing the request. " + ex.Message
                );
            }
        }

        // =======================================================================
        // ==============  MANAGER PROESS SAME WITH THE INSPECTORS ==============
        // =======================================================================
        [HttpPost]
        public async Task<ActionResult> ManagerApproveSubmit(string Regno, string Comments)
        {
            // 1. Get the RegNo for the ID update
            // 2. Press the Approve Button to update the ReportStatus to Approved or Revise
            // 3.  Send A email when Update is Done Approve or Revise
            // Option add An Remarks on the Revise to Notify the Process Owner On what to revise
            try
            {
                string getprefix = GetPrefix();

                // ===================== STEP 1: Retrieve Required Data =====================
                var emailList = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();
                var employeeEmail = emailList
                .SingleOrDefault(e => e.Position == 1 && e.DepPrefix == getprefix);


                if (employeeEmail == null)
                    return JsonValidationError("Employee email not found.");


                var registrationData = await _reg.GetRegistrationData(GetPrefix());
                string previousPdfPath = registrationData.SingleOrDefault(res => res.RegNo == Regno).Filepath;

                // ===================== STEP 2: Prepare File Paths =====================
                string timestampFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string excelFileName = $"RN_{Regno}_{timestampFile}.xlsx";
                string pdfOutputPath = excelFileName.Replace(".xlsx", ".pdf");

                bool isUpdated = await _reg.ApproveByDepartment(Regno, Comments, pdfOutputPath, employeeEmail.Employee_ID); // -- ID for the Deparment Manager

                if (!isUpdated)
                    return JsonValidationError("Failed to update process owner information.");

                CacheHelper.Remove("Registration");

                // ===================== STEP 3: Generate Updated PDF =====================
                var updatedRegData = await _reg.GetRegistrationData(GetPrefix());
                var updatedReg = updatedRegData.SingleOrDefault(r => r.RegNo == Regno);
                var updatedFindings = await _reg.GetRegisterFindings(Regno);

                await ExportFiler.UpdatePDFRegistration(
                    updatedReg,
                    updatedFindings,
                    emailList,
                    previousPdfPath,
                    excelFileName,
                    templatePath
                );

                // ===================== STEP 4: Send Notification Email =====================
                //string processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/DepartmentApproval?Regno={Regno}&mode=1";
                string processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/DivisionApproval?Regno={Regno}&mode=1";
                //string emailBody = EmailService.RegistrationEmailBody(employeeEmail.FullName, Regno, processLink);

                string strSubject = $@"[PATROL INSPECTION] 'For Review/Verification' - {Regno}";

                var getPatrolView = await GetRegistrationDetailList(Regno);
                var getFindings = await GetFindings(Regno);

                string ManagersEmail = PatrolEmailService.CreatePatrolProductionBody(
                    getPatrolView,
                    getFindings,
                    employeeEmail.FullName,
                    strSubject,
                    processLink,
                    "sendtomanager"
                 );


                var sendEmail = new SentEmailModel
                {
                    Subject = strSubject,
                    Sender = strSender,
                    BCC = "",
                    Body = ManagersEmail,
                    Recipient = employeeEmail.Email
                };

                await EmailService.SendEmailViaSqlDatabase(sendEmail);

                return JsonCreated(true, "Updated successfully.");
            }
            catch (Exception ex)
            {
                return JsonValidationError("An unexpected error occurred while processing the request." + ex.Message);
            }

        }
        // =======================================================================

        // =======================================================================
        // ==============  DEPARTMENT  SAME FLOW WITH THE INSPECTORS ==============
        // =======================================================================
        [HttpPost]
        public async Task<ActionResult> DepartmentApproveSubmit(string Regno)
        {
            // 1. Get the RegNo for the ID update
            // 2. Press the Approve Button to update the ReportStatus to Approved or Revise
            // 3.  Send A email when Update is Done Approve or Revise
            // Option add An Remarks on the Revise to Notify the Process Owner On what to revise
            try
            {
                string getprefix = GetPrefix();


                // ===================== STEP 1: Retrieve Required Data =====================
                var emailList = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();
                var employeeEmail = emailList
                .SingleOrDefault(e => e.Position == 1 && e.DepPrefix == getprefix);


                if (employeeEmail == null)
                    return JsonValidationError("Employee email not found.");


                var registrationData = await _reg.GetRegistrationData(GetPrefix());
                string previousPdfPath = registrationData.SingleOrDefault(res => res.RegNo == Regno).Filepath;

                // ===================== STEP 2: Prepare File Paths =====================
                string timestampFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string excelFileName = $"RN_{Regno}_{timestampFile}.xlsx";
                string pdfOutputPath = excelFileName.Replace(".xlsx", ".pdf");

                bool isUpdated = await _reg.ApproveByDivManager(Regno, pdfOutputPath, employeeEmail.Employee_ID); // -- ID for the Deparment Manager

                if (!isUpdated)
                    return JsonValidationError("Failed to update process owner information.");

                CacheHelper.Remove("Registration");

                // ===================== STEP 3: Generate Updated PDF =====================
                var updatedRegData = await _reg.GetRegistrationData(GetPrefix());
                var updatedReg = updatedRegData.SingleOrDefault(r => r.RegNo == Regno);
                var updatedFindings = await _reg.GetRegisterFindings(Regno);

                await ExportFiler.UpdatePDFRegistration(
                    updatedReg,
                    updatedFindings,
                    emailList,
                    previousPdfPath,
                    excelFileName,
                    templatePath
                );

                // ===================== STEP 4: Send Notification Email =====================
                string processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/DepartmentApproval?Regno={Regno}&mode=1";

                string ManagersEmail = EmailService.RegistrationEmailBodyV2(
                       employeeEmail.FullName,
                       Regno,
                       updatedReg.SectionName,
                       processLink,
                       "inpectorapproval"
                );

                var sendEmail = new SentEmailModel
                {
                    Subject = $@"[FOLLOW UP - PATROL INSPECTION REPORT] 'For Review/Verification' - {Regno}",
                    Sender = strSender,
                    BCC = "",
                    Body = ManagersEmail,
                    Recipient = employeeEmail.Email
                };


                await EmailService.SendEmailViaSqlDatabase(sendEmail);

                return JsonCreated(true, "Updated successfully.");
            }
            catch (Exception ex)
            {
                return JsonValidationError("An unexpected error occurred while processing the request." + ex.Message);
            }

        }
        // =======================================================================

        // =======================================================================
        // ==============  DEPARTMENT  SAME FLOW WITH THE INSPECTORS ==============
        // =======================================================================
        [HttpPost]
        public async Task<ActionResult> DivisionApproveSubmit(string Regno)
        {
            // 1. Get the RegNo for the ID update
            // 2. Press the Approve Button to update the ReportStatus to Approved or Revise
            // 3.  Send A email when Update is Done Approve or Revise
            // Option add An Remarks on the Revise to Notify the Process Owner On what to revise
            try
            {
                string getprefix = GetPrefix();


                // ===================== STEP 1: Retrieve Required Data =====================
                var emailList = await _reg.PatrolEmailData() ?? new List<EmailModelV2>();
                var employeeEmail = emailList
                .SingleOrDefault(e => e.Position == 1 && e.DepPrefix == getprefix);


                if (employeeEmail == null)
                    return JsonValidationError("Employee email not found.");


                var registrationData = await _reg.GetRegistrationData(GetPrefix());
                string previousPdfPath = registrationData.SingleOrDefault(res => res.RegNo == Regno).Filepath;

                // ===================== STEP 2: Prepare File Paths =====================
                string timestampFile = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string excelFileName = $"RN_{Regno}_{timestampFile}.xlsx";
                string pdfOutputPath = excelFileName.Replace(".xlsx", ".pdf");

                bool isUpdated = await _reg.ApproveByDivManager(Regno, pdfOutputPath, employeeEmail.Employee_ID); // -- ID for the Deparment Manager

                if (!isUpdated)
                    return JsonValidationError("Failed to update process owner information.");

                CacheHelper.Remove("Registration");

                // ===================== STEP 3: Generate Updated PDF =====================
                var updatedRegData = await _reg.GetRegistrationData(GetPrefix());
                var updatedReg = updatedRegData.SingleOrDefault(r => r.RegNo == Regno);
                var updatedFindings = await _reg.GetRegisterFindings(Regno);

                await ExportFiler.UpdatePDFRegistration(
                    updatedReg,
                    updatedFindings,
                    emailList,
                    previousPdfPath,
                    excelFileName,
                    templatePath
                );

                // ===================== STEP 4: Send Notification Email =====================
                string processLink = $"http://p1saportalweb.sdp.com/PC/Patrol/DepartmentApproval?Regno={Regno}&mode=1";

                string ManagersEmail = EmailService.RegistrationEmailBodyV2(
                       employeeEmail.FullName,
                       Regno,
                       updatedReg.SectionName,
                       processLink,
                       "inpectorapproval"
                );

                var sendEmail = new SentEmailModel
                {
                    Subject = $@"[FOLLOW UP - PATROL INSPECTION REPORT] 'For Review/Verification' - {Regno}",
                    Sender = strSender,
                    BCC = "",
                    Body = ManagersEmail,
                    Recipient = employeeEmail.Email
                };


                await EmailService.SendEmailViaSqlDatabase(sendEmail);

                return JsonCreated(true, "Updated successfully.");
            }
            catch (Exception ex)
            {
                return JsonValidationError("An unexpected error occurred while processing the request." + ex.Message);
            }

        }
        // =======================================================================



        [HttpPost]
        public async Task<ActionResult> DeleteRegistration()
        {
            string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
            string excelfilepath = Path.Combine(exportFolder, Request.Form["Filepath"]);

            //Debug.WriteLine("PSDsadasd : " + Request.Form["Registration"]);
            bool result = await _ins.DeleteRegistration(Request.Form["Registration"]);

            if (result)
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
        public async Task<ActionResult> SendingEmails(string Senders, string CC)
        {

            try
            {
                Debug.WriteLine("Senders : " + Senders);
                Debug.WriteLine("CC : " + CC);

                // Step 1: Split and clean Senders + CC
                var senderList = Senders?
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim().ToLower())
                    .Distinct()
                    .ToList() ?? new List<string>();

                var ccListOriginal = CC?
                  .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => s.Trim().ToLower())
                  .Distinct()
                  .ToList() ?? new List<string>();



                foreach (var sender in senderList)
                {
                    string creatbody = EmailService.CreateAEmailBody(
                            "JUAN DELA CRUZ",
                            "This is a sample email body used for testing purposes. Please disregard."
                        );

                    var SendEmail = new SentEmailModel
                    {
                        Subject = "Patrol Inspection",
                        Sender = strSender,
                        BCC = CC,
                        Body = creatbody,
                        Recipient = sender
                    };


                    await EmailService.SendEmailViaSqlDatabase(SendEmail);
                }

                return Json(new { success = true, message = "Email sent and saved successfully!" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

  

        public async Task<PatrolRegistrationViewModel> GetRegistrationDetailList(string Regno)
        {
            // ===================== STEP 1: Retrieve Require Data =====================
            var data = await _reg.GetRegistrationData(GetPrefix()) ?? new List<PatrolRegistrationViewModel>();
            // ===================== STEP 2: Filter Only One Data =====================
            var filterdata = data.SingleOrDefault(res => res.RegNo == Regno);
            return filterdata;
        }


        public async Task<List<FindingModel>> GetFindings(string Regno)
        {
            var data = await _reg.GetRegisterFindings(Regno) ?? new List<FindingModel>();
            return data;
        }

        [HttpPost]
        public async Task<ActionResult> UploadSignature(SignatureModel sig)
        {
            try
            {
                if (sig.SignatureImage != null && sig.SignatureImage.ContentLength > 0)
                {
                    //save file to the Database
                    string filename = Path.GetFileName(sig.SignatureImage.FileName);
                    var pathfile = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", filename);
                    // Ensure directory exists
                    var dir = Path.GetDirectoryName(pathfile);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    bool result = await _reg.SaveSignatureData(sig.UserID, filename);

                    // if the Image file name is Save 
                    if (result)
                    {
                        sig.SignatureImage.SaveAs(pathfile);
                    }


                    return Json(new { success = true, Data = filename,  message = "Upload Image successful" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Failed Upload" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
            if (string.IsNullOrEmpty(strfilepath))
                return HttpNotFound();

            string basePath;

            if (strfilepath.StartsWith("CM", StringComparison.OrdinalIgnoreCase))
            {
                basePath = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Countermeasure\";
            }
            else if (strfilepath.StartsWith("PF", StringComparison.OrdinalIgnoreCase)
                  || strfilepath.StartsWith("RF", StringComparison.OrdinalIgnoreCase))
            {
                basePath = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Countermeasure\";
            }
            else
            {
                basePath = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Registration\";
            }

            var filePath = Path.Combine(basePath, strfilepath);

            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            // Determine file extension
            string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

            // Check if it's an Excel file
            if (fileExtension == ".xlsx" || fileExtension == ".xls")
            {
                string contentType = fileExtension == ".xlsx"
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "application/vnd.ms-excel";

                // ✅ Download Excel file
                return File(filePath, contentType, Path.GetFileName(filePath));
            }

            // ✅ Default to PDF if not Excel
            if (fileExtension == ".pdf")
            {
                return File(filePath, "application/pdf");
            }

            // ❌ Unsupported file type
            return new HttpStatusCodeResult(415, "Unsupported file type");
        }



        public ActionResult ViewExcel(string strfilepath)
        {
            var filePath = $@"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Countermeasure\{strfilepath}";

            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            // Choose MIME type based on file extension
            string contentType = Path.GetExtension(filePath).ToLower() == ".xls"
                ? "application/vnd.ms-excel"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(filePath, "application/vnd.ms-excel", Path.GetFileName(filePath));
        }




        private System.Security.SecureString ConvertToSecureString(string password)
        {
            var secure = new System.Security.SecureString();
            foreach (char c in password)
                secure.AppendChar(c);
            secure.MakeReadOnly();
            return secure;
        }



        public ActionResult OpenFolder()
        {
            string folderPath = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Countermeasure";
            return Redirect("file:///" + folderPath.Replace("\\", "/"));
        }

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
        [CompressResponse]
        public ActionResult Inspectors() => View();
        // GET: PC/PatrolReport
        public ActionResult CheckSheet() => View();
        // GET: PC/PatrolReport
        //[RoleAuthorize("Manager", "SuperAdmin")]
        [CompressResponse]
        public ActionResult PatrolReport()
        {
            // 1️⃣ Get the browser (client) IP
            string clientIp = GetClientIpAddress(Request);

            // 2️⃣ Extract the 3rd segment (e.g., "1" from 172.29.1.121)
            string segment = GetSegment(clientIp, 3);


            Debug.WriteLine("IpAddress: " + clientIp);
            Debug.WriteLine("Third Segment: " + segment);

            ViewBag.ServerIP = clientIp;
            ViewBag.Segment = segment;

            return View();
        }
        // GET: PC/PatrolReport
        [CompressResponse]
        public ActionResult PatrolSchedule() => View();

        // GET: PC/AddReports
        [CompressResponse]
        public ActionResult AddReports() => View();
        // GET: PC/AddReports
        [CompressResponse]
        public ActionResult ReviewRegistration() => View();
        // GET: PC/PatrolReport
        [CompressResponse]
        public ActionResult PatrolReportDetails(string Regno) => View();
        [CompressResponse]
        public ActionResult Settings() => View();


        // =========== REGISTRATION FORM =====================
        // GET: PC/AddReports
        [CompressResponse]
        public ActionResult RegistrionNoForms()
        {

            ViewBag.Prefix = GetPrefix();

            return View();
        }
        // GET: PC/AddReports
        [CompressResponse]
        public ActionResult ProcessOwner(string Regno, int mode)
        {
            ViewBag.Regno = Regno;
            ViewBag.Mode = mode;
            return View();
        }
        // GET: PC/InspectorsReview
        [CompressResponse]
        public ActionResult InspectorsReview(string Regno, int mode)
        {
            ViewBag.Regno = Regno;
            ViewBag.Mode = mode;
            return View();
        }
        // GET: PC/AddReports
        [CompressResponse]
        public ActionResult ManagerView(string Regno, int mode)
        {
            ViewBag.Regno = Regno;
            ViewBag.Mode = mode;
            return View();
        }
        // GET: PC/AddReports
        [CompressResponse]
        public  ActionResult DepartmentApproval(string Regno, int mode)
        {
            ViewBag.Regno = Regno;
            ViewBag.Mode = mode;
            return View();
        }

        // GET: PC/AddReports
        [CompressResponse]
        public ActionResult DivisionApproval(string Regno, int mode)
        {
            ViewBag.Regno = Regno;
            ViewBag.Mode = mode;
            return View();
        }


        [CompressResponse]
        public ActionResult CompleteRegistration(string Regno)
        {
            ViewBag.Regno = Regno;
            return View();
        }


        private string GetClientIpAddress(HttpRequestBase request)
        {
            string ip = null;

            try
            {
                // Check forwarded headers (used by proxies, load balancers, reverse proxies)
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ip))
                {
                    // Sometimes multiple IPs are returned (client, proxy1, proxy2)
                    ip = ip.Split(',').First().Trim();
                }

                // Fallback if no proxy header is found
                if (string.IsNullOrEmpty(ip))
                    ip = request.ServerVariables["REMOTE_ADDR"];

                // Convert IPv6 loopback (::1) to IPv4 localhost (127.0.0.1)
                if (ip == "::1" || ip == "0:0:0:0:0:0:0:1")
                    ip = "127.0.0.1";
            }
            catch
            {
                ip = "Unknown";
            }

            return ip;
        }

        private string GetSegment(string ip, int segmentIndex)
        {
            if (string.IsNullOrEmpty(ip))
                return "0";

            var segments = ip.Split('.');
            if (segments.Length >= segmentIndex)
                return segments[segmentIndex - 1]; // 1-based index

            return "0";
        }


        private string GetPrefix()
        {
            // 1️⃣ Get the browser (client) IP
            string clientIp = GetClientIpAddress(Request);

            // 2️⃣ Extract the 3rd segment (e.g., "1" from 172.29.1.121)
            string segment = GetSegment(clientIp, 3);

            string prefix = (Convert.ToInt32(segment) == 2) ? "P1FA" : "P1SA";


            return prefix;
        }

        [JwtAuthorize]
        public async Task<ActionResult> HandleReportRoutingAsync(string Regno)
        {
            var getdata = await _reg.GetRegistrationData(GetPrefix());
            var report = getdata.SingleOrDefault(res => res.RegNo == Regno);
            string url = "";


            if (report == null || report.ReportStatus == 0)
                return HttpNotFound();

            switch (report.ReportStatus)
            {
                case 1:
                    url = Url.Action("ProcessOwner", new { Regno, mode = 0 });
                    break;
                case 2:
                    url = Url.Action("InspectorsReview", new { Regno, mode = 0 });
                    break;
                case 3:
                    url = Url.Action("ManagerView", new { Regno, mode = 0 });
                    break;
                case 4:
                case 5:
                    url = Url.Action("DepartmentApproval", new { Regno, mode = 0 });
                    break;
                default:
                    return View("UnknownStatus");
            }

            return JsonSuccess(url, "adsadasd");
        }


    }
}