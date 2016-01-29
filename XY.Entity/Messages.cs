using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Messages
    {
        public string TypeStr
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "留言";
                    case 1:
                        return "新闻";
                    case 2:
                        return "栏目";
                    default:
                        return "未知";
                }
            }
        }

        public string ContentStr
        {
            get
            {
                if (Content == null)
                    return "无主题";
                else if (Content.Length > 20)
                    return Content.Substring(0, 20);
                else
                    return Content;
            }
        }
        public IEnumerable<Messages_Reply> ReplyItems { get; set; }

        public string CreateTimeStr
        {
            get
            {
                return CreateTime.ToString();
            }
        }
    }
}
