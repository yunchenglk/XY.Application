using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class ProductService : BaseService<Product>
    {
        private static ProductService _instance;
        public static ProductService instance()
        {
            if (_instance == null)
            {
                _instance = new ProductService();
            }
            return _instance;
        }
        public int Insert(Product entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Product>(entity);
            });
            return result;
        }
        public int Update(Product entity)
        {
            int result = 0;
            entity.ModifyTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Update<Product>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Product>(m => m.ID == id);
            });
            return result;
        }
        public Product Single(Guid id)
        {
            Product result = new Product();
            _db.Execute(() =>
            {
                result = _db.Single<Product>(m => m.ID == id);
            });
            result.Count++;
            this.Update(result);
            result.Attr = Product_AttService.GetAttsByPID(id);
            return result;
        }
        public IEnumerable<Product> GetEnum()
        {
            IEnumerable<Product> result = new List<Product>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product>();
            });
            return result;
        }
        public IEnumerable<Product> GetEnumByID(Guid id)
        {
            IEnumerable<Product> result = new List<Product>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Product> GetProductByCid(Guid cid)
        {
            IEnumerable<Product> result = new List<Product>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product>(m => m.ClassID == cid && m.DR == false).Where(m => m.IsAudit);
            });
            if (result.Count() > 0)
            {
                result.Each(m =>
                {
                    m.Attr = Product_AttService.GetAttsByPID(m.ID);
                });
            }
            return result;
        }
        public IEnumerable<Product> GetProductByCid_IsRecommend(Guid cid)
        {
            return this.GetProductByCid(cid).Where(m => m.IsRecommend);
        }
        public IEnumerable<Product> GetProductByCid_IsTop(Guid cid)
        {
            return this.GetProductByCid(cid).Where(m => m.IsTop);
        }
        public int GetNewsCountByCID(Guid cid)
        {

            int result = 0;
            _db.Execute(() =>
            {
                result = _db.GetList<News>(m => m.ClassID == cid).Count();
            });
            return result;
        }
    }
}
