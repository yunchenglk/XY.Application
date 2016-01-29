using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class RoleService : BaseService<Role>
    {
        private static RoleService _instance;
        public static RoleService instance()
        {
            if (_instance == null)
            {
                _instance = new RoleService();
            }
            return _instance;
        }
        public int Insert(Role entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;

            _db.Execute(() =>
            {
                result = _db.Insert<Role>(entity);
                foreach (var item in entity.Item_Authoritys)
                {
                    result = Role_PK_Authority_s_Service.instance().Insert(
                        new Role_PK_Authority_s() { Authority_ID = item.ID, Role_ID = entity.ID }
                        );//.Insert<Role>(entity);
                }
            });

            return result;
        }
        public int Update(Role entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Role>(entity, m => m.ID == entity.ID);
                Role_PK_Authority_s_Service.instance().DeleteByRoleID(entity.ID);
                foreach (var item in entity.Item_Authoritys)
                {
                    result = Role_PK_Authority_s_Service.instance().Insert(
                        new Role_PK_Authority_s() { Authority_ID = item.ID, Role_ID = entity.ID }
                        );//.Insert<Role>(entity);
                }
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Role>(m => m.ID == id);
            });
            return result;
        }
        public Role Single(Guid id)
        {
            Role result = new Role();
            _db.Execute(() =>
            {
                result = _db.Single<Role>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Role> GetEnum()
        {
            IEnumerable<Role> result = new List<Role>();
            _db.Execute(() =>
            {
                result = _db.GetList<Role>();
            });
            return result;
        }
        public IEnumerable<Role> GetEnumByID(Guid id)
        {
            IEnumerable<Role> result = new List<Role>();
            _db.Execute(() =>
            {
                result = _db.GetList<Role>(m => m.ID == id);
            });
            result.Each(m => m.Item_Authoritys = AuthorityService.instance().GetAuthorityListByRole(m.ID));
            return result;
        }
        public IEnumerable<Role> GetEnumByUID(Guid uid)
        {
            List<Role> roles = new List<Role>();
            User_PK_Role_s_Service.instance().GetEnumByUID(uid).Each(m =>
            {
                roles.Add(this.Single(m.Role_ID));
            });
            return roles;
        }

    }
}
