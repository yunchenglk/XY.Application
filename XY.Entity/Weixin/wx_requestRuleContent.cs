using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class wx_requestRuleContent
    {
        public string picUrlStr
        {
            get
            {
                return Util.Utils.AddURL(picUrl);
            }
        }
        public string mediaUrlStr
        {
            get
            {
                return Util.Utils.AddURL(mediaUrl);
            }
        }
        public string meidaHDUrlStr
        {
            get
            {
                return Util.Utils.AddURL(meidaHDUrl);
            }
        }
    }
}
