using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public interface IRequestMessageEventBase : IRequestMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Event Event { get; }
        ///// <summary>
        ///// 事件KEY值，与自定义菜单接口中KEY值对应
        ///// </summary>
        //string EventKey { get; set; }
    }
}
