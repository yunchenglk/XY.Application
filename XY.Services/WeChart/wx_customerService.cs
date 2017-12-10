using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_customerService : BaseService<wx_customer>
    {
        private static wx_customerService _instance;
        public static wx_customerService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_customerService();
            }
            return _instance;
        }
        public int Insert(wx_customer entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_customer>(entity);
            });
            return result;
        }
        public int Update(wx_customer entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_customer>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_customer>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<wx_customer> GetEnum()
        {
            IEnumerable<wx_customer> result = new List<wx_customer>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_customer>();
            });
            return result;
        }
        public wx_customer Single(Guid id)
        {
            wx_customer result = new wx_customer();
            _db.Execute(() =>
            {
                result = _db.Single<wx_customer>(m => m.ID == id);
            });
            return result;
        }
        public Boolean CheckExists(Guid id, Guid companyid, string kf_account)
        {
            Boolean result = false;
            wx_customer entity = null;
            _db.Execute(() =>
            {
                entity = _db.GetList<wx_customer>(m => m.cID == companyid && m.kf_account == kf_account).FirstOrDefault();
            });
            if (id.Equals(Guid.Empty))
                result = entity == null;
            else if (entity == null)
                return true;
            else
                result = id.Equals(entity.ID);
            return result;
        }



    }
}
