using System.Web;
using System.Web.Optimization;

namespace XY.Admin
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jqueryValidate").Include(
                                    "~/Scripts/jquery.validate.js",
                                    "~/Scripts/jquery.validate.cn.js",
                                    "~/Scripts/jquery.metadata.js",
                                    "~/Scripts/jquery.validate.Base.js",
                                    "~/Scripts/jquery.form.js"));
            //分页
            bundles.Add(new ScriptBundle("~/js/jqueryPage").Include(
                        "~/Scripts/jBootstrapPage.js",
                        "~/Scripts/jquery.page.js",
                        "~/Scripts/jquery-tmpl.js"));
            //编辑器
            bundles.Add(new ScriptBundle("~/js/xedit").Include(
                                  "~/Content/Edit/xheditor.js",
                                  "~/Content/Edit/xheditor_lang/zh-cn.js"));
        }
    }
}
