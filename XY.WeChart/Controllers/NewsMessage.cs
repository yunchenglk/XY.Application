using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using XY.Entity;
using XY.Services;
using XY.Util;

namespace XY.WeChart.Controllers
{
    public class NewsMessage : Message
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
        public IEnumerable<WX_MessageGroup> Group { get; set; }
        public string ArticlesXML
        {
            get
            {
                StringBuilder XML = new StringBuilder();
                foreach (var item in Group)
                {

                    XML.Append("<item>");
                    XML.Append("<Title>");
                    XML.Append("<![CDATA[" + item.Title + "]]>");
                    XML.Append("</Title>");
                    XML.Append("<Description>");
                    XML.Append("<![CDATA[" + Utils.CutString(item.Content, 10) + "]]>");
                    XML.Append("</Description>");
                    XML.Append("<PicUrl>");
                    XML.Append("<![CDATA[" + item.ImgUrlStr + "]]>");
                    XML.Append("</PicUrl>");
                    XML.Append("<Url>");
                    XML.Append("<![CDATA[" + item.URL + "]]>");
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
                    tm.FromUserName = element.Element(Common.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(Common.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(Common.CREATE_TIME).Value;
                    tm.ArticleCount = int.Parse(element.Element(Common.ARTICLECOUNT).Value);
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
            this.CreateTime = Common.GetNowTime();
            return string.Format(this.Template,
                this.ToUserName,
                this.FromUserName,
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