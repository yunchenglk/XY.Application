using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //signature=cb40d15f2a5cddb6b7951b543d485922198b9b0b&timestamp=1456443100&nonce=1898623024&encrypt_type=aes&msg_signature=c923de30d01ddd929c927e74a13e4b66b77f1b48
            string sToken = "0359i";
            string sAppID = "wx3822e482594a911e";
            string sEncodingAESKey = "yF94w3TeWPAqCQNUaqByFD39KrLHc2exOLh6RZGXNhU";

            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);


            string sReqMsgSig = "c923de30d01ddd929c927e74a13e4b66b77f1b48";
            string sReqTimeStamp = "1456443100";
            string sReqNonce = "1898623024";
            string sReqData = @"
                            <xml>
    <AppId><![CDATA[wx3822e482594a911e]]></AppId>
    <Encrypt><![CDATA[SoHtAQe0M1sygtrVPZGgqJw12GL3v5hMVeUUS9tVf3vmq86tZf2gVag8mnjrJ8VYVBXp9pcmCr+RLCe2f/1u9rXZdA0ZnPFcHXBzbAiNtK//C4u9F3gKNMiWJg5ikKLWexMFFtKITM5oe1bU5HQ/p6oiHlafIBwHeLihFCg+Ne/UWW1A7eKyfN96ZvIq8XedF/zMQ84inD7+BntdimijxL9tilLevTT+9JA7JiWfJ+rYTLFIlc9Ptw5fKdnhWtUpL2hHmFjTLuvDg729s8bGvoGvWkm4EgNbBMKiWjrxM91NMPpCRnVYrU+pHS63LjxY7ypTOvOusbmRsNA5vcPiM/Y8uVnziVXDieXwfAAN0+ZvDLYoCF0vrFa2y+0htEEVAlN9bysu8ucJl4IfJNRN94IzVKqNlXpv6Cly/ylbTr9zlIsIbc5rJR0/riTDmToaOjBkFJh0RnvFyj8L35OjZw==]]></Encrypt>
</xml>


                                ";
            string sMsg = "";  //解析之后的明文
            int ret = 0;
            ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);



           

            Console.WriteLine();
        }
    }
}
