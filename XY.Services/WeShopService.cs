using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WeShopService : BaseService<WeShop>
    {
        private static WeShopService _instance;
        public static WeShopService instance()
        {
            if (_instance == null)
            {
                _instance = new WeShopService();
            }
            return _instance;
        }
        public int Insert(WeShop entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<WeShop>(entity);
            });
            return result;
        }
        public int Update(WeShop entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WeShop>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WeShop>(m => m.ID == id);
            });
            return result;
        }
        public WeShop Single(Guid id)
        {
            WeShop result = new WeShop();
            _db.Execute(() =>
            {
                result = _db.Single<WeShop>(m => m.ID == id);
            });

            return result;
        }
        public IEnumerable<WeShop> GetEnum()
        {
            IEnumerable<WeShop> result = new List<WeShop>();
            _db.Execute(() =>
            {
                result = _db.GetList<WeShop>();
            });
            return result;
        }
        public WeShop GetEneityByCompanyID(Guid companyid)
        {
            WeShop result = new WeShop();
            _db.Execute(() =>
            {
                result = _db.Single<WeShop>(m => m.CompanyID == companyid);
            });

            return result;
        }
    }
}
