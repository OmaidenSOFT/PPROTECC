using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            //Javascript
            bundles.Add(new ScriptBundle("~/bundles/jqueryJs").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUnobstriveJs").Include(
                        "~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/semanticJs").Include(
                      "~/Content/SemanticUI/semantic.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/loadingOverlayJs").Include(
                      "~/Content/LoadingOverlay/src/loadingoverlay.js"
                      ));
            //Css
            bundles.Add(new StyleBundle("~/bundles/semanticCss").Include(
                      "~/Content/SemanticUI/semantic.css"));
            bundles.Add(new StyleBundle("~/bundles/siteCss").Include(
                      "~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/bundles/fontAwesomeCss").Include(
                      "~/Content/font-awesome-4.7.0/css/font-awesome.css"));
        }
    }
}
