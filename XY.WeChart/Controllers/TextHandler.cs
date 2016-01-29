using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XY.Entity;
using XY.Services;

namespace XY.WeChart.Controllers
{
    /// <summary>
    /// 文本信息处理类
    /// </summary>
    public class TextHandler : IHandler
    {
        /// <summary>
        /// 请求的XML
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        public TextHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            string response = string.Empty;
            TextMessage tm = TextMessage.LoadFromXml(RequestXml);
            string orgid = tm.ToUserName;
            string content = tm.Content.Trim();
            if (string.IsNullOrEmpty(content))
            {
                response = "您什么都没输入，没法帮您啊，%>_<%。";
            }
            else
            {
                response = HandleOther(content, orgid);
            }
            tm.Content = response;
            //进行发送者、接收者转换
            string temp = tm.ToUserName;
            tm.ToUserName = tm.FromUserName;
            tm.FromUserName = temp;
            response = tm.GenerateContent();
            return response;
        }
        /// <summary>
        /// 处理其他消息
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        private string HandleOther(string key, string orgid)
        {

            WX_Config config = WX_ConfigService.instance().SingleByOrgID(orgid);
            WX_KeyWord kwentiy = WX_KeyWordService.instance().SingleByCIDAndKey(config.CompanyID, key);

            string response = "您说的，可惜，我没明白啊，试试其他关键字吧。";
            if (kwentiy != null && kwentiy.Type == 1)
            {
                response = kwentiy.Content;
            }
            return response;
        }
    }
}