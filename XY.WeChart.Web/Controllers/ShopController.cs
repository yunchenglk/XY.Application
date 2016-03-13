using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;
using XY.Services.Weixin;

namespace XY.WeChart.Web.Controllers
{
    public class ShopController : _baseController
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }


        #region 分组管理
        public ActionResult Group()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPage_Group()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "DR|equal|0#Type|equal|2#CompanyID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<Class> datalist = ClassService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<Class>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GroupAdd(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                Class m = ClassService.instance().Single(ID);
                var result = groupadd(m);
                if (result.errcode == ReturnCode.请求成功)
                {
                    m.wx_group_id = result.group_id;
                    if (ClassService.instance().Update(m) == 1)
                    {
                        return Json("操作成功", JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json("保存到数据库错误", JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    return Json(result.errcode.ToString(), JsonRequestBehavior.AllowGet);
                }
            }
            return Json("参数错误", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GroupEdit(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                Class m = ClassService.instance().Single(ID);
                var result = groupedit(m);
                return Json(result.errcode.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json("参数错误", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GroupDel(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                Class m = ClassService.instance().Single(ID);
                var result = groupdel(m);
                if (result.errcode == ReturnCode.请求成功)
                {
                    m.wx_group_id = 0;
                    if (ClassService.instance().Update(m) == 1)
                    {
                        return Json("操作成功", JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json("保存到数据库错误", JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    return Json(result.errcode.ToString(), JsonRequestBehavior.AllowGet);
                }
            }
            return Json("参数错误", JsonRequestBehavior.AllowGet);
        }
        private AddGroupResult groupadd(Class m)
        {
            return ComShopApi.AddGroup(GetToken(), new AddGroupData()
            {
                group_detail = new GroupDetail()
                {
                    group_name = m.Title
                }
            });
        }
        private WxJsonResult groupedit(Class m)
        {
            return ComShopApi.PropertyModGroup(GetToken(), new PropertyModGroup()
            {
                group_id = m.wx_group_id,
                group_name = m.Title
            });
        }
        private WxJsonResult groupdel(Class m)
        {
            return ComShopApi.DeleteGroup(GetToken(), m.wx_group_id);
        }
        public JsonResult GroupAll(string id)
        {
            bool b;
            if (bool.TryParse(id, out b))
            {
                List<Class> list = ClassService.instance().GetEnumByCID(UserDateTicket.Company.ID).Where(m => m.Type == 2).ToList();
                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        if (b)
                        {
                            if (item.wx_group_id > 0)
                            {
                                groupedit(item);
                            }
                            else {
                                var result = groupadd(item);
                                if (result.errcode == ReturnCode.请求成功)
                                {
                                    item.wx_group_id = result.group_id;
                                    ClassService.instance().Update(item);
                                }
                            }
                        }
                        else {
                            if (item.wx_group_id > 0)
                            {
                                var result = groupdel(item);
                                if (result.errcode == ReturnCode.请求成功)
                                {
                                    item.wx_group_id = 0;
                                    ClassService.instance().Update(item);
                                }
                            }

                        }
                    }
                    return Json("操作完成", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("参数错误", JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}