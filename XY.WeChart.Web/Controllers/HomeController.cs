using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;
using XY.Entity;
using XY.Services;
using XY.Services.Weixin;

namespace XY.WeChart.Web.Controllers
{
    [Filters.AuthorizeFilter]
    public class HomeController : Controller
    {
        private static List<queue> _queue;
        public ActionResult JSCallBack()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        /// <summary>
        /// 授权事件接收URL
        /// </summary>
        public void wxCall()
        {
            HttpRequestBase Request = HttpContext.Request;
            string sToken = UserDateTicket.wx_open.open_sToken;
            string sAppID = UserDateTicket.wx_open.open_sAppID;
            string sAppSecret = UserDateTicket.wx_open.open_sAppSecret;
            string sEncodingAESKey = UserDateTicket.wx_open.open_sEncodingAESKey;
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
                if (_queue.FirstOrDefault(m => { return m.time == element.Element("CreateTime").Value.ToLower().ToString() && m.InfoType == element.Element("InfoType").Value.ToLower().ToString(); }) == null)
                {
                    _queue.Add(new queue
                    {
                        CreateTime = DateTime.Now,
                        InfoType = element.Element("InfoType").Value.ToLower().ToString(),
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
                        Util.LogHelper.Info("推送component_verify_ticket协议--start");
                        string ticket = element.Element("ComponentVerifyTicket").Value.ToString();
                        if (UserDateTicket.wx_open.open_ticket != ticket)
                        {
                            UserDateTicket.wx_open.open_ticket = ticket;
                            Util.LogHelper.Info("更新ticket:" + ticket);
                            var result = ComponentApi.GetComponentAccessToken(sAppID, sAppSecret, ticket);
                            if (result.errcode == Entity.Weixin.ReturnCode.请求成功)
                            {
                                UserDateTicket.wx_open.open_access_token = result.component_access_token; ;
                                Util.LogHelper.Info("更新open_access_token" + result.component_access_token);
                                var coderesult = ComponentApi.GetPreAuthCode(sAppID, result.component_access_token);
                                if (coderesult.errcode == Entity.Weixin.ReturnCode.请求成功)
                                {
                                    UserDateTicket.wx_open.open_pre_auth_code = coderesult.pre_auth_code;
                                    Util.LogHelper.Info("更新open_pre_auth_code" + coderesult.pre_auth_code);
                                    if (wx_openInfoService.instance().Update(UserDateTicket.wx_open) == 1)
                                    {
                                        Util.LogHelper.Info("推送component_verify_ticket协议--成功--end");
                                    }
                                    else {
                                        Util.LogHelper.Info("推送component_verify_ticket协议--失败--end");
                                    }
                                }
                            }
                        }
                        else {
                            Util.LogHelper.Info("推送component_verify_ticket协议--不需更新--end");
                        }
                        break;

                    case "UNAUTHORIZED"://取消授权通知
                        /*
                        <xml>
                            <AppId>第三方平台appid</AppId>
                            <CreateTime>1413192760</CreateTime>
                            <InfoType>unauthorized</InfoType>
                            <AuthorizerAppid>公众号appid</AuthorizerAppid>
                        </xml>
                        */
                        break;
                    case "AUTHORIZED"://授权成功通知
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
        public void Callback()
        {
            string cid = Request["cid"];
            Guid CID;
            int dbresult = 0;
            if (Guid.TryParse(cid, out CID))
            {
                string auth_code = Request["auth_code"];
                string expires_in = Request["expires_in"];


                string open_access_token = UserDateTicket.wx_open.open_access_token;
                string open_sAppID = UserDateTicket.wx_open.open_sAppID;
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

                    if (entity == null)
                    {
                        entity.CompanyID = CID;
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
                Response.Redirect("/Info/Index/id=true");
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
        public string InfoType { get; set; }
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