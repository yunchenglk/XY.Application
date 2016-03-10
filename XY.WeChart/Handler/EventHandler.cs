using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;

namespace XY.WeChart
{
    public partial class EventHandler : IHandler
    {
        /// <summary>
        /// 请求的xml
        /// </summary>
        private string RequestXml { get; set; }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml"></param>
        public EventHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }
        public string HandleRequest()
        {
            EventMessage em = EventMessage.LoadFromXml(RequestXml);
            if (em != null)
            {
                var eventtype = (Event)Enum.Parse(typeof(Event), em.Event.ToUpper());
                wx_requestRule entity = new wx_requestRule();
                switch (eventtype)
                {
                    case Event.CLICK:
                        entity = wx_requestRuleService.instance().GetByreqestType_Key(0, CommFun.companyid, em.EventKey);
                        break;
                    case Event.SUBSCRIBE://首次关注推送
                        entity = wx_requestRuleService.instance().GetByRequestType(6, CommFun.companyid);
                        subscribeMessage sub = subscribeMessage.LoadFromXml(RequestXml);
                        sub.GenerateContent();
                        break;
                    case Event.UNSUBSCRIBE://取消关注
                        unsubscribeMessage unsub = unsubscribeMessage.LoadFromXml(RequestXml);
                        return unsub.GenerateContent();
                    case Event.LOCATION://位置
                        LocationMessage location = LocationMessage.LoadFromXml(RequestXml);
                        location.GenerateContent();
                        break;
                    default:
                        break;
                }
                return CommFun.MakeResponseXML(entity, this.RequestXml);
            }
            return "";
        }
    }
}