using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Services.Weixin.Entities
{
    public interface IMessageBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
    }
}
