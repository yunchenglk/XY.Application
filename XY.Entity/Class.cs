using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Class
    {
        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "文章";
                    case 1:
                        return "新闻";
                    case 2:
                        return "产品";
                    default:
                        return "未知";
                }
            }
        }
        public string CompanyName { get; set; }
        public string DescriptionStr
        {
            get
            {
                return XY.Util.Utils.ImgAddURL(Description);
            }
        }
        public string Short_Desc
        {
            get
            {
                return Util.Utils.DropHTML(Description);
            }
        }


        public bool Ishaschild
        {
            get
            {
                return Childs == null ? false : Childs.Count() > 0;
            }
        }
        public IEnumerable<Class> Childs { get; set; }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
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
