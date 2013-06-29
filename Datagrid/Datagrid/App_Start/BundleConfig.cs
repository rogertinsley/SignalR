using System.Web.Optimization;

namespace Datagrid.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js")
                                                            .Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.js"));
            bundles.Add(new ScriptBundle("~/bundles/signalr").Include("~/Scripts/jquery.signalR-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/knockoutjs").Include("~/Scripts/knockout-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/application").Include("~/Scripts/js/app.js"));
        }
    }
}