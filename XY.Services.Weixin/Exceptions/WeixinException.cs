using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Services.Weixin.Exceptions
{
    public class WeixinException : ApplicationException
    {
        /// <summary>
        /// 微信自定义异常基类
        /// </summary>
        public WeixinException(string message) : base(message, null)
        {

        }
        public WeixinException(string message, Exception inner)
           : base(message, inner)
        {
        }
    }
}
