using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class ClassService : BaseService<Class>
    {
        private static ClassService _instance;
        public static ClassService instance()
        {
            if (_instance == null)
            {
                _instance = new ClassService();
            }
            return _instance;
        }
        public int Insert(Class entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Class>(entity);
            });
            return result;
        }
        public int Update(Class entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Class>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Class>(m => m.ID == id);
            });
            return result;
        }
        public Class Single(Guid id)
        {
            Class result = new Class();
            _db.Execute(() =>
            {
                result = _db.Single<Class>(m => m.ID == id);
            });
            result.Count++;
            this.Update(result);
            return result;
        }
        public IEnumerable<Class> GetEnum()
        {
            IEnumerable<Class> result = new List<Class>();
            _db.Execute(() =>
            {
                result = _db.GetList<Class>();
            });
            return result;
        }
        public IEnumerable<Class> GetEnumByID(Guid id)
        {
            IEnumerable<Class> result = new List<Class>();
            _db.Execute(() =>
            {
                result = _db.GetList<Class>(m => m.ID == id);
            });
            return result;
        }
        /// <summary>
        /// 根据公司ID获取值
        /// </summary>
        /// <param name="cid">公司ID</param>
        /// <returns></returns>
        public IEnumerable<Class> GetEnumByCID(Guid cid)
        {
            IEnumerable<Class> result = new List<Class>();
            _db.Execute(() =>
            {
                result = _db.GetList<Class>(m => m.CompanyID == cid);
            });
            result.Each(m =>
            {

                m.CompanyName = CompanyService.instance().GetNameByID(m.CompanyID);
                m.Childs = ClassService.instance().GetChildByID(m.ID, m.CompanyID);
            });
            return result;
        }
        /// <summary>
        /// 获取子栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Class> GetChildByID(Guid id, Guid cid)
        {
            IEnumerable<Class> result = new List<Class>();
            _db.Execute(() =>
            {
                result = _db.GetList<Class>(m => m.ParentID == id && m.CompanyID == cid && m.DR == false).OrderBy(m => m.Sort);
            });
            result.Each(m =>
            {

                m.CompanyName = CompanyService.instance().GetNameByID(m.CompanyID);
                m.Childs = ClassService.instance().GetChildByID(m.ID, m.CompanyID);
            });
            return result;
        }

        public int GetEnumCountByCID(Guid cid)
        {

            int result = 0;
            _db.Execute(() =>
            {
                result = _db.GetList<Class>(m => m.CompanyID == cid).Count();
            });
            return result;
        }
        public void AddNum(Guid id)
        {
            Class n = this.Single(id);
            n.Count++;
            this.Update(n);
        }


    }
}
