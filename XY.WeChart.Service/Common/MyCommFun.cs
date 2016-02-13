using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace XY.WeChart.Service
{
    public class MyCommFun
    {
        public enum RequestObjType
        {
            intType, stringType, floatType, guidType
        }
        #region request参数处理
        /// <summary>
        /// 判断request的参数是否合法
        /// </summary>
        /// <param name="param">参数名称</param>
        /// <param name="otype"></param>
        /// <returns></returns>
        public static bool IsRequestStr(string param, RequestObjType otype)
        {
            bool ret = true;
            if (HttpContext.Current.Request[param] == null || HttpContext.Current.Request[param].ToString().Trim() == "")
            {
                return false;
            }

            string pValue = HttpContext.Current.Request[param].ToString().Trim();

            switch (otype)
            {
                case RequestObjType.intType:
                    int tmpInt = 0;
                    if (!int.TryParse(pValue, out tmpInt))
                    {
                        return false;
                    }
                    break;
                case RequestObjType.floatType:
                    float tmpFloat = 0;
                    if (!float.TryParse(pValue, out tmpFloat))
                    {
                        return false;
                    }
                    break;
                case RequestObjType.stringType:
                    break;
                case RequestObjType.guidType:
                    Guid tmpGuid;
                    if (!Guid.TryParse(pValue, out tmpGuid))
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// url请求里的参数
        /// 过滤非法字符
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string QueryString(string param)
        {
            if (HttpContext.Current.Request[param] == null || HttpContext.Current.Request[param].ToString().Trim() == "")
            {
                return "";
            }
            string ret = HttpContext.Current.Request[param].ToString().Trim();
            ret = ret.Replace(",", "");
            ret = ret.Replace("'", "");
            ret = ret.Replace(";", "");

            return ret;
        }

        /// <summary>
        /// 取request参数的值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Guid RequestGuid(string param)
        {
            if (IsRequestStr(param, RequestObjType.guidType))
            {
                return Guid.Parse(HttpContext.Current.Request[param]);
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 取request参数的值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int RequestInt(string param, int defaultInt)
        {
            if (IsRequestStr(param, RequestObjType.intType))
            {
                return int.Parse(HttpContext.Current.Request[param]);
            }
            else
            {
                return defaultInt;
            }
        }

        /// <summary>
        /// 取request参数的值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static float RequestFloat(string param, float defaultFloat)
        {
            if (IsRequestStr(param, RequestObjType.floatType))
            {
                return float.Parse(HttpContext.Current.Request[param]);
            }
            else
            {
                return defaultFloat;
            }
        }


        /// <summary>
        /// 获得参数openid
        /// 过滤非法字符
        /// </summary>
        /// <returns></returns>
        public static string RequestOpenid()
        {
            string ret = "";
            if (HttpContext.Current.Request.Params["openid"] != null)
            {
                ret = HttpContext.Current.Request.Params["openid"].ToString();
            }
            if (ret == "")
            {
                ret = "loseopenid";
            }
            ret = ret.Replace(",", "");
            ret = ret.Replace("'", "");
            ret = ret.Replace(";", "");

            return ret;
        }

        /// <summary>
        /// 获得参数wid
        /// </summary>
        /// <returns></returns>
        //public static Guid RequestWid()
        //{
        //    int ret =  RequestInt("wid");
        //    return ret;
        //}



        public static string RequestParam(string paramName)
        {
            string ret = "";
            if (HttpContext.Current.Request.Params[paramName] != null)
            {
                ret = HttpContext.Current.Request.Params[paramName].ToString();
            }
            return ret;
        }
        #endregion

        #region 判断数据类型和处理数据类型转换
        /// <summary>
        /// object 转 str
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ObjToStr(object o)
        {
            try
            {
                if (o == null)
                    return "";
                else
                    return o.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// object 转 str，若转换失败，则返回nullReplaceStr字符串
        /// </summary>
        /// <param name="o"></param>
        /// <param name="nullReplaceStr"></param>
        /// <returns></returns>
        public static string ObjToStr(object o, string nullReplaceStr)
        {
            try
            {
                if (o == null)
                    return nullReplaceStr;
                else
                    return o.ToString();
            }
            catch (Exception ex)
            {
                return nullReplaceStr;
            }
        }

        /// <summary>
        /// 判断对象是否可以转成int型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isNumber(object o)
        {
            int tmpInt;
            if (o == null)
                return false;
            if (o.ToString().Trim().Length == 0)
                return false;
            if (!int.TryParse(o.ToString(), out tmpInt))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断是否是合法的时间类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isDateTime(object o)
        {
            DateTime tmpInt = new DateTime();
            if (o == null)
                return false;
            if (o.ToString().Trim().Length == 0)
                return false;
            if (!DateTime.TryParse(o.ToString(), out tmpInt))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isDecimal(object o)
        {
            decimal tmpInt;
            if (o == null)
                return false;
            if (o.ToString().Trim().Length == 0)
                return false;
            if (!decimal.TryParse(o.ToString(), out tmpInt))
                return false;
            else
                return true;
        }
        /// <summary>
        /// 字符串转变成数字，如果转行失败，则返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Str2Int(string str)
        {
            if (isNumber(str))
                return int.Parse(str);
            else
                return 0;
        }
        /// <summary>
        /// 字符串转变成数字，如果转行失败，则返回defaultInt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Str2Int(string str, int defaultInt)
        {
            if (isNumber(str))
                return int.Parse(str);
            else
                return defaultInt;
        }
        /// <summary>
        /// 对象object转int，若失败则为0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Obj2Int(object obj)
        {
            if (isNumber(obj))
                return int.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 对象object转decimal，若失败则为defaultDec
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal Obj2Decimal(object obj, decimal defaultDec)
        {
            decimal tmpDecimal = 0;
            if (obj == null || obj.ToString().Trim() == "")
                return defaultDec;
            if (decimal.TryParse(obj.ToString(), out tmpDecimal))
                return tmpDecimal;
            else
                return defaultDec;
        }

        /// <summary>
        /// 对象object转int，若失败则为defaultInt
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultInt"></param>
        /// <returns></returns>
        public static int Obj2Int(object obj, int defaultInt)
        {
            if (isNumber(obj))
                return int.Parse(obj.ToString());
            else
                return defaultInt;
        }


        public static float Str2Float(string str)
        {
            if (str == null || str.Trim().Length <= 0)
                return 0;
            float tmpFloat = 0;
            if (float.TryParse(str, out tmpFloat))
                return tmpFloat;
            else
                return 0;

        }
        public static decimal Str2Decimal(string str)
        {
            if (str == null || str.Trim().Length <= 0)
                return 0;
            decimal tmpFloat = 0;
            if (decimal.TryParse(str, out tmpFloat))
                return tmpFloat;
            else
                return 0;
        }
        /// <summary>
        /// object 2时间类型，如果不成功则返回1990-1-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime Obj2DateTime(object obj)
        {
            if (isDateTime(obj))
                return DateTime.Parse(obj.ToString());
            else
                return DateTime.Parse("1990-1-1");
        }
        public static decimal decimalF2(decimal num)
        {
            decimal ret = 0;
            ret = decimal.Parse(num.ToString("f2"));
            return ret;
        }

        #endregion

        #region 微信后缀操作

        public static string getWXApiUrl(string url, string token, string openid)
        {
            string ret = "";
            if (openid == "")
                openid = "loseopenid";
            if (token == "")
                token = "losetoken";
            if (url.Contains("?"))
                ret = url + "&token=" + token + "&openid=" + openid;
            else
                ret = url + "?token=" + token + "&openid=" + openid;
            return ret;
        }

        /// <summary>
        /// 给url地址添加openid
        /// </summary>
        /// <param name="orginUrl"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static string urlAddOpenid(string orginUrl, string openid)
        {
            if (openid == "")
                openid = "loseopenid";
            if (orginUrl.Contains("?openid=") || orginUrl.Contains("&openid="))
                return orginUrl;
            if (orginUrl.Contains("?"))
                return orginUrl + "&openid=" + openid;
            else
                return orginUrl + "?openid=" + openid;
        }
        /// <summary>
        /// 设置微信支持的一键拨号手机的url后缀
        /// </summary>
        /// <returns></returns>
        public static string getWxSuffixByTel()
        {
            string suffix = "";
            if (ConfigurationManager.AppSettings["nati_suffix"] != null)
                suffix = ConfigurationManager.AppSettings["nati_suffix"].ToString();
            return suffix;
        }

        #endregion

    }
}