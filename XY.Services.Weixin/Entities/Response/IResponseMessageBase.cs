using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public interface IResponseMessageBase : IMessageBase
    {
        ResponseMsgType MsgType { get; }
        string Content { get; set; }
        bool FuncFlag { get; set; }
    }
}
