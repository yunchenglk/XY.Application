﻿

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// 事件之Wi-Fi连网成功
    /// </summary>
    public class RequestMessageEvent_WifiConnected : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.WIFICONNECTED; }
        }

        /// <summary>
        /// 连网时间
        /// </summary>
        public int ConnectTime { get; set; }
        /// <summary>
        /// 系统保留字段，固定值
        /// </summary>
        public int ExpireTime { get; set; }
        /// <summary>
        /// 系统保留字段，固定值
        /// </summary>
        public string VendorId { get; set; }
        /// <summary>
        /// 连网的门店id
        /// </summary>
        public string PlaceId { get; set; }
        /// <summary>
        /// 连网的设备无线mac地址，对应bssid
        /// </summary>
        public string DeviceNo { get; set; }
    }
}
