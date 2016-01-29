using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WX_KeyWordService : BaseService<WX_KeyWord>
    {
        private static WX_KeyWordService _instance;
        public static WX_KeyWordService instance()
        {
            if (_instance == null)
            {
                _instance = new WX_KeyWordService();
            }
            return _instance;
        }
        public int Insert(WX_KeyWord entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<WX_KeyWord>(entity);
            });
            return result;
        }
        public int Update(WX_KeyWord entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WX_KeyWord>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_KeyWord>(m => m.ID == id);
            });
            return result;
        }
        public WX_KeyWord Single(Guid id)
        {
            WX_KeyWord result = new WX_KeyWord();
            _db.Execute(() =>
            {
                result = _db.Single<WX_KeyWord>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<WX_KeyWord> GetEnum()
        {
            IEnumerable<WX_KeyWord> result = new List<WX_KeyWord>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_KeyWord>();
            });
            return result;
        }
        public IEnumerable<WX_KeyWord> GetEnumByCompanyID(Guid CompanyID)
        {
            IEnumerable<WX_KeyWord> result = new List<WX_KeyWord>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_KeyWord>(m => m.CompanyID == CompanyID);
            });
            return result;
        }
        public WX_KeyWord SingleByCIDAndKey(Guid CompanyID, string key)
        {
            WX_KeyWord result = new WX_KeyWord();
            _db.Execute(() =>
            {
                result = _db.Single<WX_KeyWord>(m => m.CompanyID == CompanyID && m.KeyWords == key);
            });
            return result;
        }
        public Boolean CheckKey(Guid id, string ChecKey, Guid CompanyiD)
        {
            Boolean result = false;
            WX_KeyWord entity = null;

            _db.Execute(() =>
            {
                entity = _db.GetList<WX_KeyWord>(m => m.KeyWords == ChecKey && m.CompanyID == CompanyiD).FirstOrDefault();
            });
            if (id.Equals(Guid.Empty))
                result = entity == null;
            else
                result = id.Equals(entity.ID);
            return result;
        }
    }
}
