using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    /// <summary>
    /// 品牌表
    /// </summary>
    public class P_BrandCategoriesService : BaseService<P_BrandCategories>
    {
        private static P_BrandCategoriesService _instance;
        public static P_BrandCategoriesService instance()
        {
            if (_instance == null)
            {
                _instance = new P_BrandCategoriesService();
            }
            return _instance;
        }
        public int Insert(P_BrandCategories entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<P_BrandCategories>(entity);
            });
            return result;
        }
        public int Update(P_BrandCategories entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<P_BrandCategories>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<P_BrandCategories>(m => m.ID == id);
            });
            return result;
        }
        public P_BrandCategories Single(Guid id)
        {
            P_BrandCategories result = new P_BrandCategories();
            _db.Execute(() =>
            {
                result = _db.Single<P_BrandCategories>(m => m.ID == id);
            }); 
            return result;
        }
        public IEnumerable<P_BrandCategories> GetEnum()
        {
            IEnumerable<P_BrandCategories> result = new List<P_BrandCategories>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_BrandCategories>();
            });
            return result;
        } 
    }
}
