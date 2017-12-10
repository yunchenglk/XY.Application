using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_requestRuleContentService : BaseService<wx_requestRuleContent>
    {
        private static wx_requestRuleContentService _instance;
        public static wx_requestRuleContentService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_requestRuleContentService();
            }
            return _instance;
        }
        public int Insert(wx_requestRuleContent entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_requestRuleContent>(entity);
            });
            return result;
        }
        public int Update(wx_requestRuleContent entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_requestRuleContent>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_requestRuleContent>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<wx_requestRuleContent> GetEnum()
        {
            IEnumerable<wx_requestRuleContent> result = new List<wx_requestRuleContent>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_requestRuleContent>();
            });
            return result;
        }
        public int DeleteByRuleID(Guid ruleid)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_requestRuleContent>(m => m.RuleID == ruleid);
            });
            return result;
        }
        public wx_requestRuleContent Single(Guid id)
        {
            wx_requestRuleContent result = new wx_requestRuleContent();
            _db.Execute(() =>
            {
                result = _db.Single<wx_requestRuleContent>(m => m.ID == id);
            });
            return result;
        }
        public wx_requestRuleContent SingleByRuleID(Guid ruleid)
        {
            wx_requestRuleContent result = new wx_requestRuleContent();
            _db.Execute(() =>
            {
                result = _db.Single<wx_requestRuleContent>(m => m.RuleID == ruleid);
            });
            return result;
        }
        public IEnumerable<wx_requestRuleContent> GetByRuleID(Guid ruleid)
        {
            IEnumerable<wx_requestRuleContent> result = new List<wx_requestRuleContent>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_requestRuleContent>(m => m.RuleID == ruleid);
            });

            return result;
        }
    }
}
