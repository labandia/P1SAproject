using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProgramPartListWeb.Utilities;
using System.Diagnostics;

namespace ProgramPartListWeb.Controllers
{
    [CompressResponse]
    public class P1SAportalwebController : ExtendController
    {
   
        //############################  GET DATA  ###############################################//
        public async Task<ActionResult> GetProjectList()
        {
            string strquery = "SELECT Project_Name, Links, SystemImage, DepartmentID, Version " +
                                  "FROM ProjectList " +
                                  "WHERE DeviceID = 1 AND Project_Name != 'All'";
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
            Debug.WriteLine("HERE");
            // Map your RegNo to a file path or file bytes
            var filePath = $"\\\\SDP010F6C\\Users\\USER\\Pictures\\Access\\" + strfilepath;

            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            return File(filePath, "application/pdf");
        }


        // GET: P1SAportalweb
        public ActionResult Index() =>   View();
        // GET: GuideInstall
        public ActionResult GuideInstall() => View();
    }
}