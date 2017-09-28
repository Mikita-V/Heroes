using System.Web.Optimization;

namespace MVCPL
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax")
                .Include("~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom")
                .Include("~/Scripts/custom/site.js"));


            //Styles

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/custom")
                .Include("~/Content/custom/site.css"));
        }
    }
}
