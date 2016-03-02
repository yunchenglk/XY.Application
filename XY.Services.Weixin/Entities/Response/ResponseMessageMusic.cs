using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class ResponseMessageMusic : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Image; }
        }

        public Music Music { get; set; }
        //public string ThumbMediaId { get; set; } //把该参数移动到Music对象内部
        public ResponseMessageMusic()
        {
            Music = new Music();
        }
    }
}
