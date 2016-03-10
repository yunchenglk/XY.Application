﻿using System.Xml.Linq;
using XY.Services.Weixin;

namespace XY.WeChart
{

    /// <summary>
    /// 回复语音消息
    /// </summary>
    public class VoiceMessage : MessageBase
    {
        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string m_Template;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public VoiceMessage()
        {
            this.MsgType = "voice";
        }
        public string access_token { get; set; }
        public string filePath { get; set; }
        public string media
        {
            get
            {
                if (string.IsNullOrEmpty(access_token) || string.IsNullOrEmpty(filePath))
                    return "";
                else
                {
                    var tempre = CommonApi.Upload(access_token, Entity.Weixin.UploadMediaFileType.voice, filePath);
                    if (tempre.errcode == Entity.Weixin.ReturnCode.请求成功)
                    {
                        return tempre.media_id;
                    }
                    return "";
                }
            }
        }
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static VoiceMessage LoadFromXml(string xml)
        {
            VoiceMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new VoiceMessage();
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
            //收发互换
            return string.Format(this.Template,
                this.FromUserName,
                this.ToUserName,
                this.CreateTime,
                this.MsgType,
                this.media);
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
                        <Voice>
                        <MediaId><![CDATA[{4}]]></MediaId>
                        </Voice>
                        </xml>";
        }
    }
}