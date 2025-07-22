using System.Web;
using System.Web.Optimization;

namespace ProgramPartListWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.min.css"));
            //bundles.Add(new StyleBundle("~/Content/css/css").Include(
            //     "~/Content/css/loginlayout.css",
            //     "~/Content/css/Main.css",
            //     "~/Content/css/Sidebar_one.css",
            //     "~/Content/css/Sidebar.css",
            //     "~/Content/css/Partslocator.css",
            //     "~/Content/css/PatrolLayout.css",
            //     "~/Content/css/Programpartlist.css"));
        }
    }
}
