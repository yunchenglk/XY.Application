using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Product_PriceService : BaseService<Product_Price>
    {
        private static Product_PriceService _instance;
        public static Product_PriceService instance()
        {
            if (_instance == null)
            {
                _instance = new Product_PriceService();
            }
            return _instance;
        }
        public int Insert(Product_Price entity)
        {
            int result = 0;
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Product_Price>(entity);
            });
            return result;
        }
        public int Update(Product_Price entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Product_Price>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Product_Price>(m => m.ID == id);
            });
            return result;
        }
        public int DeleteByProductID(Guid Pid)
        {
            if (GetEnumByProductID(Pid).Count() == 0)
                return 1;
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Product_Price>(m => m.ProductID == Pid);
            });
            return result;
        }
        public Product_Price Single(Guid id)
        {
            Product_Price result = new Product_Price();
            _db.Execute(() =>
            {
                result = _db.Single<Product_Price>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Product_Price> GetEnum()
        {
            IEnumerable<Product_Price> result = new List<Product_Price>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product_Price>();
            });
            return result;
        }
        public IEnumerable<Product_Price> GetEnumByProductID(Guid pid)
        {
            IEnumerable<Product_Price> result = new List<Product_Price>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product_Price>(m => m.ProductID == pid);
            });
            return result;
        }
        public Product_Price GetEnumByKVP(Guid pid, Guid key, Guid val)
        {
            Product_Price result = new Product_Price();
            _db.Execute(() =>
            {
                result = _db.Single<Product_Price>(m => m.ProductID == pid && m.Att_Key == key && m.Att_Val == val);
            });
            return result;
        }

    }
}
