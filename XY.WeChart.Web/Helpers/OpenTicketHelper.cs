using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Services.Weixin.Exceptions;

namespace XY.WeChart.Web.Helpers
{
    /// <summary>
    /// OpenTicket即ComponentVerifyTicket
    /// </summary>
    public class OpenTicketHelper
    {
        public static string GetOpenTicket(string componentAppId)
        {



            return UserDateTicket.wx_open.open_ticket;
            ////实际开发过程不一定要用文件记录，也可以用数据库。
            //var openTicketPath = Server.GetMapPath("~/App_Data/OpenTicket");
            //string openTicket = null;
            //var filePath = Path.Combine(openTicketPath, string.Format("{0}.txt", componentAppId));
            //if (File.Exists(filePath))
            //{
            //    using (TextReader tr = new StreamReader(filePath))
            //    {
            //        openTicket = tr.ReadToEnd();
            //    }
            //}
            //else
            //{
            //    throw new WeixinException("OpenTicket不存在！");
            //}

            ////其他逻辑

            //return openTicket;
        }
    }
}
