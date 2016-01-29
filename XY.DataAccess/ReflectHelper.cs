using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XY.DataAccess
{
    public class ReflectHelper
    {

        #region Get Data Info [method]=PopulateChangeFromIDataReader<T>
        /// <summary>
        /// PopulateChangeFromIDataReader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T PopulateChangeFromIDataReader<T>(IDataReader dr) where T : new()
        {
            if (dr == null) throw new ArgumentNullException("dr");
            T t = new T();
            DataTable dt = dr.GetSchemaTable();
            //处理一下，做一下缓存
            System.Reflection.PropertyInfo[] propertys = GetPropertys(t.GetType());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //检索所有字段
                foreach (PropertyInfo property in propertys)
                {
                    if (dt.Rows[i][0].ToString().ToLower() == property.Name.ToLower())
                    {
                        if (dr[property.Name] == System.DBNull.Value)
                        {
                            switch (property.PropertyType.Name.ToLower())
                            {
                                case "datetime":
                                    property.SetValue(t, new DateTime(1970, 1, 1), null);
                                    break;
                                //add by 董炳会 2011-02-28 当guid为dbnull时，用默认值代替
                                case "guid":
                                    property.SetValue(t, Guid.Empty, null);
                                    break;
                                case "int":
                                    property.SetValue(t, -1, null);
                                    break;
                                case "byte":
                                    property.SetValue(t, -1, null);
                                    break;
                                case "bool":
                                    property.SetValue(t, false, null);
                                    break;
                                case "decimal":
                                    property.SetValue(t, decimal.Parse("0"), null);
                                    break;
                                default:
                                    property.SetValue(t, Convert.ChangeType(dr[property.Name], property.PropertyType), null);
                                    break;
                            }
                        }
                        else if (property.PropertyType.IsEnum)
                        {
                            property.SetValue(t, Enum.Parse(property.PropertyType, dr[property.Name].ToString()), null);
                        }
                        else
                        {
                            property.SetValue(t, Convert.ChangeType(dr[property.Name], property.PropertyType), null);
                            break;
                        }
                    }
                }
            }
            return t;
        }
        #endregion

        /// <summary>
        /// GetFieldValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static object GetFieldValue<T>(T t, string FieldName)
        {
            if (string.IsNullOrEmpty(FieldName)) throw new ArgumentNullException("FieldName");
            if (t == null) throw new ArgumentNullException("t");
            object ReturnObjValue = null;
            try
            {
                System.Reflection.PropertyInfo[] propertys = GetPropertys(t.GetType());
                //检索所有字段
                foreach (PropertyInfo property in propertys)
                {
                    if (property.Name.ToLower() == FieldName.ToLower())
                    {
                        ReturnObjValue = property.GetValue(t, null);
                        break;
                    }
                }
            }
            catch
            {
                ReturnObjValue = null;
            }

            return ReturnObjValue;
        }

        /// <summary>
        /// GetEntityByXmlAttributeCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlAttributeCollection"></param>
        /// <returns></returns>
        public static T GetEntityByXmlAttributeCollection<T>(XmlAttributeCollection xmlAttributeCollection) where T : new()
        {
            if (xmlAttributeCollection == null) throw new ArgumentNullException("FieldName");
            T t = new T();
            System.Reflection.PropertyInfo[] propertys = GetPropertys(t.GetType());
            //检索所有字段
            foreach (PropertyInfo property in propertys)
            {
                foreach (XmlAttribute xmlAttribute in xmlAttributeCollection)
                {
                    if (property.Name.ToLower() == xmlAttribute.Name.ToLower())
                    {
                        if (property.PropertyType.IsEnum)
                        {
                            property.SetValue(t, Enum.Parse(property.PropertyType, xmlAttribute.Value), null);
                        }
                        else
                        {
                            property.SetValue(t, Convert.ChangeType(xmlAttribute.Value, property.PropertyType), null);
                        }
                        break;
                    }
                }
            }
            return t;
        }

        #region cacheData
        /// <summary>
        /// cache propertys data  
        /// ??? 该方法如果两个实体名称相同的话，因为使用的是实体名称做key而不是完全限定名，所以有可能获得不同的实体出现“对象与目标类型不匹配”问题
        /// </summary>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertys(Type EntityType)
        {
            string EntityKey = EntityType.Name.ToString();
            PropertyInfo[] propertys = CacheHelper.Get(EntityKey) as PropertyInfo[];

            if (propertys == null)
            {
                propertys = EntityType.GetProperties();

                CacheHelper.Insert(EntityKey, propertys, CacheHelper.DayFactor * 5);
            }
            return propertys;
        }

        public static object GetDelegateMethod(string AppName, Type DelegateType)
        {
            object ReturnObj = CacheHelper.Get(AppName);
            if (ReturnObj == null)
            {
                //做容错处理以下
                string TempStr = ConfigurationManager.AppSettings.Get(AppName);
                if (!string.IsNullOrEmpty(TempStr))
                {
                    string[] Str = TempStr.Split(',');
                    if (Str.Length == 3)
                    {

                        Object theObj = Assembly.Load(Str[0]).CreateInstance(Str[0] + "." + Str[1]);
                        ReturnObj = (object)Delegate.CreateDelegate(DelegateType, theObj, theObj.GetType().GetMethod(Str[2]));
                        CacheHelper.Insert(AppName, ReturnObj, CacheHelper.DayFactor * 5);
                    }
                }
            }
            return ReturnObj;
        }
        #endregion
    }
}
