using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XY.Entity;
using XY.Services;

namespace XY.WeChart.Controllers
{
    public class EventHandler : IHandler
    {
        /// <summary>
        /// 请求的xml
        /// </summary>
        private string RequestXml { get; set; }
        public EventHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }
        public string HandleRequest()
        {
            string response = string.Empty;
            EventMessage em = EventMessage.LoadFromXml(RequestXml);
            if (em != null)
            {
                LogHelper.Info(em.Event.ToLower());
                switch (em.Event.ToLower())
                {
                    case ("subscribe")://首次关注推送
                        response = SubscribeEventHandler(em);
                        break;
                    case "click":
                        response = ClickEventHandler(em);
                        break;
                    case "text":
                        response = ClickEventHandler(em);
                        break;
                }
            }
            return response;
        }
        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string SubscribeEventHandler(EventMessage em)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = Common.GetNowTime();
            tm.Content = "谢谢您的关注！\n\n";
            return tm.GenerateContent();
        }
        /// <summary>
        /// 处理点击事件
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string ClickEventHandler(EventMessage em)
        {
            string result = string.Empty;
            if (em != null && em.EventKey != null)
            {
                string key = em.EventKey;
                string OrgID = em.ToUserName;


                LogHelper.Info(key + ":" + OrgID);
                WX_Config config = WX_ConfigService.instance().SingleByOrgID(OrgID);
                WX_KeyWord kwentiy = WX_KeyWordService.instance().SingleByCIDAndKey(config.CompanyID, key);
                kwentiy.Push++;
                WX_KeyWordService.instance().Update(kwentiy);
                LogHelper.Info(kwentiy.CompanyID.ToString());


                switch (kwentiy.Type)
                {
                    case 0://图文消息
                        NewsMessage nm = new NewsMessage();
                        nm.ToUserName = em.FromUserName;
                        nm.FromUserName = em.ToUserName;
                        WX_Message wm = WX_MessageService.instance().Single(kwentiy.RelationID);
                        nm.Group = wm.Groups;
                        nm.CreateTime = Common.GetNowTime();
                        result = nm.GenerateContent();
                        break;
                    case 1://文字
                        TextMessage tm = new TextMessage();
                        tm.ToUserName = em.FromUserName;
                        tm.FromUserName = em.ToUserName;
                        tm.CreateTime = Common.GetNowTime();
                        tm.Content = kwentiy.Content;
                        result = tm.GenerateContent();
                        break;
                    case 2://图片
                        break;
                    case 3://语音
                        break;
                    case 4: //视频
                        break;
                    case 5://API
                        break;
                }
            }
            LogHelper.Info(result);
            return result;
        }
    }
}