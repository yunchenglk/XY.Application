using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Product
    {
        public string DescriptionStr
        {
            get
            {
                return XY.Util.Utils.ImgAddURL(Description);
            }
        }
        public int IsRecommend_
        {
            get
            {
                if (IsRecommend)
                    return 1;
                return 0;

            }
        }
        public int IsAudit_
        {
            get
            {
                if (IsAudit)
                    return 1;
                return 0;
            }
        }
        public int IsTop_
        {
            get
            {
                if (IsTop)
                    return 1;
                return 0;
            }
        }
        public string sourceWeb
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sourceWeb"];
            }
        }
        public string Short_Dec
        {
            get
            {
                return Util.Utils.DropHTML(Description);
            }
        }
        public string sourceWeb_
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sourceWeb_"];
            }
        }
        public IEnumerable<ProductAttr> Attr { get; set; }
        public Time CreateTime_
        {
            get
            {
                return new Time()
                {
                    Year = CreateTime.Year.ToString(),
                    Month = CreateTime.Month.ToString(),
                    Day = CreateTime.Day.ToString(),
                    Hour = CreateTime.Hour.ToString(),
                    Minute = CreateTime.Minute.ToString(),
                    Second = CreateTime.Second.ToString()
                };
            }
        }
    }
}
