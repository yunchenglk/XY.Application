using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin
{
    public partial class CommonApi
    {
        /// <summary>
        /// 获取自动回复规则
        /// </summary>
        public static class AutoReplyApi
        {
            /// <summary>
            /// 获取自动回复规则
            /// </summary>
            /// <param name="accessTokenOrAppId">调用接口凭证</param>
            /// <returns></returns>
            public static GetCurrentAutoreplyInfoResult GetCurrentAutoreplyInfo(string accessToken)
            {
                string urlFormat = "https://api.weixin.qq.com/cgi-bin/get_current_autoreply_info?access_token={0}";
                return CommonJsonSend.Send<GetCurrentAutoreplyInfoResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
            }
        }
    }
}
