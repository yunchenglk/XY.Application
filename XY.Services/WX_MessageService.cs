using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WX_MessageService : BaseService<WX_Message>
    {
        private static WX_MessageService _instance;
        public static WX_MessageService instance()
        {
            if (_instance == null)
            {
                _instance = new WX_MessageService();
            }
            return _instance;
        }
        public int Insert(WX_Message entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Insert<WX_Message>(entity);
            });
            return result;
        }
        public int Update(WX_Message entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WX_Message>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_Message>(m => m.ID == id);
            });
            return result;
        }
        public WX_Message Single(Guid id)
        {
            WX_Message result = new WX_Message();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Message>(m => m.ID == id);
            });
            if (result != null)
                result.Groups = WX_MessageGroupService.instance().GetEnumByMessID(result.ID).OrderBy(m => m.Short);
            return result;
        }
        public IEnumerable<WX_Message> GetEnum()
        {
            IEnumerable<WX_Message> result = new List<WX_Message>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Message>();
            });
            return result;
        }
        public IEnumerable<WX_Message> GetEnumByCID(Guid cid)
        {
            IEnumerable<WX_Message> result = new List<WX_Message>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Message>(m => m.CompanyID == cid);
            });
            result.Each(m =>
            {
                m.Groups = WX_MessageGroupService.instance().GetEnumByMessID(m.ID).OrderBy(n => n.Short);
            });

            return result;
        }
    }
}
