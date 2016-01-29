using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    [XY.Admin.Filters.AuthorizeFilter]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(string id)
        {
            Role m;
            if (string.IsNullOrEmpty(id))
                m = new Role();
            else
            {
                m = RoleService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
                m.Item_Authoritys.Each(n =>
                {
                    m.Item_AuthorityIDs = m.Item_AuthorityIDs + "|" + n.ID;
                });
                if (m.Item_Authoritys.Count() > 0)
                {
                    m.Item_AuthorityIDs = m.Item_AuthorityIDs.Substring(1);
                }
            }
            return View(m);
        }
        public ActionResult Recycle()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            IList<Authority> items = new List<Authority>();
            Role m = new Role();
            TryUpdateModel<Role>(m, form);
            foreach (var item in form["authIds"].Split('|'))
            {
                items.Add(AuthorityService.instance().GetEnumByID(new Guid(item)).FirstOrDefault());
            }
            m.Item_Authoritys = items;
            if (m.ID == Guid.Empty)
                result.status = RoleService.instance().Insert(m);
            else
                result.status = RoleService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Role/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Role> datalist = RoleService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Role m = RoleService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (RoleService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllJson()
        {
            return Json(RoleService.instance().GetEnum().Select(m => new { id = m.ID, name = m.Name }), JsonRequestBehavior.AllowGet);
        }
    }
}