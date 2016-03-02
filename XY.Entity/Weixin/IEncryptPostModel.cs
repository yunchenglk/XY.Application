using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    /// <summary>
    /// 接收解密信息统一接口
    /// </summary>
    public interface IEncryptPostModel
    {
        string Signature { get; set; }
        string Msg_Signature { get; set; }
        string Timestamp { get; set; }
        string Nonce { get; set; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用
        string Token { get; set; }
        string EncodingAESKey { get; set; }
    }
}
