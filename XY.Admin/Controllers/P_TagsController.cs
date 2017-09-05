using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    public class P_TagsController : Controller
    {
        public ActionResult Create(string id)
        {
            P_Tags m;
            if (string.IsNullOrEmpty(id))
                m = new P_Tags() { };
            else
                m = P_TagsService.instance().Single(new Guid(id));
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            P_Tags m = new P_Tags();
            TryUpdateModel<P_Tags>(m, form);
            //m.Description = Util.Utils.ImgRemoveURL(Server.UrlDecode(m.Description));
            if (m.ID == Guid.Empty)
                result.status = P_TagsService.instance().Insert(m);
            else
                result.status = P_TagsService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = form["ResultURL"];
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            P_Tags m = P_TagsService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (P_TagsService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, ID = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _Delete(string id)
        {
            P_Tags m = P_TagsService.instance().Single(new Guid(id));
            if (m != null)
            {
                P_TagsService.instance().Delete(new Guid(id));
            }
            return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<P_Tags> datalist = P_TagsService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            //datalist.Each(m =>
            //{ 
            //    m.CompanyName = P_BrandCategoriesService.instance().GetNameByID(m.CompanyID);
            //    m.Childs = P_BrandCategoriesService.instance().GetChildByID(m.ID, m.CompanyID);
            //});
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
 


        public ActionResult Recycle()
        {
            return View();
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}