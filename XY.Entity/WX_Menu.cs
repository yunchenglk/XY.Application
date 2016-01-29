using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class WX_Menu
    {
        public string TypeStr
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "click";

                    case 1:
                        return "view";
                    default:
                        return "";
                }
            }
        }
        public IEnumerable<WX_Menu> Childs { get; set; }

        public bool Ishaschild
        {
            get
            {
                return Childs == null ? false : Childs.Count() > 0;
            }
        }
        public WX_KeyWord KeyWord { get; set; }
    }
}
