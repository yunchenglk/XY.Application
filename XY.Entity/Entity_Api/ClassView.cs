using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class ClassView
    {
        public Guid ID { get; set; }

        public String Title { get; set; }
        public String EnTitle { get; set; }

        public Guid ParentID { get; set; }

        public Int32 Count { get; set; }
        public String Publisher { get; set; }

        public String Pic { get; set; }

        public String URL { get; set; }

        public string DescriptionStr { get; set; }

        public string _descriptionstr
        {
            get
            {
                return Util.Utils.DropHTML(DescriptionStr);
            }
        }

        public Boolean IsPublic { get; set; }

        public Int32 Sort { get; set; }

        public Int32 Type { get; set; }

        public DateTime CreateTime { get; set; }

        public Boolean DR { get; set; }

        public Guid CompanyID { get; set; }
    }

}

