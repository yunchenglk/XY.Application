using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Services.Weixin.Exceptions
{
    public class UnknownRequestMsgTypeException : WeixinException
    {
        public UnknownRequestMsgTypeException(string message)
           : base(message, null)
        {
        }

        public UnknownRequestMsgTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
