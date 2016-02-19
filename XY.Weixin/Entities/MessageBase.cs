﻿using System;

namespace XY.Entity.Weixin
{
    public interface IMessageBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 所有Request和Response消息的基类
    /// </summary>
    public class MessageBase : IMessageBase
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}