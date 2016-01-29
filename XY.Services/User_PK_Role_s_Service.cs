using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class User_PK_Role_s_Service : BaseService<User_PK_Role_s>
    {
        private static User_PK_Role_s_Service _instance;
        public static User_PK_Role_s_Service instance()
        {
            if (_instance == null)
                _instance = new User_PK_Role_s_Service();
            return _instance;
        }
        public int Insert(User_PK_Role_s entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            _db.Execute(() =>
            {
                result = _db.Insert<User_PK_Role_s>(entity);
            });
            return result;
        }
        public int Delete(Guid uid)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<User_PK_Role_s>(m => m.User_ID == uid);
            });
            return result;
        }
        public IEnumerable<User_PK_Role_s> GetEnumByUID(Guid uid)
        {
            IEnumerable<User_PK_Role_s> result = new List<User_PK_Role_s>();
            _db.Execute(() =>
            {
                result = _db.GetList<User_PK_Role_s>(m => m.User_ID == uid);
            });
            return result;
        }
    }
}
