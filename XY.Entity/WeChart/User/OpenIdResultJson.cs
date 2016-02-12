using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.WeChart
{
    public class OpenIdResultJson : WxJsonResult
    {
        public int total { get; set; }
        public int count { get; set; }
        public OpenIdResultJson_Data data { get; set; }
        public string next_openid { get; set; }
    }
}
