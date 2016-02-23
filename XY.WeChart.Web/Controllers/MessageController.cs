using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services.WeChart;
using XY.Services.Weixin;

namespace XY.WeChart.Web.Controllers
{
    [Filters.AuthorizeFilter]
    public class MessageController : _baseController
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            Guid ID;
            wx_customer m = new wx_customer();
            if (Guid.TryParse(id, out ID))
            {
                m = wx_customerService.instance().Single(ID);
            }
            return View(m);
        }
        [HttpPost]
        public JsonResult Edit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_customer m = new wx_customer();
            TryUpdateModel<wx_customer>(m, form);
            m.wID = UserDateTicket.wx_user.ID;
            m.cID = UserDateTicket.Company.ID;
            if (m.ID == Guid.Empty)
            {
                var resultcode = CommonApi.Custom_Add(GetToken(), new Entity.Weixin.Custom_add
                {
                    kf_account = m.kf_account + "@" + UserDateTicket.wx_user.weixinCode,
                    nickname = m.nickname,
                    password = Util.Utils.MD5(m.password)
                });
                if (resultcode.errcode == Entity.Weixin.ReturnCode.请求成功)
                    result.status = wx_customerService.instance().Insert(m);
                else
                    result.msg = resultcode.errcode.ToString();
            }
            else
            {
                var resultcode = CommonApi.Custom_Edit(GetToken(), new Entity.Weixin.Custom_add
                {
                    kf_account = m.kf_account + "@" + UserDateTicket.wx_user.weixinCode,
                    nickname = m.nickname,
                    password = Util.Utils.MD5(m.password)
                });
                if (resultcode.errcode == Entity.Weixin.ReturnCode.请求成功)
                    result.status = wx_customerService.instance().Update(m);
                else
                    result.msg = resultcode.errcode.ToString();
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_customer> datalist = wx_customerService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_customer>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckExists(string id, string kf_account)
        {
            return Json(wx_customerService.instance().CheckExists(new Guid(id), UserDateTicket.Company.ID, kf_account), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_customer entity = wx_customerService.instance().Single(ID);
                if (entity != null)
                {
                    var resultcode = CommonApi.Custom_Del(GetToken(), new Entity.Weixin.Custom_add()
                    {
                        kf_account = entity.kf_account + "@" + UserDateTicket.wx_user.weixinCode,
                        nickname = entity.nickname,
                        password = Util.Utils.MD5(entity.password)
                    });
                    if (resultcode.errcode == Entity.Weixin.ReturnCode.请求成功)
                        wx_customerService.instance().Delete(entity.ID);
                    return Json(resultcode.errcode.ToString(), JsonRequestBehavior.AllowGet);

                }

            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
    }
}