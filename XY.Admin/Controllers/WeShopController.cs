using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;
using XY.Util;

namespace XY.Admin.Controllers
{
    public class WeShopController : Controller
    {
        private string GetToken()
        {
            WeShop m = WeShopService.instance().GetEneityByCompanyID(UserDateTicket.Company.ID);
            return WSApi.GetToken(m.appkey, m.secret);
        }
        public JsonResult GetAccessToken()
        {
            return Json(GetToken(), JsonRequestBehavior.AllowGet);
        }
        #region 基本信息
        // GET: WeShop
        public ActionResult Index()
        {
            WeShop m = WeShopService.instance().GetEneityByCompanyID(UserDateTicket.Company.ID);
            if (m == null)
                m = new WeShop();
            return View(m);
        }
        [HttpPost]
        public JsonResult Index(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            WeShop m = new WeShop();
            TryUpdateModel<WeShop>(m, form);
            m.CompanyID = UserDateTicket.Company.ID;
            if (m.ID == Guid.Empty)
                result.status = WeShopService.instance().Insert(m);
            else
                result.status = WeShopService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/WeShop/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 产品分类
        public JsonResult vdian_shop_cate_add(string id)
        {
            Class c = ClassService.instance().Single(new Guid(id));
            string result = WSApi.vdian_shop_cate_add(WSApiJson.vdian_shop_cate_add(GetToken(), c));
            string msg = Utils.GetJsonValue(result, "status_reason");
            if (msg == "success")
            {
                string catesJson = WSApi.vdian_shop_cate_get(GetToken());
                catesJson = JsonHelper.GetJsonValue(catesJson, "result");
                List<cates_result> list = JsonHelper.DeserializeJsonToList<cates_result>(catesJson);
                cates_result cate_ = list.Find(m => m.cate_name == c.Title);
                if (cate_ != null)
                {
                    c.cate_id = cate_.cate_id;
                    ClassService.instance().Update(c);
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult vdian_shop_cate_del(string id)
        {
            Class c = ClassService.instance().Single(new Guid(id));
            string result = WSApi.vdian_shop_cate_del(GetToken(), c.cate_id);
            string msg = Utils.GetJsonValue(result, "status_reason");
            c.cate_id = "";
            ClassService.instance().Update(c);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region 商品管理
        public ActionResult Product(string id)
        {
            Product p = ProductService.instance().Single(new Guid(id));
            p.Attr = Product_AttService.GetAttsByPID(p.ID);
            ViewBag.files = FilesService.instance().GetFilesByRelationID(p.ID);
            ViewBag.cate_ids = ClassService.instance().Single(p.ClassID).cate_id;
            return View(p);
        }
        [HttpPost]
        public JsonResult Product(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            Guid ID = new Guid(form["ID"]);
            Product p = ProductService.instance().Single(ID);
            if (!string.IsNullOrEmpty(p.itemid))
            {
                string del_msg = WSApi.vdian_item_delete(GetToken(), p.itemid);
                if (Utils.GetJsonValue(del_msg, "status_code") != "0")
                {
                    result.msg = Utils.GetJsonValue(del_msg, "status_reason");
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            product_item entity = new product_item();
            entity.item_name = form["Description"];
            entity.price = form["Price"];
            entity.stock = form["Stock"];
            entity.imgs = new List<string>();
            entity.cate_ids = new List<string>() { form["cate_ids"] };
            entity.skus = new List<skus>();
            entity.merchant_code = "";
            foreach (var item in FilesService.instance().GetFilesByRelationID(ID))
            {
                string reulst_msg = WSApi.upload(GetToken(), item.FilePath);
                if (Utils.GetJsonValue(reulst_msg, "status_code") == "0")
                {
                    string imgurl = Utils.GetJsonValue(reulst_msg, "result");
                    if (imgurl.Split('?').Length > 0)
                        entity.imgs.Add(imgurl.Split('?')[0]);
                    else
                        entity.imgs.Add(imgurl);
                }
            }
            foreach (var item in Product_AttService.GetAttsByPID(ID))
            {
                entity.skus.Add(new skus()
                {
                    price = item.price.Price.ToString(),
                    sku_merchant_code = "",
                    stock = item.price.Stock.ToString(),
                    title = item.key.Name + ":" + item.val.Value

                });
            };
            string json = WSApiJson.vdian_item_add(entity);
            string msg = WSApi.vdian_item_add(GetToken(), json);
            result.status = Convert.ToInt32(Utils.GetJsonValue(msg, "status_code"));
            if (result.status == 0)
            {
                p.itemid = Utils.GetJsonValue(msg, "itemid");
                p.opt = 1;
                ProductService.instance().Update(p);
            }
            result.msg = result.status == 0 ? "操作成功" : "操作失败";
            result.ResultURL = "/WeShop/Product/" + p.ID;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}