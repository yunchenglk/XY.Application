
using System;
using XY.Entity.Weixin.MP;

namespace XY.Weixin.MP.Context
{
    public class MessageContext : IMessageContext
    {
        private int _maxRecordCount;

        public event EventHandler<WeixinContextRemovedEventArgs> MessageContextRemoved;
        public string UserName
        {
            get;
            set;
        }
        public DateTime LastActiveTime
        {
            get;
            set;
        }
        public MessageContainer<IRequestMessageBase> RequestMessages
        {
            get;
            set;
        }
        public MessageContainer<IResponseMessageBase> ResponseMessages
        {
            get;
            set;
        }
        public int MaxRecordCount
        {
            get
            {
                return this._maxRecordCount;
            }
            set
            {
                this.RequestMessages.MaxRecordCount = value;
                this.ResponseMessages.MaxRecordCount = value;
                this._maxRecordCount = value;
            }
        }
        public object StorageData
        {
            get;
            set;
        }
        private void OnMessageContextRemoved(WeixinContextRemovedEventArgs e)
        {
            EventHandler<WeixinContextRemovedEventArgs> messageContextRemoved = this.MessageContextRemoved;
            if (messageContextRemoved != null)
            {
                messageContextRemoved(this, e);
            }
        }
        public MessageContext()
        {
            this.RequestMessages = new MessageContainer<IRequestMessageBase>(this.MaxRecordCount);
            this.ResponseMessages = new MessageContainer<IResponseMessageBase>(this.MaxRecordCount);
            this.LastActiveTime = DateTime.Now;
        }
        public virtual void OnRemoved()
        {
            WeixinContextRemovedEventArgs e = new WeixinContextRemovedEventArgs(this);
            this.OnMessageContextRemoved(e);
        }
    }
}
