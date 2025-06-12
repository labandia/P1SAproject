using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProgramPartListWeb.Services;
using System.IO;
using ProgramPartListWeb.Utilities;

namespace ProgramPartListWeb.Controllers
{
    [GlobalErrorException]
    [CompressResponse]
    public class P1SAportalwebController : ExtendController
    {
   
        //############################  GET DATA  ###############################################//
        public async Task<ActionResult> GetProjectList()
        {
            try
            {
                string strquery = "SELECT Project_Name, Links, SystemImage, DepartmentID, Version " +
                                  "FROM ProjectList " +
                                  "WHERE DeviceID = 1 AND Project_Name != 'All'";
                var data = await SqlDataAccess.GetData<ProjectsModel>(strquery);
                if (data == null || !data.Any())
                    return JsonNotFound("No registration data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
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



        // GET: P1SAportalweb
        public ActionResult Index() =>   View();
        // GET: GuideInstall
        public ActionResult GuideInstall() => View();
    }
}