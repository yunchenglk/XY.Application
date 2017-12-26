using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.DataAccess;
using YunChengLK.Framework.Data;

namespace XY.Services
{
    public class BaseService<T> where T : IEntity, new()
    {
        public IDatabase _db;
        public DbHelper _DbHelper;
        private Type typeT = typeof(T);
        public BaseService()
         {
            if (System.Configuration.ConfigurationManager.AppSettings["DB_location"].ToUpper() == "TRUE")
            {
                _DbHelper = new DbHelper("Test");
                _db = Database.Test;
            }
            else
            {
                _DbHelper = new DbHelper("ApplicationDB_ConnectionString");
                _db = Database.ApplicationDB;
            }

        }
        //分页查询
        public virtual IEnumerable<T> GetPageByDynamic(int pageindex, int pagesize, out int totalcount, string sortname, string sortorder, string wheres)
        {
            IEnumerable<T> datalist = new List<T>();
            StringBuilder sqllist = new StringBuilder();
            sqllist.Append("SELECT * FROM (");
            StringBuilder sql = new StringBuilder();

            sql.Append(string.Format("SELECT ROW_NUMBER() OVER ( ORDER BY CreateTime DESC ) AS ROW,*  FROM [{0}]", typeT.Name));

            sql.Append(" WHERE 1 = 1 ");
            #region WHERE
            string[] Where = wheres.Split('#');
            foreach (string item in Where)
            {
                if (string.IsNullOrEmpty(item)) continue;
                string[] titem = item.Split('|');
                switch (titem[1])
                {
                    case "like"://相似
                        //WHERE LoginName LIKE '%a%'
                        sql.Append(string.Format(" AND {0} LIKE '%{1}%'", titem[0], titem[2]));
                        //strwhere += titem[0] + " like '%'||:" + titem[0] + "||'%'" + strand;
                        break;
                    case "equal"://相等
                        sql.Append(string.Format(" AND {0} = '{1}'", titem[0], titem[2]));
                        //strwhere += titem[0] + " = :" + titem[0] + " " + strand;
                        break;
                    case "notequal"://不相等
                        sql.Append(string.Format(" AND {0} <> '{1}'", titem[0], titem[2]));
                        //strwhere += titem[0] + " <> :" + titem[0] + " " + strand;
                        break;
                    case "startwith"://以..开始
                        sql.Append(string.Format(" AND {0} LIKE '{1}%'", titem[0], titem[2]));
                        // strwhere += titem[0] + " like ''||:" + titem[0] + "||'%' " + strand;
                        break;
                    case "endwith"://以..结束
                        sql.Append(string.Format(" AND {0} LIKE '%{1}'", titem[0], titem[2]));
                        // strwhere += titem[0] + " like '%'||:" + titem[0] + "||'' " + strand;
                        break;
                    case "greater"://大于
                        sql.Append(string.Format(" AND {0} > '{1}'", titem[0], titem[2]));
                        // strwhere += titem[0] + " > ':" + titem[0] + "' " + strand;
                        break;
                    case "greaterorequal"://大于或等于
                        sql.Append(string.Format(" AND {0} >= '{1}'", titem[0], titem[2]));
                        //strwhere += titem[0] + " >= ':" + titem[0] + "' " + strand;
                        break;
                    case "less"://小于
                        sql.Append(string.Format(" AND {0} < '{1}'", titem[0], titem[2]));
                        //strwhere += titem[0] + " < :" + titem[0] + " " + strand;
                        break;
                    case "lessorequal"://小于或等于
                        sql.Append(string.Format(" AND {0} <= '{1}'", titem[0], titem[2]));
                        //strwhere += titem[0] + " <= :" + titem[0] + " " + strand;
                        break;
                    case "in"://包括在...
                    case "not in"://不包括...
                        sql.Append(string.Format(" AND {0} {1} ({2})", titem[0], titem[1], titem[2]));
                        break;
                    default:
                        break;
                }
            }
            #endregion

            sqllist.Append(sql.ToString());
            sqllist.Append(") TEMP WHERE ROW BETWEEN ( " + pageindex + " - 1 ) * " + pagesize + " + 1 AND " + pageindex + " * " + pagesize + "");
            if (!string.IsNullOrEmpty(sortname) && !string.IsNullOrEmpty(sortorder))
            {
                sql.Append(string.Format(" ORDER BY {0} {1} ", sortname, sortorder));
                sqllist.Append(string.Format(" ORDER BY {0} {1} ", sortname, sortorder));
            }
            int listcount = 0;
            _db.Execute(() =>
            {
                listcount = _DbHelper.GetDataList(sql.ToString(), CommandType.Text, _DbHelper.GetDataReader<T>, null).Count();
                datalist = _DbHelper.GetDataList(sqllist.ToString(), CommandType.Text, _DbHelper.GetDataReader<T>, null);
            });
            totalcount = listcount;
            return datalist;
        }
        //修改字段
        public virtual bool ModifColumn(Dictionary<string, object> dic, string id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("UPDATE [{0}] SET ", typeT.Name));
            foreach (KeyValuePair<string, object> item in dic)
            {
                sql.Append(string.Format("{0} = '{1}',", item.Key, item.Value));
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(string.Format(" WHERE ID='{0}'", id));
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.ExecuteNonQuery(sql.ToString());
            });
            return result == 1;
        }
        public virtual IEnumerable<T> GetEnumerable(int count = 0)
        {
            IEnumerable<T> result = new List<T>();
            StringBuilder sql = new StringBuilder();
            if (count > 0)
                sql.Append(string.Format("SELECT TOP {0} * FROM [{1}]", count, typeT.Name));
            else
                sql.Append(string.Format("SELECT * FROM [{0}]", typeT.Name));
            _db.Execute(() =>
            {
                result = _DbHelper.GetDataList(sql.ToString(), CommandType.Text, _DbHelper.GetDataReader<T>, null);
            });
            return result;
        }
        public virtual IEnumerable<T> GetEnumerable(string where, int count = 0)
        {
            IEnumerable<T> result = new List<T>();
            StringBuilder sql = new StringBuilder();
            if (count > 0)
                sql.Append(string.Format("SELECT TOP {0} * FROM [{1}] WHERE {2}", count, typeT.Name, where));
            else
                sql.Append(string.Format("SELECT * FROM [{0}] WHERE {1}", typeT.Name, where));
            _db.Execute(() =>
            {
                result = _DbHelper.GetDataList(sql.ToString(), CommandType.Text, _DbHelper.GetDataReader<T>, null);
            });
            return result;
        }
        public virtual IEnumerable<T> GetEnumerableByID(Guid id)
        {
            IEnumerable<T> result = new List<T>();
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("SELECT * FROM [{0}] WHERE ID = '{1}'", typeT.Name, id));
            _db.Execute(() =>
            {
                result = _DbHelper.GetDataList(sql.ToString(), CommandType.Text, _DbHelper.GetDataReader<T>, null);
            });
            return result;
        }
    }
}
