using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_userweixinService : BaseService<wx_userweixin>
    {
        private static wx_userweixinService _instance;
        public static wx_userweixinService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_userweixinService();
            }
            return _instance;
        }
        public int Insert(wx_userweixin entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<wx_userweixin>(entity);
            });
            return result;
        }
        public int Update(wx_userweixin entity)
        {
            int result = 0;
            entity.ModifyTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Update<wx_userweixin>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<wx_userweixin>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<wx_userweixin> GetEnum()
        {
            IEnumerable<wx_userweixin> result = new List<wx_userweixin>();
            _db.Execute(() =>
            {
                result = _db.GetList<wx_userweixin>();
            });
            return result;
        }
        public wx_userweixin Single(Guid id)
        {
            wx_userweixin result = new wx_userweixin();
            _db.Execute(() =>
            {
                result = _db.Single<wx_userweixin>(m => m.ID == id);
            });
            return result;
        }
        public wx_userweixin SingleByCompanyID(Guid companyid)
        {
            wx_userweixin result = new wx_userweixin();
            _db.Execute(() =>
            {
                result = _db.Single<wx_userweixin>(m => m.CompanyID == companyid);
            });
            return result;
        }
        public wx_userweixin SingleBywxID(string wxid)
        {
            wx_userweixin result = new wx_userweixin();
            _db.Execute(() =>
            {
                result = _db.Single<wx_userweixin>(m => m.wxId == wxid);
            });
            return result;
        }
        public wx_userweixin SingleByAppId(string AppId)
        {
            wx_userweixin result = new wx_userweixin();
            _db.Execute(() =>
            {
                result = _db.Single<wx_userweixin>(m => m.AppId == AppId);
            });
            return result;
        }
    }
}
