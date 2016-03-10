using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public interface IRequestMessageBase : IMessageBase
    {
        RequestMsgType MsgType { get; }
        string Encrypt { get; set; }
        long MsgId { get; set; }
    }
}
