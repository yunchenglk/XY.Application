using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class wx_userinfoService : BaseService<wx_userinfo>
    {
        private static wx_userinfoService _instance;
        public static wx_userinfoService instance()
        {
            if (_instance == null)
            {
                _instance = new wx_userinfoService();
            }
            return _instance;
        }
        public int Insert(wx_userinfo entity)
        {
            int result = 0;
            if (this.Single(entity.openid, entity.cID) == null)
            {
                entity.ID = Guid.NewGuid();
                entity.CreateTime = DateTime.Now;
                entity.unsubscribe_time = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                _db.Execute(() =>
                {
                    result = _db.Insert<wx_userinfo>(entity);
                });
            }
            else
            {
                this.Update(entity);
            }
            return result;
        }

        public wx_userinfo Single(Guid id)
        {
            wx_userinfo result = new wx_userinfo();
            _db.Execute(() =>
            {
                result = _db.Single<wx_userinfo>(m => m.ID == id);
            });
            return result;
        }
        public wx_userinfo Single(string openid, Guid cid)
        {
            wx_userinfo result = new wx_userinfo();
            _db.Execute(() =>
            {
                result = _db.Single<wx_userinfo>(m => m.openid == openid && m.cID == cid);
            });
            return result;
        }
        public int Update(wx_userinfo entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<wx_userinfo>(entity, m => m.ID == entity.ID);
            });
            return result;
        }

    }
}
