namespace XY.Entity.Weixin
{
    /// <summary>
    /// 获取授权方的账户信息返回结果
    /// </summary>
    public class GetAuthorizerInfoResult : WxJsonResult
    {
        /// <summary>
        /// 授权方信息
        /// </summary>
        public AuthorizerInfo authorizer_info { get; set; }
        /// <summary>
        /// 授权信息
        /// </summary>
        public AuthorizationInfo authorization_info { get; set; }
    }
}
