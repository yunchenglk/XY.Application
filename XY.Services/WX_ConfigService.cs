using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WX_ConfigService : BaseService<WX_Config>
    {
        private static WX_ConfigService _instance;
        public static WX_ConfigService instance()
        {
            if (_instance == null)
            {
                _instance = new WX_ConfigService();
            }
            return _instance;
        }
        public int Insert(WX_Config entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<WX_Config>(entity);
            });
            return result;
        }
        public int Update(WX_Config entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WX_Config>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_Config>(m => m.ID == id);
            });
            return result;
        }
        public WX_Config Single(Guid id)
        {
            WX_Config result = new WX_Config();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Config>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<WX_Config> GetEnum()
        {
            IEnumerable<WX_Config> result = new List<WX_Config>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Config>();
            });
            return result;
        }
        public WX_Config SingleByCompanyID(Guid CompanyID)
        {
            WX_Config result = new WX_Config();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Config>(m => m.CompanyID == CompanyID);
            });
            return result;
        }
        public WX_Config SingleByOrgID(string orgid)
        {
            WX_Config result = new WX_Config();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Config>(m => m.OrgID == orgid);
            });
            return result;

        }
    }
}
