using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity.Weixin
{
    /// <summary>
    /// 语音理解
    /// </summary>
    public class BaseSemanticResultJson : WxJsonResult
    {
        public int res { get; set; }//用于标识用户请求后的状态
        public string query { get; set; }//用户的输入字符串
        public string type { get; set; }//服务的全局类别id
        //public BaseSemantic semantic { get; set; }//语义理解后的结构化标识，各个服务不同
        public string answer { get; set; }//部分类别的结果html5展示，目前不支持
        public string text { get; set; }//特殊回复说明
    }
    public class BaseSemantic
    {
        public string intent { get; set; }
    }

    public class SearchResultJson : BaseSemanticResultJson
    {
        public SearchSemantic semantic { get; set; }
    }
    public class SearchSemantic : BaseSemantic
    {
        public SearchSemanticDetail details { get; set; }
    }
    public class SearchSemanticDetail
    {
        public string keyword { get; set; }
        public string channel { get; set; }
    }
}
