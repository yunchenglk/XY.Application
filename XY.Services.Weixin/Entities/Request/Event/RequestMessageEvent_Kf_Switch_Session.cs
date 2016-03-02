

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// 事件之多客服转接会话kf_switch_session)
    /// </summary>
    public class RequestMessageEvent_Kf_Switch_Session : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.KF_SWITCH_SESSION; }
        }

        /// <summary>
        /// 完整客服账号，格式为：账号前缀@公众号微信号
        /// </summary>
        public string FromKfAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ToKfAccount { get; set; }
    }
}
