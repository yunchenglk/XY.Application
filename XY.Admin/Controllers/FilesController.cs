using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    public class FilesController : Controller
    {
        [HttpPost]
        public JsonResult AddFiles(string RelationID, string files, bool isDel)
        {
            string[] arrFile = Server.UrlDecode(files).Split('|');
            if (isDel)
                FilesService.instance().DeleteByRelationID(new Guid(RelationID));
            foreach (var item in arrFile)
            {
                Files f = new Files();
                f.ID = Guid.NewGuid();
                f.CompanyID = UserDateTicket.Company.ID;
                f.RelationID = new Guid(RelationID);
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
        public ActionResult AddFiles(string id)
        {
            ViewBag.RelationID = id;
            return View();
        }
    }
}