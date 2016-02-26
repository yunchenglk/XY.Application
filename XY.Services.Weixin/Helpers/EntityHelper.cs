using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XY.Services.Weixin.Entities;

namespace XY.Services.Weixin.Helpers
{
    public static class EntityHelper
    {
        /// <summary>
        /// ResponseMessageBase.CreateFromRequestMessage<T>(requestMessage)的扩展方法
        /// </summary>
        /// <typeparam name="T">需要生成的ResponseMessage类型</typeparam>
        /// <param name="requestMessage">IRequestMessageBase接口下的接收信息类型</param>
        /// <returns></returns>
        public static T CreateResponseMessage<T>(this IRequestMessageBase requestMessage) where T : ResponseMessageBase
        {
            return ResponseMessageBase.CreateFromRequestMessage<T>(requestMessage);
        }
        /// <summary>
        /// 根据XML信息填充实实体
        /// </summary>
        /// <typeparam name="T">MessageBase为基类的类型，Response和Request都可以</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="doc">XML</param>
        public static void FillEntityWithXml<T>(this T entity, XDocument doc) where T : /*MessageBase*/ class, new()
        {
            entity = entity ?? new T();
            var root = doc.Root;

            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (root.Element(propName) != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        //case "String":
                        //    goto default;
                        case "DateTime":
                            prop.SetValue(entity, DateTimeHelper.GetDateTimeFromXml(root.Element(propName).Value), null);
                            break;
                        case "Boolean":
                            if (propName == "FuncFlag")
                            {
                                prop.SetValue(entity, root.Element(propName).Value == "1", null);
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        case "Int32":
                            prop.SetValue(entity, int.Parse(root.Element(propName).Value), null);
                            break;
                        case "Int64":
                            prop.SetValue(entity, long.Parse(root.Element(propName).Value), null);
                            break;
                        case "Double":
                            prop.SetValue(entity, double.Parse(root.Element(propName).Value), null);
                            break;
                        //以下为枚举类型
                        case "RequestMsgType":
                            //已设为只读
                            //prop.SetValue(entity, MsgTypeHelper.GetRequestMsgType(root.Element(propName).Value), null);
                            break;
                        case "ResponseMsgType"://Response适用
                                               //已设为只读
                                               //prop.SetValue(entity, MsgTypeHelper.GetResponseMsgType(root.Element(propName).Value), null);
                            break;
                        case "Event":
                            //已设为只读
                            //prop.SetValue(entity, EventHelper.GetEventType(root.Element(propName).Value), null);
                            break;
                        //以下为实体类型
                        case "List`1"://List<T>类型，ResponseMessageNews适用
                            var genericArguments = prop.PropertyType.GetGenericArguments();
                            if (genericArguments[0].Name == "Article")//ResponseMessageNews适用
                            {
                                //文章下属节点item
                                List<Article> articles = new List<Article>();
                                foreach (var item in root.Element(propName).Elements("item"))
                                {
                                    var article = new Article();
                                    FillEntityWithXml(article, new XDocument(item));
                                    articles.Add(article);
                                }
                                prop.SetValue(entity, articles, null);
                            }
                            else if (genericArguments[0].Name == "Account")
                            {
                                List<CustomerServiceAccount> accounts = new List<CustomerServiceAccount>();
                                foreach (var item in root.Elements(propName))
                                {
                                    var account = new CustomerServiceAccount();
                                    FillEntityWithXml(account, new XDocument(item));
                                    accounts.Add(account);
                                }
                                prop.SetValue(entity, accounts, null);
                            }
                            else if (genericArguments[0].Name == "PicItem")
                            {
                                List<PicItem> picItems = new List<PicItem>();
                                foreach (var item in root.Elements(propName).Elements("item"))
                                {
                                    var picItem = new PicItem();
                                    var picMd5Sum = item.Element("PicMd5Sum").Value;
                                    Md5Sum md5Sum = new Md5Sum() { PicMd5Sum = picMd5Sum };
                                    picItem.item = md5Sum;
                                    picItems.Add(picItem);
                                }
                                prop.SetValue(entity, picItems, null);
                            }
                            break;
                        case "Music"://ResponseMessageMusic适用
                            Music music = new Music();
                            FillEntityWithXml(music, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, music, null);
                            break;
                        case "Image"://ResponseMessageImage适用
                            Image image = new Image();
                            FillEntityWithXml(image, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, image, null);
                            break;
                        case "Voice"://ResponseMessageVoice适用
                            Voice voice = new Voice();
                            FillEntityWithXml(voice, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, voice, null);
                            break;
                        case "Video"://ResponseMessageVideo适用
                            Video video = new Video();
                            FillEntityWithXml(video, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, video, null);
                            break;
                        case "ScanCodeInfo"://扫码事件中的ScanCodeInfo适用
                            ScanCodeInfo scanCodeInfo = new ScanCodeInfo();
                            FillEntityWithXml(scanCodeInfo, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, scanCodeInfo, null);
                            break;
                        case "SendLocationInfo"://弹出地理位置选择器的事件推送中的SendLocationInfo适用
                            SendLocationInfo sendLocationInfo = new SendLocationInfo();
                            FillEntityWithXml(sendLocationInfo, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, sendLocationInfo, null);
                            break;
                        case "SendPicsInfo"://系统拍照发图中的SendPicsInfo适用
                            SendPicsInfo sendPicsInfo = new SendPicsInfo();
                            FillEntityWithXml(sendPicsInfo, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, sendPicsInfo, null);
                            break;
                        default:
                            prop.SetValue(entity, root.Element(propName).Value, null);
                            break;
                    }
                }
            }
        }
    }
}
