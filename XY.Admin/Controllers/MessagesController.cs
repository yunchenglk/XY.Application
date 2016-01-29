using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    [Filters.AuthorizeFilter]
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Show(string id)
        {
            Messages result = MessagesService.instance().Single(new Guid(id));
            return View(result);
        }
        public ActionResult MessagesDetail(string id)
        {
            Messages result = MessagesService.instance().Single(new Guid(id));
            return View(result);
        }
        public JsonResult GetAllessages_Reply(string id)
        {
            Messages result = MessagesService.instance().Single(new Guid(id));
            result.ReplyItems.Each(m =>
            {
                if (m.IsChild)
                    m.ChildItem = Messages_ReplyService.instance().GetChild(m.ID);
            });
            return Json(result.ReplyItems, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult MessagesDetail(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            Messages_Reply m = new Messages_Reply();
            TryUpdateModel<Messages_Reply>(m, form);
            m.Content = Server.UrlDecode(m.Content);
            if (m.ID == Guid.Empty)
                result.status = Messages_ReplyService.instance().Insert(m);
            else
                result.status = Messages_ReplyService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Class/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Messages> datalist = MessagesService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            datalist.Each(m =>
            {

            });
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Messages_Reply m = Messages_ReplyService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (Messages_ReplyService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }



    }
}