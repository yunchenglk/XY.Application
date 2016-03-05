﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using XY.Services.Weixin;
using XY.Services.Weixin.Entities;
using XY.Services.Weixin.Helpers;
using XY.Services.Weixin.MessageHandlers;
using XY.WeChart.Web.Helpers;

namespace XY.WeChart.Web.MessageHandlers
{
    /// <summary>
    /// 开放平台全网发布之前需要做的验证
    /// </summary>
    public class OpenCheckMessageHandler : MessageHandler<CustomMessageContext>
    {
        //private string testAppId = "wx570bc396a51b8ff8";
        private string componentAppId = UserDateTicket.wx_open.open_sAppID;
        private string componentSecret = UserDateTicket.wx_open.open_sAppSecret;

        public OpenCheckMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {

        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            if (requestMessage.Content == "TESTCOMPONENT_MSG_TYPE_TEXT")
            {
                var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = requestMessage.Content + "_callback";//固定为TESTCOMPONENT_MSG_TYPE_TEXT_callback
                return responseMessage;
            }

            if (requestMessage.Content.StartsWith("QUERY_AUTH_CODE:"))
            {
                string openTicket = OpenTicketHelper.GetOpenTicket(componentAppId);
                var query_auth_code = requestMessage.Content.Replace("QUERY_AUTH_CODE:", "");
                try
                {
                    var component_access_token = ComponentApi.GetComponentAccessToken(componentAppId, componentSecret, openTicket).component_access_token;
                    var oauthResult = ComponentApi.QueryAuth(component_access_token, componentAppId, query_auth_code);

                    //调用客服接口
                    var content = query_auth_code + "_from_api";
                    var sendResult = CommonApi.SendText(oauthResult.authorization_info.authorizer_access_token,
                          requestMessage.FromUserName, content);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return null;
        }

        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = requestMessage.Event + "from_callback";
            return responseMessage;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "默认消息";
            return responseMessage;
        }
    }
}
