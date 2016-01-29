using System;
using System.Web.Mvc;
using XY.Entity;
using XY.Services;

namespace XY.Admin.Controllers
{
    public class GetJsonController : Controller
    {
        public JsonResult GetProduct_Att_Keys()
        {
            return Json(Product_Att_KeyService.instance().GetEnumByCID(UserDateTicket.Company.ID), JsonRequestBehavior.AllowGet);
        }
    }
}