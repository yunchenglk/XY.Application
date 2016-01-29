using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;
using XY.Util;

namespace XY.Services
{
    public static class WXApiJson
    {
        /// <summary>
        ///  生成菜单Json
        /// </summary>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        public static string GetMenuJsonStr(Guid CompanyID)
        {
            StringBuilder sb = new StringBuilder();
            List<button> btns = new List<button>();
            IEnumerable<WX_Menu> menus = WX_MenuService.instance().GetEnumByCompanyID(CompanyID);

            foreach (WX_Menu m in menus)
            {
                button btn = new button();
                btn.name = m.Name;
                btn.type = m.TypeStr;
                btn.url = m.URL;
                btn.key = m.KeyWord == null ? "" : m.KeyWord.KeyWords;
                if (m.Ishaschild)
                {
                    btn.sub_button = new List<sub_button>();
                    foreach (var item in m.Childs)
                    {
                        WX_KeyWord key = WX_KeyWordService.instance().GetEnumerableByID(item.KeyWordID).FirstOrDefault();
                        sub_button b = new sub_button();
                        b.name = item.Name;
                        b.key = key == null ? "" : key.KeyWords;
                        b.url = item.URL;
                        b.type = item.TypeStr;
                        btn.sub_button.Add(b);
                    }
                }
                btns.Add(btn);
            }
            sb.Append("{\"button\":");
            sb.Append(JsonHelper.SerializeObject(btns));
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 生成图文信息Json
        /// </summary>
        /// <param name="Messageid"></param>
        /// <returns></returns>
        public static string GetArticlesJsonStr(Guid Messageid)
        {
            WX_Message Messages = WX_MessageService.instance().Single(Messageid);
            StringBuilder sbArticlesJson = new StringBuilder();
            //  articles
            if (Messages.Groups.Count() > 0)
            {
                List<articles> list = new List<articles>();
                foreach (var item in Messages.Groups)
                {
                    articles a = new articles();
                    a.title = item.Title;
                    a.author = item.Author;
                    a.content = item.Content;
                    a.content_source_url = item.URL;
                    a.digest = "";
                    a.show_cover_pic = 0;
                    a.thumb_media_id = item.Img_media_id;
                    list.Add(a);
                }
                sbArticlesJson.Append("{\"articles\":");
                sbArticlesJson.Append(JsonHelper.SerializeObject(list));
                sbArticlesJson.Append("}");
                return sbArticlesJson.ToString();
            }
            return "";
        }

        /// <summary>
        /// 生成文本发送json
        /// </summary>
        /// <param name="text"></param>
        /// <param name="openidList"></param>
        /// <returns></returns>
        public static string CreateTextJson(string text, List<string> openidList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"touser\":[");
            sb.Append(string.Join(",", openidList.ConvertAll<string>(a => "\"" + a + "\"").ToArray()));
            sb.Append("],");
            sb.Append("\"msgtype\":\"text\",");
            sb.Append("\"text\":{\"content\":\"" + text.Trim() + "\"}");
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 生成图片发送json
        /// </summary>
        public static string CreateImageJson(string media_id, List<string> openidList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"touser\":[");
            sb.Append(string.Join(",", openidList.ConvertAll<string>(a => "\"" + a + "\"").ToArray()));
            sb.Append("],");
            sb.Append("\"msgtype\":\"image\",");
            sb.Append("\"image\":{\"media_id\":\"" + media_id + "\"}");
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 生成图文消息发送json
        /// </summary>
        public static string CreateNewsJson(string media_id, List<string> openidList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"touser\":[");
            sb.Append(string.Join(",", openidList.ConvertAll<string>(a => "\"" + a + "\"").ToArray()));
            sb.Append("],");
            sb.Append("\"msgtype\":\"mpnews\",");
            sb.Append("\"mpnews\":{\"media_id\":\"" + media_id + "\"}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
