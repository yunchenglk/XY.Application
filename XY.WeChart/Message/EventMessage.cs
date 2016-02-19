using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace XY.WeChart
{
    public class EventMessage : MessageBase
    {

        /// <summary>
        /// 
        /// </summary>
        private static string mTemplate;
        /// <summary>
        /// 模板
        /// </summary>
        public override string Template
        {
            get
            {
                if (string.IsNullOrEmpty(mTemplate))
                {
                    mTemplate = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[event]]></MsgType>
                                <Event><![CDATA[{3}]]></Event>
                                <EventKey>{4}</EventKey>
                            </xml>";
                }

                return mTemplate;
            }
        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public EventMessage()
        {
            this.MsgType = "event";
        }
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static EventMessage LoadFromXml(string xml)
        {
            EventMessage em = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    em = new EventMessage();
                    em.FromUserName = element.Element(CommConfig.FROM_USERNAME).Value;
                    em.ToUserName = element.Element(CommConfig.TO_USERNAME).Value;
                    em.CreateTime = element.Element(CommConfig.CREATE_TIME).Value;
                    em.Event = element.Element(CommConfig.EVENT).Value;
                    if (element.Element(CommConfig.EVENT_KEY) != null)
                        em.EventKey = element.Element(CommConfig.EVENT_KEY).Value;
                }
            }
            return em;
        }
    }
}