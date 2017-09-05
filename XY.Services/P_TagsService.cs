using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class P_TagsService : BaseService<P_Tags>
    {
        private static P_TagsService _instance;
        public static P_TagsService instance()
        {
            if (_instance == null)
            {
                _instance = new P_TagsService();
            }
            return _instance;
        }
        public int Insert(P_Tags entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<P_Tags>(entity);
            });
            return result;
        }
        public int Update(P_Tags entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<P_Tags>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<P_Tags>(m => m.ID == id);
            });
            return result;
        }
        public P_Tags Single(Guid id)
        {
            P_Tags result = new P_Tags();
            _db.Execute(() =>
            {
                result = _db.Single<P_Tags>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<P_Tags> GetEnum()
        {
            IEnumerable<P_Tags> result = new List<P_Tags>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_Tags>();
            });
            return result;
        }
    }
}
