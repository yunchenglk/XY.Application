using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using XY.Entity;
using XY.Services;

namespace XY.WeChart.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                Response.Redirect("/Home/Index");
            else
            {
                FormsAuthentication.SignOut();
                Request.Cookies.Clear();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form, string ReturnUrl)
        {
            Hashtable json = new Hashtable();
            try
            {
                string url = "/Home/Index";
                if (!string.IsNullOrEmpty(ReturnUrl))
                    url = ReturnUrl;
                string uname = form["userid"];
                if (uname.Trim().Length <= 0)
                {
                    ViewBag.Msg = "请输入登录账号";
                    return View();
                }
                ViewBag.uname = uname;
                string upwd = form["userpass"];
                if (upwd.Trim().Length <= 0)
                {
                    ViewBag.Msg = "请输入登录密码";
                    return View();
                }
                json = UserService.instance().Login(uname, upwd);
                if (Convert.ToBoolean(json["status"]))
                {
                    Guid UID = new Guid(json["uid"].ToString());

                    USER m = UserService.instance().Single(UID);
                    UserDateTicket.Uname = m.Name;
                    UserDateTicket.Company = CompanyService.instance().Single(m.CompanyID);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        json["uid"].ToString(), DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true,
                        "");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    var wxuser = wx_userweixinService.instance().SingleByCompanyID(m.CompanyID);// WX_ConfigService.instance().SingleByCompanyID(m.CompanyID);
                    if (wxuser != null)
                        UserDateTicket.wx_user = wxuser;
                    else
                        UserDateTicket.wx_user = new wx_userweixin();
                    Response.Cookies.Add(cookie);
                    Response.Redirect(url);
                }
                else
                {
                    ViewBag.Msg = json["error"];
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "未知错误！" + ex.ToString();
            }
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Request.Cookies.Clear();
            Response.Redirect("/Account/Login");
            return View();
        }
    }
}