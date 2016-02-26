using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.Services.Weixin.Entities
{
    public class RequestMessageEventBase : RequestMessageBase, IRequestMessageEventBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.EVENT; }
        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual Event Event
        {
            get { return Event.ENTER; }
        }
        ///// <summary>
        ///// 事件KEY值，与自定义菜单接口中KEY值对应，如果是View，则是跳转到的URL地址
        ///// </summary>
        public string EventKey { get; set; }
    }
}
