using System;
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
    public static class WSApi
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        public static string GetToken(string appkey, string secret)
        {
            string strJson = HttpUtitls.RequestUrl(string.Format("https://api.vdian.com/token?grant_type=client_credential&appkey={0}&secret={1}", appkey, secret));
            return Utils.GetJsonValue(strJson, "access_token");
        }
        /// <summary>
        /// 新增商品分类
        /// </summary>
        /// <param name="getData"></param>
        /// <returns></returns>
        public static string vdian_shop_cate_add(string getData)
        {
            string strJson = HttpUtitls.RequestUrl(string.Format("http://api.vdian.com/api?param={0}", getData), "GET");
            return strJson;
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <param name="getData"></param>
        /// <returns></returns>
        public static string vdian_shop_cate_get(string access_token)
        {
            string getData = JsonHelper.SerializeObject(new @public("vdian.shop.cate.get", access_token));
            string strJson = HttpUtitls.RequestUrl(string.Format("http://api.vdian.com/api?param={0}&public={1}", "{}", getData), "GET");
            return strJson;
        }
        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="cate_id"></param>
        /// <returns></returns>
        public static string vdian_shop_cate_del(string access_token, string cate_id)
        {
            string getData = JsonHelper.SerializeObject(new @public("vdian.shop.cate.delete", access_token));
            string strJson = HttpUtitls.RequestUrl(string.Format("http://api.vdian.com/api?param={0}&public={1}", ("{\"cate_id\":" + cate_id + "}"), getData), "GET");
            return strJson;
        }
        /// <summary>
        /// 创建微店商品
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="getData"></param>
        /// <returns></returns>
        public static string vdian_item_add(string access_token, string getData)
        {
            string _public = JsonHelper.SerializeObject(new @public("vdian.item.add", access_token));
            string strJson = HttpUtitls.RequestUrl(string.Format("http://api.vdian.com/api?public={0}&param={1}", _public, getData), "GET");
            return strJson;
        }
        /// <summary>
        /// 删除微店商品
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public static string vdian_item_delete(string access_token, string itemid)
        {
            string _public = JsonHelper.SerializeObject(new @public("vdian.item.delete", access_token));
            string strJson = HttpUtitls.RequestUrl(string.Format("http://api.vdian.com/api?param={0}&public={1}", ("{\"itemid\":" + itemid + "}"), _public), "GET");
            return strJson;
        }
        public static string upload(string access_token, string path)
        {
            string url = string.Format("http://api.vdian.com/media/upload?access_token={0}", access_token);
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
            string result = HttpUtitls.HttpUploadFile(url, savePath, "image");
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            return result;
        }




    }
}
