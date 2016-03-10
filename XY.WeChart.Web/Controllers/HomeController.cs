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
    public class HomeController : _baseController
    {
        public ActionResult JSCallBack()
        {
            return View();
        }
        [Filters.AuthorizeFilter]
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
        public ActionResult wxContent(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_requestRuleContent content = wx_requestRuleContentService.instance().Single(ID);
                wx_requestRule rule = wx_requestRuleService.instance().Single(content.RuleID);
                Company com = CompanyService.instance().Single(rule.cID);
                ViewBag.content = content;
                ViewBag.rule = rule;
                ViewBag.com = com;
            }
            return View();
        }
    }



}