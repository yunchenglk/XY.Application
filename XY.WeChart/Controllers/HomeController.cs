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
using XY.Services;

namespace XY.WeChart.Controllers
{
    public class HomeController : Controller
    {
        //验证token
        public bool Check_token(HttpRequestBase request)
        {
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            List<string> list = new List<string>();
            list.Add(ConfigurationManager.AppSettings["token"].ToString());
            list.Add(timestamp);
            list.Add(nonce);
            //排序
            list.Sort();
            string GetStr = "";
            list.ForEach(a => GetStr += a);
            GetStr = SecurityUtility.SHA1Encrypt(GetStr);
            return GetStr == signature;
        }
        //解析XML
        public static IHandler CreateHandler(string requestXml)
        {
            IHandler handler = null;
            XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(requestXml);
            XmlNode node = doc.SelectSingleNode("/xml/MsgType");
            if (node != null)
            {
                XmlCDataSection section = node.FirstChild as XmlCDataSection;
                if (section != null)
                {
                    string msgType = section.Value;

                    LogHelper.Info(msgType);
                    LogHelper.Info(requestXml);
                    switch (msgType.ToUpper())
                    {
                        case "TEXT":
                            handler = new TextHandler(requestXml);
                            break;
                        case "EVENT":
                            handler = new EventHandler(requestXml);
                            break;
                    }
                }
            }
            return handler;
        }
        public string Index()
        {
            HttpRequestBase Request = HttpContext.Request;
            string method = Request.HttpMethod.ToUpper();
            LogHelper.Info(method + ":" + Request.Url.ToString());
            string responseMsg = "";
            switch (method)
            {
                case "GET":
                    if (Check_token(Request))
                    {
                        responseMsg = Request.QueryString["echostr"];
                    }
                    else
                    {
                        responseMsg = "错误";
                    }
                    break;
                case "POST":
                    using (Stream s = Request.InputStream)
                    {
                        using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                        {
                            IHandler handler = CreateHandler(reader.ReadToEnd());
                            responseMsg = handler.HandleRequest();
                        }
                    }
                    break;
                default:
                    break;
            }
            //LogHelper.Info(responseMsg);
            return responseMsg;
        }
    }
}