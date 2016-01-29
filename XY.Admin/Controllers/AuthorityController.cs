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
    public class AuthorityController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(string id)
        {
            Authority m;
            if (string.IsNullOrEmpty(id))
                m = new Authority();
            else
                m = AuthorityService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            return View(m);
        }
        public ActionResult Recycle()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            Authority m = new Authority();
            TryUpdateModel<Authority>(m, form);
            if (m.ID == Guid.Empty)
                result.status = AuthorityService.instance().Insert(m);
            else
                result.status = AuthorityService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Authority/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Authority> datalist = AuthorityService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            datalist.Each(m =>
            {
                m.PIDName = AuthorityService.instance().GetNameByPID(m.PID);
                m.Childs = AuthorityService.instance().GetEnumByPID(m.ID).OrderBy(n => n.Sort);
            });
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Authority m = AuthorityService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (AuthorityService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllTop()
        {
            var result = AuthorityService.instance().GetAllTop1();
            return Json(result.Select(m => new { id = m.ID, name = m.Name }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChilds(string id)
        {
            return Json(AuthorityService.instance().GetChilds(new Guid(id)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItems(string id)
        {
            return Json(AuthorityService.instance().GetItems(), JsonRequestBehavior.AllowGet);
        }
    }
}