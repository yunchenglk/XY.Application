using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;
using XY.Util;

namespace XY.Admin.Controllers
{
    [XY.Admin.Filters.AuthorizeFilter]
    public class HomeController : Controller
    {

        public ActionResult JSCallBack()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}