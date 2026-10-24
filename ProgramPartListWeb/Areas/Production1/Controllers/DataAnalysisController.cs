using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Production1.Controllers
{
    public class DataAnalysisController : ExtendController
    {
        private readonly string certificatePath =
       @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Certificate";

        public ActionResult GetAwardeeImages()
        {
            try
            {
                if (!Directory.Exists(certificatePath))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Network folder does not exist or is inaccessible.",
                        data = new string[0]
                    }, JsonRequestBehavior.AllowGet);
                }

                string[] allowedExtensions =
                {
                ".jpg", ".jpeg", ".png",
                ".gif", ".bmp", ".webp"
            };

                var images = Directory
                     .GetFiles(certificatePath, "Awardee*.*", SearchOption.TopDirectoryOnly)
                     .Where(file => allowedExtensions.Contains(
                         Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                     .Select(file => Path.GetFileName(file))
                     .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                     .ToList();

                return Json(new
                {
                    success = true,
                    count = images.Count,
                    data = images
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    data = new string[0]
                }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult DisplaytheImage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return File(Server.MapPath("~/Content/Images/no-image.png"), "image/png");

            // Strip any directory info the caller tried to sneak in — keeps only the file name itself
            string safeFileName = Path.GetFileName(filename);
            Debug.WriteLine("CALL HERE" + safeFileName);

            if (string.IsNullOrWhiteSpace(safeFileName) || safeFileName != filename)
            {
                // filename contained path separators / traversal — reject it
                return File(Server.MapPath("~/Content/Images/no-image.png"), "image/png");
            }

            string folderPath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Certificate\";
            string fullPath = Path.Combine(folderPath, safeFileName);

            if (!System.IO.File.Exists(fullPath))
                return File(Server.MapPath("~/Content/Images/no-image.png"), "image/png");

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, GetContentType(fullPath));
        }

        private static string GetContentType(string path)
        {
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                default:
                    return "image/png";
            }
        }


        // GET: Production1/DataAnalysis
        public ActionResult Awardees()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}