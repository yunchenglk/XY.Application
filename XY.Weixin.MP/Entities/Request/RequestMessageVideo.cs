namespace XY.Entity.Weixin.MP
{
    public class RequestMessageVideo : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Video; }
        }

        public string MediaId { get; set; }
        public string ThumbMediaId { get; set; }
    }
}
