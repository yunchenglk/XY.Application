using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Role
    {
        public IEnumerable<Authority> Item_Authoritys { get; set; }
        public string Item_AuthorityIDs { get; set; }
    }
}
