using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Authority
    {
        public string PIDName { get; set; }
        public bool Ishaschild
        {
            get
            {
                return Childs == null ? false : Childs.Count() > 0;
            }
        }
        public IEnumerable<Authority> Childs { get; set; }
        public Authority ParentAuth { get; set; }
    }
}
