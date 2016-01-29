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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET: User/Create
        public ActionResult Create(string id)
        {
            USER m;
            if (string.IsNullOrEmpty(id))
                m = new USER();
            else
                m = UserService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            return View(m);
        }
        public ActionResult Recycle()
        {
            return View();
        }
        // POST: User/Create
        [HttpPost]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            USER m = new USER();
            TryUpdateModel<USER>(m, form);
            if (m.ID == Guid.Empty)
                result.status = UserService.instance().Insert(m);
            else
                result.status = UserService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/User/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckLoginNameExists(string id, string LoginName)
        {
            return Json(UserService.instance().CheckUser(new Guid(id), LoginName), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<USER> datalist = UserService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            datalist.Each(m => m.CompanyName = CompanyService.instance().GetNameByID(m.CompanyID));
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            USER m = UserService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (UserService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Assign_roles(string id)
        {
            if (!UserDateTicket.IsSuper)
                return Content("<script>alert(\"没有权限\");history.go(-1)</script>", "text/html");
            ViewBag.UID = id;
            var ids = User_PK_Role_s_Service.instance().GetEnumByUID(new Guid(id)).Select(m => m.Role_ID.ToString()).ToList();
            if (ids.Count() == 0)
                ViewBag.ids = Guid.Empty;
            else
                ViewBag.ids = ids.Aggregate((i, j) => i.ToString() + "|" + j.ToString());
            return View();
        }
        public JsonResult UpdateUerRole(string uid, string ids)
        {
            User_PK_Role_s_Service.instance().Delete(new Guid(uid));
            if (!string.IsNullOrEmpty(ids))
                foreach (var item in ids.Split('|'))
                {
                    User_PK_Role_s_Service.instance().Insert(new User_PK_Role_s
                    {
                        User_ID = new Guid(uid),
                        Role_ID = new Guid(item)
                    });
                }
            return Json(new { status = 1, uid = uid }, JsonRequestBehavior.AllowGet);
        }

    }
}

