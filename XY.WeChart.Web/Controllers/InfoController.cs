using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.WeChart.Web.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index()
        {
            wx_userweixin m = new wx_userweixin();
            if (UserDateTicket.wx_user != null)
                m = wx_userweixinService.instance().SingleByCompanyID(UserDateTicket.Company.ID);
            return View(m);
        }
        [HttpPost]
        public JsonResult Index(FormCollection form)
        {
            ResultBase_form result = new ResultBase_form();
            wx_userweixin m = wx_userweixinService.instance().SingleByCompanyID(UserDateTicket.Company.ID);
            TryUpdateModel<wx_userweixin>(m, form);
            m.Access_Token = "";
            m.expires_in = 0;
            result.status = wx_userweixinService.instance().Update(m);
            if (result.status == 1)
                UserDateTicket.wx_user = m;
            result.msg = result.status == 0 ? "操作失败" : "操作成功";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}