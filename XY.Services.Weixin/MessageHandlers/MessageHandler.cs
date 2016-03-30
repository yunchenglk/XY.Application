using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tencent;
using XY.Entity.Weixin;
using XY.Services.Weixin.Context;
using XY.Services.Weixin.Entities;
using XY.Services.Weixin.Exceptions;
using XY.Services.Weixin.Helpers;

namespace XY.Services.Weixin.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TC> :
        MessageHandler_<TC, IRequestMessageBase, IResponseMessageBase>, IMessageHandler
        where TC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 上下文（仅限于当前MessageHandler基类内）
        /// </summary>
        public static WeixinContext<TC, IRequestMessageBase, IResponseMessageBase> GlobalWeixinContext = new WeixinContext<TC, IRequestMessageBase, IResponseMessageBase>();
        //TODO:这里如果用一个MP自定义的WeixinContext，继承WeixinContext<TC, IRequestMessageBase, IResponseMessageBase>，在下面的WeixinContext中将无法转换成基类要求的类型


        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public override WeixinContext<TC, IRequestMessageBase, IResponseMessageBase> WeixinContext
        {
            get
            {
                return GlobalWeixinContext;
            }
        }

        /// <summary>
        /// 原始的加密请求（如果不加密则为null）
        /// </summary>
        public XDocument EcryptRequestDocument { get; set; }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public override XDocument ResponseDocument
        {
            get
            {
                if (ResponseMessage == null)
                {
                    return null;
                }
                return EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase);
            }
        }

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 这里是Senparc.Weixin.MP，根据请求消息半段在ResponseDocument基础上是否需要再次进行加密（每次获取重新加密，所以结果会不同）
        /// </summary>
        public override XDocument FinalResponseDocument
        {
            get
            {
                if (ResponseDocument == null)
                {
                    return null;
                }

                if (!UsingEcryptMessage)
                {
                    return ResponseDocument;
                }

                var timeStamp = DateTime.Now.Ticks.ToString();
                var nonce = DateTime.Now.Ticks.ToString();

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
                string finalResponseXml = null;
                msgCrype.EncryptMsg(ResponseDocument.ToString().Replace("\r\n", "\n")/* 替换\r\n是为了处理iphone设备上换行bug */, timeStamp, nonce, ref finalResponseXml);//TODO:这里官方的方法已经把EncryptResponseMessage对应的XML输出出来了

                return XDocument.Parse(finalResponseXml);
            }
        }


        /// <summary>
        /// 请求实体
        /// </summary>
        public new IRequestMessageBase RequestMessage
        {
            get
            {
                return base.RequestMessage as IRequestMessageBase;
            }
            set
            {
                base.RequestMessage = value;
            }
        }

        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public new IResponseMessageBase ResponseMessage
        {
            get
            {
                return base.ResponseMessage as IResponseMessageBase;
            }
            set
            {
                base.ResponseMessage = value;
            }
        }

        private PostModel _postModel;

        /// <summary>
        /// 是否使用了加密信息
        /// </summary>
        public bool UsingEcryptMessage { get; set; }

        /// <summary>
        /// 是否使用了兼容模式加密信息
        /// </summary>
        public bool UsingCompatibilityModelEcryptMessage { get; set; }

        /// <summary>
        /// 动态去重判断委托，仅当返回值为false时，不使用消息去重功能
        /// </summary>
        public Func<IRequestMessageBase, bool> OmitRepeatedMessageFunc = null;

        /// <summary>
        /// 构造MessageHandler
        /// </summary>
        /// <param name="inputStream">请求消息流</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">单个用户上下文消息列表储存的最大长度</param>
        /// <param name="developerInfo">微微嗨开发者信息，如果不为空，则优先请求云端应用商店的资源</param>
        public MessageHandler(Stream inputStream, PostModel postModel = null, int maxRecordCount = 0)
            : base(inputStream, maxRecordCount, postModel)
        {
        }

        /// <summary>
        /// 构造MessageHandler
        /// </summary>
        /// <param name="requestDocument">请求消息的XML</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">单个用户上下文消息列表储存的最大长度</param>
        /// <param name="developerInfo">微微嗨开发者信息，如果不为空，则优先请求云端应用商店的资源</param>
        public MessageHandler(XDocument requestDocument, PostModel postModel = null, int maxRecordCount = 0)
            : base(requestDocument, maxRecordCount, postModel)
        {
        }

        /// <summary>
        /// 直接传入IRequestMessageBase，For UnitTest
        /// </summary>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">单个用户上下文消息列表储存的最大长度</param>
        /// <param name="developerInfo">微微嗨开发者信息，如果不为空，则优先请求云端应用商店的资源</param>
        /// <param name="requestMessageBase"></param>
        public MessageHandler(RequestMessageBase requestMessageBase, PostModel postModel = null, int maxRecordCount = 0)
            : base(requestMessageBase, maxRecordCount, postModel)
        {

            var postDataDocument = requestMessageBase.ConvertEntityToXml();
            base.CommonInitialize(postDataDocument, maxRecordCount, postModel);
        }


        public override XDocument Init(XDocument postDataDocument, object postData = null)
        {
            //进行加密判断并处理
            _postModel = postData as PostModel;
            var postDataStr = postDataDocument.ToString();

            XDocument decryptDoc = postDataDocument;

            if (_postModel != null && postDataDocument.Root.Element("Encrypt") != null && !string.IsNullOrEmpty(postDataDocument.Root.Element("Encrypt").Value))
            {
                //使用了加密
                UsingEcryptMessage = true;
                EcryptRequestDocument = postDataDocument;

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
                string msgXml = null;
                var result = msgCrype.DecryptMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataStr, ref msgXml);

                //判断result类型
                if (result != 0)
                {
                    //验证没有通过，取消执行
                    CancelExcute = true;
                    return null;
                }

                if (postDataDocument.Root.Element("FromUserName") != null && !string.IsNullOrEmpty(postDataDocument.Root.Element("FromUserName").Value))
                {
                    //TODO：使用了兼容模式，进行验证即可
                    UsingCompatibilityModelEcryptMessage = true;
                }

                decryptDoc = XDocument.Parse(msgXml);//完成解密
            }

            RequestMessage = RequestMessageFactory.GetRequestEntity(decryptDoc);
            if (UsingEcryptMessage)
            {
                RequestMessage.Encrypt = postDataDocument.Root.Element("Encrypt").Value;
            }


            //记录上下文
            if (WeixinContextGlobal.UseWeixinContext)
            {
                WeixinContext.InsertMessage(RequestMessage);
            }

            return decryptDoc;
        }

        /// <summary>
        /// 根据当前的RequestMessage创建指定类型的ResponseMessage
        /// </summary>
        /// <typeparam name="TR">基于ResponseMessageBase的响应消息类型</typeparam>
        /// <returns></returns>
        public TR CreateResponseMessage<TR>() where TR : ResponseMessageBase
        {
            if (RequestMessage == null)
            {
                return null;
            }

            return RequestMessage.CreateResponseMessage<TR>();
        }

        /// <summary>
        /// 执行微信请求
        /// </summary>
        public override void Execute()
        {
            if (CancelExcute)
            {
                return;
            }

            OnExecuting();

            if (CancelExcute)
            {
                return;
            }

            try
            {
                if (RequestMessage == null)
                {
                    return;
                }

                switch (RequestMessage.MsgType)
                {
                    case RequestMsgType.TEXT:
                        {
                            var requestMessage = RequestMessage as RequestMessageText;
                            ResponseMessage = OnTextOrEventRequest(requestMessage) ?? OnTextRequest(requestMessage);
                        }
                        break;
                    case RequestMsgType.LOCATION:
                        ResponseMessage = OnLocationRequest(RequestMessage as RequestMessageLocation);
                        break;
                    case RequestMsgType.IMAGE:
                        ResponseMessage = OnImageRequest(RequestMessage as RequestMessageImage);
                        break;
                    case RequestMsgType.VOICE:
                        ResponseMessage = OnVoiceRequest(RequestMessage as RequestMessageVoice);
                        break;
                    case RequestMsgType.VIDEO:
                        ResponseMessage = OnVideoRequest(RequestMessage as RequestMessageVideo);
                        break;
                    case RequestMsgType.LINK:
                        ResponseMessage = OnLinkRequest(RequestMessage as RequestMessageLink);
                        break;
                    case RequestMsgType.SHORTVIDEO:
                        ResponseMessage = OnShortVideoRequest(RequestMessage as RequestMessageShortVideo);
                        break;
                    case RequestMsgType.EVENT:
                        {
                            var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();
                            ResponseMessage = OnTextOrEventRequest(requestMessageText)
                                                ?? OnEventRequest(RequestMessage as IRequestMessageEventBase);
                            //Util.LogHelper.Info(ResponseMessage.Content);
                        }
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                }

                //记录上下文
                if (WeixinContextGlobal.UseWeixinContext && ResponseMessage != null)
                {
                    WeixinContext.InsertMessage(ResponseMessage);
                }
            }
            catch (Exception ex)
            {
                throw new MessageHandlerException("MessageHandler中Execute()过程发生错误：" + ex.Message, ex);
            }
            finally
            {
                OnExecuted();
            }
        }

        public virtual void OnExecuting()
        {
            //消息去重
            if ((OmitRepeatedMessageFunc == null || OmitRepeatedMessageFunc(RequestMessage) == true)
                && OmitRepeatedMessage && CurrentMessageContext.RequestMessages.Count > 1
                //&& !(RequestMessage is RequestMessageEvent_Merchant_Order)批量订单的MsgId可能会相同
                )
            {
                var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count - 2];
                if ((lastMessage.MsgId != 0 && lastMessage.MsgId == RequestMessage.MsgId)//使用MsgId去重
                    ||
                    ((lastMessage.CreateTime == RequestMessage.CreateTime && lastMessage.MsgType == RequestMessage.MsgType))//使用CreateTime去重（OpenId对象已经是同一个）
                    )
                {
                    CancelExcute = true;//重复消息，取消执行
                    return;
                }
            }

            base.OnExecuting();

            //判断是否已经接入开发者信息
            //if (DeveloperInfo != null || CurrentMessageContext.AppStoreState == AppStoreState.Enter)
            //{
            //    //优先请求云端应用商店资源

            //}
        }

        public virtual void OnExecuted()
        {
            base.OnExecuted();
        }

    }
}
