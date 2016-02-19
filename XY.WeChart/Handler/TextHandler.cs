using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using XY.Entity;
using XY.Services;
using XY.Services.Weixin;

namespace XY.WeChart
{
    /// <summary>
    /// 文本信息处理类
    /// </summary>
    public class TextHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        public string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public TextHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }

        public string HandleRequest()
        {
            TextMessage tm = TextMessage.LoadFromXml(RequestXml);
            XElement xdoc = XElement.Parse(RequestXml);
            var key = xdoc.Element("Content").Value;
            var rule = wx_requestRuleService.instance().GetByreqestType_Key(0, CommFun.companyid, key);
            if (rule == null)
            {
                tm.Content = "不明白你在说什么";
                string xx = tm.GenerateContent();
                return tm.GenerateContent();
            }
            return CommFun.MakeResponseXML(rule, this.RequestXml);
        }

    }



}