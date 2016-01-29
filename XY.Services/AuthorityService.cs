using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class AuthorityService : BaseService<Authority>
    {
        private static AuthorityService _instance;
        public static AuthorityService instance()
        {
            if (_instance == null)
            {
                _instance = new AuthorityService();
            }
            return _instance;
        }
        public int Insert(Authority entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Authority>(entity);
            });
            return result;
        }
        public int Update(Authority entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Authority>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Authority>(m => m.ID == id);
            });
            return result;
        }
        public Authority Single(Guid id)
        {
            Authority result = new Authority();
            _db.Execute(() =>
            {
                result = _db.Single<Authority>(m => m.ID == id);
            });
            if (result.PID != Guid.Empty)
            {
                result.ParentAuth = this.Single(result.PID);
            }
            return result;
        }
        public IEnumerable<Authority> GetEnum()
        {
            IEnumerable<Authority> result = new List<Authority>();
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>();
            });
            return result;
        }
        public IEnumerable<Authority> GetEnumByID(Guid id)
        {
            IEnumerable<Authority> result = new List<Authority>();
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Authority> GetEnumByPID(Guid PID)
        {
            IEnumerable<Authority> result = new List<Authority>();
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>(m => m.PID == PID);
            });
            result.Each(m => m.PIDName = this.GetNameByPID(m.PID));
            return result;
        }
        public IEnumerable<Authority> GetAllTop1()
        {
            IEnumerable<Authority> result = new List<Authority>();
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>(m => m.PID == Guid.Empty);
            });
            return result.Where(m => !m.DR).ToList();
        }
        public IEnumerable<Authority> GetChilds(Guid id)
        {
            IEnumerable<Authority> result = new List<Authority>();
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>(m => m.PID == id);
            });
            //result = result.Where(m => !m.DR).ToList();
            result.Each(m => m.PIDName = this.GetNameByPID(m.PID));
            //result.Each(m =>
            //{
            //    m.Childs = this.GetEnumByPID(m.ID).OrderBy(n => n.Sort);
            //});
            return result;
        }
        public IEnumerable<Authority> GetItems()
        {
            IEnumerable<Authority> result = new List<Authority>();
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>(m => m.PID == Guid.Empty);
            });
            result = result.Where(m => !m.DR).ToList();
            result.Each(m => m.PIDName = this.GetNameByPID(m.PID));
            result.Each(m =>
            {
                m.Childs = this.GetEnumByPID(m.ID).OrderBy(n => n.Sort);
            });
            return result;
        }
        public string GetNameByPID(Guid pid)
        {
            if (pid.Equals(Guid.Empty))
                return "一级权限";
            string result = "";
            _db.Execute(() =>
            {
                result = _db.GetList<Authority>(m => m.ID == pid).FirstOrDefault().Name;
            });
            return result;
        }
        /// <summary>
        /// 根据角色ID获取所有的权限
        /// </summary>
        /// <param name="Rid"></param>
        /// <returns></returns>
        public IEnumerable<Authority> GetAuthorityListByRole(Guid Rid)
        {
            List<Authority> result = new List<Authority>();
            var authIDs = Role_PK_Authority_s_Service.instance().GetAuthIDS_ByRoleID(Rid);
            foreach (var item in authIDs)
            {
                result.Add(this.Single(item));
            }
            return result;
        }
    }
}
