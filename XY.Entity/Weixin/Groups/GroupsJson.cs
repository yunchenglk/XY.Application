using System.Collections.Generic;

namespace XY.Entity.Weixin
{
    public class GroupsJson : WxJsonResult
    {
        public List<GroupsJson_Group> groups { get; set; }
    }
}
