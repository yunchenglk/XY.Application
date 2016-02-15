using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_requestRuleService : BaseService<wx_requestRule>
    {
        private static wx_requestRuleService _instance;
        public static wx_requestRuleService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_requestRuleService();
            }
            return _instance;
        }
        public int Insert(wx_requestRule entity)
        {
            int result = 0;
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_requestRule>(entity);
            });
            return result;
        }
        public int Update(wx_requestRule entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_requestRule>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_requestRule>(m => m.ID == id);
            });
            return result;
        }
        public wx_requestRule Single(Guid id)
        {
            wx_requestRule result = new wx_requestRule();
            _db.Execute(() =>
            {
                result = _db.Single<wx_requestRule>(m => m.ID == id);
            });
            return result;
        }
        public wx_requestRule SingleByCompanyID(Guid companyid)
        {
            wx_requestRule result = new wx_requestRule();
            _db.Execute(() =>
            {
                result = _db.Single<wx_requestRule>(m => m.cID == companyid);
            });
            return result;
        }
        public IEnumerable<wx_requestRule> GetByResponseType(int type, Guid companyid)
        {
            IEnumerable<wx_requestRule> result = new List<wx_requestRule>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_requestRule>(m => m.responseType == type && m.cID == companyid);
            });
            return result;
        }
        public wx_requestRule GetByRequestType(int type, Guid companyid)
        {
            wx_requestRule result = new wx_requestRule();
            _db.Execute(() =>
            {
                result = _db.Single<wx_requestRule>(m => m.reqestType == type && m.cID == companyid);
            });
            return result;
        }

        /// <summary>
        /// 根据请求类型和关键字匹配回复
        /// </summary>
        /// <param name="type">请求类型
        /// 0为默认回复,1文字，2图片，3语音，4链接，5地理位置，6关注，7取消关注，8扫描带参数二维码事件，9上报地理位置事件，10自定义菜单事件
        /// </param>
        /// <param name="companyid"></param>
        /// <param name="reqKeywords">关键字</param>
        /// <returns></returns>
        public wx_requestRule GetByreqestType_Key(int type, Guid companyid, string reqKeywords)
        {
            if (string.IsNullOrEmpty(reqKeywords))
                return null;
            wx_requestRule result = new wx_requestRule();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT TOP 1 * FROM [WX_REQUESTRULE] ");
            sql.Append(string.Format(" WHERE CID = '{0}' ", companyid));
            sql.Append(string.Format(" AND REQESTTYPE = {0}", type));
            sql.Append(string.Format(" AND ( REQKEYWORDS LIKE '{0}|%'", reqKeywords));
            sql.Append(string.Format(" OR REQKEYWORDS LIKE '%|{0}'", reqKeywords));
            sql.Append(string.Format(" OR REQKEYWORDS LIKE '%|{0}|%'", reqKeywords));
            sql.Append(string.Format(" OR REQKEYWORDS LIKE '%{0}%' )", reqKeywords));
            _db.Execute(() =>
            {
                result = _DbHelper.GetDataList(sql.ToString(), CommandType.Text, _DbHelper.GetDataReader<wx_requestRule>, null).FirstOrDefault();
            });
            return result;
        }



    }
}
