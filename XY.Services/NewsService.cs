using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class NewsService : BaseService<News>
    {
        private static NewsService _instance;
        public static NewsService instance()
        {
            if (_instance == null)
            {
                _instance = new NewsService();
            }
            return _instance;
        }
        public int Insert(News entity)
        {
            int result = 0;
            if (entity.ID == Guid.Empty)
                entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<News>(entity);
            });
            return result;
        }
        public int Update(News entity)
        {
            entity.ModifyTime = DateTime.Now;
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<News>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<News>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<News> GetEnum()
        {
            IEnumerable<News> result = new List<News>();
            _db.Execute(() =>
            {
                result = _db.GetList<News>();
            });
            return result;
        }
        public IEnumerable<News> GetEnumByID(Guid id)
        {
            IEnumerable<News> result = new List<News>();
            _db.Execute(() =>
            {
                result = _db.GetList<News>(m => m.ID == id);
            });
            return result;
        }
        public News Single(Guid id)
        {
            News entity = new News();
            _db.Execute(() =>
            {
                entity = _db.Single<News>(m => m.ID == id);
            });
            if (entity != null)
            {
                entity.Count++;
                this.Update(entity); 
            }
            entity.ClassName = ClassService.instance().Single(entity.ClassID).Title;
            return entity;
        }
        public IEnumerable<News> GetNewsByCid(Guid cid)
        {
            IEnumerable<News> result = new List<News>();
            _db.Execute(() =>
            {
                result = _db.GetList<News>(m => m.ClassID == cid && m.DR == false).Where(m => m.IsAudit).OrderByDescending(m=>m.CreateTime);
            });
            return result;
        }

        public IEnumerable<News> GetNewsByCid_IsRecommend(Guid cid)
        {
            return this.GetNewsByCid(cid).Where(m => m.IsRecommend);
        }
        public IEnumerable<News> GetNewsByCid_IsTop(Guid cid)
        {
            return this.GetNewsByCid(cid).Where(m => m.IsTop);
        }
        public IEnumerable<News> GetNewsByCid_IsComm(Guid cid)
        {
            return this.GetNewsByCid(cid).Where(m => m.IsComm);
        }
        public IEnumerable<News> GetNewsByCid_IsVote(Guid cid)
        {
            return this.GetNewsByCid(cid).Where(m => m.IsVote);
        }



        public int GetNewsCountByCID(Guid cid)
        {

            int result = 0;
            _db.Execute(() =>
            {
                result = _db.GetList<News>(m => m.ClassID == cid).Count();
            });
            return result;
        }
    }
}
