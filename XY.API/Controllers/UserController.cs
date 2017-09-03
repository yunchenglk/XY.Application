using System;
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
    public class UserController : ApiController
    {
        // GET: api/User
        public HttpResponseMessage Get()
        {
            return null;
        }

        // GET: api/User/5
        public HttpResponseMessage Get(Guid id)
        {
            return null;
        }

        // POST: api/User
        public void Post([FromBody]UserView value)
        {
            int retult = 0;
            try
            {
                USER u = new USER();
                u.PID = u.CompanyID = new Guid(value.comid);
                u.LoginName = value.LoginName;
                u.LoginPwd = value.LoginPwd;
                retult = UserService.instance().Insert(u);
            }
            catch (Exception ex)
            {

            }
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString() + "?v=" + retult);
        }

        // PUT: api/User/5
        public HttpResponseMessage Put(int id, [FromBody]UserView value)
        {
            return null;
        }

        // DELETE: api/User/5
        public HttpResponseMessage Delete(int id)
        {
            return null;
        }
    }
}
