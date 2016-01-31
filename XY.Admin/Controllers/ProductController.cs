using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ActionResult Index(string id)
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
        public ActionResult Create(string id, string pid)
        {
            if (string.IsNullOrEmpty(id))
                return Content("<script>alert(\"参数错误\");location.href=\"/\"</script>", "text/html");
            Class ml = new Class();
            if (CheckRoleService.instance().CheckRole_ClassID(new Guid(id), UserDateTicket.UID))
                ml = ClassService.instance().GetEnumByID(new Guid(id)).FirstOrDefault();
            else
                return Content("<script>alert(\"没有权限\")</script>", "text/html");
            Product m = new Product();
            if (string.IsNullOrEmpty(pid))
                m = new Product();
            else
                m = ProductService.instance().GetEnumByID(new Guid(pid)).FirstOrDefault();
            ViewBag.ClassName = ml.Title;
            ViewBag.ClassID = ml.ID;
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            Product m = new Product();
            TryUpdateModel<Product>(m, form);
            m.IsAudit = form["IsAudit"] == "1";
            m.IsRecommend = form["IsRecommend"] == "1";
            m.IsTop = form["IsTop"] == "1";
            m.Description = Util.Utils.ImgRemoveURL(Server.UrlDecode(m.Description));
            if (m.ID == Guid.Empty)
                result.status = ProductService.instance().Insert(m);
            else
                result.status = ProductService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = form["ResultURL"];
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Product> datalist = ProductService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Product m = ProductService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (ProductService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Recycle()
        {
            return View();
        }

        //参数
        public ActionResult Parameter(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public JsonResult GetAtt_val(string id)
        {
            return null;
        }
        [HttpPost]
        public JsonResult UpdateParameters(IEnumerable<_post_Product_Attr> items)
        {
            ResultBase_form result = new ResultBase_form();
            result.status = 1;
            result.msg = "操作成功";
            try
            {
                //清除数据
                Guid pid = items.First().ProductID;
                if (Product_Att_ValService.instance().DeleteByProductID(pid) > 0 && Product_PriceService.instance().DeleteByProductID(pid) > 0)
                {
                    foreach (var item in items)
                    {
                        //添加属性值
                        Product_Att_Val pv = new Product_Att_Val();
                        pv.ID = Guid.NewGuid();
                        pv.ProductID = pid;
                        pv.Value = item.Att_val;
                        pv.Att_Key_ID = item.Att_key;
                        pv.Short = item.Short;
                        if (Product_Att_ValService.instance().Insert(pv) > 0)
                        {
                            //添加价格
                            Product_Price pp = new Product_Price();
                            pp.ID = Guid.NewGuid();
                            pp.ProductID = pid;
                            pp.Price = item.Price;
                            pp.Stock = item.Stock;
                            pp.Att_Key = item.Att_key;
                            pp.Att_Val = pv.ID;
                            Product_PriceService.instance().Insert(pp);
                        }
                    }
                }
            }
            catch
            {
                result.status = 0;
                result.msg = "操作失败";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetParameters(string id)
        {
            Guid pid;
            List<_post_Product_Attr> result = new List<_post_Product_Attr>();
            if (Guid.TryParse(id, out pid))
            {
                IEnumerable<Product_Att_Val> pvs = Product_Att_ValService.instance().GetEnumByProductID(pid).OrderBy(m=>m.Short);
                foreach (var pv in pvs)
                {
                    _post_Product_Attr att = new _post_Product_Attr();
                    att.ProductID = pid;
                    att.Att_val = pv.Value;
                    att.Att_key = pv.Att_Key_ID;
                    Product_Price pp = Product_PriceService.instance().GetEnumByKVP(pid, pv.Att_Key_ID, pv.ID);
                    att.Price = pp.Price;
                    att.Stock = pp.Stock;
                    att.Short = pv.Short;
                    result.Add(att);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

}