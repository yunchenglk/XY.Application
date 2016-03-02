
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageEvent_Card_Not_Pass_Check : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.CARD_NOT_PASS_CHECK; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
    }
}
