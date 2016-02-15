using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XY.WeChart
{
    public class MessageBase : ITemplate
    {
        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 接收方账号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        public MessageBase()
        {

        }

        public virtual string Template
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string GenerateContent()
        {
            throw new NotImplementedException();
        }
    }
}