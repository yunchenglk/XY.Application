using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;
using XY.Services.Weixin;
using XY.Services.Weixin.Entities;
using XY.Services.Weixin.MessageHandlers;
using XY.WeChart.Web.MessageHandlers;

namespace XY.WeChart.Web.Controllers
{
    public class OpenController : Controller
    {
        private static List<queue> _queue;

        // GET: Open
        /// <summary>
        /// 授权事件接收URL
        /// </summary>
        [HttpPost]
        public void Notice()
        {
            /*
            <xml><AppId><![CDATA[wx3822e482594a911e]]></AppId>
            <CreateTime>1456903904</CreateTime>
            <InfoType><![CDATA[component_verify_ticket]]></InfoType>
            <ComponentVerifyTicket><![CDATA[ticket@@@Iv_fUfslI38h5WzYWsa6s1nvVyw8NI8ZxumDTw7nIa0WmE0y9yp0UFU3XLo7CuVKtUvSbwG2eG7-W1EirYHSvA]]></ComponentVerifyTicket>
            </xml>
            */
            wx_openInfo wx_open = wx_openInfoService.instance().Single(new Guid(System.Configuration.ConfigurationManager.AppSettings["openID"]));
            HttpRequestBase Request = HttpContext.Request;
            string sToken = wx_open.open_sToken;
            string sAppID = wx_open.open_sAppID;
            string sAppSecret = wx_open.open_sAppSecret;
            string sEncodingAESKey = wx_open.open_sEncodingAESKey;
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
            string sReqMsgSig = Request["msg_signature"];
            string sReqTimeStamp = Request["timestamp"];
            string sReqNonce = Request["nonce"];
            string sReqData = ReadRequest(HttpContext.Request);
            string sMsg = "";  //解析之后的明文
            int ret = 0;
            ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);
            if (!string.IsNullOrEmpty(sMsg))
            {
                XElement element = XElement.Parse(sMsg);

                if (_queue == null)
                {
                    _queue = new List<queue>();
                }
                else if (_queue.Count >= 50)
                {
                    _queue = _queue.Where(q => { return q.CreateTime.AddSeconds(20) > DateTime.Now; }).ToList();//保留20秒内未响应的消息
                }
                if (_queue.FirstOrDefault(m => { return m.time == element.Element("CreateTime").Value.ToLower().ToString() && m.FromUserName == element.Element("InfoType").Value.ToLower().ToString(); }) == null)
                {
                    _queue.Add(new queue
                    {
                        CreateTime = DateTime.Now,
                        FromUserName = element.Element("InfoType").Value.ToLower().ToString(),
                        time = element.Element("CreateTime").Value.ToLower().ToString()
                    });
                }
                else
                {
                    return;
                }
                #region 处理事件
                string type = element.Element("InfoType").Value.ToString().ToUpper();

                switch (type)
                {
                    case "COMPONENT_VERIFY_TICKET"://推送component_verify_ticket协议,每隔10分钟定时推送
                        Util.LogHelper.Info("推送component_verify_ticket协议--start【");
                        string ticket = element.Element("ComponentVerifyTicket").Value.ToString();
                        //更新ticket
                        if (wx_open.open_ticket != ticket)
                        {
                            wx_open.open_ticket = ticket;
                            Util.LogHelper.Info("更新ticket:" + ticket);
                        }
                        else {
                            Util.LogHelper.Info("ticket无需更新:" + ticket);
                        }
                        //更新open_access_token
                        var result = ComponentApi.GetComponentAccessToken(sAppID, sAppSecret, ticket);
                        if (result.errcode == ReturnCode.请求成功)
                        {
                            if (wx_open.open_access_token != result.component_access_token)
                            {
                                wx_open.open_access_token = result.component_access_token;
                                wx_open.access_token_expires_in = result.expires_in;
                                Util.LogHelper.Info("更新open_access_token:" + result.component_access_token);
                            }
                            else {
                                Util.LogHelper.Info("open_access_token无需更新:" + result.component_access_token);
                            }
                        }
                        else {
                            Util.LogHelper.Info("更新open_access_token错误:" + result.errcode.ToString());
                        }
                        //更新open_pre_auth_cod
                        var coderesult = ComponentApi.GetPreAuthCode(sAppID, result.component_access_token);
                        if (coderesult.errcode == ReturnCode.请求成功)
                        {
                            if (wx_open.open_pre_auth_code != coderesult.pre_auth_code)
                            {
                                wx_open.open_pre_auth_code = coderesult.pre_auth_code;
                                wx_open.pre_auth_code_wxpires_in = coderesult.expires_in;
                                Util.LogHelper.Info("更新open_pre_auth_code:" + coderesult.pre_auth_code);
                            }
                            else {
                                Util.LogHelper.Info("open_pre_auth_code无需更新:" + coderesult.pre_auth_code);
                            }
                        }
                        else {
                            Util.LogHelper.Info("更新open_pre_auth_code错误:" + coderesult.errcode.ToString());
                        }
                        wx_open.ModifyTime = DateTime.Now;
                        //保存到数据库
                        if (wx_openInfoService.instance().Update(wx_open) == 1)
                        {
                            Util.LogHelper.Info("推送component_verify_ticket协议--成功--end】");
                        }
                        else {
                            Util.LogHelper.Info("推送component_verify_ticket协议--失败--end】");
                        }






                        //if (wx_open.open_ticket != ticket)
                        //{
                        //    wx_open.open_ticket = ticket;
                        //    Util.LogHelper.Info("更新ticket:" + ticket);
                        //    var result = ComponentApi.GetComponentAccessToken(sAppID, sAppSecret, ticket);
                        //    if (result.errcode == Entity.Weixin.ReturnCode.请求成功)
                        //    {
                        //        wx_open.open_access_token = result.component_access_token;
                        //        wx_open.expires_in = result.expires_in;
                        //        Util.LogHelper.Info("更新open_access_token" + result.component_access_token);
                        //        var coderesult = ComponentApi.GetPreAuthCode(sAppID, result.component_access_token);
                        //        if (coderesult.errcode == Entity.Weixin.ReturnCode.请求成功)
                        //        {
                        //            wx_open.open_pre_auth_code = coderesult.pre_auth_code;
                        //            Util.LogHelper.Info("更新open_pre_auth_code" + coderesult.pre_auth_code);
                        //            if (wx_openInfoService.instance().Update(wx_open) == 1)
                        //            {
                        //                Util.LogHelper.Info("推送component_verify_ticket协议--成功--end");
                        //            }
                        //            else {
                        //                Util.LogHelper.Info("推送component_verify_ticket协议--失败--end");
                        //            }
                        //        }
                        //    }
                        //}
                        //else {
                        //    Util.LogHelper.Info("推送component_verify_ticket协议--不需更新--end");
                        //}
                        break;

                    case "UNAUTHORIZED"://取消授权通知
                        string appid = element.Element("AuthorizerAppid").Value;
                        Util.LogHelper.Info(appid + "取消授权--start");
                        var entity = wx_userweixinService.instance().SingleByAppId(appid);
                        if (entity != null)
                        {
                            entity.DR = true;
                            if (wx_userweixinService.instance().Update(entity) == 1)
                                Util.LogHelper.Info(appid + "取消授权--成功--start");
                            else
                                Util.LogHelper.Info(appid + "取消授权--失败--start");
                        }
                        break;
                    case "AUTHORIZED"://授权成功通知 
                        string au_appid = element.Element("AuthorizerAppid").Value.ToString();
                        Util.LogHelper.Info(au_appid + "授权--start");
                        var au_entity = wx_userweixinService.instance().SingleByAppId(au_appid);
                        if (au_entity != null)
                        {
                            au_entity.DR = false;
                            Util.LogHelper.Info(au_appid + "授权--更新--start");
                            if (wx_userweixinService.instance().Update(au_entity) == 1)
                                Util.LogHelper.Info(au_appid + "授权--成功--start");
                            else
                                Util.LogHelper.Info(au_appid + "授权--失败--start");
                        }
                        else
                        {
                            Util.LogHelper.Info(au_appid + "数据库没有数据");
                        }
                        /*<xml>
                            <AppId>第三方平台appid</AppId>
                            <CreateTime>1413192760</CreateTime>
                            <InfoType>authorized</InfoType>
                            <AuthorizerAppid>公众号appid</AuthorizerAppid>
                            <AuthorizationCode>授权码（code）</AuthorizationCode>
                            <AuthorizationCodeExpiredTime>过期时间</AuthorizationCodeExpiredTime>
                        </xml>*/
                        break;
                    case "UPDATEAUTHORIZED"://授权更新通知
                        /*
                        <xml>
                            <AppId>第三方平台appid</AppId>
                            <CreateTime>1413192760</CreateTime>
                            <InfoType>updateauthorized</InfoType>
                            <AuthorizerAppid>公众号appid</AuthorizerAppid>
                            <AuthorizationCode>授权码（code）</AuthorizationCode>
                            <AuthorizationCodeExpiredTime>过期时间</AuthorizationCodeExpiredTime>
                        </xml>*/
                        break;
                    default:
                        break;
                }
                #endregion
            }

        }
        /// <summary>
        /// 读取请求对象的内容
        /// 只能读一次
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <returns>string</returns>
        public static string ReadRequest(HttpRequestBase request)
        {
            string reqStr = string.Empty;
            using (Stream s = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                {
                    reqStr = reader.ReadToEnd();
                }
            }
            return reqStr;
        }
        /// <summary>
        /// 微信服务器会不间断推送最新的Ticket（10分钟一次）
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Callback(PostModel postModel)
        {
            var logPath = Server.MapPath(string.Format("~/App_Data/Open/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            wx_openInfo wx_open = wx_openInfoService.instance().Single(new Guid(System.Configuration.ConfigurationManager.AppSettings["openID"]));
            postModel.EncodingAESKey = wx_open.open_sEncodingAESKey; //根据自己后台的设置保持一致
            postModel.AppId = wx_open.open_sAppID; //根据自己后台的设置保持一致
            postModel.Token = wx_open.open_sToken;
            var maxRecordCount = 10;
            MessageHandler<CustomMessageContext> messageHandler = null;
            try
            {
                var checkPublish = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["checkPublish"]);// false; //是否在“全网发布”阶段
                if (checkPublish)
                {
                    messageHandler = new OpenCheckMessageHandler(Request.InputStream, postModel, 10);
                }
                else
                {
                    messageHandler = new CustomMessageHandler(Request.InputStream, postModel, maxRecordCount);
                }

                messageHandler.RequestDocument.Save(Path.Combine(logPath,
                    string.Format("{0}_Request_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.FromUserName)));
                messageHandler.Execute(); //执行
                return new FixWeixinBugWeixinResult(messageHandler);
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Open/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }

                    if (ex.InnerException != null)
                    {
                        tw.WriteLine("========= InnerException =========");
                        tw.WriteLine(ex.InnerException.Message);
                        tw.WriteLine(ex.InnerException.Source);
                        tw.WriteLine(ex.InnerException.StackTrace);
                    }

                    tw.Flush();
                    tw.Close();
                    return Content("");
                }
            }
        }
        public void Callback(string id)
        {
            //string cid = Request["cid"];
            //http://weixin.com/Open/Callback?id=02a07495-5484-4162-a70d-b7341096a1d4&auth_code=queryauthcode@@@zQaA25MwNP71mnGlwrwuRnx2lxw3NvCpK7n-BM01BJlBIkGIZaSluaGzAvdz_oX8sI4kc4wRx7MfLfaEqa8gJA&expires_in=3600
            Guid CID;
            int dbresult = 0;
            wx_openInfo wx_open = wx_openInfoService.instance().Single(new Guid(System.Configuration.ConfigurationManager.AppSettings["openID"]));
            if (Guid.TryParse(id, out CID))
            {
                string auth_code = Request["auth_code"];
                string expires_in = Request["expires_in"];
                var result = ComponentApi.GetComponentAccessToken(wx_open.open_sAppID, wx_open.open_sAppSecret, wx_open.open_ticket);
                if (result.errcode == Entity.Weixin.ReturnCode.请求成功)
                {
                    string open_access_token = result.component_access_token;
                    string open_sAppID = wx_open.open_sAppID;
                    var infoRes = ComponentApi.QueryAuth(open_access_token, open_sAppID, auth_code);
                    if (infoRes.errcode == Entity.Weixin.ReturnCode.请求成功)
                    {

                        wx_userweixin entity = wx_userweixinService.instance().SingleByCompanyID(CID);
                        if (entity == null)
                            entity = new wx_userweixin();
                        entity.expires_in = infoRes.authorization_info.expires_in;
                        entity.AppId = infoRes.authorization_info.authorizer_appid;
                        entity.Access_Token = infoRes.authorization_info.authorizer_access_token;
                        entity.refresh_token = infoRes.authorization_info.authorizer_refresh_token;

                        if (entity.ID == Guid.Empty)
                        {
                            entity.CompanyID = CID;
                            entity.ID = Guid.NewGuid();
                            dbresult = wx_userweixinService.instance().Insert(entity);
                        }
                        else {
                            dbresult = wx_userweixinService.instance().Update(entity);
                        }

                    }
                }
                if (dbresult > 0)
                {
                    UserDateTicket.wx_user = wx_userweixinService.instance().SingleByCompanyID(CID);
                    Response.Redirect("/Info/Index");
                }
                else
                {
                    Response.Write("<script>alert('拉去信息错误');</script>");
                }
            }


        }
        public class queue
        {
            /// <summary>
            /// 事件标识
            /// </summary>
            public string FromUserName { get; set; }
            /// <summary>
            /// 事件时间标识
            /// </summary>
            public string time { get; set; }
            /// <summary>
            /// 添加到队列的时间
            /// </summary>
            public DateTime CreateTime { get; set; }
        }
    }
}