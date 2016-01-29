using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Product_Att_ValService : BaseService<Product_Att_Val>
    {
        private static Product_Att_ValService _instance;
        public static Product_Att_ValService instance()
        {
            if (_instance == null)
                _instance = new Product_Att_ValService();
            return _instance;
        }
        public int Insert(Product_Att_Val entity)
        {
            int result = 0;
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Product_Att_Val>(entity);
            });
            return result;
        }
        public int Update(Product_Att_Val entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Product_Att_Val>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Product_Att_Val>(m => m.ID == id);
            });
            return result;
        }
        public int DeleteByProductID(Guid ProductID)
        {
            if (GetEnumByProductID(ProductID).Count() == 0)
            {
                return 1;
            }
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Product_Att_Val>(m => m.ProductID == ProductID);
            });
            return result;
        }
        public Product_Att_Val Single(Guid id)
        {
            Product_Att_Val result = new Product_Att_Val();
            _db.Execute(() =>
            {
                result = _db.Single<Product_Att_Val>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Product_Att_Val> GetEnum()
        {
            IEnumerable<Product_Att_Val> result = new List<Product_Att_Val>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product_Att_Val>();
            });
            return result;
        }
        public IEnumerable<Product_Att_Val> GetEnumByProductID(Guid pid)
        {
            IEnumerable<Product_Att_Val> result = new List<Product_Att_Val>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product_Att_Val>(m => m.ProductID == pid);
            });
            return result;
        }
    }
}
