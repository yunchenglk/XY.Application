using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class wx_userinfo
    {
        public string SexStr
        {
            get
            {
                switch (sex)
                {
                    case 1:
                        return "男";
                    case 2:
                        return "女";
                    default:
                        return "未知";
                }
            }
        }
        public string location { get { return country + "-" + province + "-" + city; } }
        public string subscribe_timeStr { get { return subscribe_time.ToString("yyyy-MM-dd HH:mm"); } }
    }
}
