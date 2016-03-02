using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;
using XY.Services.Weixin;

namespace XY.WeChart.Web.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index(string id)
        {
            wx_userweixin m = new wx_userweixin() { DR = true };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {

                    var infoRes = ComponentApi.GetAuthorizerInfo(UserDateTicket.wx_open.open_access_token,
                           UserDateTicket.wx_open.open_sAppID,
                           UserDateTicket.wx_user.AppId);
                    if (infoRes.errcode == Entity.Weixin.ReturnCode.请求成功)
                    {
                        UserDateTicket.wx_user.wxName = infoRes.authorizer_info.nick_name;
                        UserDateTicket.wx_user.headerpic = infoRes.authorizer_info.head_img;
                        UserDateTicket.wx_user.wxType = (int)infoRes.authorizer_info.service_type_info.id;
                        UserDateTicket.wx_user.verify_type_info = (int)infoRes.authorizer_info.verify_type_info.id;
                        UserDateTicket.wx_user.wxId = infoRes.authorizer_info.user_name;
                        UserDateTicket.wx_user.weixinCode = infoRes.authorizer_info.alias;
                        UserDateTicket.wx_user.qrcode_url = infoRes.authorizer_info.qrcode_url;
                        UserDateTicket.wx_user.open_card = Convert.ToBoolean(infoRes.authorizer_info.business_info.open_card);
                        UserDateTicket.wx_user.open_pay = Convert.ToBoolean(infoRes.authorizer_info.business_info.open_pay);
                        UserDateTicket.wx_user.open_scan = Convert.ToBoolean(infoRes.authorizer_info.business_info.open_scan);
                        UserDateTicket.wx_user.open_shake = Convert.ToBoolean(infoRes.authorizer_info.business_info.open_shake);
                        UserDateTicket.wx_user.open_store = Convert.ToBoolean(infoRes.authorizer_info.business_info.open_store);
                        wx_userweixinService.instance().Update(UserDateTicket.wx_user);
                    }
                }
                ViewBag.CompanyID = UserDateTicket.Company.ID;
                ViewBag.pre_auth_code = UserDateTicket.wx_open.open_pre_auth_code;

                if (UserDateTicket.wx_user != null)
                    m = wx_userweixinService.instance().SingleByCompanyID(UserDateTicket.Company.ID);
            }
            catch
            {
            }
            return View(m);
        }
        [HttpPost]
        public JsonResult Index(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_userweixin m = wx_userweixinService.instance().SingleByCompanyID(UserDateTicket.Company.ID);
            TryUpdateModel<wx_userweixin>(m, form);
            m.Access_Token = "";
            m.expires_in = 0;
            result.status = wx_userweixinService.instance().Update(m);
            if (result.status == 1)
                UserDateTicket.wx_user = m;
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}