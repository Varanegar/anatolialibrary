using System.Web;
using System.Web.Optimization;

namespace Mvc_Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.cookie.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                     "~/Scripts/knockout-3.4.0.js",
                     "~/Scripts/knockout.mapping-latest.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                     "~/Scripts/kendo.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                     "~/Scripts/app.js"));
            #endregion
            
            #region css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/toastr.min.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                     "~/Content/KendoUI/kendo.common-material.min.css",
                     "~/Content/KendoUI/kendo.rtl.min.css",
                     "~/Content/KendoUI/kendo.material.min.css",
                     "~/Content/KendoUI/kendo.material.mobile.min.css"));
            #endregion
        }
    }
}
