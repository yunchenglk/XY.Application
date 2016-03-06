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
            UserDateTicket.wx_open = wx_openInfoService.instance().Single(new Guid("47012F6E-98C8-4207-AFAF-D9A15B1C39F0"));
            //UserDateTicket.Company = CompanyService.instance().Single(new Guid("02A07495-5484-4162-A70D-B7341096A1D4"));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
