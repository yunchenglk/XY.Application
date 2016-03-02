

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// 事件之二维码扫描（关注微信）
    /// </summary>
    public class RequestMessageEvent_Scan : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SCAN; }
        }

        public string Ticket { get; set; }

        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; }
    }
}
