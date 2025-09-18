using System.IO;
using System.Web;
using System.Web.Optimization;

namespace PMACS_V2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/Shared/jquery-{version}.js"));

            // =======================
            // 1. SHARED CSS
            // =======================
            bundles.Add(new StyleBundle("~/Content/shared-css")
                .Include(
                    "~/Content/Shared/bootstrap.min.css",
                    "~/Content/Shared/site.min.css",
                    "~/Content/Shared/sweetalert2.min.css"
                ));

            // =======================
            // 2. SHARED JS (CORE LIBRARIES)
            // =======================     
            bundles.Add(new ScriptBundle("~/bundles/shared-js").Include(
                 "~/Scripts/Shared/jquery-3.7.1.js",
                 "~/Scripts/Shared/jquery.validate.js",
                 "~/Scripts/Shared/all.min.js",
                 "~/Scripts/Shared/sweetalert2.min.js",
                 "~/Scripts/Shared/Utilities.min.js"
            ));

            // =======================
            // 3. Validation + Modernizr
            // =======================
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/Shared/jquery.validate*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            // For PMACS CSS
            bundles.Add(new StyleBundle("~/Content/pmacs-css").Include("~/Content/css/PMACS_Layout.min.css"));

            // For Planning Monitor CSS
            bundles.Add(new StyleBundle("~/Content/planning-css").Include("~/Content/css/Planning.min.css",
                 "~/Content/Shared/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/live-css").Include("~/Content/css/live.min.css",
                "~/Content/Shared/bootstrap.min.css"));


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
