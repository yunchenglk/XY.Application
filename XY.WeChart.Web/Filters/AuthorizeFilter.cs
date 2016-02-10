using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace XY.WeChart.Web.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Path));
            }
        }
    }

}