using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class UserService : BaseService<USER>
    {
        private static UserService _instance;
        public static UserService instance()
        {
            if (_instance == null)
                _instance = new UserService();
            return _instance;
        }

        public int Insert(USER entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.Last_Login_Time = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<USER>(entity);
            });
            return result;
        }
        public int Update(USER entity)
        {
            int result = 0;
            entity.Last_Login_Time = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Update<USER>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;

            _db.Execute(() =>
            {
                result = _db.Delete<USER>(m => m.ID == id);
            });
            return result;
        }
        public USER Single(Guid id)
        {
            USER result = new USER();
            _db.Execute(() =>
            {
                result = _db.Single<USER>(m => m.ID == id);
            });
            return result;
        }
        public Boolean CheckUser(Guid id, string LoginName)
        {
            Boolean result = false;
            USER entity = null;
            _db.Execute(() =>
            {
                entity = _db.GetList<USER>(m => m.LoginName == LoginName).FirstOrDefault();
            });
            if (id.Equals(Guid.Empty))
                result = entity == null;
            else
                result = id.Equals(entity.ID);
            return result;
        }
        public IEnumerable<USER> GetEnumByLoginName(string loginName)
        {
            IEnumerable<USER> result = new List<USER>();
            _db.Execute(() =>
            {
                result = _db.GetList<USER>(m => m.LoginName == loginName);
            });
            return result;
        }
        public IEnumerable<USER> GetEnumByID(Guid id)
        {
            IEnumerable<USER> result = new List<USER>();
            _db.Execute(() =>
            {
                result = _db.GetList<USER>(m => m.ID == id);
            });
            return result;
        }
        public USER GetEntityByID(Guid id)
        {
            USER result = new USER();
            _db.Execute(() =>
            {
                result = _db.Single<USER>(m => m.ID == id);
            });
            return result;
        }
        public USER GetDataByName(string loginName)
        {
            USER result = new USER();
            _db.Execute(() =>
            {
                result = _db.GetList<USER>(m => m.LoginName == loginName).FirstOrDefault();
            });
            return result;
        }
        public Hashtable Login(string uname, string pwd)
        {
            Hashtable hash = new Hashtable();
            USER entity = this.GetEnumByLoginName(uname).FirstOrDefault();
            if (entity == null)
            {
                hash["status"] = false;
                hash["error"] = "账号密码错误";
                return hash;
            }
            if (!pwd.Equals(entity.LoginPwd))
            {
                hash["status"] = false;
                hash["error"] = "账号密码错误";
                return hash;
            }
            if (entity.DR)
            {
                hash["status"] = false;
                hash["error"] = "账号被禁用";
                return hash;
            }
            hash["status"] = true;
            hash["uid"] = entity.ID;
            return hash;
        }
    }
}
