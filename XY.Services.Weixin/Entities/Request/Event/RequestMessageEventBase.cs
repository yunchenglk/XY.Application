

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// 请求消息的事件推送消息基类
    /// </summary>
    public class RequestMessageEventBase : RequestMessageBase, IRequestMessageEventBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.EVENT; }
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual Event Event
        {
            get { return Event.ENTER; }
        }
    }
}
