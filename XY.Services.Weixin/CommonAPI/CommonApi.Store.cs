using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin
{
    /// <summary>
    /// 创建卡券接口
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 批量导入门店信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data">门店数据</param>
        /// <returns></returns>
        public static StoreResultJson Store_BatchAdd(string accessToken, StoreLocationData data)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/location/batchadd?access_token={0}", accessToken);

            return CommonJsonSend.Send<StoreResultJson>(null, urlFormat, data);
        }
        /// <summary>
        /// 拉取门店列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="offset">偏移量，0 开始</param>
        /// <param name="count">拉取数量</param>
        /// <returns></returns>
        public static StoreGetResultJson Store_BatchGet(string accessToken, int offset, int count)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/location/batchget?access_token={0}", accessToken);

            var data = new
            {
                offset = offset,
                count = count
            };

            return CommonJsonSend.Send<StoreGetResultJson>(null, urlFormat, data);
        }
    }
}
