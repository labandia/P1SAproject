using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class FinalAssemblyController : ExtendController
    {
        private readonly INCRDashboardRepository _manu;


        public FinalAssemblyController(INCRDashboardRepository manu) => _manu = manu;
        //======================================================
        //============== DASHBOARD  ===========
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> SampleGet()
        {
            string ip = ClientsInfo.GetClientIpAddress();
            string hostName = ClientsInfo.GetHostName(ip);
            var (account, email) = ClientsInfo.GetAccountAndEmail(hostName);

            var context = HttpContext;
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("REMOTE_ADDR: " + context.Request.ServerVariables["REMOTE_ADDR"]);
            sb.AppendLine("REMOTE_HOST: " + context.Request.ServerVariables["REMOTE_HOST"]);
            sb.AppendLine("HTTP_X_FORWARDED_FOR: " + context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            sb.AppendLine("HTTP_X_REAL_IP: " + context.Request.ServerVariables["HTTP_X_REAL_IP"]);
            sb.AppendLine("LOCAL_ADDR: " + context.Request.ServerVariables["LOCAL_ADDR"]);
            sb.AppendLine("Request.UserHostAddress: " + context.Request.UserHostAddress);

            return JsonSuccess(sb.ToString());
        }
        [HttpGet]
        public async Task<ActionResult> GetListof4ManFactor()
        {
            //await _manu.AutoUpdateShopOrderLine();
        //var info = ClientsInfo.GetClientInfo();
        //string account = ClientsInfo.GetLoggedInUserViaWmi(info.ComputerName);
        //Debug.WriteLine($@"Hostname: {info.ComputerName} - IP : {info.IpAddress} - Account name : {account}");




            var res = await _manu.GetFourMSummary();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetListofGroupProcess()
        {
            //await _manu.AutoUpdateShopOrderLine();


            var res = await _manu.GetGroupSummary();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }
        [HttpGet]
        public async Task<ActionResult> GetBestLineList()
        {
            //await _manu.AutoUpdateShopOrderLine();


            var res = await _manu.GetBestLines();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetProcessGroupList(int groups)
        {

            var res = await _manu.SetsProcessGroupData(groups);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetMonthyear()
        {
            var strmonth = await _manu.GetMonthName();
            return JsonSuccess(strmonth);
        }

        //======================================================
        //============== MANAGE DATA  ===========
        //====================================================
        [HttpGet]
        public async Task<ActionResult> GetRegistrationList(string search, int month = 0)
        {
            // 0 (or any value outside 1-12) falls back to the current month
            if (month < 1 || month > 12)
                month = DateTime.Today.Month;

            //await _manu.AutoUpdateShopOrderLine();
            var res = await _manu.GetRegistrationData(search, month);
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }

        [HttpPost]
        public async Task<ActionResult> SaveRegistration(RegistrationFinalModel model)
        {
            try
            {
      
                bool result = (model.NCRID == 0) ? await _manu.AddRegistrationData(model) : await _manu.EditRegistrationData(model);
                if (!result) return JsonPostError("Insert failed.", 500);


                return JsonCreated(result, "Update Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> deleteRegistration(int NCRID)
        {
            try
            {

                bool result = await _manu.DeleteRegistrationData(NCRID);
                if (!result) return JsonPostError("Insert failed.", 500);


                return JsonCreated(result, "Delete Registration Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }
        private static readonly string[] allowedExtensionsForCleanup = { ".png", ".jpg", ".jpeg" };

        [HttpPost]
        public async Task<ActionResult> DeleteAwardsData(int AwardID)
        {
            Debug.WriteLine("Awardss: " + AwardID);

            try
            {
                bool result = await _manu.DeleteAwardData(AwardID);
                if (!result) return JsonPostError("Insert failed.", 500);
                return JsonCreated(result, "Delete Award Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }



        [HttpPost]
        public async Task<ActionResult> UploadCertificate(string awardeesName, bool isDisplayed,
            HttpPostedFileBase certificateImage)
        {
            if (string.IsNullOrWhiteSpace(awardeesName))
                return JsonError("Name is required.");

            string newFileName = null;

            // Only save a new image if one was uploaded
            if (certificateImage != null && certificateImage.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(certificateImage.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(ext))
                {
                    return JsonError("Only PNG or JPEG files are allowed.");
                }
                newFileName = $"{Guid.NewGuid()}{ext}";
            }

            try
            {
                var folderPath = ConfigurationManager.AppSettings["CertificateUploadPath"];
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // ExistingFile is now a string
                string existingFileName = await _manu.GetAwardName();
                if (string.IsNullOrWhiteSpace(existingFileName))
                {
                    return JsonError("No existing awardee found with that name.");
                }

                if (newFileName != null)
                {
                    // clear the folder before saving — it should only ever hold the one
                    // current certificate, so nothing accumulates as an orphan
                    try
                    {
                        var filesToRemove = Directory.GetFiles(folderPath, "*.*")
                            .Where(f => allowedExtensionsForCleanup.Contains(Path.GetExtension(f).ToLowerInvariant()));

                        foreach (var filePath in filesToRemove)
                        {
                            try { System.IO.File.Delete(filePath); }
                            catch { /* TODO: log — file locked/in-use, continue with the rest */ }
                        }
                    }
                    catch { /* TODO: log — couldn't enumerate folder */ }

                    certificateImage.SaveAs(Path.Combine(folderPath, newFileName));
                }

                bool result = await _manu.EditAwardsData(new AwardDto
                {
                    WinnerName = awardeesName,
                    CertificateImage = newFileName, // null means don't replace image
                    IsDisplayed = isDisplayed
                });

                if (!result)
                    return JsonValidationError("Update failed.");

                return JsonSuccess(newFileName ?? existingFileName, "Awardee updated successfully");
            }
            catch (UnauthorizedAccessException)
            {
                return JsonError("Server lacks permission to write to the image share.");
            }
            catch (Exception)
            {
                return JsonError("Server error while saving the file.");
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetAwardData()
        {
            //await _manu.AutoUpdateShopOrderLine();
            var res = await _manu.GetAwardsData();
            if (res == null)
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }

        [HttpPost]
        public async Task<ActionResult> SaveAwardsData(AwardDto model, HttpPostedFileBase certificateImage)
        {

            //string newFileName = null;

            if (certificateImage != null && certificateImage.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var ext = Path.GetExtension(certificateImage.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(ext))
                    return JsonError("Only PNG or JPEG files are allowed.");

                var newFileName = $"{Guid.NewGuid()}{ext}";

                model.CertificateImage = newFileName;

                var folderPath = ConfigurationManager.AppSettings["CertificateUploadPath"];

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var savePath = Path.Combine(folderPath, newFileName);
                certificateImage.SaveAs(savePath);
            }

            bool result = model.AwardID > 0
                   ? await _manu.EditAwardsData(model)
                   : await _manu.AddAwardsData(model);


            if (!result)
                return JsonPostError(model.AwardID > 0 ? "Update failed." : "Insert failed.", 500);

            return JsonCreated(true, model.AwardID > 0 ? "Award updated successfully." : "Award saved successfully.");

        }

      

        // Streams the certificate image bytes from the network share to the browser
        public ActionResult CertificateImage(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
                return HttpNotFound();

            // Basic guard: only allow paths under the expected share root
            const string allowedRoot = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Certificate\";
            if (!path.StartsWith(allowedRoot, StringComparison.OrdinalIgnoreCase))
                return new HttpStatusCodeResult(403);

            var bytes = System.IO.File.ReadAllBytes(path);
            var contentType = System.Web.MimeMapping.GetMimeMapping(path); // e.g. image/png
            return File(bytes, contentType);
        }

        public ActionResult DisplaytheImage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return HttpNotFound();

            string folderPath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Certificate\";
            string fullPath = Path.Combine(folderPath, filename);

            if (!System.IO.File.Exists(fullPath))
                return File("~/Content/Images/no-image.png", "image/png");

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "image/png"); // Change to "image/jpeg" if needed
        }




        //======================================================
        //============== GROUP DASHBOARD DATA  ===========
        //====================================================
        [HttpGet]
        public async Task<ActionResult> GetGroupDashboardData()
        {

            var res = await _manu.GetGroupDataSummary();
            if (res == null)
                return JsonNotFound("No Active Lines found");
            return JsonSuccess(res);
        }


        [HttpGet]
        public async Task<ActionResult> GetAuditInfo()
        {
            try
            {
                var res = await _manu.GetAuditInfo();
                if (res == null)
                    return JsonNotFound("No Manpower data found");

                return JsonSuccess(res);
            }
            catch (Exception ex)
            {
                // TODO: log via your existing logging mechanism
                return Json(new { Success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveAuditInfo(string field, string value)
        {
            var allowedFields = new HashSet<string> { "Customer", "AuditDate", "AuditTime", "CoverageArea" };
            if (string.IsNullOrWhiteSpace(field) || !allowedFields.Contains(field))
                return Json(new { Success = false, Message = "Invalid field" });

            try
            {
    
                var current = await _manu.GetAuditInfo() ?? new AuditInfoModel();

                switch (field)
                {
                    case "Customer": current.Customer = value; break;
                    case "AuditDate": current.AuditDate = value; break;
                    case "AuditTime": current.AuditTime = value; break;
                    case "CoverageArea": current.CoverageArea = value; break;
                }

                current.ModifiedBy = User?.Identity?.Name ?? "Unknown";

                bool success = await _manu.SaveAuditInfo(current);
                return JsonCreated(success, "Update Audit Successfully");
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }




        // GET: Production1/FinalAssembly
        public ActionResult Dashboard()
        {
            return View();
        }


        // GET: Production1/FinalAssembly
        public ActionResult ManagementData()
        {
            return View();
        }

        // GET: Production1/FinalAssembly
        public ActionResult GroupData()
        {
            return View();
        }

    }
}