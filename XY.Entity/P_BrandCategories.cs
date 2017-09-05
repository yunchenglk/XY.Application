using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class P_BrandCategories
    {
        public string sourceWeb
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sourceWeb"];
            }
        }
        public string LogoStr
        {
            get
            {

                return XY.Util.Utils.AddURL(Logo);
            }
        }
    }
}
