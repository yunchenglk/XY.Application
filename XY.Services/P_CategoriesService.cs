using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class P_CategoriesService : BaseService<P_Categories>
    {
        private static P_CategoriesService _instance;
        public static P_CategoriesService instance()
        {
            if (_instance == null)
            {
                _instance = new P_CategoriesService();
            }
            return _instance;
        }
        public int Insert(P_Categories entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<P_Categories>(entity);
            });
            return result;
        }
        public int Update(P_Categories entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<P_Categories>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<P_Categories>(m => m.ID == id);
            });
            return result;
        }
        public P_Categories Single(Guid id)
        {
            P_Categories result = new P_Categories();
            _db.Execute(() =>
            {
                result = _db.Single<P_Categories>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<P_Categories> GetEnum()
        {
            IEnumerable<P_Categories> result = new List<P_Categories>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_Categories>();
            });
            return result;
        }
        public IEnumerable<P_Categories> GetAllTop()
        {
            IEnumerable<P_Categories> result = new List<P_Categories>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_Categories>(m => m.ParentCategoryId == Guid.Empty);
            });
            return result.Where(m => !m.DR).ToList();
        }
    }
}
