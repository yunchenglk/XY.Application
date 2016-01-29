using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class WX_KeyWord
    {
        public string TypeStr
        {
            get
            {
                switch (Type)
                {
                    case 0://图文消息
                        return "图文消息";
                    case 1://文字
                        return "文字";
                    case 2://图片
                        return "图片";
                    case 3://语音
                        return "语音";
                    case 4: //视频
                        return "视频";
                    case 5://API
                        return "API";
                    default:
                        return "未知";
                }
            }
        }
    }
}
