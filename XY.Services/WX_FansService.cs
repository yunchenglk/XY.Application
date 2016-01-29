using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WX_FansService : BaseService<WX_Fans>
    {
        private static WX_FansService _instance;
        public static WX_FansService instance()
        {
            if (_instance == null)
            {
                _instance = new WX_FansService();
            }
            return _instance;
        }
        public int Insert(WX_Fans entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            if (WX_FansService.instance().GetEnumByCompanyIDAndOpenid(entity.ConpanyID, entity.OPENID) == null)
                _db.Execute(() =>
                {
                    result = _db.Insert<WX_Fans>(entity);
                });
            return result;
        }
        public int Update(WX_Fans entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WX_Fans>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_Fans>(m => m.ID == id);
            });
            return result;
        }
        public WX_Fans Single(Guid id)
        {
            WX_Fans result = new WX_Fans();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Fans>(m => m.ID == id);
            });

            return result;
        }
        public IEnumerable<WX_Fans> GetEnum()
        {
            IEnumerable<WX_Fans> result = new List<WX_Fans>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Fans>();
            });
            return result;
        }
        public IEnumerable<WX_Fans> GetEnumByCompanyID(Guid companyid)
        {
            IEnumerable<WX_Fans> result = new List<WX_Fans>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Fans>(m => m.ConpanyID == companyid);
            });
            return result;
        }
        public WX_Fans GetEnumByCompanyIDAndOpenid(Guid companyid, string openid)
        {
            WX_Fans result = new WX_Fans();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Fans>(m => m.ConpanyID == companyid && m.OPENID == openid);
            });
            return result;
        }


    }
}
