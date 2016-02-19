using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin
{
    /// <summary>
    /// 语意理解接口
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 发送语义理解请求
        /// </summary>
        /// <returns></returns>
        public static SearchResultJson SemanticUnderStand(string accessToken, string query, string category, string city, string appid)
        {
            var urlFormat = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";
            var data = new
            {
                query = query,
                category = category,
                city = city,
                appid = appid
            };
            return CommonJsonSend.Send<SearchResultJson>(accessToken, urlFormat, data);
        }
    }
}
