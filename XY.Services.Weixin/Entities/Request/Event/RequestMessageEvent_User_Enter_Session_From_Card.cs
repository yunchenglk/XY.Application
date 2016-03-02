

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageEvent_User_Enter_Session_From_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 从卡券进入公众号会话
        /// </summary>
        public override Event Event
        {
            get { return Event.USER_ENTER_SESSION_FROM_CARD; }
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
