using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class wx_userweixin
    {
        public string headerpicStr
        {
            get
            {
                return Util.Utils.AddURL(headerpic);
            }
        }
        public string apiurlStr
        {
            get
            {
                return Util.Utils.AddConfigURL("WeChartURL", apiurl);
            }
        }
    }
}
