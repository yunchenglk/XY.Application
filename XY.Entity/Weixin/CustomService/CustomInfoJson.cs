using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.Weixin
{
    public class CustomInfoJson : WxJsonResult
    {
        /// <summary>
        /// 客服列表
        /// </summary>
        public List<CustomInfo_Json> kf_list { get; set; }
    }

    public class CustomInfo_Json
    {
        /// <summary>
        /// 客服账号
        /// </summary>
        public string kf_account { get; set; }

        /// <summary>
        /// 客服昵称
        /// </summary>
        public string kf_nick { get; set; }

        /// <summary>
        /// 客服工号
        /// </summary>
        public int kf_id { get; set; }
    }
    public class Custom_add 
    {
        /// <summary>
        /// 客服账号
        /// </summary>
        public string kf_account { get; set; }
        /// <summary>
        /// 客服昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 客服密码
        /// </summary>
        public string password { get; set; }

    }
}