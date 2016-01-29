using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WX_MessageGroupService : BaseService<WX_MessageGroup>
    {
        private static WX_MessageGroupService _instance;
        public static WX_MessageGroupService instance()
        {
            if (_instance == null)
            {
                _instance = new WX_MessageGroupService();
            }
            return _instance;
        }
        public int Insert(WX_MessageGroup entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<WX_MessageGroup>(entity);
            });
            return result;
        }
        public int Update(WX_MessageGroup entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WX_MessageGroup>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_MessageGroup>(m => m.ID == id);
            });
            return result;
        }
        public int DeleteByMID(Guid mid)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_MessageGroup>(m => m.MessageID == mid);
            });
            return result;
        }
        public WX_MessageGroup Single(Guid id)
        {
            WX_MessageGroup result = new WX_MessageGroup();
            _db.Execute(() =>
            {
                result = _db.Single<WX_MessageGroup>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<WX_MessageGroup> GetEnum()
        {
            IEnumerable<WX_MessageGroup> result = new List<WX_MessageGroup>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_MessageGroup>();
            });
            return result;
        }

        public IEnumerable<WX_MessageGroup> GetEnumByMessID(Guid messageID)
        {
            IEnumerable<WX_MessageGroup> result = new List<WX_MessageGroup>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_MessageGroup>(m => m.MessageID == messageID);
            });
            return result;
        }
    }
}
