using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XY.Weixin.MP.Context
{
    public class WeixinContextRemovedEventArgs : EventArgs
    {
        public string OpenId
        {
            get
            {
                return this.MessageContext.UserName;
            }
        }
        public DateTime LastActiveTime
        {
            get
            {
                return this.MessageContext.LastActiveTime;
            }
        }
        public IMessageContext MessageContext
        {
            get;
            set;
        }
        public WeixinContextRemovedEventArgs(IMessageContext messageContext)
        {
            this.MessageContext = messageContext;
        }
    }
}
