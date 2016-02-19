using XY.Entity.Weixin;
using XY.Services.Weixin;

namespace XY.Services
{
    public static partial class WeChartAPI
    {
        public static CreateGroupResult CreateGroup(string accessToken, string name)
        {
            return  CommonApi.Groups_Create(accessToken, name);
        }
        public static WxJsonResult UpdateGroup(string accessToken, int id, string name)
        {
            return CommonApi.Groups_Update(accessToken, id, name);
        }
        public static WxJsonResult DeleteGroup(string accessToken, int id)
        {
            return CommonApi.Groups_Delete(accessToken, id);
        }
        public static OpenIdResultJson GetUsers(string accessToken)
        {
            return CommonApi.User_Get(accessToken);
        }
        public static UserInfoJson GetUserInfo(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            return CommonApi.User_Info(accessToken, openId);
        }
        public static WxJsonResult UserMoveGroup(string accessToken, string openId, int toGroupId)
        {
            return CommonApi.Groups_MemberUpdate(accessToken, openId, toGroupId);
        }
    }
}
