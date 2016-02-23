using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using XY.Entity;
using XY.Services;

namespace XY.WeChart.Controllers
{
    public class HomeController : Controller
    {
        private static object obj = new object();
        public string Index()
        {
            Guid CompanyID = CommFun.RequestGuid("cid");
            if (CompanyID.Equals(Guid.Empty))
            {
                return ("参数非法");
            }
            lock (obj)
            {
                CommFun.companyid = CompanyID;
                if (string.IsNullOrEmpty(CommFun.access_token))
                {
                    return ("不存在该微信号！");
                }
                HttpRequestBase Request = HttpContext.Request;
                string method = Request.HttpMethod.ToUpper();


                switch (method)
                {
                    case "GET":
                        string signature = Request["signature"];
                        string timestamp = Request["timestamp"];
                        string nonce = Request["nonce"];
                        string echostr = Request["echostr"];
                        if (CheckSignature.Check(signature, timestamp, nonce))
                            return (echostr);
                        else
                            return ("failed:" + signature + ",token:" + CommFun.access_token + " " + CheckSignature.GetSignature(timestamp, nonce, CommFun.access_token) + "。" +
                                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                    case "POST":
                        string requestXml = CommFun.ReadRequest(Request);
                        IHandler handler = HandlerFactory.CreateHandler(requestXml);
                        if (handler != null)
                        {
                            return handler.HandleRequest();
                        }
                        return string.Empty;
                    default:
                        return "无法处理";
                }
            }
        }
    }
}