using System.Collections.Generic;

namespace XY.Entity.Weixin.MP
{
    public class ResponseMessageTransfer_Customer_Service : ResponseMessageBase, IResponseMessageBase
    {
        public ResponseMessageTransfer_Customer_Service()
        {
            TransInfo = new List<CustomerServiceAccount>();
        }

        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Transfer_Customer_Service; }
        }

        public List<CustomerServiceAccount> TransInfo { get; set; }
    }

    public class CustomerServiceAccount
    {
        public string KfAccount { get; set; }
    }
}
