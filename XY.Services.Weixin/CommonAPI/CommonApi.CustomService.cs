using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using XY.Entity.Weixin;
using XY.Services.Weixin.Helpers;

namespace XY.Services.Weixin
{
    /// <summary>
    /// 多客服接口聊天记录接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E5%AE%A2%E6%9C%8D%E8%81%8A%E5%A4%A9%E8%AE%B0%E5%BD%95
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 添加客服。每个公众号最多添加10个客服账号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static WxJsonResult Custom_Add(string accessToken, Custom_add data)
        {
            var urlFormat = "https://api.weixin.qq.com/customservice/kfaccount/add?access_token={0}";
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 修改客服
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static WxJsonResult Custom_Edit(string accessToken, Custom_add data)
        {
            var urlFormat = "https://api.weixin.qq.com/customservice/kfaccount/update?access_token={0}";
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 删除客服帐号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static WxJsonResult Custom_Del(string accessToken, Custom_add data)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}&kf_account={1}", accessToken, data.kf_account);
            return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, null, CommonJsonSendType.GET);
        }
        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CustomUploadHeadimg(string accessToken, string kfAccount, string file, int timeOut = Config.TIME_OUT)
        {

            file = FileHelper.DownLoadFile(file);
            var url = string.Format("http://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken, kfAccount);
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            var result = HTTPPost.PostFileGetJson<WxJsonResult>(url, null, fileDictionary, null, timeOut: timeOut);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            return result;
        }
        /// <summary>
        /// 获取用户聊天记录
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="startTime">查询开始时间，会自动转为UNIX时间戳</param>
        /// <param name="endTime">查询结束时间，会自动转为UNIX时间戳，每次查询不能跨日查询</param>
        /// <param name="openId">（非必须）普通用户的标识，对当前公众号唯一</param>
        /// <param name="pageSize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageIndex">查询第几页，从1开始</param>
        public static GetRecordResult GetRecord(string accessToken, DateTime startTime, DateTime endTime, string openId = null, int pageSize = 10, int pageIndex = 1)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/customservice/getrecord?access_token={0}";

            //规范页码
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            else if (pageSize > 1000)
            {
                pageSize = 1000;
            }


            //组装发送消息
            var data = new
            {
                starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                endtime = DateTimeHelper.GetWeixinDateTime(endTime),
                openId = openId,
                pagesize = pageSize,
                pageIndex = pageIndex
            };

            return CommonJsonSend.Send<GetRecordResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 获取在线客服接待信息
        /// 官方API：http://dkf.qq.com/document-3_2.html
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public static CustomOnlineJson GetCustomOnlineInfo(string accessToken)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken);
            return GetCustomInfoResult<CustomOnlineJson>(urlFormat);
        }
        /// <summary>
		/// 获取客服基本信息
		/// 官方API：http://dkf.qq.com/document-3_1.html
		/// </summary>
		/// <param name="accessToken">调用接口凭证</param>
		/// <returns></returns>
		public static CustomInfoJson GetCustomBasicInfo(string accessToken)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}", accessToken);
            return GetCustomInfoResult<CustomInfoJson>(urlFormat);
        }

        private static T GetCustomInfoResult<T>(string urlFormat)
        {
            var jsonString = RequestUtility.HttpGet(urlFormat, Encoding.UTF8);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(jsonString);
        }
    }
}
