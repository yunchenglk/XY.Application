﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XY.Entity.Weixin.MP;
using XY.Weixin.Exceptions;
using XY.Weixin.MP.Helpers;

namespace XY.Weixin.MP
{
    public static class ResponseMessageFactory
    {
        //<?xml version="1.0" encoding="utf-8"?>
        //<xml>
        //  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
        //  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
        //  <CreateTime>63497820384</CreateTime>
        //  <MsgType>text</MsgType>
        //  <Content><![CDATA[您刚才发送了文字信息：中文
        //您还可以发送【位置】【图片】【语音】信息，查看不同格式的回复。
        //SDK官方地址：http://weixin.senparc.com]]></Content>
        //  <FuncFlag>0</FuncFlag>
        //</xml>

        /// <summary>
        /// 获取XDocument转换后的IResponseMessageBase实例（通常在反向读取日志的时候用到）。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IResponseMessageBase GetResponseEntity(XDocument doc)
        {
            ResponseMessageBase responseMessage = null;
            Entity.Weixin.ResponseMsgType msgType;
            try
            {
                msgType = MsgTypeHelper.GetResponseMsgType(doc);
                switch (msgType)
                {
                    case Entity.Weixin.ResponseMsgType.Text:
                        responseMessage = new ResponseMessageText();
                        break;
                    case Entity.Weixin.ResponseMsgType.Image:
                        responseMessage = new ResponseMessageImage();
                        break;
                    case Entity.Weixin.ResponseMsgType.Voice:
                        responseMessage = new ResponseMessageVoice();
                        break;
                    case Entity.Weixin.ResponseMsgType.Video:
                        responseMessage = new ResponseMessageVideo();
                        break;
                    case Entity.Weixin.ResponseMsgType.Music:
                        responseMessage = new ResponseMessageMusic();
                        break;
                    case Entity.Weixin.ResponseMsgType.News:
                        responseMessage = new ResponseMessageNews();
                        break;
                    case Entity.Weixin.ResponseMsgType.Transfer_Customer_Service:
                        responseMessage = new ResponseMessageTransfer_Customer_Service();
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在ResponseMessageFactory中没有对应的处理程序！", msgType), new ArgumentOutOfRangeException());
                }
                EntityHelper.FillEntityWithXml(responseMessage, doc);
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("ResponseMessage转换出错！可能是MsgType不存在！，XML：{0}", doc.ToString()), ex);
            }
            return responseMessage;
        }


        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IResponseMessageBase GetResponseEntity(string xml)
        {
            return GetResponseEntity(XDocument.Parse(xml));
        }
    }
}