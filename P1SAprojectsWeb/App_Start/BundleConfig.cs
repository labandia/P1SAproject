using System.IO;
using System.Web;
using System.Web.Optimization;

namespace P1SAprojectsWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // =======================
            // 1. Validation + Modernizr
            // =======================
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // =======================
            // 2. SHARED CSS
            // =======================
            bundles.Add(new StyleBundle("~/Content/shared-css")
                .Include(
                    "~/Content/Shared/bootstrap.min.css",
                    "~/Content/Shared/site.min.css"
                ));

            // =======================
            // 2. SHARED JS (CORE LIBRARIES)
            // =======================     
            bundles.Add(new ScriptBundle("~/bundles/shared-js").Include(
                 "~/Scripts/all.min.js",
                 "~/Scripts/sweetalert2.min.js",
                 "~/Scripts/Cryptojs.min.js"
            ));


            // For Patrol Inspection  CSS
            bundles.Add(new StyleBundle("~/Content/patrol-css").Include("~/Content/Design/PatrolLayout.min.css"));

            // For Progam Partlist Inspection  CSS
            bundles.Add(new StyleBundle("~/Content/partlist-css").Include("~/Content/Design/Programpartlist.min.css"));

            // For Progam Partlist Inspection  CSS
            bundles.Add(new StyleBundle("~/Content/Hydro-css").Include("~/Content/Design/HydroDesign.min.css"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // =======================
            // 4. AUTO-DETECT AREA CSS/JS
            // =======================
            string contentPath = HttpContext.Current.Server.MapPath("~/Content");
            foreach (var dir in Directory.GetDirectories(contentPath))
            {
                string folderName = Path.GetFileName(dir);

                if (folderName.Equals("Shared", System.StringComparison.OrdinalIgnoreCase))
                    continue; // skip Shared

                string cssBundlePath = $"~/Content/{folderName.ToLower()}-css";
                bundles.Add(new StyleBundle(cssBundlePath)
                    .Include($"~/Content/{folderName}/*.css"));
            }
        }
    }
}
