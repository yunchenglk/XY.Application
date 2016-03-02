using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Services.Weixin.Exceptions
{
    public class MessageHandlerException : WeixinException
    {
        public MessageHandlerException(string message)
          : base(message, null)
        {
        }

        public MessageHandlerException(string message, Exception inner)
          : base(message, inner)
        {
        }
    }
}
