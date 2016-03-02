using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;
using XY.Entity;
using XY.Services;
using XY.Services.Weixin;

namespace XY.WeChart.Web.Controllers
{
    [Filters.AuthorizeFilter]
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
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}