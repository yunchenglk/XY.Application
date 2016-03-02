

using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    /// <summary>
    /// Beacon的参数以及距离
    /// </summary>
    public class BaseBeaconItem
    {
        public string Uuid { get; set; }
        public long Major { get; set; }
        public long Minor { get; set; }
        /// <summary>
        /// 设备与用户的距离（浮点数；单位：米）
        /// </summary>
        public double Distance { get; set; }
    }

    public class ChosenBeacon : BaseBeaconItem
    {
        
    }

    public class AroundBeacon : BaseBeaconItem
    {

    }
}
