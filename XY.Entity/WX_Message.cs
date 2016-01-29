using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class WX_Message
    {
        public IEnumerable<WX_MessageGroup> Groups { get; set; }
    }
}
