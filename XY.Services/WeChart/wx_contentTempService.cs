using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_contentTempService : BaseService<wx_contentTemp>
    {
        private static wx_contentTempService _instance;
        public static wx_contentTempService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_contentTempService();
            }
            return _instance;
        }
        public int Insert(wx_contentTemp entity)
        {
            int result = 0;
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_contentTemp>(entity);
            });
            return result;
        }
        public int Update(wx_contentTemp entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_contentTemp>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_contentTemp>(m => m.ID == id);
            });
            return result;
        }
        public wx_contentTemp Single(Guid id)
        {
            wx_contentTemp result = new wx_contentTemp();
            _db.Execute(() =>
            {
                result = _db.Single<wx_contentTemp>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<wx_contentTemp> GetEnum()
        {
            IEnumerable<wx_contentTemp> result = new List<wx_contentTemp>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_contentTemp>();
            });
            return result;
        }
        public IEnumerable<wx_contentTemp> GetEnumByID(Guid id)
        {
            IEnumerable<wx_contentTemp> result = new List<wx_contentTemp>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_contentTemp>(m => m.ID == id);
            });
            return result;
        }


    }
}
