using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using XY.Entity;
using XY.Services;

namespace XY.WeChart
{
    public class LocationMessage : MessageBase
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 精确
        /// </summary>
        public double Precision { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LocationMessage()
        {
            this.MsgType = "event";
        }
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static LocationMessage LoadFromXml(string xml)
        {
            LocationMessage em = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    em = new LocationMessage();
                    em.FromUserName = element.Element(CommConfig.FROM_USERNAME).Value;
                    em.ToUserName = element.Element(CommConfig.TO_USERNAME).Value;
                    em.CreateTime = element.Element(CommConfig.CREATE_TIME).Value;
                    em.Latitude = Convert.ToDouble(element.Element("Latitude").Value);
                    em.Longitude = Convert.ToDouble(element.Element("Longitude").Value);
                    em.Precision = Convert.ToDouble(element.Element("Precision").Value);
                }
            }
            return em;
        }
        //执行操作
        public override string GenerateContent()
        {
            wx_userinfo entity = wx_userinfoService.instance().GetByopenidAndCompanyID(this.FromUserName, CommFun.companyid);
            if (entity != null)
            {
                entity.Latitude = this.Latitude.ToString();
                entity.Longitude = this.Longitude.ToString();
                entity.Precision = this.Precision.ToString();
                wx_userinfoService.instance().Update(entity);
            }

            return null;
        }

    }
}