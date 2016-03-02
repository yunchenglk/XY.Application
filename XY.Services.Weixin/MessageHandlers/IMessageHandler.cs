using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Services.Weixin.Entities;

namespace XY.Services.Weixin.MessageHandlers
{
    public interface IMessageHandler : IMessageHandler_<IRequestMessageBase, IResponseMessageBase>
    {
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }
}
