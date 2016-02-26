using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageBase : MessageBase, IRequestMessageBase
    {
        public string Encrypt { get; set; }

        public long MsgId { get; set; }

        public virtual RequestMsgType MsgType
        {
            get { return RequestMsgType.TEXT; }
        }
    }
}
