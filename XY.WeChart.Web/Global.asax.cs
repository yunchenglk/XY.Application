using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XY.Services;

namespace XY.WeChart.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UserDateTicket.wx_open = wx_openInfoService.instance().Single(new Guid("477F0554-837C-4D10-9C12-3DFE44B8DD60"));
            //UserDateTicket.Company = CompanyService.instance().Single(new Guid("02A07495-5484-4162-A70D-B7341096A1D4"));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
