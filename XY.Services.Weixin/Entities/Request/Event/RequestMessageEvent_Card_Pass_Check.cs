

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageEvent_Card_Pass_Check : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.CARD_PASS_CHECK; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
    }
}
