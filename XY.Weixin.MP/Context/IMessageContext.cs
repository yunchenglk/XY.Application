
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XY.Entity.Weixin.MP;

namespace XY.Weixin.MP.Context
{
    public interface IMessageContext
    {
        event EventHandler<WeixinContextRemovedEventArgs> MessageContextRemoved;
        string UserName
        {
            get;
            set;
        }
        DateTime LastActiveTime
        {
            get;
            set;
        }
        MessageContainer<IRequestMessageBase> RequestMessages
        {
            get;
            set;
        }

        MessageContainer<IResponseMessageBase> ResponseMessages
        {
            get;
            set;
        }
        int MaxRecordCount
        {
            get;
            set;
        }
        object StorageData
        {
            get;
            set;
        }
        void OnRemoved();
    }
}
