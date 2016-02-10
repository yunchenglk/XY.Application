using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    [XY.Admin.Filters.AuthorizeFilter]
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Recycle()
        {
            return View();
        }
        public ActionResult Create(string id)
        {
            Company m;
            if (string.IsNullOrEmpty(id))
                m = new Company();
            else
                m = CompanyService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            Company m = new Company();
            TryUpdateModel<Company>(m, form);
            m.Description = Server.UrlDecode(m.Description);
            if (m.ID == Guid.Empty)
                result.status = CompanyService.instance().Insert(m);
            else
                result.status = CompanyService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Company/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Company> datalist = CompanyService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Company m = CompanyService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (CompanyService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_All_NameID()
        {
            return Json(CompanyService.instance().GetEnum().Select(m => new { id = m.ID, name = m.Name }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult WeChart(string id)
        {
            wx_userweixin wxuser = wx_userweixinService.instance().SingleByCompanyID(new Guid(id));
            if (wxuser == null)
                wxuser = new wx_userweixin() { CompanyID = new Guid(id) };
            return View(wxuser);
        }
        [HttpPost]
        public JsonResult WeChart(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_userweixin m = new wx_userweixin();
            TryUpdateModel<wx_userweixin>(m, form);
            if (m.ID == Guid.Empty)
                result.status = wx_userweixinService.instance().Insert(m);
            else
                result.status = wx_userweixinService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Company/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}