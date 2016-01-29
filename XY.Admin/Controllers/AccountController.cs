using System;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using XY.Entity;
using System.Web.Security;
using XY.Services;
using System.Drawing;

namespace XY.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string ReturnUrl)
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
                string code = form["verifycode"];
                if (code.Trim().Length <= 0)
                {
                    ViewBag.Msg = "请输入验证码";
                    return View();
                }
                if (!code.ToUpper().Equals(Session["CheckCode"]))
                {
                    ViewBag.Msg = "验证码不正确";
                    return View();
                }
                USER u = new USER();
                json = UserService.instance().Login(uname, upwd);
                if (Convert.ToBoolean(json["status"]))
                {

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        json["uid"].ToString(), DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), false,
                        "");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    //初始化ticket
                    USER m = UserService.instance().GetEntityByID(new Guid(json["uid"].ToString()));
                    UserDateTicket.Uname = m.Name;
                    UserDateTicket.Company = CompanyService.instance().Single(m.CompanyID);
                    UserDateTicket.wx_config = WX_ConfigService.instance().SingleByCompanyID(m.CompanyID);
                    UserDateTicket.IsSuper = m.Type == 99 ? true : false;
                    UserDateTicket.MenuHTML = new MenuService(m.ID).Html;

                    //end初始化ticket

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

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        public void GetRandomCode()
        {
            Util.RandomCode randomCode = new Util.RandomCode();
            Session["CheckCode"] = randomCode.CreateRandomCode(5);
            ColorConverter colConvert = new ColorConverter();
            System.IO.MemoryStream img = randomCode.CreateImage(Session["CheckCode"].ToString(), (Color)colConvert.ConvertFromString("#312bff"), (Color)colConvert.ConvertFromString("#c5babc"));
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(img.ToArray());
        }
    }
}