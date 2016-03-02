using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageImage : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.IMAGE; }
        }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
    }
}
