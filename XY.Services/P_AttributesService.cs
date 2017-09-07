using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class P_AttributesService : BaseService<P_Attributes>
    {
        #region 基本操作
        private static P_AttributesService _instance;
        public static P_AttributesService instance()
        {
            if (_instance == null)
            {
                _instance = new P_AttributesService();
            }
            return _instance;
        }
        public int Insert(P_Attributes entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<P_Attributes>(entity);
            });
            return result;
        }
        public int Update(P_Attributes entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<P_Attributes>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<P_Attributes>(m => m.ID == id);
            });
            return result;
        }
        public P_Attributes Single(Guid id)
        {
            P_Attributes result = new P_Attributes();
            _db.Execute(() =>
            {
                result = _db.Single<P_Attributes>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<P_Attributes> GetEnum()
        {
            IEnumerable<P_Attributes> result = new List<P_Attributes>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_Attributes>();
            });
            return result;
        }
        #endregion
    }
}
