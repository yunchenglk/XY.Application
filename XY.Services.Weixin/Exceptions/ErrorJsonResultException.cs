using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Exceptions
{
    public class ErrorJsonResultException : WeixinException
    {
        public WxJsonResult JsonResult { get; set; }
        public string Url { get; set; }
        public ErrorJsonResultException(string message, Exception inner, WxJsonResult jsonResult, string url = null)
           : base(message, inner)
        {
            JsonResult = jsonResult;
            Url = url;
            Util.LogHelper.Error("ErrorJsonResultException");
            Util.LogHelper.Error(string.Format("URL：{0}", url));
            Util.LogHelper.Error(string.Format("errcode：{0}", JsonResult.errcode));
            Util.LogHelper.Error(string.Format("errmsg：{0}", JsonResult.errmsg));
        }
    }
}
