using System;
using XY.Entity;
using XY.Services.Weixin.CommonAPI;

namespace XY.Services
{
    public static partial class WeChartAPI
    {
        /// <summary>
        /// 及时获得access_token值
        /// access_token是公众号的全局唯一票据，公众号调用各接口时都需使用access_token。正常情况下access_token有效期为7200秒，
        /// 重复获取将导致上次获取的access_token失效。
        /// 每日限额获取access_token.我们将access_token保存到数据库里，间隔时间为20分钟，从微信公众平台获得一次。
        /// </summary>
        /// <returns></returns>
        public static string getAccessToken(Guid CompanyID, out string error)
        {
            string token = "";
            error = "";
            try
            {
                wx_userweixin wx = wx_userweixinService.instance().SingleByCompanyID(CompanyID);
                if (wx.AppId == null || wx.AppSecret == null || wx.AppId.Trim().Length <= 0 || wx.AppSecret.Trim().Length <= 0)
                {
                    error = "appId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！";
                    return "";
                }
                TimeSpan chajun = DateTime.Now - wx.ModifyTime;
                if (string.IsNullOrEmpty(wx.Access_Token) || chajun.TotalSeconds >= wx.expires_in)
                {
                    var result = CommonApi.GetToken(wx.AppId, wx.AppSecret);
                    wx.Access_Token = result.access_token;
                    wx.expires_in = result.expires_in;
                    wx.ModifyTime = DateTime.Now;
                    if (wx_userweixinService.instance().Update(wx) == 1)
                    {
                        token = result.access_token;
                    }
                }
                else
                {
                    token = wx.Access_Token;
                }
            }
            catch (Exception ex)
            {
                error = "获得access_token出错:" + ex.Message;
            }
            return token;
        }

        public static string ReloadToken(Guid CompanyID)
        {
            wx_userweixin wx = wx_userweixinService.instance().SingleByCompanyID(CompanyID);
            if (wx.AppId == null || wx.AppSecret == null || wx.AppId.Trim().Length <= 0 || wx.AppSecret.Trim().Length <= 0)
            {
                return "appId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！";
            }
            var result = CommonApi.GetToken(wx.AppId, wx.AppSecret);
            if (result.errcode == Entity.Weixin.ReturnCode.请求成功)
            {
                wx.Access_Token = result.access_token;
                wx.expires_in = result.expires_in;
                wx.ModifyTime = DateTime.Now;
                if (wx_userweixinService.instance().Update(wx) == 1)
                    return "ok";
                return "更新数据库出错";
            }
            return result.errcode.ToString();
        }
    }
}
