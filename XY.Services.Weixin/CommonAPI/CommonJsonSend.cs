﻿using System;
using System.IO;
using System.Text;
using XY.Entity.Weixin;
using XY.Services.Weixin.Exceptions;
using XY.Services.Weixin.Helpers;

namespace XY.Services.Weixin
{
    public enum CommonJsonSendType
    {
        GET,
        POST
    }
    public static class CommonJsonSend
    {
        #region old
        ///// <summary>
        ///// 向需要AccessToken的API发送消息的公共方法
        ///// </summary>
        ///// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        ///// <param name="urlFormat"></param>
        ///// <param name="data">如果是Get方式，可以为null</param>
        ///// <returns></returns>
        //public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST)
        //{
        //    return Send<WxJsonResult>(accessToken, urlFormat, data, sendType);
        //}
        ///// <summary>
        ///// 向需要AccessToken的API发送消息的公共方法
        ///// </summary>
        ///// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        ///// <param name="urlFormat"></param>
        ///// <param name="data">如果是Get方式，可以为null</param>
        ///// <returns></returns>
        //public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST)
        //{
        //    var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken);
        //    switch (sendType)
        //    {
        //        case CommonJsonSendType.GET:
        //            return HTTPGet.GetJson<T>(url);
        //        case CommonJsonSendType.POST:
        //            SerializerHelper serializerHelper = new SerializerHelper();
        //            var jsonString = serializerHelper.GetJsonString(data);
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                var bytes = Encoding.UTF8.GetBytes(jsonString);
        //                ms.Write(bytes, 0, bytes.Length);
        //                ms.Seek(0, SeekOrigin.Begin);

        //                return HTTPPost.PostGetJson<T>(url, null, ms);
        //            }
        //        default:
        //            throw new ArgumentOutOfRangeException("sendType");
        //    }
        //}
        #endregion
        #region 同步请求

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return Send<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut);
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat">用accessToken参数填充{0}</param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult"></param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            //TODO:此方法可以设定一个日志记录开关

            try
            {
                var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken);
                switch (sendType)
                {
                    case CommonJsonSendType.GET:
                        return HTTPGet.GetJson<T>(url);
                    case CommonJsonSendType.POST:
                        SerializerHelper serializerHelper = new SerializerHelper();
                        var jsonString = serializerHelper.GetJsonString(data, jsonSetting);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            var bytes = Encoding.UTF8.GetBytes(jsonString);
                            ms.Write(bytes, 0, bytes.Length);
                            ms.Seek(0, SeekOrigin.Begin);

                            return HTTPPost.PostGetJson<T>(url, null, ms, timeOut: timeOut, checkValidationResult: checkValidationResult);
                        }

                    //TODO:对于特定的错误类型自动进行一次重试，如40001（目前的问题是同样40001会出现在不同的情况下面）
                    default:
                        throw new ArgumentOutOfRangeException("sendType");
                }
            }
            catch (ErrorJsonResultException ex)
            {
                ex.Url = urlFormat;
                throw;
            }
        }

        #endregion

    }
}
