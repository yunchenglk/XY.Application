using System;
using System.Linq;
using System.Web.Mvc;
using XY.Entity;
using XY.Entity.Weixin;
using XY.Services;

namespace XY.WeChart.Web.Controllers
{
    public class _baseController : Controller
    {
        public string GetToken()
        {
            string error;
            string token = WeChartAPI.getAccessToken(UserDateTicket.Company.ID, out error);
            if (!string.IsNullOrEmpty(error))
            {
                string xx = WeChartAPI.ReloadToken(UserDateTicket.Company.ID);
                return error;
            }
            return token;
        }
        public JsonResult ReloadToken()
        {
            return Json(WeChartAPI.ReloadToken(UserDateTicket.Company.ID), JsonRequestBehavior.AllowGet);
        }
        





        //关注时回复
        public JsonResult _subscribe(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                wx_requestRule olddate = wx_requestRuleService.instance().GetByRequestType(6, UserDateTicket.Company.ID);
                if (olddate != null)
                {
                    olddate.reqestType = 0;
                    if (wx_requestRuleService.instance().Update(olddate) != 1)
                        return Json("更新old信息错误", JsonRequestBehavior.AllowGet);
                }
                wx_requestRule newdata = wx_requestRuleService.instance().Single(ID);
                newdata.reqestType = 6;
                if (wx_requestRuleService.instance().Update(newdata) != 1)
                    return Json("更新New信息错误");
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return Json("错误", JsonRequestBehavior.AllowGet);

        }

        public Guid IsGID(string id)
        {
            Guid ID = Guid.Empty;
            if (Guid.TryParse(id, out ID))
            {
                ID = new Guid(id);
            }
            return ID;
        }

        public int IsInt(string id)
        {
            int ID = 0;
            if (int.TryParse(id, out ID))
            {
                ID = int.Parse(id);
            }
            return ID;
        }

    }
}