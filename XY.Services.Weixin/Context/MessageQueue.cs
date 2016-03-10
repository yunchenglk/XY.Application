using System.Collections.Generic;
using XY.Services.Weixin.Entities;

namespace XY.Services.Weixin.Context
{
    /// <summary>
    /// 微信消息列队（针对单个账号的往来消息）
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    public class MessageQueue<TM, TRequest, TResponse> : List<TM>
        where TM : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {

    }
}
