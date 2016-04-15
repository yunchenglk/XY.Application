using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class News
    {
        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "普通";
                    case 1:
                        return "图片";
                    case 2:
                        return "视频";
                    default:
                        return "未知";
                }
            }
        }
        public string ClassName
        {
            get;
            set;
        }
        public string Short_Dec
        {
            get
            {
                return Util.Utils.DropHTML(Description);
            }
        }
        public string DescriptionStr
        {
            get
            {
                return XY.Util.Utils.ImgAddURL(Description);
            }
        }
        public string SlidePicStr
        {
            get
            {

                return XY.Util.Utils.AddURL(SlidePic);
            }
        }
        public string SlidePicStr_
        {
            get
            {

                return XY.Util.Utils.AddURL_(SlidePic);
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
        public int IsComm_
        {
            get
            {
                if (IsComm)
                    return 1;
                return 0;
            }
        }
        public int IsVote_
        {
            get
            {
                if (IsVote)
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
        public string sourceWeb_
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sourceWeb_"];
            }
        }
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
