using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.WeChart
{
    public class GroupsJson : WxJsonResult
    {
        public List<GroupsJson_Group> groups { get; set; }
    }
}
