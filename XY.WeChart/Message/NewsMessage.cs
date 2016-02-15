using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;
using XY.Services.Weixin.CommonAPI;
using XY.Util;
using XY.WeChart.Helpers;

namespace XY.WeChart
{
    public class NewsMessage : MessageBase
    {
        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string m_Template;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public int ArticleCount
        {
            get
            {
                return Group.Count();
            }
            set { }
        }
        public NewsMessage()
        {
            this.MsgType = "news";
        }
        public IEnumerable<wx_requestRuleContent> Group { get; set; }

        public string ArticlesXML
        {
            get
            {
                StringBuilder XML = new StringBuilder();
                foreach (var item in Group.OrderBy(m => m.seq))
                {
                    XML.Append("<item>");
                    XML.Append("<Title>");
                    XML.Append("<![CDATA[" + item.rContent + "]]>");
                    XML.Append("</Title>");
                    XML.Append("<Description>");
                    XML.Append("<![CDATA[" + item.rContent + "]]>");
                    XML.Append("</Description>");
                    XML.Append("<PicUrl>");
                    XML.Append("<![CDATA[" + item.picUrlStr + "]]>");
                    XML.Append("</PicUrl>");
                    XML.Append("<Url>");
                    XML.Append("<![CDATA[http://www.baidu.com]]>");
                    XML.Append("</Url>");
                    XML.Append("</item>");
                }
                return XML.ToString();
            }
        }
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static NewsMessage LoadFromXml(string xml)
        {
            NewsMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new NewsMessage();
                    tm.FromUserName = element.Element(CommConfig.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(CommConfig.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(CommConfig.CREATE_TIME).Value;
                }
            }

            return tm;
        }
        /// <summary>
        /// 模板
        /// </summary>
        public override string Template
        {
            get
            {
                if (string.IsNullOrEmpty(m_Template))
                {
                    LoadTemplate();
                }

                return m_Template;
            }
        }
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public override string GenerateContent()
        {
            this.CreateTime = CommConfig.GetNowTime();
            return string.Format(this.Template,
                this.FromUserName,
                this.ToUserName,
                this.CreateTime,
                this.MsgType,
                this.ArticleCount,
                this.ArticlesXML);
        }
        /// <summary>
        /// 加载模板
        /// </summary>
        private static void LoadTemplate()
        {
            m_Template = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[{3}]]></MsgType>
                                <ArticleCount>{4}</ArticleCount>
                                <Articles>{5}</Articles>
                            </xml>";
        }

    }
}