using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;

namespace XY.WeChart.Web.Controllers
{
    public class _baseController : Controller
    {
        public string GetToken()
        {
            string error;
            string token = WeChartAPI.getAccessToken(UserDateTicket.Company.ID, out error);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            return token;
        }
        public JsonResult ReloadToken()
        {
            return Json(WeChartAPI.ReloadToken(UserDateTicket.Company.ID), JsonRequestBehavior.AllowGet);
        }
        //推送消息
        public JsonResult _push(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_requestRule rule = wx_requestRuleService.instance().Single(ID);
                string[] openIds = wx_userinfoService.instance().GetByCompanyID(UserDateTicket.Company.ID).Select(n => n.openid).ToArray();
                if (rule == null || openIds.Count() == 0) { return Json("错误", JsonRequestBehavior.AllowGet); }
                SendResult result = new SendResult();
                var contentlist = wx_requestRuleContentService.instance().GetByRuleID(rule.ID);
                switch (rule.responseType)
                {

                    //文本1，图文2，语音3，视频4
                    case 1:
                        result = Services.Weixin.CommonAPI.CommonApi.SendTextGroupMessageByOpenId(GetToken(),
                           contentlist.First().rContent, openIds);
                        break;
                    case 2:

                        break;
                    case 3:
                        var voicetempre = Services.Weixin.CommonAPI.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.voice, contentlist.First().mediaUrl);
                        if (voicetempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                        {
                            result = Services.Weixin.CommonAPI.CommonApi.SendGroupMessageByOpenId(GetToken(),
                                                                                   GroupMessageType.voice,
                                                                                   voicetempre.media_id,
                                                                                   openIds);
                        }
                        break;
                    case 4:
                        var videotempre = Services.Weixin.CommonAPI.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.video, contentlist.First().meidaHDUrl);
                        if (videotempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                        {
                            result = Services.Weixin.CommonAPI.CommonApi.SendVideoGroupMessageByOpenId(GetToken(),
                                                                                    contentlist.First().rContent,
                                                                                    contentlist.First().rContent2,
                                                                                    videotempre.media_id,
                                                                                    openIds);
                        }
                        break;

                }
                return Json(result.errcode.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json("错误", JsonRequestBehavior.AllowGet);
        }
        //关注时回复
        public JsonResult _subscribe(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_requestRule olddate = wx_requestRuleService.instance().GetByRequestType(6, UserDateTicket.Company.ID);
                if (olddate != null)
                {
                    olddate.reqestType = 0;
                    if (wx_requestRuleService.instance().Update(olddate) != 1)
                        return Json("更新old信息错误", JsonRequestBehavior.AllowGet);
                }
                wx_requestRule newdata = wx_requestRuleService.instance().Single(ID);
                newdata.reqestType = 6;
                if (wx_requestRuleService.instance().Update(newdata) != 1)
                    return Json("更新New信息错误");
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("错误", JsonRequestBehavior.AllowGet);

        }
    }
}