using System.Web;
using System.Web.Optimization;

namespace XY.WeChart.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jqueryValidate").Include(
                "~/Content/js/jquery.metadata.js",
                "~/Content/js/plugins/jquery.validate.min.js",
                "~/Content/js/jquery.form.js",
                "~/Content/js/jquery.form.base.js",
                "~/Content/js/plugins/jquery.uniform.min.js"
                ));
        }
    }
}
