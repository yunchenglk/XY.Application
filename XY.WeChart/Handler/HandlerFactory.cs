using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using XY.Entity.Weixin;

namespace XY.WeChart
{
    /// <summary>
    /// 处理器工厂类
    /// </summary>
    public class HandlerFactory
    {
        private static List<BaseMsg> _queue;
        /// <summary>
        /// 创建处理器
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        /// <returns>IHandler对象</returns>
        public static IHandler CreateHandler(string requestXml)
        {
            if (_queue == null)
            {
                _queue = new List<BaseMsg>();
            }
            else if (_queue.Count >= 50)
            {
                _queue = _queue.Where(q => { return q.CreateTime.AddSeconds(20) > DateTime.Now; }).ToList();//保留20秒内未响应的消息
            }
            IHandler handler = null;
            XElement xdoc = XElement.Parse(requestXml);
            
            var FromUserName = xdoc.Element("FromUserName").Value;
            var msgtype = xdoc.Element("MsgType").Value.ToUpper();
            RequestMsgType type = (RequestMsgType)Enum.Parse(typeof(RequestMsgType), msgtype);




            if (type != RequestMsgType.EVENT)
            {
                var MsgId = xdoc.Element("MsgId").Value;
                if (_queue.FirstOrDefault(m => { return m.MsgFlag == MsgId; }) == null)
                {
                    _queue.Add(new BaseMsg
                    {
                        CreateTime = DateTime.Now,
                        FromUser = FromUserName,
                        MsgFlag = MsgId
                    });
                }
                else
                {
                    return null;
                }

            }
            else
            {
                var CreateTime = xdoc.Element("CreateTime").Value;
                if (_queue.FirstOrDefault(m => { return m.MsgFlag == CreateTime; }) == null)
                {
                    _queue.Add(new BaseMsg
                    {
                        CreateTime = DateTime.Now,
                        FromUser = FromUserName,
                        MsgFlag = CreateTime
                    });
                }
                else
                {
                    return null;
                }
            }




            switch (type)
            {
                case RequestMsgType.TEXT://文本请求
                    handler = new TextHandler(requestXml);
                    break;
                case RequestMsgType.EVENT://事件类型
                    handler = new EventHandler(requestXml);
                    break;
                case RequestMsgType.LOCATION:
                    handler = new EventHandler(requestXml);
                    break;
                case RequestMsgType.IMAGE:
                    handler = new EventHandler(requestXml);
                    break;
            }
            return handler;
        }
    }
    public class BaseMsg
    {
        /// <summary>
        /// 发送者标识
        /// </summary>
        public string FromUser { get; set; }
        /// <summary>
        /// 消息表示。普通消息时，为msgid，事件消息时，为事件的创建时间
        /// </summary>
        public string MsgFlag { get; set; }
        /// <summary>
        /// 添加到队列的时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}