using System;
using System.Web;
using XY.Services.Weixin.Entities;

namespace XY.Services.WeixinComm
{
    public partial class WeiXCommFun
    {
        /// <summary>
        /// 推送纯文字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessageTxt(RequestMessageText requestMessage, Guid RuleID, int wid)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();
            responseMessage.Content = wx_requestRuleContentService.instance().SingleByRuleID(RuleID).rContent;
            //---------将用户请求的数据和系统回复的数据保存到数据库，数据落地
            return responseMessage;
        }

        /// <summary>
        /// 推送纯文字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessageTxt(RequestMessageEventBase requestMessage, Guid Indexid, int wid)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();
            responseMessage.Content = "asdf";// getDataTxtComm(wid, Indexid, openid, token);

            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            else if (requestMessage.EventKey != null)
            {
                EventName += requestMessage.EventKey.ToString();
            }

            //wxResponseBaseMgr.Add(wid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "text", responseMessage.Content, requestMessage.ToUserName);

            return responseMessage;
        }


        /// <summary>
        /// 推送纯文字
        /// 2013-9-12
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessageTxtByContent(RequestMessageEventBase requestMessage, string content, int wid)
        {

            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            //var locationService = new LocationService();
            responseMessage.Content = content;
            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            else if (requestMessage.EventKey != null)
            {
                EventName += requestMessage.EventKey.ToString();
            }
            //---------将用户请求的数据和系统回复的数据保存到数据库，数据落地

            return responseMessage;
        }
        /// <summary>
        /// 推送纯文字
        /// 2013-9-12
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessageTxtByContent(RequestMessageText requestMessage, string content, int wid)
        {

            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            //var locationService = new LocationService();
            responseMessage.Content = content;
            //wxResponseBaseMgr.Add(wid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "text", "文字请求，推送纯粹文字，内容为：" + content, requestMessage.ToUserName);
            return responseMessage;
        }
        /// <summary>
        /// 2014-9-8新增抽奖功能
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="Indexid"></param>
        /// <param name="wid"></param>
        /// <returns></returns>
        public IResponseMessageBase GetResponseMessageTxt(RequestMessageEventBase requestMessage, int wid)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            string openid = requestMessage.FromUserName;
            string token = ConvertDateTimeInt(DateTime.Now).ToString();
            responseMessage.Content = string.Format("感谢您参与本次活动，您的幸运号码为 WDS2014920{0} ,请凭本号码参与后续精彩活动！", getDataTxtComm(openid));

            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            else if (requestMessage.EventKey != null)
            {
                EventName += requestMessage.EventKey.ToString();
            }
            //---------将用户请求的数据和系统回复的数据保存到数据库，数据落地
            return responseMessage;
        }





        /// <summary>
        /// 获取apiid
        /// </summary>
        /// <returns></returns>
        public int getApiid()
        {
            if (HttpContext.Current.Request["apiid"] == null || HttpContext.Current.Request["apiid"].ToString().Length < 1)
            {
                return 0;
            }
            int tmpInt = 0;
            if (!int.TryParse(HttpContext.Current.Request["apiid"].ToString(), out tmpInt))
            {
                return 0;
            }
            int apiid = int.Parse(HttpContext.Current.Request["apiid"].ToString());
            return apiid;

        }
        /// <summary>
        /// 判断该微帐号与原始Id号是否一致，如果不一致，则返回false，如果一致则返回true
        /// </summary>
        /// <param name="apiid"></param>
        /// <param name="wxid">原始Id号</param>
        /// <returns></returns>
        public bool ExistApiidAndWxId(int apiid, string wxid)
        {
            bool exists = true;
            //DAL.wx_userweixin weixinDal = new DAL.wx_userweixin();
            //if (weixinDal.ExistsWidAndWxId(apiid, wxid))
            //{
            //    exists = true;
            //}
            //else
            //{
            //    exists = false;
            //}

            return exists;
        }














        public long ConvertDateTimeInt(System.DateTime time)
        {
            long intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (long)(time - startTime).TotalSeconds;
            return intResult;
        }
    }
}
