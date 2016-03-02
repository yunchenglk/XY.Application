

using System.Collections.Generic;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// 事件之摇一摇事件通知(ShakearoundUserShake)
    /// </summary>
    public class RequestMessageEvent_ShakearoundUserShake : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SHAKEAROUNDUSERSHAKE; }
        }

        /// <summary>
        /// 最近的IBeacon信息
        /// </summary>
        public ChosenBeacon ChosenBeacon { get; set; }

        /// <summary>
        /// 附近的IBeacon信息
        /// </summary>
        public List<AroundBeacon> AroundBeacons { get; set; }
    }
}
