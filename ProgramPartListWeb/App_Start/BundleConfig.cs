using System.IO;
using System.Web;
using System.Web.Optimization;

namespace ProgramPartListWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // =======================
            // 1. SHARED CSS
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
                 "~/Scripts/Shared/jquery-{version}.js",
                 "~/Scripts/jquery.validate*",
                 "~/Scripts/all.min.js",
                 "~/Scripts/sweetalert2.min.js",
                 "~/Scripts/Cryptojs.min.js"
            ));


            // =======================
            // 3. Validation + Modernizr
            // =======================
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));


            // For Patrol Inspection  CSS
            bundles.Add(new StyleBundle("~/Content/patrol-css").Include("~/Content/css/PatrolLayout.min.css"));

            // For Progam Partlist Inspection  CSS
            bundles.Add(new StyleBundle("~/Content/partlist-css").Include("~/Content/css/Programpartlist.min.css"));


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

            string scriptsPath = HttpContext.Current.Server.MapPath("~/Scripts");
            foreach (var dir in Directory.GetDirectories(scriptsPath))
            {
                string folderName = Path.GetFileName(dir);

                if (folderName.Equals("Shared", System.StringComparison.OrdinalIgnoreCase))
                    continue; // skip Shared

                string jsBundlePath = $"~/bundles/{folderName.ToLower()}-js";
                bundles.Add(new ScriptBundle(jsBundlePath)
                    .Include($"~/Scripts/{folderName}/*.js"));
            }

            // =======================
            // Enable Bundling/Minification
            // =======================
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;
        }
    }
}
