﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XY.Entity.Weixin;
using XY.Services.Weixin.Exceptions;

namespace XY.Services.Weixin
{
    public static class HTTPGet
    {
        public static T GetJson<T>(string url, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpGet(url, encoding);

            JavaScriptSerializer js = new JavaScriptSerializer();

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
                                        (int)errorResult.errcode,
                                        errorResult.errmsg),
                                      null, errorResult);
                }
            }

            T result = js.Deserialize<T>(returnText);

            return result;
        }

        public static void Download(string url, Stream stream)
        {
            WebClient wc = new WebClient();
            var data = wc.DownloadData(url);
            foreach (var b in data)
            {
                stream.WriteByte(b);
            }
        }
    }
}