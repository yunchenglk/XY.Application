﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XY.Entity.Weixin.MP
{
    public class RequestMessageEvent_Enter : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.ENTER; }
        }
    }
}
