using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.Weixin
{
    public class GetSendResult : WxJsonResult
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string msg_id { get; set; }

        /// <summary>
        /// 消息发送后的状态，SEND_SUCCESS表示发送成功
        /// </summary>
        public string msg_status { get; set; }
    }
}
