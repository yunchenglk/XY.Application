using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mail;
using System.Web.Http;
using XY.Services;
using XY.Entity;
using System.Text;
using System.Collections;

namespace XY.API.Controllers
{
    /// <summary>
    /// 留言管理
    /// </summary>
    [Filters.AuthorizeFilter]
    public class MessageController : ApiController
    {
        private static readonly string url = System.Web.HttpContext.Current.Request.UrlReferrer.GetLeftPart(UriPartial.Authority);

        Company Company = CompanyService.instance().GetCompanyByUrl(url);

        /// <summary>
        /// 根据CompanyID获取留言
        /// </summary>
        /// <param name="id">ComnpanyID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id)
        {
            Guid CID;
            if (Guid.TryParse(id, out CID))
            {
                return Util.Utils.ConvertToJson(MessagesDetailsService.instance().GetEnum(CID));
            }
            return null;
        }
        /// <summary>
        /// 获取子类留言
        /// </summary>
        /// <param name="pid">上级ID</param>
        /// <param name="mid">上级ID</param>
        /// <returns></returns>
        public HttpResponseMessage GetRelpy(string pid, string mid)
        {
            Guid PID;
            Guid MID;
            if (Guid.TryParse(pid, out PID) && Guid.TryParse(mid, out MID))
            {
                return Util.Utils.ConvertToJson(Messages_ReplyDetailsService.instance().GetChilds(PID, MID));
            }
            return null;
        }

        /// <summary>
        /// 发表留言
        /// </summary>
        /// <param name="mes">post留言模板</param>
        public HttpResponseMessage Post([FromBody]_post_Message mes)
        {
            Hashtable hash = new Hashtable();
            try
            {
                //StringBuilder str = new StringBuilder();
                //str.Append("姓名：" + mes.Name + "<br/>");
                //str.Append("邮箱：" + mes.Mail + "<br/>");
                //str.Append("电话：" + mes.Phone + "<br/>");
                //str.Append("留言：" + mes.Content);
                //SendMail sendmail = new SendMail(Company.Email, Company.Email, Company.EmailPass, str.ToString(), mes.Title);
                //sendmail.Send();

                if (string.IsNullOrEmpty(mes.MessageID))
                {
                    //添加到数据库
                    Messages message = new Messages();
                    message.CompanyID = Company.ID;
                    message.Type = 0;
                    message.RelationID = Guid.Empty;
                    message.UserID = Guid.Empty;
                    message.Content = Util.Utils.UrlDecode(mes.Content);
                    message.Count = 0;
                    message.Name = mes.Name;
                    message.Phone = mes.Phone;
                    message.Email = mes.Mail;
                    MessagesService.instance().Insert(message);
                }
                else
                {
                    Messages_Reply reply = new Messages_Reply();
                    reply.Content = Util.Utils.UrlDecode(mes.Content);
                    reply.UserID = Guid.Empty;
                    reply.MessageID = new Guid(mes.MessageID);
                    reply.ParentID = new Guid(mes.PID);
                    Messages_ReplyService.instance().Insert(reply);
                }
            }
            catch (Exception ex)
            {
                hash["error"] = 1;
                hash["message"] = ex.ToString();
            }
            hash["error"] = 0;
            hash["message"] = "success";
            return Util.Utils.ConvertToJson(hash);
        }

    }
}
