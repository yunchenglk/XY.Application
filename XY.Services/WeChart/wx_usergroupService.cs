using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_usergroupService : BaseService<wx_usergroup>
    {
        private static wx_usergroupService _instance;
        public static wx_usergroupService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_usergroupService();
            }
            return _instance;
        }
        public int Insert(wx_usergroup entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_usergroup>(entity);
            });
            return result;
        }
        public int Update(wx_usergroup entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_usergroup>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_usergroup>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<wx_usergroup> GetEnum()
        {
            IEnumerable<wx_usergroup> result = new List<wx_usergroup>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_usergroup>();
            });
            return result;
        }
        public wx_usergroup Single(Guid id)
        {
            wx_usergroup result = new wx_usergroup();
            _db.Execute(() =>
            {
                result = _db.Single<wx_usergroup>(m => m.ID == id);
            });
            return result;
        }
        public wx_usergroup Single(int gid, Guid companyid)
        {
            wx_usergroup result = new wx_usergroup();
            _db.Execute(() =>
            {
                result = _db.Single<wx_usergroup>(m => m.gid == gid && m.cID == companyid);
            });
            return result;
        }
        public IEnumerable<wx_usergroup> GetAll(Guid companyid)
        {
            IEnumerable<wx_usergroup> result = new List<wx_usergroup>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_usergroup>(m => m.cID == companyid);
            });
            return result;

        }
        public Boolean CheckName(Guid id, Guid companyid, string name)
        {
            Boolean result = false;
            wx_usergroup entity = null;
            _db.Execute(() =>
            {
                entity = _db.GetList<wx_usergroup>(m => m.cID == companyid && m.gname == name).FirstOrDefault();
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
