using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Create(string id, string nid)
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
            else {
                m = NewsService.instance().GetEnumByID(new Guid(nid)).FirstOrDefault();
                Files file = FilesService.instance().Single(new Guid(nid));
                ViewBag.filepath = file == null ? "" : file.FilePath;
            }
            ViewBag.ClassID = id;
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            News m = new News();
            TryUpdateModel<News>(m, form);
            m.IsAudit = form["IsAudit"] == "1";
            m.IsRecommend = form["IsRecommend"] == "1";
            m.IsTop = form["IsTop"] == "1";
            m.Description = Util.Utils.ImgRemoveURL(Server.UrlDecode(m.Description));

            if (m.ID == Guid.Empty)
            {
                m.ID = Guid.NewGuid();
                result.status = NewsService.instance().Insert(m);
            }
            else
                result.status = NewsService.instance().Update(m);
            if (result.status == 1)
            {
                //video_file
                string file = form["video_file"];
                Files f = new Files(); // new Files();
                f.ID = m.ID;
                f.Type = 1;
                f.FilePath = file;
                f.Large = f.Middle = f.Small = m.SlidePic;
                f.CompanyID = UserDateTicket.Company.ID;
                f.FileExt = Util.Utils.GetFileExt(file);
                f.FileSize = Util.Utils.GetFileSize(file);
                if (FilesService.instance().Single(f.ID) == null)
                    FilesService.instance().Insert(f);
                else
                    FilesService.instance().Update(f);

            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/Video/Index/" + m.ClassID;
            return Json(result, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<News> datalist = NewsService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
    }
}