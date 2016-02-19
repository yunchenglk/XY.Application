using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace XY.WeChart.Message
{
    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class ImageMessage : MessageBase
    {
        public string media_id { get; set; }


        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static ImageMessage LoadFromXml(string xml)
        {
            ImageMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new ImageMessage();
                    tm.FromUserName = element.Element(CommConfig.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(CommConfig.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(CommConfig.CREATE_TIME).Value;
                }
            }

            return tm;
        }

        public ImageMessage()
        {
            MsgType = "image";
        }

        /// <summary>
        /// 模板
        /// </summary>
        public override string Template
        {
            get
            {
                return @" 
                        <xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[{3}]]></MsgType>
                        <Image>
                        <MediaId><![CDATA[{4}]]></MediaId>
                        </Image>
                        </xml>";
            }
        }


        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        //public override string GenerateContent()
        //{
        //    this.CreateTime = CommConfig.GetNowTime();
        //    return string.Format(this.Template, this.FromUserName, 
        //        this.ToUserName, this.CreateTime,
        //        this.MsgType, this.Content, this.MsgId);
        //}





        /*
       
        */
    }
}