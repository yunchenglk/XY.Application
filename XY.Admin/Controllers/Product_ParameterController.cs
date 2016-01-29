using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    public class Product_ParameterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Product_Att_Key> datalist = Product_Att_KeyService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(string id, string val)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                Product_Att_Key entity = Product_Att_KeyService.instance().Single(ID);
                entity.Name = val;
                entity.CompanyID = UserDateTicket.Company.ID;
                return Json(Product_Att_KeyService.instance().Update(entity), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Product_Att_Key entity = new Product_Att_Key();
                entity.Name = val;
                entity.CompanyID = UserDateTicket.Company.ID;
                return Json(Product_Att_KeyService.instance().Insert(entity), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Single(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                Product_Att_Key m = Product_Att_KeyService.instance().Single(ID);
                return Json(new { status = 1, id = m.ID, name = m.Name }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Product_Att_Key m = Product_Att_KeyService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (Product_Att_KeyService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}