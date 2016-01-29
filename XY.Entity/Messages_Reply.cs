using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Messages_Reply
    {
        public Boolean IsChild
        {
            get
            {
                return ChildItem == null ? false : ChildItem.Count() > 0;
            }
        }
        public IEnumerable<Messages_Reply> ChildItem { get; set; }
    }
}
