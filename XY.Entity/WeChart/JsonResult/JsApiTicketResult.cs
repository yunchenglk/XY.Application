namespace XY.Entity.Weixin
{
    /// <summary>
    /// jsapi_ticket请求后的JSON返回格式
    /// </summary>
    public class JsApiTicketResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
