using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using XY.Entity;
using XY.Services;
using XY.Services.Weixin;

namespace XY.WeChart
{
    public enum RequestObjType
    {
        intType, stringType, floatType, guidType
    }
    public class CommFun
    {
        public static wx_userweixin wxconfig
        {
            get
            {
                return wx_userweixinService.instance().SingleByCompanyID(companyid);
            }
            set
            {

            }
        }
        public static Guid companyid { get; set; }
        public static string access_token
        {
            get
            {
                string error;
                return WeChartAPI.getAccessToken(companyid, out error);
                //var result = Services.Weixin.CommonAPI.CommonApi.GetToken(wxconfig.AppId, wxconfig.AppSecret);
                //if (result.errcode == Entity.Weixin.ReturnCode.请求成功)
                //    return result.access_token;
                //return "";

            }
        }
        /// <summary>
        /// 生成各消息类型XML
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        public static string MakeResponseXML(wx_requestRule rule, string requestXml)
        {

            var listContext = wx_requestRuleContentService.instance().GetByRuleID(rule.ID);
            switch (rule.responseType)//文本1，图文2，语音3，视频4,第三方接口5
            {
                case 1:
                    TextMessage tm = TextMessage.LoadFromXml(requestXml);
                    tm.Content = listContext.First().rContent;
                    return tm.GenerateContent();
                case 2:
                    NewsMessage nm = NewsMessage.LoadFromXml(requestXml);
                    nm.Group = listContext;
                    return nm.GenerateContent();
                case 3:
                    VoiceMessage vm = VoiceMessage.LoadFromXml(requestXml);
                    vm.access_token = access_token;
                    vm.filePath = listContext.First().mediaUrl;
                    return vm.GenerateContent();
                case 4:
                    VideoMessage vim = VideoMessage.LoadFromXml(requestXml);
                    vim.access_token = access_token;
                    vim.filePath = listContext.First().meidaHDUrl;
                    vim.Title = listContext.First().rContent;
                    vim.Description = listContext.First().rContent2;
                    return vim.GenerateContent();
                case 5:
                    break;
                default:
                    break;
            }
            return "";
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
        /// 判断request的参数是否合法
        /// </summary>
        /// <param name="param">参数名称</param>
        /// <param name="otype"></param>
        /// <returns></returns>
        public static bool IsRequestStr(string param, RequestObjType otype)
        {
            bool ret = true;
            if (HttpContext.Current.Request[param] == null || HttpContext.Current.Request[param].ToString().Trim() == "")
            {
                return false;
            }

            string pValue = HttpContext.Current.Request[param].ToString().Trim();

            switch (otype)
            {
                case RequestObjType.intType:
                    int tmpInt = 0;
                    if (!int.TryParse(pValue, out tmpInt))
                    {
                        return false;
                    }
                    break;
                case RequestObjType.floatType:
                    float tmpFloat = 0;
                    if (!float.TryParse(pValue, out tmpFloat))
                    {
                        return false;
                    }
                    break;
                case RequestObjType.stringType:
                    break;
                case RequestObjType.guidType:
                    Guid ID;
                    if (!Guid.TryParse(pValue, out ID))
                        return false;
                    break;
                default:
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 取request参数的值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Guid RequestGuid(string param)
        {
            if (IsRequestStr(param, RequestObjType.guidType))
            {
                return Guid.Parse(HttpContext.Current.Request[param]);
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}