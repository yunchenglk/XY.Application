using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Services;
using XY.Entity;
using System.Text.RegularExpressions;
using System.Configuration;

namespace XY.Admin.Controllers
{
    public class GeneralController : Controller
    {
        public ActionResult About(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Content("<script>alert(\"参数错误\");location.href=\"/\"</script>", "text/html");
            Class m = new Class();
            if (CheckRoleService.instance().CheckRole_ClassID(new Guid(id), UserDateTicket.UID))
                m = ClassService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            else
                return Content("<script>alert(\"没有权限\")</script>", "text/html");
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult About(FormCollection form)
        {
            string id = form["ID"];
            if (string.IsNullOrEmpty(id))
                return Content("<script>alert(\"参数错误\");</script>", "text/html");
            Class m = ClassService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            m.Description = Util.Utils.ImgRemoveURL(Server.UrlDecode(form["Description"]));
            //string url = "";
            //System.Text.RegularExpressions.Regex.Replace(m.Description, "src=['|\"]([^\"]+)['|\"]", "src='" + url + "$1'", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            int result = ClassService.instance().Update(m);
            if (result == 0)
                return Content("<script>alert(\"操作失败,请稍后重试.\");window.history.go(-1);</script>", "text/html");
            return Content("<script>alert(\"操作成功.\");location.href=\"/General/About/" + m.ID + "\"</script>", "text/html");
        }
        public JsonResult Delete(string id)
        {
            News m = NewsService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (NewsService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewEdit(string id, string nid)
        {
            if (string.IsNullOrEmpty(id))
                return Content("<script>alert(\"参数错误\");location.href=\"/\"</script>", "text/html");
            Class ml = new Class();
            if (CheckRoleService.instance().CheckRole_ClassID(new Guid(id), UserDateTicket.UID))
                ml = ClassService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            else
                return Content("<script>alert(\"没有权限\")</script>", "text/html");
            News m = new News();
            if (string.IsNullOrEmpty(nid))
                m = new News();
            else
                m = NewsService.instance().GetEnumByID(new Guid(nid)).FirstOrDefault();
            ViewBag.ClassName = ml.Title;
            ViewBag.ClassID = ml.ID;
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult NewEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            News m = new News();
            TryUpdateModel<News>(m, form);
            m.IsAudit = form["IsAudit"] == "1";
            m.IsRecommend = form["IsRecommend"] == "1";
            m.IsTop = form["IsTop"] == "1";
            m.IsComm = form["IsComm"] == "1";
            m.IsVote = form["IsVote"] == "1";
            m.Description = Util.Utils.ImgRemoveURL(Server.UrlDecode(m.Description));
            //m.SlidePic = form["filePath"];
            if (m.ID == Guid.Empty)
                result.status = NewsService.instance().Insert(m);
            else
                result.status = NewsService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/General/NewList/" + m.ClassID;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewList(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Content("<script>alert(\"参数错误\");location.href=\"/\"</script>", "text/html");
            Class ml = new Class();
            if (CheckRoleService.instance().CheckRole_ClassID(new Guid(id), UserDateTicket.UID))
                ml = ClassService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            else
                return Content("<script>alert(\"没有权限\")</script>", "text/html");
            ViewBag.ClassID = ml.ID;
            ViewBag.ClassName = ml.Title;
            return View();
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<News> datalist = NewsService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Files(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public JsonResult UpFiles(string id, string files)
        {

            string[] arrFile = Server.UrlDecode(files).Split('|');
            FilesService.instance().DeleteByRelationID(new Guid(id));
            foreach (var item in arrFile)
            {
                Files f = new Files();
                f.ID = Guid.NewGuid();
                f.CompanyID = UserDateTicket.Company.ID;
                f.RelationID = new Guid(id);
                f.Type = 0;
                f.Large = f.FilePath = f.Middle = f.Small = item;
                f.FileExt = System.IO.Path.GetExtension(item);
                f.FileSize = Util.Utils.GetFileSize_(System.Configuration.ConfigurationManager.AppSettings["sourceWeb"] + item);
                FilesService.instance().Insert(f);

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFiles(string id)
        {
            //4f251b12-29aa-4324-a882-c17e3757946d
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                return Json(FilesService.instance().GetFilesByRelationID(ID).Select(m => new { m.ID, m.RelationID, m.FilePath, m.FilePathStr }), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

    }
}