using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProgramPartListWeb.Utilities;
using System.Diagnostics;
using System;

namespace ProgramPartListWeb.Controllers
{
    public class P1SAportalwebController : ExtendController
    {
   
        //############################  GET DATA  ###############################################//
        public async Task<ActionResult> GetProjectList()
        {
            string strquery = $@"SELECT Project_Name, Links, SystemImage, DepartmentID, Version 
                                  FROM ProjectList 
                                  WHERE DeviceID = 1 AND Project_Name != 'All'";
            var data = await SqlDataAccess.GetData<ProjectsModel>(strquery);
            if (data == null || !data.Any())
                return JsonNotFound("No Projects data found");

            return JsonSuccess(data);
        }

        public ActionResult SampleEmail()
        {
            EmailService.SendEmailOutlook("jaye.labandia@sanyodenki.com", "Test", "Sample Body"); 
            return Json("GOOD", JsonRequestBehavior.AllowGet);
        }

        // IMAGE DISPLAY
        public ActionResult Get(string fileName)
        {
            var filePath = Path.Combine(@"\\SDP010F6C\Users\USER\Pictures\Access\SystemApps", fileName ?? "No_Data.png");

            if (!System.IO.File.Exists(filePath))
                filePath = Server.MapPath("~/Content/Images/No_Data.png");

            var mimeType = MimeMapping.GetMimeMapping(filePath);
            return File(System.IO.File.ReadAllBytes(filePath), mimeType);
        }


        public ActionResult ViewPDF(string strfilepath)
        {
            var filePath = $"\\\\SDP010F6C\\Users\\USER\\Pictures\\Access\\" + strfilepath;

            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            return File(filePath, "application/pdf");
        }


        [HttpPost]
        public ActionResult RunConsoleAppV2()
        {
            // Maps virtual path ~/Contents/OpenExcelApp.exe to physical path
            string exePath = Server.MapPath("~/Content/OpenExcelApp.exe");

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = false,       // Required for IIS
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true          // Prevent server window
                };

                using (Process proc = Process.Start(psi))
                {
                    proc.WaitForExit(10000); // optional timeout
                    string output = proc.StandardOutput.ReadToEnd();
                    string errors = proc.StandardError.ReadToEnd();

                    return Content($"Console app executed.\nOutput: {output}\nErrors: {errors}");
                }
            }
            catch (Exception ex)
            {
                return Content($"Failed to run console app: {ex.Message}");
            }
        }

        // GET: P1SAportalweb
        public ActionResult Index() =>   View();
        // GET: GuideInstall
        public ActionResult GuideInstall() => View();

        public ActionResult SampleView() => View();
    }
}