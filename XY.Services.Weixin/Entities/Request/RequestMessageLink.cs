using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageLink : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.LINK; }
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
}
