using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Services.Weixin.Exceptions
{
    public class WeixinMenuException : WeixinException
    {
        public WeixinMenuException(string name): base(name,null)
        {

        }
        public WeixinMenuException(string message, Exception inner)
           : base(message, inner)
        {
        }
    }
}
