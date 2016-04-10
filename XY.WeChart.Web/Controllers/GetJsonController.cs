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
    [Filters.AuthorizeFilter]
    public class GetJsonController : _baseController
    {
        /// <summary>
        /// 根据输出类型获取集合
        /// </summary>
        /// <param name="id">文本1，图文2，语音3，视频4,第三方接口5</param>
        /// <returns></returns>
        public JsonResult GetByResponseType(string id)
        {
            int type = IsInt(id);
            var list = wx_requestRuleService.instance().GetByResponseType(type, UserDateTicket.Company.ID);
            return Json(list.Select(m => new { id = m.ID, name = m.ruleName }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <returns></returns>
        public JsonResult GetGroup()
        {
            var list = wx_usergroupService.instance().GetAll(UserDateTicket.Company.ID);
            return Json(list.Select(m => new { id = m.ID, name = m.gname + "(" + m.gcount + ")", gid = m.gid }), JsonRequestBehavior.AllowGet);
        }






        //推送消息
        [HttpGet]
        public JsonResult _push(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_requestRule rule = wx_requestRuleService.instance().Single(ID);
                string[] openIds = wx_userinfoService.instance().GetByCompanyID(UserDateTicket.Company.ID).Select(n => n.openid).ToArray();
                if (rule == null || openIds.Count() == 0) { return Json("错误", JsonRequestBehavior.AllowGet); }
                SendResult result = this.push(rule, openIds);
                //SendResult result = new SendResult();
                //var contentlist = wx_requestRuleContentService.instance().GetByRuleID(rule.ID);
                //switch (rule.responseType)
                //{

                //    //文本1，图文2，语音3，视频4
                //    case 1:
                //        result = Services.Weixin.CommonApi.SendTextGroupMessageByOpenId(GetToken(),
                //           contentlist.First().rContent, openIds);
                //        break;
                //    case 2:

                //        break;
                //    case 3:
                //        var voicetempre = Services.Weixin.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.voice, contentlist.First().mediaUrl);
                //        if (voicetempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                //        {
                //            result = Services.Weixin.CommonApi.SendGroupMessageByOpenId(GetToken(),
                //                                                                   GroupMessageType.voice,
                //                                                                   voicetempre.media_id,
                //                                                                   openIds);
                //        }
                //        break;
                //    case 4:
                //        var videotempre = Services.Weixin.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.video, contentlist.First().meidaHDUrl);
                //        if (videotempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                //        {
                //            result = Services.Weixin.CommonApi.SendVideoGroupMessageByOpenId(GetToken(),
                //                                                                    contentlist.First().rContent,
                //                                                                    contentlist.First().rContent2,
                //                                                                    videotempre.media_id,
                //                                                                    openIds);
                //        }
                //        break;

                //}
                return Json(result.errcode.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json("错误", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult _pushByGroup(string rid, int gid)
        {
            Guid ID;
            if (Guid.TryParse(rid, out ID))
            {
                wx_requestRule rule = wx_requestRuleService.instance().Single(ID);
                string[] openIds = wx_userinfoService.instance().GetByGroupID(UserDateTicket.Company.ID, gid).Select(n => n.openid).ToArray();
                if (rule == null || openIds.Count() == 0) { return Json("错误", JsonRequestBehavior.AllowGet); }
                SendResult result = this.push(rule, openIds);
                return Json(result.errcode.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json("错误", JsonRequestBehavior.AllowGet);
        }

        private SendResult push(wx_requestRule rule, string[] openIds)
        {
            SendResult result = new SendResult();
            var contentlist = wx_requestRuleContentService.instance().GetByRuleID(rule.ID);
            switch (rule.responseType)
            {

                //文本1，图文2，语音3，视频4
                case 1:
                    result = Services.Weixin.CommonApi.SendTextGroupMessageByOpenId(GetToken(),
                       contentlist.First().rContent, openIds);
                    break;
                case 2:

                    //var newtemp = Services.Weixin.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.news, contentlist.First().picUrl);
                    var file = @"D:\1.jpg";
                    var fileresult = Services.Weixin.CommonApi.UploadForeverMedia(GetToken(), file);
                    var new1 = new NewsModel()
                    {
                        author = "test",
                        content = "test",
                        content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                        digest = "test",
                        show_cover_pic = "1",
                        thumb_media_id = fileresult.media_id,
                        title = "test"
                    };

                    var new2 = new NewsModel()
                    {
                        author = "test",
                        content = "test111",
                        content_source_url = "http://qy.weiweihi.com/Content/Images/app/qyhelper.png",
                        digest = "test",
                        show_cover_pic = "1",
                        thumb_media_id = fileresult.media_id,
                        title = "test"
                    };

                    var newtemp = Services.Weixin.CommonApi.UploadNews(GetToken(), 10000, new1, new2);

                    result = Services.Weixin.CommonApi.SendGroupMessageByOpenId(GetToken(), GroupMessageType.mpnews, newtemp.media_id, openIds);



                    break;
                case 3:
                    var voicetempre = Services.Weixin.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.voice, contentlist.First().mediaUrl);
                    if (voicetempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                    {
                        result = Services.Weixin.CommonApi.SendGroupMessageByOpenId(GetToken(),
                                                                               GroupMessageType.voice,
                                                                               voicetempre.media_id,
                                                                               openIds);
                    }
                    break;
                case 4:
                    var videotempre = Services.Weixin.CommonApi.Upload(GetToken(), Entity.Weixin.UploadMediaFileType.video, contentlist.First().meidaHDUrl);
                    if (videotempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                    {
                        result = Services.Weixin.CommonApi.SendVideoGroupMessageByOpenId(GetToken(),
                                                                                contentlist.First().rContent,
                                                                                contentlist.First().rContent2,
                                                                                videotempre.media_id,
                                                                                openIds);
                    }
                    break;

            }
            return result;
        }








    }
}