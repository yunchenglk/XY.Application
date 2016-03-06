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
        public ActionResult Index()
        {
            ViewBag.CompanyID = UserDateTicket.Company.ID;
            ViewBag.open_sAppid = UserDateTicket.wx_open.open_sAppID;
            var result = ComponentApi.GetComponentAccessToken(UserDateTicket.wx_open.open_sAppID, UserDateTicket.wx_open.open_sAppSecret, UserDateTicket.wx_open.open_ticket);
            if (result.errcode == Entity.Weixin.ReturnCode.请求成功)
            {
                var coderesult = ComponentApi.GetPreAuthCode(UserDateTicket.wx_open.open_sAppID, result.component_access_token);
                if (coderesult.errcode == Entity.Weixin.ReturnCode.请求成功)
                {
                    UserDateTicket.wx_open.open_pre_auth_code = coderesult.pre_auth_code;
                    ViewBag.pre_auth_code = UserDateTicket.wx_open.open_pre_auth_code;
                }
            }
            wx_userweixin m;

            if (UserDateTicket.wx_user.ID == Guid.Empty)
                m = new wx_userweixin() { DR = true };
            else
                m = wx_userweixinService.instance().SingleByCompanyID(UserDateTicket.Company.ID);
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
        public JsonResult reLoad()
        {
            ResultBase_form result = new ResultBase_form();
            var temp = ComponentApi.GetComponentAccessToken(UserDateTicket.wx_open.open_sAppID, UserDateTicket.wx_open.open_sAppSecret, UserDateTicket.wx_open.open_ticket);
            if (temp.errcode == Entity.Weixin.ReturnCode.请求成功)
            {
                UserDateTicket.wx_open.open_access_token = temp.component_access_token;
                var infoRes = ComponentApi.GetAuthorizerInfo(temp.component_access_token,
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
                    result.status = wx_userweixinService.instance().Update(UserDateTicket.wx_user);
                }
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}