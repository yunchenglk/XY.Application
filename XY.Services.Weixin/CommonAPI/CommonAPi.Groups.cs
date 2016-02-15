using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.CommonAPI
{
    /// <summary>
    /// 用户组接口
    /// </summary>
    public static partial class CommonApi
    {
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public static CreateGroupResult Groups_Create(string accessToken, string name)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
            var data = new
            {
                group = new
                {
                    name = name
                }
            };
            return CommonJsonSend.Send<CreateGroupResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GroupsJson Groups_GetAll(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";
            var url = string.Format(urlFormat, accessToken);
            return HTTPGet.GetJson<GroupsJson>(url);
        }
        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">用户的OpenID</param>
        /// <returns></returns>
        public static GetGroupIdResult Groups_GetId(string accessToken, string openId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";
            var data = new { openid = openId };
            return CommonJsonSend.Send<GetGroupIdResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="id">分组id，由微信分配</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        public static WxJsonResult Groups_Update(string accessToken, int id, string name)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";
            var data = new
            {
                group = new
                {
                    id = id,
                    name = name
                }
            };
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">用户唯一标识符</param>
        /// <param name="toGroupId">分组id</param>
        /// <returns></returns>
        public static WxJsonResult Groups_MemberUpdate(string accessToken, string openId, int toGroupId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";
            var data = new
            {
                openid = openId,
                to_groupid = toGroupId
            };
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="id">分组的id</param>
        /// <returns></returns>
        public static WxJsonResult Groups_Delete(string accessToken, int id)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}";
            var data = new
            {
                group = new
                {
                    id = id
                }
            };
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }
    }
}
