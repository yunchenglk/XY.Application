namespace XY.Entity.Weixin
{
    public class BusinessInfo
    {
        //是否开通微信支付功能
        public int open_pay { get; set; }
        //是否开通微信摇一摇功能
        public int open_shake { get; set; }
        //是否开通微信扫商品功能
        public int open_scan { get; set; }
        //否开通微信卡券功能
        public int open_card { get; set; }
        //是否开通微信门店功能
        public int open_store { get; set; }
    }
}
