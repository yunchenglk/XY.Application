using System;
using System.Collections.Generic;
using System.Web;
using XY.Entity;
using XY.Services;

namespace XY
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
        public static Boolean IsWeShop
        {
            get;
            set;
        }
        public static int IsWeShop_
        {
            get
            {
                if (IsWeShop)
                    return 1;
                return 0;
            }
        }
        public static Boolean IsSuper
        {
            get;
            set;
        }
        public static string MenuHTML
        {
            get;
            set;
        }
        public static WX_Config wx_config
        {
            get;
            set;
        }

    }
}
