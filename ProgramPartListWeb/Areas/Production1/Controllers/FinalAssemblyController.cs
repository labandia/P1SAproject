using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Areas.Final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using ProgramPartListWeb.Utilities;
using DocumentFormat.OpenXml.EMMA;

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
    }
}