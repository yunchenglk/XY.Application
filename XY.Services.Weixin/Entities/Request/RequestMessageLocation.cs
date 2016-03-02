using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageLocation : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.LOCATION; }
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Location_X { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }
}
