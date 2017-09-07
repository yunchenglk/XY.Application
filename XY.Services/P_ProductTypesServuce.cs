using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class P_ProductTypesServuce : BaseService<P_ProductTypes>
    {
        #region 基本操作
        private static P_ProductTypesServuce _instance;
        public static P_ProductTypesServuce instance()
        {
            if (_instance == null)
            {
                _instance = new P_ProductTypesServuce();
            }
            return _instance;
        }
        public int Insert(P_ProductTypes entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<P_ProductTypes>(entity);
            });
            return result;
        }
        public int Update(P_ProductTypes entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<P_ProductTypes>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<P_ProductTypes>(m => m.ID == id);
            });
            return result;
        }
        public P_ProductTypes Single(Guid id)
        {
            P_ProductTypes result = new P_ProductTypes();
            _db.Execute(() =>
            {
                result = _db.Single<P_ProductTypes>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<P_ProductTypes> GetEnum()
        {
            IEnumerable<P_ProductTypes> result = new List<P_ProductTypes>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_ProductTypes>();
            });
            return result;
        }
        #endregion
    }
}
