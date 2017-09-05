using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XY.Services;
using XY.Util;

namespace XY.API.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        private const string Origin = "Origin";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

        //private const string originHeaderdefault = "";
        private IEnumerable<string> originHeaderdefaults = new List<string>();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //string comid = Utils.URLgetparsmByKey(actionContext.Request.RequestUri.Query.ToString(), "comid");
            ////originHeaderdefaults = XY.Services.CompanyService.instance().URLAll;
            //string url = actionContext.Request.Headers.Referrer.GetLeftPart(UriPartial.Authority);
            ////http://chuanmei.com
            //bool isok = CheckAuthorize.instance().CheckedCompanyByUrl(url, new Guid(comid));
            //if (!isok)
            //    HttpContext.Current.Response.Redirect(url);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //originHeaderdefaults = XY.Services.CompanyService.instance().URLAll;
            //string url = actionExecutedContext.Request.Headers.Referrer.GetLeftPart(UriPartial.Authority);
            //if (!originHeaderdefaults.Contains(url))
            //{
            //    HttpContext.Current.Response.Redirect(url);
            //}

            //actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, originHeaderdefaults.Aggregate((x, y) => x + "," + y));
            //actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, "http://ycdzsw.cn");
            //actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, "http://a.com");
        }
    }
}