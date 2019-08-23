using System.Web.Optimization;

namespace Answer.Web
{
    public class BundleConfig
    {
        // DO NOT INCLUDE MINIFIED SOURCE FILES IN BUNDLES
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js/fileinput")
                .Include("~/assets/js/BootstrapFileInput/js/fileinput-custom.js")
                .Include("~/assets/js/JqueryConfirm/jquery-confirm.min.js")
                .Include("~/assets/js/bluebird.min.js")
                .Include("~/assets/js/msr/fileupload/uploader.js")
                .Include("~/assets/js/msr/fileupload/uploader-grid.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid")
                .Include("~/assets/js/plugins/bootstrap3/bootstrap-dialog.js")
                .Include("~/assets/js/jquery.jqGrid.js")
                .Include("~/assets/js/i18n/grid.locale-en.js")
                .Include("~/assets/js/msr/grid-common.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-wip")
               .Include("~/assets/js/grids/engineer.js")
           );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-parts")
                .Include("~/assets/js/grids/parts.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-parttype")
                .Include("~/assets/js/grids/parttype.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-actualparts")
                .Include("~/assets/js/grids/actualparts.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-procedures")
                .Include("~/assets/js/grids/procedures.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-procedureverbs")
                .Include("~/assets/js/grids/procedureverbs.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-procedurestep")
                .Include("~/assets/js/grids/procedurestep.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-people")
                .Include("~/assets/js/grids/people.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-roles")
                .Include("~/assets/js/grids/user-roles.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-companies")
                .Include("~/assets/js/grids/companies.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-locations")
                .Include("~/assets/js/grids/locations.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-regions")
                .Include("~/assets/js/grids/regions.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-Monitors")
                .Include("~/assets/js/grids/Monitors.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-production-planning")
                .Include("~/assets/js/Grids/production-planning.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-purchase-order")
                .Include("~/assets/js/Grids/purchase-order.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-invoices")
                .Include("~/assets/js/grids/invoices.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-purchases")
                .Include("~/assets/js/Grids/purchases.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-approval-workflows")
                .Include("~/assets/js/grids/approval-workflows.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-approvalstages")
                .Include("~/assets/js/grids/approvalstages.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-approvalgroups")
                .Include("~/assets/js/grids/approvalgroups.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-equipment-maintenance")
                .Include("~/assets/js/grids/equipment-maintenance.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-delivery")
                .Include("~/assets/js/grids/delivery.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-documents")
                .Include("~/assets/js/grids/documents.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-help")
                .Include("~/assets/js/grids/help.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/js/sub-part")
                .Include("~/assets/js/msr/parts/PartUpdate.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/js/procedure-monitor")
                .Include("~/assets/js/procedure-monitor.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/js/wip-step-controls")
                .Include("~/assets/js/msr/wip/wip-step.js")
            );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js/pending-approvals")
                .Include("~/assets/js/grids/pending-approvals.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/js/object-search")
                .Include("~/assets/js/grids/object-search.js")
            );

            //LOGIN
            bundles.Add(new StyleBundle("~/assets/css/login")
               .Include("~/assets/css/animate.css")
               .Include("~/assets/css/login.css")
               .Include("~/assets/css/style.css")
            );

            bundles.Add(new ScriptBundle("~/assets/js/login")
                .Include("~/assets/js/jquery.js")
                .Include("~/assets/js/bootstrap.js")
            );

            //MAIN Layout

            // bootstrap by itself to solve import hella and default to min of minification fails
            bundles.Add(new StyleBundle("~/assets/css/bootstrap")
                .Include("~/assets/css/bootstrap.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/bundles/css/fileinput").Include(
                "~/assets/js/BootstrapFileInput/css/fileinput.min.css",
                "~/assets/js/JqueryConfirm/jquery-confirm.min.css")
            );

            bundles.Add(new StyleBundle("~/assets/css/main")
               .Include("~/assets/css/font-awesome.css", new CssRewriteUrlTransform())
               .Include("~/assets/css/animate.css")
               .Include("~/assets/css/bootstrap-switch.css")
               .Include("~/assets/css/checkbox3.css")
               .Include("~/assets/css/dataTables.bootstrap.css")
               .Include("~/assets/css/select2.css")
               .Include("~/assets/css/themes/flat-blue.css")
               .Include("~/assets/css/ui.jqgrid.css")
               .Include("~/assets/css/ui.jqgrid-bootstrap-ui.css")
               .Include("~/assets/css/ui.jqgrid-bootstrap.css")
               .Include("~/assets/css/style.css")
               .Include("~/assets/css/flexslider.css", new CssRewriteUrlTransform())
               .Include("~/assets/css/bootstrap-select.css")
               .Include("~/assets/css/bootstrap-datepicker.css")
               .Include("~/assets/css/bootstrap-datetimepicker.css")
               .Include("~/assets/css/wip-detail.css", new CssRewriteUrlTransform())
               .Include("~/assets/js/plugins/bootstrap3/bootstrap-multiselect.css", new CssRewriteUrlTransform())
           );

            bundles.Add(new ScriptBundle("~/assets/js/main")
                .Include("~/assets/js/jquery.js")
                .Include("~/assets/js/bootstrap.js")
                .Include("~/assets/js/Chart.js")
                .Include("~/assets/js/bootstrap-switch.js")
                .Include("~/assets/js/jquery.matchHeight.js")
                .Include("~/assets/js/jquery.dataTables.js")
                .Include("~/assets/js/select2.full.js")
                .Include("~/assets/js/ace/ace.js")
                .Include("~/assets/js/ace/mode-html.js")
                .Include("~/assets/js/ace/theme-github.js")
                .Include("~/assets/js/app.js")
                .Include("~/assets/js/help_form.js")
                .Include("~/assets/js/eModal.min.js")
                .Include("~/assets/js/docviewer.js")
                .Include("~/assets/js/printThis.js")
                .Include("~/assets/js/common.js")
                .Include("~/assets/js/bootstrap-datepicker.js")
                .Include("~/assets/js/plugins/jquery.blockUI.js")
                .Include("~/assets/js/plugins/bootstrap3/bootstrap-multiselect.js")
           );

            bundles.Add(new ScriptBundle("~/assets/js/jgridDefaults")
                .Include("~/assets/js/grids/jgrid.defaults.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/interceptors")
                .Include("~/assets/js/blockLayout.ui.js"));
       

            bundles.Add(new ScriptBundle("~/bundles/js/invoice-setup")
             .Include("~/assets/js/invoice-setup.js")
         );

            bundles.Add(new ScriptBundle("~/bundles/js/jqgrid-training")
               .Include("~/assets/js/grids/training.js")
           );

#if DEBUG
            BundleTable.EnableOptimizations = true;
#else
                            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
