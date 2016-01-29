using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Messages_ReplyService : BaseService<Messages_Reply>
    {
        private static Messages_ReplyService _instance;
        public static Messages_ReplyService instance()
        {
            if (_instance == null)
            {
                _instance = new Messages_ReplyService();
            }
            return _instance;
        }
        public int Insert(Messages_Reply entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Messages_Reply>(entity);
            });
            return result;
        }
        public int Update(Messages_Reply entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Messages_Reply>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Messages_Reply>(m => m.ID == id);
            });
            return result;
        }
        public Messages_Reply Single(Guid id)
        {
            Messages_Reply result = new Messages_Reply();
            _db.Execute(() =>
            {
                result = _db.Single<Messages_Reply>(m => m.ID == id);
            });
            result.ChildItem = GetChild(result.ID);
            return result;
        }
        public IEnumerable<Messages_Reply> GetEnum()
        {
            IEnumerable<Messages_Reply> result = new List<Messages_Reply>();
            _db.Execute(() =>
            {
                result = _db.GetList<Messages_Reply>();
            });
            return result;
        }
        public IEnumerable<Messages_Reply> GetTop(Guid mid)
        {
            IEnumerable<Messages_Reply> result = new List<Messages_Reply>();
            _db.Execute(() =>
            {
                result = _db.GetList<Messages_Reply>(m => m.MessageID == mid && m.DR == false && m.ParentID == Guid.Empty);
            });
            result.Each(m =>
            {
                m.ChildItem = this.GetChild(m.ID);
            });
            return result;
        }
        public IEnumerable<Messages_Reply> GetChild(Guid id)
        {
            IEnumerable<Messages_Reply> result = new List<Messages_Reply>();
            _db.Execute(() =>
            {
                result = _db.GetList<Messages_Reply>(m => m.DR == false && m.ParentID == id);
            });
            result.Each(m =>
            {
                m.ChildItem = this.GetChild(m.ID);
                if (m.IsChild)
                {
                    m.ChildItem = this.GetChild(m.ID);
                }
            });
            return result;
        }
    }
}
