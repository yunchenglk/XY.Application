using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Role_PK_Authority_s_Service : BaseService<Role_PK_Authority_s>
    {
        private static Role_PK_Authority_s_Service _instance;
        public static Role_PK_Authority_s_Service instance()
        {
            if (_instance == null)
            {
                _instance = new Role_PK_Authority_s_Service();
            }
            return _instance;
        }
        public int Insert(Role_PK_Authority_s entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            _db.Execute(() =>
            {
                result = _db.Insert<Role_PK_Authority_s>(entity);
            });
            return result;
        }
        public int DeleteByRoleID(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Role_PK_Authority_s>(m => m.Role_ID == id);
            });
            return result;
        }
        public IEnumerable<Role_PK_Authority_s> GetEnum()
        {
            IEnumerable<Role_PK_Authority_s> result = new List<Role_PK_Authority_s>();
            _db.Execute(() =>
            {
                result = _db.GetList<Role_PK_Authority_s>();
            });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rid">RoleID</param>
        /// <returns></returns>
        public IEnumerable<Guid> GetAuthIDS_ByRoleID(Guid Rid)
        {
            IEnumerable<Guid> result = new List<Guid>();
            _db.Execute(() =>
            {
                result = _db.GetList<Role_PK_Authority_s>(m => m.Role_ID == Rid).Select(m => m.Authority_ID);
            });
            return result;
        }
    }
}
