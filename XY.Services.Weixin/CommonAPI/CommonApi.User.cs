using XY.Entity.Weixin;

namespace XY.Services.Weixin
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public static partial class CommonApi
    {
        /// <summary>
        /// 获取用户基本信息（包括UnionID机制）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static UserInfoJson User_Info(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                accessToken, openId, lang.ToString());
            return HTTPGet.GetJson<UserInfoJson>(url);

            //错误时微信会返回错误码等信息，JSON数据包示例如下（该示例为AppID无效错误）:
            //{"errcode":40013,"errmsg":"invalid appid"}
        }
        /// <summary>
        /// 获取关注着OpenId信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static OpenIdResultJson User_Get(string accessToken)
        {
            OpenIdResultJson data = User_Get(accessToken, null);
            //while (data.count > 0)
            //{
            //    var result = Get(accessToken, data.next_openid);
            //    data.data.openid.AddRange(result.data.openid);
            //    data.next_openid = result.next_openid;
            //    data.count = result.count;
            //}
            return User_Get(accessToken, null);
        }
        private static OpenIdResultJson User_Get(string accessToken, string nextOpenId)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}",
                accessToken);
            if (!string.IsNullOrEmpty(nextOpenId))
            {
                url += "&next_openid=" + nextOpenId;
            }
            return HTTPGet.GetJson<OpenIdResultJson>(url);
        }
    }
}
