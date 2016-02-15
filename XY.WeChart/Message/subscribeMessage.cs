using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using XY.Entity;
using XY.Services;
using XY.Services.Weixin.Helpers;
using XY.WeChart.Helpers;

namespace XY.WeChart
{
    public class subscribeMessage : MessageBase
    {
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static subscribeMessage LoadFromXml(string xml)
        {
            subscribeMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new subscribeMessage();
                    tm.FromUserName = element.Element(CommConfig.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(CommConfig.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(CommConfig.CREATE_TIME).Value;
                }
            }

            return tm;
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public override string GenerateContent()
        {
            wx_userinfo entity = wx_userinfoService.instance().GetByopenidAndCompanyID(this.FromUserName, CommFun.companyid);
            if (entity != null)
            {
                entity.subscribe = 1;
                entity.unsubscribe_time = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                wx_userinfoService.instance().Update(entity);
            }
            else
            {
                entity = new wx_userinfo();
                entity.openid = this.FromUserName;
                entity.cID = CommFun.companyid;
                entity.subscribe = 1;
                entity.wID = wx_userweixinService.instance().SingleByCompanyID(CommFun.companyid).ID;
                entity.subscribe_time = DateTimeHelper.GetDateTimeFromXml(this.CreateTime);
                wx_userinfoService.instance().Insert(entity);

            }
            return null;
        }
    }




    public class unsubscribeMessage : MessageBase
    {
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static unsubscribeMessage LoadFromXml(string xml)
        {
            unsubscribeMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new unsubscribeMessage();
                    tm.FromUserName = element.Element(CommConfig.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(CommConfig.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(CommConfig.CREATE_TIME).Value;
                }
            }

            return tm;
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public override string GenerateContent()
        {
            wx_userinfo entity = wx_userinfoService.instance().GetByopenidAndCompanyID(this.FromUserName, CommFun.companyid);
            if (entity != null)
            {
                entity.subscribe = 0;
                entity.unsubscribe_time = DateTimeHelper.GetDateTimeFromXml(this.CreateTime);
                wx_userinfoService.instance().Update(entity);
            }
            return null;
        }
    }
}