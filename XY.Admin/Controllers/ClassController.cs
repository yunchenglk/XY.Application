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
    public class ClassController : Controller
    {

        // GET: Class
        public ActionResult Index()
        {
            return View();
        }
        // GET: Class/Create
        public ActionResult Create(string id)
        {
            Class m;
            if (string.IsNullOrEmpty(id))
                m = new Class() { Publisher = UserDateTicket.Company.Name };
            else
                m = ClassService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            return View(m);
        }

        // POST: Class/Create
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            Class m = new Class();
            TryUpdateModel<Class>(m, form);
            m.Pic = form["filePath"];
            m.Description = Server.UrlDecode(m.Description);
            if (m.ID == Guid.Empty)
                result.status = ClassService.instance().Insert(m);
            else
                result.status = ClassService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Class/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Class> datalist = ClassService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            datalist.Each(m =>
            {

                m.CompanyName = CompanyService.instance().GetNameByID(m.CompanyID);
                m.Childs = ClassService.instance().GetChildByID(m.ID, m.CompanyID);
            });
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Class m = ClassService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (ClassService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        // GET: Class/Delete/5
        public ActionResult Recycle()
        {
            return View();
        }

        public JsonResult GetTopClass(string cid)
        {
            return Json(ClassService.instance().GetChildByID(Guid.Empty, new Guid(cid)).Select(m => new { id = m.ID, name = m.Title }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChild(string id, string cid)
        {
            return Json(ClassService.instance().GetChildByID(new Guid(id), new Guid(cid)), JsonRequestBehavior.AllowGet);
        }

    }
}
