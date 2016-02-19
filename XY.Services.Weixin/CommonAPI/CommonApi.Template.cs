using XY.Entity.Weixin;

namespace XY.Services.Weixin
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public partial class CommonApi
    {
        public static SendTemplateMessageResult SendTemplateMessage<T>(string accessToken, string openId, string templateId, string topcolor, string url, T data)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
            var msgData = new TempleteModel()
            {
                touser = openId,
                template_id = templateId,
                topcolor = topcolor,
                url = url,
                data = data
            };
            return CommonJsonSend.Send<SendTemplateMessageResult>(accessToken, urlFormat, msgData);
        }
    }
}
