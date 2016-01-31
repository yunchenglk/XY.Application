using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XY.Entity;
using XY.Util;

namespace XY.Services
{
    public static class WXApi
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        public static string GetToken(string appid, string secret)
        {
            string strJson = HttpUtitls.RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret));
            return Utils.GetJsonValue(strJson, "access_token");
        }
        /// <summary>
        /// 验证Token是否过期
        /// </summary>
        public static bool TokenExpired(string access_token)
        {
            string jsonStr = HttpUtitls.RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", access_token));
            if (string.IsNullOrEmpty(Utils.GetJsonValue(jsonStr, "errcode")))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="Companyid"></param>
        /// <returns></returns>
        public static string CreateMenu(string access_token, Guid Companyid)
        {
            string menuJsonStr = WXApiJson.GetMenuJsonStr(Companyid);
            return HttpUtitls.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", access_token), menuJsonStr);
        }
        /// <summary>
        /// 上传图文消息素材返回media_id
        /// </summary>
        public static string UploadNews(string access_token, string postData)
        {
            return HttpUtitls.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", access_token), postData);
        }
        /// <summary>
        /// 上传媒体返回媒体ID
        /// </summary>
        public static string UploadMedia(string access_token, string type, string path)
        {
            // 设置参数
            string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", access_token, type);
            string savePath = HttpContext.Current.Server.MapPath(path);
            string dirPath = HttpContext.Current.Server.MapPath(path.Substring(0, path.LastIndexOf("/") + 1));
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.Configuration.WebConfigurationManager.AppSettings["sourceWeb"] + path);
                response = request.GetResponse();
                stream = response.GetResponseStream();

                byte[] buffer = new byte[1024];
                Stream outStream = null;
                Stream inStream = null;
                if (File.Exists(savePath)) File.Delete(savePath);

                if (!Directory.Exists(dirPath))
                {
                    DirectoryInfo d = new DirectoryInfo(dirPath);
                    d.Create();
                }
                outStream = System.IO.File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0) outStream.Write(buffer, 0, l);
                } while (l > 0);
                if (outStream != null) outStream.Close();
                if (inStream != null) inStream.Close();
            }
            finally
            {
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
            return HttpUtitls.HttpUploadFile(url, savePath, "image");
        }
        /// <summary>
        /// 获取关注者OpenID集合
        /// </summary>
        public static List<string> GetOpenIDs(string access_token)
        {
            List<string> result = new List<string>();
            List<string> openidList = GetOpenIDs(access_token, null);
            result.AddRange(openidList);

            while (openidList.Count > 0)
            {
                openidList = GetOpenIDs(access_token, openidList[openidList.Count - 1]);
                result.AddRange(openidList);
            }

            return result;
        }
        /// <summary>
        /// 获取关注者OpenID集合
        /// </summary>
        public static List<string> GetOpenIDs(string access_token, string next_openid)
        {
            // 设置参数
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", access_token, string.IsNullOrWhiteSpace(next_openid) ? "" : next_openid);
            string returnStr = HttpUtitls.RequestUrl(url);
            int count = int.Parse(Utils.GetJsonValue(returnStr, "count"));
            if (count > 0)
            {
                string startFlg = "\"openid\":[";
                int start = returnStr.IndexOf(startFlg) + startFlg.Length;
                int end = returnStr.IndexOf("]", start);
                string openids = returnStr.Substring(start, end - start).Replace("\"", "");
                return openids.Split(',').ToList<string>();
            }
            else
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// 上传永久素材（图片）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Material_add_img(string access_token, string path)
        {

            string savePath = HttpContext.Current.Server.MapPath(path);
            string dirPath = HttpContext.Current.Server.MapPath(path.Substring(0, path.LastIndexOf("/") + 1));
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.Configuration.WebConfigurationManager.AppSettings["sourceWeb"] + path);
                response = request.GetResponse();
                stream = response.GetResponseStream();

                byte[] buffer = new byte[1024];
                Stream outStream = null;
                Stream inStream = null;
                if (File.Exists(savePath)) File.Delete(savePath);

                if (!Directory.Exists(dirPath))
                {
                    DirectoryInfo d = new DirectoryInfo(dirPath);
                    d.Create();
                }
                outStream = System.IO.File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0) outStream.Write(buffer, 0, l);
                } while (l > 0);
                if (outStream != null) outStream.Close();
                if (inStream != null) inStream.Close();
            }
            finally
            {
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type=image", access_token);
            string rsult_msg = HttpUtitls.HttpUploadFile(url, savePath, "image");
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            return rsult_msg;
        }
        /// <summary>
        /// 新增永久素材（图文）
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Material_add_news(string access_token, string postData)
        {
            return HttpUtitls.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}", access_token), postData);
        }
        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Material_get(string access_token, string postData)
        {
            return HttpUtitls.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}", access_token), postData);
        }

        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        public static string Send(string access_token, string postData)
        {
            return HttpUtitls.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", access_token), postData);
        }
    }
}
