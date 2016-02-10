using System;
using System.Collections.Generic;
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
    }
}
