namespace XY.Entity.Weixin
{
    /// <summary>
    /// 二维码创建返回结果
    /// </summary>
    public class QrCodeResult : WxJsonResult
    {
        public string ticket { get; set; }
        public int expire_seconds { get; set; }
    }
}
