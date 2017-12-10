using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_openInfoService : BaseService<wx_openInfo>
    {
        private static wx_openInfoService _instance;
        public static wx_openInfoService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_openInfoService();
            }
            return _instance;
        }
        public int Insert(wx_openInfo entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_openInfo>(entity);
            });
            return result;
        }
        public int Update(wx_openInfo entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_openInfo>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_openInfo>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<wx_openInfo> GetEnum()
        {
            IEnumerable<wx_openInfo> result = new List<wx_openInfo>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_openInfo>();
            });
            return result;
        }
        public wx_openInfo Single(Guid id)
        {
            wx_openInfo result = new wx_openInfo();
            _db.Execute(() =>
            {
                result = _db.Single<wx_openInfo>(m => m.ID == id);
            });
            return result;
        }
    }
}
