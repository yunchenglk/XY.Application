namespace XY.Entity.Weixin.MP
{
    public class ResponseMessageMusic : ResponseMessageBase, IResponseMessageBase
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Music; }
        }

        public Music Music { get; set; }
        //public string ThumbMediaId { get; set; } //把该参数移动到Music对象内部
        public ResponseMessageMusic()
        {
            Music = new Music();
        }
    }
}
