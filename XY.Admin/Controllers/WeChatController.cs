using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;
using XY.Util;

namespace XY.Admin.Controllers
{
    public class WeChatController : Controller
    {
        public string GetToken()
        {
            string access_token = string.Empty;
            if (string.IsNullOrEmpty(UserDateTicket.wx_config.access_token))
            {
                access_token = WXApi.GetToken(UserDateTicket.wx_config.AppID, UserDateTicket.wx_config.AppSecret);
            }
            else
            {
                if (WXApi.TokenExpired(UserDateTicket.wx_config.access_token))
                {
                    access_token = WXApi.GetToken(UserDateTicket.wx_config.AppID, UserDateTicket.wx_config.AppSecret);
                }
                else
                {
                    return UserDateTicket.wx_config.access_token;
                }
            }
            WX_Config m = WX_ConfigService.instance().SingleByCompanyID(UserDateTicket.Company.ID);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("access_token", access_token);
            WX_ConfigService.instance().ModifColumn(dic, m.ID.ToString());
            UserDateTicket.wx_config = WX_ConfigService.instance().SingleByCompanyID(m.CompanyID);
            return access_token;
        }

        public JsonResult GetAccessToken()
        {
            return Json(AsyncUser(), JsonRequestBehavior.AllowGet);
        }

        //同步粉丝
        public bool AsyncUser()
        {
            List<string> openidList = WXApi.GetOpenIDs(GetToken()); //获取关注者OpenID列表
            foreach (var item in openidList)
            {
                WX_Fans fan = new WX_Fans();
                fan.ConpanyID = UserDateTicket.Company.ID;
                fan.OPENID = item;
                WX_FansService.instance().Insert(fan);

            }
            return true;
        }

        #region 基本信息
        public ActionResult Index()
        {
            WX_Config m = WX_ConfigService.instance().SingleByCompanyID(UserDateTicket.Company.ID) ?? new WX_Config();
            return View(m);
        }
        [HttpPost]
        public JsonResult Index(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            WX_Config m = new WX_Config();
            TryUpdateModel<WX_Config>(m, form);
            m.CompanyID = UserDateTicket.Company.ID;
            if (m.ID == Guid.Empty)
                result.status = WX_ConfigService.instance().Insert(m);
            else
                result.status = WX_ConfigService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/WeChat/Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 菜单管理
        public ActionResult Menu_Index()
        {
            return View();
        }
        public ActionResult Menu_Create(string id)
        {
            WX_Menu m;
            if (string.IsNullOrEmpty(id))
                m = new WX_Menu();
            else
                m = WX_MenuService.instance().Single(new Guid(id));
            return View(m);
        }
        [HttpPost]
        public ActionResult Menu_Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            WX_Menu m = new WX_Menu();
            TryUpdateModel<WX_Menu>(m, form);
            m.CompanyID = UserDateTicket.Company.ID;
            switch (m.Type)
            {
                case 0:
                    m.URL = "";
                    break;
                case 1:
                    m.KeyWordID = Guid.Empty;
                    break;
                case 2:
                    m.URL = "";
                    m.KeyWordID = Guid.Empty;
                    break;
            }
            if (m.ID == Guid.Empty)
                result.status = WX_MenuService.instance().Insert(m);
            else
                result.status = WX_MenuService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/WeChat/Menu_Index";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Menu_Delete(string id)
        {
            WX_Menu m = WX_MenuService.instance().Single(new Guid(id));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DR", !m.DR);
            if (WX_MenuService.instance().ModifColumn(dic, id))
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTopMenu()
        {
            return Json(WX_MenuService.instance().GetTopEnumByCompanyID(UserDateTicket.Company.ID).Select(m => new { id = m.ID, name = m.Name }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChileMenu(string id)
        {
            return Json(WX_MenuService.instance().GetChilds(new Guid(id)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMenuPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<WX_Menu> datalist = WX_MenuService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            datalist.Each(m =>
            {
                m.Childs = WX_MenuService.instance().GetChilds(m.ID);
                m.KeyWord = WX_KeyWordService.instance().GetEnumerableByID(m.KeyWordID).FirstOrDefault();
            });
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AsyncMenu()
        {
            string result = WXApi.CreateMenu(GetToken(), UserDateTicket.Company.ID);
            if (Utils.GetJsonValue(result, "errcode") == "0")
            {
                return Json(new { code = 1, msg = "" }, JsonRequestBehavior.AllowGet);// "{\"code\":1,\"msg\":\"\"}";
            }
            else
            {
                return Json(new
                {
                    code = 0,
                    msg = "errcode:" +
                        Utils.GetJsonValue(result, "errcode") +
                        ", errmsg:" + Utils.GetJsonValue(result, "errmsg")
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 图片库
        public ActionResult Picture()
        {
            ViewBag.RelationID = UserDateTicket.wx_config.ID;
            ViewBag.CompanyID = UserDateTicket.Company.ID;
            return View();
        }
        [HttpPost]
        public JsonResult GetPicPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Files> datalist = FilesService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOnlinPic()
        {
            IEnumerable<Files> files = FilesService.instance().GetFilesByCompanyID(UserDateTicket.Company.ID);
            return Json(files.Where(n => !string.IsNullOrEmpty(n.media_id)).Select(m => new { file = m.FilePathStr, _file = m.FilePath, id = m.ID }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AsyncPic()
        {
            IEnumerable<Files> files = FilesService.instance().GetFilesByCompanyID(UserDateTicket.Company.ID);
            files = files.Where(m => m.RelationID == UserDateTicket.wx_config.ID);
            foreach (var item in files)
            {
                if (string.IsNullOrEmpty(item.media_id))
                {
                    string media_id = Utils.GetJsonValue(WXApi.Material_add_img(GetToken(), item.FilePath), "media_id");
                    item.media_id = media_id;
                    FilesService.instance().Update(item);
                }
            }
            return Json("ok", JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region 图文消息
        public ActionResult TextMessage()
        {
            return View();
        }
        public ActionResult TextMessage_Create(string id)
        {
            WX_Message m;
            if (string.IsNullOrEmpty(id))
                m = new WX_Message();
            else
                m = WX_MessageService.instance().Single(new Guid(id));
            return View(m);
        }
        [HttpPost]
        public JsonResult TextMessage_Create(string title, string id, List<WX_MessageGroup> list)
        {

            if (new Guid(id) == Guid.Empty)
            {
                WX_Message wm = new WX_Message()
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                };
                wm.CompanyID = UserDateTicket.Company.ID;
                wm.ConfigID = UserDateTicket.wx_config.ID;
                wm.Title = title;
                if (WX_MessageService.instance().Insert(wm) == 1)
                {
                    foreach (var item in list)
                    {
                        Files f = FilesService.instance().Single(item.FilesID);
                        item.MessageID = wm.ID;
                        item.ImgUrl = f.FilePath;
                        item.Content = Server.UrlDecode(item.Content);
                        item.Img_media_id = f.media_id;
                        WX_MessageGroupService.instance().Insert(item);
                    }
                }
            }
            else
            {
                WX_MessageGroupService.instance().DeleteByMID(new Guid(id));
                WX_Message wm = WX_MessageService.instance().Single(new Guid(id));
                wm.media_id = "";
                wm.Title = title;
                WX_MessageService.instance().Update(wm);
                foreach (var item in list)
                {
                    Files f = FilesService.instance().Single(item.FilesID);
                    item.MessageID = wm.ID;
                    item.ImgUrl = f.FilePath;
                    item.Img_media_id = f.media_id;
                    item.Content = Server.UrlDecode(item.Content);
                    WX_MessageGroupService.instance().Insert(item);
                }
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult TextMessage_Creates()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetMessagePage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<WX_Message> datalist = WX_MessageService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            datalist.Each(m =>
            {
                m.Groups = WX_MessageGroupService.instance().GetEnumByMessID(m.ID).OrderBy(n => n.Short);
            });
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMessage(string id)
        {
            return Json(WX_MessageService.instance().Single(new Guid(id)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DelMessage(string id)
        {
            WX_Message wm = WX_MessageService.instance().Single(new Guid(id));
            if (wm != null)
            {
                WX_MessageGroupService.instance().DeleteByMID(new Guid(id));
                WX_MessageService.instance().Delete(new Guid(id));
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AsyncMessage()
        {
            Hashtable hash = new Hashtable();
            IEnumerable<WX_Message> Messages = WX_MessageService.instance().GetEnumByCID(UserDateTicket.Company.ID);
            foreach (var item in Messages)
            {
                StringBuilder ArticlesJson = new StringBuilder();

                if (string.IsNullOrEmpty(item.media_id))
                {
                    ArticlesJson.Append(WXApiJson.GetArticlesJsonStr(item.ID));
                    string msg = WXApi.Material_add_news(GetToken(), ArticlesJson.ToString());

                    if (Utils.GetJsonValue(msg, "errcode") != "")
                    {
                        hash["code"] = 0;
                        hash["errcode"] = Utils.GetJsonValue(msg, "errcode");
                        hash["errmsg"] = Utils.GetJsonValue(msg, "errmsg");
                        return Json(hash, JsonRequestBehavior.AllowGet);
                    }
                    string media_id = Utils.GetJsonValue(msg, "media_id");

                    string backInfo = WXApi.Material_get(GetToken(), "{\"media_id\":\"" + media_id + "\"}");
                    if (Utils.GetJsonValue(backInfo, "errcode") != "")
                    {
                        hash["code"] = 0;
                        hash["errcode"] = Utils.GetJsonValue(msg, "errcode");
                        hash["errmsg"] = Utils.GetJsonValue(msg, "errmsg");
                        return Json(hash, JsonRequestBehavior.AllowGet);
                    }

                    var serializer = JsonSerializer.Create();
                    var obj = serializer.Deserialize(new JsonTextReader(new StringReader(backInfo))) as JObject;
                    var data = obj["news_item"];
                    for (int i = 0; i < item.Groups.Count(); i++)
                    {
                        if (string.IsNullOrEmpty(item.Groups.ToList()[i].URL))
                        {
                            item.Groups.ToList()[i].URL = JsonHelper.GetJsonValue(data[i].ToString(), "url");
                            WX_MessageGroupService.instance().Update(item.Groups.ToList()[i]);
                        }

                    }
                    item.media_id = media_id;
                    WX_MessageService.instance().Update(item);
                }
            }
            hash["code"] = 1;
            return Json(hash, JsonRequestBehavior.AllowGet);

            //if (Utils.GetJsonValue(msg, "errcode") == "0")
            //{
            //    return Json(new { code = 1, msg = "" }, JsonRequestBehavior.AllowGet);// "{\"code\":1,\"msg\":\"\"}";
            //}
            //else
            //{
            //    return Json(new
            //    {
            //        code = 0,
            //        msg = "errcode:" +
            //            Utils.GetJsonValue(msg, "errcode") +
            //            ", errmsg:" + Utils.GetJsonValue(msg, "errmsg")
            //    }, JsonRequestBehavior.AllowGet);
            //}

        }
        public JsonResult GetOnlieMessage()
        {
            return Json(WX_MessageService.instance().GetEnumByCID(UserDateTicket.Company.ID).Where(m => !string.IsNullOrEmpty(m.media_id)), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 关键字自动回复
        public ActionResult KeyWords()
        {
            return View();
        }
        public ActionResult KeyWords_Create(string id)
        {
            WX_KeyWord m;
            if (string.IsNullOrEmpty(id))
                m = new WX_KeyWord() { Type = 1 };
            else
                m = WX_KeyWordService.instance().Single(new Guid(id));
            return View(m);
        }
        [HttpPost]
        public JsonResult KeyWords_Create(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            WX_KeyWord m = new WX_KeyWord();
            TryUpdateModel<WX_KeyWord>(m, form);
            m.CompanyID = UserDateTicket.Company.ID;
            m.ConfigID = UserDateTicket.wx_config.ID;
            switch (m.Type)
            {
                case 0://图文消息
                    m.Content = "";
                    break;
                case 1://文字
                    m.RelationID = Guid.Empty;
                    break;
                case 2://图片
                    break;
                case 3://语音
                    break;
                case 4: //视频
                    break;
                case 5://API
                    m.RelationID = Guid.Empty;
                    break;
            }
            if (m.ID == Guid.Empty)
                result.status = WX_KeyWordService.instance().Insert(m);
            else
                result.status = WX_KeyWordService.instance().Update(m);
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            result.ResultURL = "/WeChat/KeyWords";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetKeyWordsPage(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<WX_KeyWord> datalist = WX_KeyWordService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist.ToList();
            return Json(page, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckKey(string id, string key)
        {
            return Json(WX_KeyWordService.instance().CheckKey(new Guid(id), key, UserDateTicket.Company.ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetKeyWords()
        {
            return Json(WX_KeyWordService.instance().GetEnumByCompanyID(UserDateTicket.Company.ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete_KeyWord(string id)
        {

            WX_KeyWord m = WX_KeyWordService.instance().Single(new Guid(id));
            if (WX_KeyWordService.instance().Delete(m.ID) == 1)
                return Json(new { status = 1, id = m.ID, dr = !m.DR }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region  消息推送
        public ActionResult PushNews()
        {
            return View();
        }
        public JsonResult SendText(string txt)
        {
            string resultMsg = WXApi.Send(GetToken(), WXApiJson.CreateTextJson(txt, WXApi.GetOpenIDs(GetToken())));
            return Json("ok", JsonRequestBehavior.AllowGet);

        }
        public JsonResult SendImg(string media_id)
        {
            string resultMsg = WXApi.Send(GetToken(), WXApiJson.CreateImageJson(media_id, WXApi.GetOpenIDs(GetToken())));
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendNewGroup(string media_id)
        {
            string resultMsg = WXApi.Send(GetToken(), WXApiJson.CreateNewsJson(media_id, WXApi.GetOpenIDs(GetToken())));
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}