using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Product_Att_KeyService : BaseService<Product_Att_Key>
    {
        private static Product_Att_KeyService _instance;
        public static Product_Att_KeyService instance()
        {
            if (_instance == null)
                _instance = new Product_Att_KeyService();
            return _instance;
        }
        public int Insert(Product_Att_Key entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Product_Att_Key>(entity);
            });
            return result;
        }
        public int Update(Product_Att_Key entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Product_Att_Key>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Product_Att_Key>(m => m.ID == id);
            });
            return result;
        }
        public Product_Att_Key Single(Guid id)
        {
            Product_Att_Key result = new Product_Att_Key();
            _db.Execute(() =>
            {
                result = _db.Single<Product_Att_Key>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Product_Att_Key> GetEnum()
        {
            IEnumerable<Product_Att_Key> result = new List<Product_Att_Key>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product_Att_Key>();
            });
            return result;
        }
        public IEnumerable<Product_Att_Key> GetEnumByCID(Guid companyID, bool? idDR = false)
        {
            IEnumerable<Product_Att_Key> result = new List<Product_Att_Key>();
            _db.Execute(() =>
            {
                result = _db.GetList<Product_Att_Key>().Where(m => m.CompanyID == companyID && m.DR == idDR);
            });
            return result;
        }
    }
}
