

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// 事件之接收会员信息事件通知（submit_membercard_user_info）
    /// 卡券 会员卡
    /// </summary>
    public class RequestMessageEvent_Submit_Membercard_User_Info : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SUBMIT_MEMBERCARD_USER_INFO; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
