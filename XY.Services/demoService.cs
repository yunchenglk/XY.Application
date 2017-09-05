using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class demoService : BaseService<Demo>
    {
        private static demoService _instance;
        public static demoService instance()
        {
            if (_instance == null)
            {
                _instance = new demoService();
            }
            return _instance;
        }
        public int Insert(Demo entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Demo>(entity);
            });
            return result;
        }
        public int Update(Demo entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Demo>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Demo>(m => m.ID == id);
            });
            return result;
        }
        public Demo Single(Guid id)
        {
            Demo result = new Demo();
            _db.Execute(() =>
            {
                result = _db.Single<Demo>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Demo> GetEnum()
        {
            IEnumerable<Demo> result = new List<Demo>();
            _db.Execute(() =>
            {
                result = _db.GetList<Demo>();
            });
            return result;
        }
    }
}
