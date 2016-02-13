using XY.Entity.Weixin;
using XY.Weixin.MP.AdvancedAPI;

namespace XY.Services
{
    public static partial class WeChartAPI
    {
        public static CreateGroupResult CreateGroup(string accessToken, string name)
        {
            return Groups.Create(accessToken, name);
        }
        public static WxJsonResult UpdateGroup(string accessToken, int id, string name)
        {
            return Groups.Update(accessToken, id, name);
        }
        public static WxJsonResult DeleteGroup(string accessToken, int id)
        {
            return Groups.Delete(accessToken, id);
        }
        public static OpenIdResultJson GetUsers(string accessToken)
        {
            return User.Get(accessToken);
        }
        public static UserInfoJson GetUserInfo(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            return User.Info(accessToken, openId);
        }
        public static WxJsonResult UserMoveGroup(string accessToken, string openId, int toGroupId)
        {
            return Groups.MemberUpdate(accessToken, openId, toGroupId);
        }
    }
}
