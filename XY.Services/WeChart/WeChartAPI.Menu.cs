using XY.Entity.Weixin;
using XY.Services.Weixin;

namespace XY.Services
{
    public static partial class WeChartAPI
    {
        /// <summary>
        /// 获取当前菜单，如果菜单不存在，将返回null
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenu(string accessToken)
        {
            return CommonApi.GetMenu(accessToken);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="buttonData"></param>
        /// <returns></returns>
        public static WxJsonResult CreateMenu(string accessToken, ButtonGroup buttonData)
        {
            return CommonApi.CreateMenu(accessToken, buttonData);
        }

    }
}
