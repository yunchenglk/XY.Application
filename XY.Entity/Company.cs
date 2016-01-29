using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Company
    {
        public string Logo_Str
        {
            get
            {
                return Logo == null ? "" : System.Configuration.ConfigurationManager.AppSettings["sourceWeb"] + Logo;
            }
        }
        public string WatermarkPIC_Str
        {
            get
            {
                return WatermarkPIC == null ? "" : System.Configuration.ConfigurationManager.AppSettings["sourceWeb"] + WatermarkPIC;
            }
        }

    }
}
