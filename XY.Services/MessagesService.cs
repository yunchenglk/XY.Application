using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class MessagesService : BaseService<Messages>
    {
        private static MessagesService _instance;
        public static MessagesService instance()
        {
            if (_instance == null)
            {
                _instance = new MessagesService();
            }
            return _instance;
        }
        public int Insert(Messages entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Messages>(entity);
            });
            return result;
        }
        public int Update(Messages entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Messages>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Messages>(m => m.ID == id);
            });
            return result;
        }
        public Messages Single(Guid id)
        {
            Messages result = new Messages();
            _db.Execute(() =>
            {
                result = _db.Single<Messages>(m => m.ID == id);
            });
            result.ReplyItems = Messages_ReplyService.instance().GetTop(result.ID);

            result.Count++;
            this.Update(result);
            return result;
        }
        public IEnumerable<Messages> GetEnum(Guid cid, bool? isChild = true)
        {
            IEnumerable<Messages> result = new List<Messages>();
            _db.Execute(() =>
            {
                result = _db.GetList<Messages>(m => m.CompanyID == cid);
            });
            if (isChild.Value)
                result.Each(m =>
                {
                    m.ReplyItems = Messages_ReplyService.instance().GetTop(m.ID);
                });
            return result;
        }
    }
}
