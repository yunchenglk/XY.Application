using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XY.Entity;

namespace XY.WeChart.Web
{
    public static class UserDateTicket
    {
        public static Guid UID
        {
            get
            {
                return new Guid(HttpContext.Current.User.Identity.Name);
            }
        }
        public static string Uname
        {
            get;
            set;
        }
        public static Company Company
        {
            get;
            set;
        }
        public static wx_userweixin wx_user
        {
            get;
            set;
        } 
    }
}