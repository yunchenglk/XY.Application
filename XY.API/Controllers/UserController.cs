using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using XY.Entity;
using XY.Services;
using XY.Util;

namespace XY.API.Controllers
{
    [Filters.AuthorizeFilter]
    /// <summary>
    /// 会员管理
    /// </summary> 
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        Company company = CompanyService.instance().GetCompanyByUrl("http://" + HttpContext.Current.Request.UrlReferrer.Host);
        //// GET: api/User
        //public HttpResponseMessage Get()
        //{
        //    return null;
        //}

        //// GET: api/User/5
        //public HttpResponseMessage Get(Guid id)
        //{
        //    return null;
        //}
        /// <summary>
        /// 登陆名查重
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="loginname">登陆账号</param>
        /// <returns></returns>  
        public HttpResponseMessage Get(Guid id, string loginname)
        {
            try
            {
                //Company company = CompanyService.instance().GetCompanyByUrl("http://" + HttpContext.Current.Request.UrlReferrer.Host);
                return Util.Utils.ConvertToJson(UserService.instance().CheckUser(id, loginname, company.ID));

            }
            catch (Exception ex)
            {
            }
            return Util.Utils.ConvertToJson(false);
        }


        // POST: api/User
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="value"></param> 
        public void Post([FromBody]UserView value)
        {
            int retult = 0;
            try
            {
                if (UserService.instance().CheckUser(Guid.Empty, value.LoginName, company.ID))
                {
                    USER u = new USER();
                    u.PID = u.CompanyID = company.ID;
                    u.LoginName = value.LoginName;
                    u.LoginPwd = value.LoginPwd;
                    retult = UserService.instance().Insert(u);
                }
            }
            catch (Exception ex)
            {
            }
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString() + "?v=" + retult);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="value">登陆账号和密码</param>
        [Route("login")]
        [HttpPost]
        public void Login([FromBody]UserView value)
        {
            Hashtable json = new Hashtable();
            try
            {
                string url = "/Home/Index";
                string uname = value.LoginName;
                if (uname.Trim().Length <= 0)
                {
                    json["error"] = "请输入登录账号";
                }
                string upwd = value.LoginPwd;
                if (upwd.Trim().Length <= 0)
                {
                    json["error"] = "请输入登录密码";
                }
                USER u = new USER();
                json = UserService.instance().Login(uname, upwd, company.ID);
                if (Convert.ToBoolean(json["status"]))
                {
                    Guid UID = new Guid(json["uid"].ToString());
                }
            }
            catch (Exception ex)
            {
                json["error"] = "未知错误！" + ex.ToString();
            }
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString() + "?v=" + Util.Utils.ToJson(json));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [Route("del")]
        [HttpGet]
        // DELETE: api/User/5
        public HttpResponseMessage Delete(Guid id)
        {
            Hashtable json = new Hashtable();
            json["status"] = UserService.instance().Delete(id, company.ID) == 1;

            return Util.Utils.ConvertToJson(json);
        }
    }
}
