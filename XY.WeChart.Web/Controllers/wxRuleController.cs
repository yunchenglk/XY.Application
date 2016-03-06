using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.WeChart.Web.Controllers
{
    [XY.WeChart.Web.Filters.AuthorizeFilter]
    public class wxRuleController : _baseController
    {
        #region 文本回复
        public ActionResult wenBen()
        {
            return View();
        }
        public ActionResult wenBenEdit(string id)
        {
            Guid ID;
            wx_requestRule m = new wx_requestRule();
            if (Guid.TryParse(id, out ID))
            {
                m = wx_requestRuleService.instance().Single(ID);
                ViewBag.rContent = wx_requestRuleContentService.instance().SingleByRuleID(m.ID).rContent;
            }
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult wenBenEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_requestRule m = new wx_requestRule();
            TryUpdateModel<wx_requestRule>(m, form);
            m.wID = UserDateTicket.wx_user.ID;
            m.cID = UserDateTicket.Company.ID;
            m.wxId = UserDateTicket.wx_user.wxId;
            m.reqestType = 0;
            m.responseType = 1;
            m.isLikeSearch = form["isLikeSearch"] == "1";
            if (m.ID == Guid.Empty)
            {
                m.ID = Guid.NewGuid();
                result.status = wx_requestRuleService.instance().Insert(m);
                wx_requestRuleContent rc = new wx_requestRuleContent();
                rc.rContent = Server.UrlDecode(form["rContent"]);
                rc.RuleID = m.ID;
                wx_requestRuleContentService.instance().Insert(rc);
            }
            else
            {
                wx_requestRuleContent rc = wx_requestRuleContentService.instance().SingleByRuleID(m.ID);
                rc.rContent = Server.UrlDecode(form["rContent"]);
                rc.RuleID = m.ID;
                wx_requestRuleContentService.instance().Update(rc);
                result.status = wx_requestRuleService.instance().Update(m);
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage_Wenben()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "responseType|equal|1#cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_requestRule> datalist = wx_requestRuleService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_requestRule>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Del_wenben(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                if (wx_requestRuleContentService.instance().DeleteByRuleID(ID) == 1)
                {
                    wx_requestRuleService.instance().Delete(ID);
                }
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 语音回复
        public ActionResult yuYin()
        {
            return View();
        }
        public ActionResult yuYinEdit(string id)
        {
            Guid ID;
            wx_requestRule m = new wx_requestRule();
            if (Guid.TryParse(id, out ID))
            {
                m = wx_requestRuleService.instance().Single(ID);
                wx_requestRuleContent rc = wx_requestRuleContentService.instance().SingleByRuleID(m.ID);
                ViewBag.rContent = rc.rContent;
                ViewBag.rContent2 = rc.rContent2;
                ViewBag.mediaUrl = rc.mediaUrl;
            }
            return View(m);
        }
        [HttpPost]
        public JsonResult yuYinEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_requestRule m = new wx_requestRule();
            TryUpdateModel<wx_requestRule>(m, form);
            m.wID = UserDateTicket.wx_user.ID;
            m.cID = UserDateTicket.Company.ID;
            m.wxId = UserDateTicket.wx_user.wxId;
            //m.ruleName = "语音回复";
            m.reqestType = 0;
            m.responseType = 3;
            m.isLikeSearch = form["isLikeSearch"] == "1";
            if (m.ID == Guid.Empty)
            {
                m.ID = Guid.NewGuid();
                result.status = wx_requestRuleService.instance().Insert(m);
                wx_requestRuleContent rc = new wx_requestRuleContent();
                rc.rContent = m.ruleName;
                rc.rContent2 = form["rContent2"];
                rc.RuleID = m.ID;
                rc.mediaUrl = form["mediaUrl"];
                wx_requestRuleContentService.instance().Insert(rc);
            }
            else
            {
                wx_requestRuleContent rc = wx_requestRuleContentService.instance().SingleByRuleID(m.ID);
                rc.rContent = m.ruleName;
                rc.rContent2 = form["rContent2"];
                rc.RuleID = m.ID;
                rc.mediaUrl = form["mediaUrl"];
                wx_requestRuleContentService.instance().Update(rc);
                result.status = wx_requestRuleService.instance().Update(m);
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage_Yuyin()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "responseType|equal|3#cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_requestRule> datalist = wx_requestRuleService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_requestRule>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 图文回复
        public ActionResult tuWen()
        {
            return View();
        }
        public ActionResult tuWenEdit(string id)
        {
            Guid ID;
            wx_requestRule m = new wx_requestRule();
            if (Guid.TryParse(id, out ID))
                m = wx_requestRuleService.instance().Single(ID);
            return View(m);
        }
        [HttpPost]
        public ActionResult tuWenEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_requestRule m = new wx_requestRule();
            TryUpdateModel<wx_requestRule>(m, form);
            m.wID = UserDateTicket.wx_user.ID;
            m.cID = UserDateTicket.Company.ID;
            m.wxId = UserDateTicket.wx_user.wxId;
            //m.ruleName = "图文回复";
            m.responseType = 2;
            m.reqestType = 0;
            m.isLikeSearch = form["isLikeSearch"] == "1";
            if (m.ID == Guid.Empty)
            {
                m.ID = Guid.NewGuid();
                result.status = wx_requestRuleService.instance().Insert(m);
            }
            else
            {
                result.status = wx_requestRuleService.instance().Update(m);
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult tuWenList(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult tuWenListEdit(string id, string rid)
        {
            Guid ID;
            wx_requestRuleContent m = new wx_requestRuleContent();
            if (Guid.TryParse(id, out ID))
                m = wx_requestRuleContentService.instance().Single(ID);
            ViewBag.RID = rid;
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult tuWenListEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_requestRuleContent m = new wx_requestRuleContent();
            TryUpdateModel<wx_requestRuleContent>(m, form);
            if (m.ID == Guid.Empty)
            {
                m.ID = Guid.NewGuid();
                result.status = wx_requestRuleContentService.instance().Insert(m);
            }
            else
            {
                result.status = wx_requestRuleContentService.instance().Update(m);
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetPage_Tuwen()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "responseType|equal|2#cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_requestRule> datalist = wx_requestRuleService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_requestRule>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetPage_TuwenList(string id)
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "RuleID|equal|" + id;
            IEnumerable<wx_requestRuleContent> datalist = wx_requestRuleContentService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_requestRuleContent>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Del_tuwenList(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                if (wx_requestRuleContentService.instance().Delete(ID) == 1)
                    return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add_ContentTemp(string id, string coutent)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_contentTemp entity = wx_contentTempService.instance().Single(ID);
                if (entity == null)
                {
                    entity = new wx_contentTemp()
                    {
                        ID = Guid.NewGuid(),
                        Content = coutent
                    };
                    wx_contentTempService.instance().Insert(entity);
                }
                else {
                    entity.Content = coutent;
                    wx_contentTempService.instance().Update(entity);
                }
                return Json(new { status = 1, id = entity.ID }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region 视频回复
        public ActionResult shiPin()
        {
            return View();
        }
        public ActionResult shiPinEdit(string id)
        {
            Guid ID;
            wx_requestRule m = new wx_requestRule();
            if (Guid.TryParse(id, out ID))
            {
                m = wx_requestRuleService.instance().Single(ID);
                wx_requestRuleContent rc = wx_requestRuleContentService.instance().SingleByRuleID(m.ID);
                ViewBag.rContent = rc.rContent;
                ViewBag.rContent2 = rc.rContent2;
                ViewBag.mediaUrl = rc.mediaUrl;

            }
            return View(m);
        }
        [HttpPost]
        public JsonResult shiPinEdit(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_requestRule m = new wx_requestRule();
            TryUpdateModel<wx_requestRule>(m, form);
            m.wID = UserDateTicket.wx_user.ID;
            m.cID = UserDateTicket.Company.ID;
            m.wxId = UserDateTicket.wx_user.wxId;
            //m.ruleName = "视频回复";
            m.reqestType = 0;
            m.responseType = 4;
            m.isLikeSearch = form["isLikeSearch"] == "1";
            if (m.ID == Guid.Empty)
            {
                m.ID = Guid.NewGuid();
                result.status = wx_requestRuleService.instance().Insert(m);
                wx_requestRuleContent rc = new wx_requestRuleContent();
                rc.rContent = m.ruleName;
                rc.rContent2 = form["rContent2"];
                rc.RuleID = m.ID;
                rc.mediaUrl = form["mediaUrl"];
                wx_requestRuleContentService.instance().Insert(rc);
            }
            else
            {
                wx_requestRuleContent rc = wx_requestRuleContentService.instance().SingleByRuleID(m.ID);
                rc.rContent = m.ruleName;
                rc.rContent2 = form["rContent2"];
                rc.RuleID = m.ID;
                rc.mediaUrl = form["mediaUrl"];
                wx_requestRuleContentService.instance().Update(rc);
                result.status = wx_requestRuleService.instance().Update(m);
            }
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPage_Shipin()
        {
            int totalcount = 0;
            int start = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["length"]);
            int pageIndex = 1;
            if (start > 0)
                pageIndex = (start / pageSize) + 1;
            string sortname = Request["columns[" + Convert.ToInt32(Request["order[0][column]"]) + "][data]"];
            string sortorder = Request["order[0][dir]"];
            string wheres = "responseType|equal|4#cID|equal|" + UserDateTicket.Company.ID;
            IEnumerable<wx_requestRule> datalist = wx_requestRuleService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            var data = new Returneddata<wx_requestRule>(Convert.ToInt32(Request["draw"]), totalcount);
            data.data = datalist.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion



        public JsonResult GetKeywordsByType(int type)
        {
            var result = wx_requestRuleService.instance().GetByResponseType(type, UserDateTicket.Company.ID);
            return Json(result.Select(m => m.reqKeywords), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Push(string id)
        {
            Guid ID = IsGID(id);
            wx_requestRule m = new wx_requestRule();
            m = wx_requestRuleService.instance().Single(ID);
            return View(m);
        }

    }
}