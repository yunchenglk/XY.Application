
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XY.Entity.Weixin.MP;

namespace XY.Weixin.MP.Context
{
    public class WeixinContext<TM> where TM : class, IMessageContext, new()
    {
        private int _maxRecordCount;

        public Dictionary<string, TM> MessageCollection
        {
            get;
            set;
        }

        public List<TM> MessageQueue
        {
            get;
            set;
        }

        public double ExpireMinutes
        {
            get;
            set;
        }

        public int MaxRecordCount
        {
            get;
            set;
        }

        public WeixinContext()
        {
            this.Restore();
        }

        public void Restore()
        {
            this.MessageCollection = new Dictionary<string, TM>(StringComparer.OrdinalIgnoreCase);
            this.MessageQueue = new List<TM>();
            this.ExpireMinutes = 90.0;
        }

        private TM GetMessageContext(string userName)
        {
            while (this.MessageQueue.Count > 0)
            {
                TM tM = this.MessageQueue[0];
                if ((DateTime.Now - tM.LastActiveTime).TotalMinutes < this.ExpireMinutes)
                {
                    break;
                }
                this.MessageQueue.RemoveAt(0);
                this.MessageCollection.Remove(tM.UserName);
                tM.OnRemoved();
            }
            if (!this.MessageCollection.ContainsKey(userName))
            {
                return default(TM);
            }
            return this.MessageCollection[userName];
        }

        private TM GetMessageContext(string userName, bool createIfNotExists)
        {
            TM messageContext = this.GetMessageContext(userName);
            if (messageContext == null)
            {
                if (!createIfNotExists)
                {
                    return default(TM);
                }
                Dictionary<string, TM> arg_42_0 = this.MessageCollection;
                TM value = Activator.CreateInstance<TM>();
                value.UserName = userName;
                value.MaxRecordCount = this.MaxRecordCount;
                arg_42_0[userName] = value;
                messageContext = this.GetMessageContext(userName);
                this.MessageQueue.Add(messageContext);
            }
            return messageContext;
        }

        public TM GetMessageContext(IRequestMessageBase requestMessage)
        {
            TM messageContext;
            lock (WeixinContextGlobal.Lock)
            {
                messageContext = this.GetMessageContext(requestMessage.FromUserName, true);
            }
            return messageContext;
        }

        public TM GetMessageContext(IResponseMessageBase responseMessage)
        {
            TM messageContext;
            lock (WeixinContextGlobal.Lock)
            {
                messageContext = this.GetMessageContext(responseMessage.ToUserName, true);
            }
            return messageContext;
        }

        public void InsertMessage(IRequestMessageBase requestMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                string userName = requestMessage.FromUserName;
                TM messageContext = this.GetMessageContext(userName, true);
                if (messageContext.RequestMessages.Count > 0)
                {
                    int num = this.MessageQueue.FindIndex((TM z) => z.UserName == userName);
                    if (num >= 0)
                    {
                        this.MessageQueue.RemoveAt(num);
                        this.MessageQueue.Add(messageContext);
                    }
                }
                messageContext.LastActiveTime = DateTime.Now;
                messageContext.RequestMessages.Add(requestMessage);
            }
        }

        public void InsertMessage(IResponseMessageBase responseMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                TM messageContext = this.GetMessageContext(responseMessage.ToUserName, true);
                messageContext.ResponseMessages.Add(responseMessage);
            }
        }

        public IRequestMessageBase GetLastRequestMessage(string userName)
        {
            IRequestMessageBase result;
            lock (WeixinContextGlobal.Lock)
            {
                TM messageContext = this.GetMessageContext(userName, true);
                result = messageContext.RequestMessages.LastOrDefault<IRequestMessageBase>();
            }
            return result;
        }

        public IResponseMessageBase GetLastResponseMessage(string userName)
        {
            IResponseMessageBase result;
            lock (WeixinContextGlobal.Lock)
            {
                TM messageContext = this.GetMessageContext(userName, true);
                result = messageContext.ResponseMessages.LastOrDefault<IResponseMessageBase>();
            }
            return result;
        }
    }
}
