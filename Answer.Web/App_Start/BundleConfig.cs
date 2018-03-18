using System.Web.Optimization;

namespace Answer.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js/fileinput")
                .Include("~/assets/js/BootstrapFileInput/js/fileinput.min.js")
                .Include("~/assets/js/JqueryConfirm/jquery-confirm.min.js")
                .Include("~/assets/js/bluebird.min.js")
                .Include("~/assets/js/msr/fileupload/uploader.js")
                .Include("~/assets/js/msr/fileupload/uploader-grid.js")
            );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/css/fileinput").Include(
                "~/assets/js/BootstrapFileInput/css/fileinput.min.css",
                "~/assets/js/JqueryConfirm/jquery-confirm.min.css")
            );
        }
    }
}
