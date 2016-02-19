using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;
using XY.Services.Weixin.Helpers;

namespace XY.WeChart.Web.Controllers
{
    public class wxUserInfoController : _baseController
    {
        int[] groupids = { 0, 1, 2 };
        string[] groupnames = { "未分组", "黑名单", "星标组" };

        public ActionResult Index()
        {
            var markersList = new List<BaiduMarkers>();
            markersList.Add(new BaiduMarkers()
            {
                Longitude = 31.285774,
                Latitude = 120.597610,
                Color = "red",
                Label = "O",
                Size = BaiduMarkerSize.Default,
            });
            markersList.Add(new BaiduMarkers()
            {
                Longitude = 31.289774,
                Latitude = 120.597910,
                Color = "blue",
                Label = "T",
                Size = BaiduMarkerSize.Default,
            });
            var url = BaiduMapHelper.GetBaiduStaticMap(31.285774, 120.597610, 2, 16, markersList);








            return View();
        }
        public ActionResult Edit(string id)
        {
            Guid ID;
            wx_userinfo m = new wx_userinfo();
            if (Guid.TryParse(id, out ID))
                m = wx_userinfoService.instance().Single(ID);
            return View(m);
        }
        [HttpPost]
        public JsonResult GetPage_User()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_userinfo> datalist = wx_userinfoService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_userinfo>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult syncOnlineUser()
        {
            OpenIdResultJson data = WeChartAPI.GetUsers(GetToken());
            if (data.errcode == ReturnCode.请求成功)
            {
                foreach (var item in data.data.openid)
                {
                    var info = WeChartAPI.GetUserInfo(GetToken(), item);
                    if (info.errcode == ReturnCode.请求成功)
                    {
                        wx_userinfo entity = new wx_userinfo();
                        entity.openid = item;
                        entity.cID = UserDateTicket.Company.ID;
                        entity.wID = UserDateTicket.wx_user.ID;
                        entity.groupid = info.groupid;
                        if (!groupids.Contains(entity.groupid))
                            entity.groupname = wx_usergroupService.instance().Single(info.groupid, UserDateTicket.Company.ID).gname;
                        else
                            entity.groupname = groupnames[info.groupid];
                        entity.headimgul = info.headimgurl;
                        entity.language = info.language;
                        entity.nickname = info.nickname;
                        entity.province = info.province;
                        entity.city = info.city;
                        entity.country = info.country;
                        entity.sex = info.sex;
                        entity.subscribe = info.subscribe;
                        entity.subscribe_time = Util.Utils.StampToDateTime(info.subscribe_time);
                        wx_userinfoService.instance().Insert(entity);
                    }
                }
            }
            return Json(data.errcode.ToString(), JsonRequestBehavior.AllowGet);
        }
        #region 用户分组管理
        // GET: wxUserInfo
        public ActionResult Group()
        {
            return View();
        }
        public ActionResult GroupEdit(string id)
        {
            Guid ID;
            wx_usergroup m = new wx_usergroup();
            if (Guid.TryParse(id, out ID))
                m = wx_usergroupService.instance().Single(ID);
            return View(m);
        }
        [HttpPost]
        public ActionResult GroupEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_usergroup m = new wx_usergroup();
            TryUpdateModel<wx_usergroup>(m, form);
            m.wID = UserDateTicket.wx_user.ID;
            m.cID = UserDateTicket.Company.ID;
            if (m.ID == Guid.Empty)
            {
                CreateGroupResult data = WeChartAPI.CreateGroup(GetToken(), m.gname);
                if (data.errcode == ReturnCode.请求成功)
                {
                    m.ID = Guid.NewGuid();
                    m.gid = data.group.id;
                    result.status = wx_usergroupService.instance().Insert(m);
                }
            }
            else
            {
                WxJsonResult data = WeChartAPI.UpdateGroup(GetToken(), m.gid, m.gname);
                if (data.errcode == ReturnCode.请求成功)
                {
                    result.status = wx_usergroupService.instance().Update(m);
                }
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage_Group()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_usergroup> datalist = wx_usergroupService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_usergroup>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Del_Group(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_usergroup entity = wx_usergroupService.instance().Single(ID);
                if (entity != null)
                {
                    var data = WeChartAPI.DeleteGroup(GetToken(), entity.gid);
                    if (data.errcode == ReturnCode.请求成功)
                        if (wx_usergroupService.instance().Delete(ID) == 1)
                            return Json("ok", JsonRequestBehavior.AllowGet);

                }

            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckGroupNameExists(string id, string name)
        {
            return Json(wx_usergroupService.instance().CheckName(new Guid(id), UserDateTicket.Company.ID, name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllGroup()
        {
            return Json(wx_usergroupService.instance().GetAll(UserDateTicket.Company.ID).Select(m => new { id = m.gid, name = m.gname }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult MoveGroup(string id, string gid)
        {
            Guid ID;
            int GID;
            if (Guid.TryParse(id, out ID) && int.TryParse(gid, out GID))
            {
                wx_userinfo info = wx_userinfoService.instance().Single(ID);
                wx_usergroup group = wx_usergroupService.instance().Single(GID, UserDateTicket.Company.ID);
                if (groupids.Contains(GID) || group != null)
                {
                    var result = WeChartAPI.UserMoveGroup(GetToken(), info.openid, GID);
                    if (result.errcode == ReturnCode.请求成功)
                    {
                        info.groupid = GID;
                        if (!groupids.Contains(info.groupid))
                            info.groupname = wx_usergroupService.instance().Single(info.groupid, UserDateTicket.Company.ID).gname;
                        else
                            info.groupname = groupnames[info.groupid];
                        if (wx_userinfoService.instance().Update(info) == 1)
                        {
                            return Json("ok", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
        #endregion



    }
}