using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
   public class P_AttributeValuesService: BaseService<P_AttributeValues>
    {
        #region 基本操作
        private static P_AttributeValuesService _instance;
        public static P_AttributeValuesService instance()
        {
            if (_instance == null)
            {
                _instance = new P_AttributeValuesService();
            }
            return _instance;
        }
        public int Insert(P_AttributeValues entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<P_AttributeValues>(entity);
            });
            return result;
        }
        public int Update(P_AttributeValues entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<P_AttributeValues>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<P_AttributeValues>(m => m.ID == id);
            });
            return result;
        }
        public P_AttributeValues Single(Guid id)
        {
            P_AttributeValues result = new P_AttributeValues();
            _db.Execute(() =>
            {
                result = _db.Single<P_AttributeValues>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<P_AttributeValues> GetEnum()
        {
            IEnumerable<P_AttributeValues> result = new List<P_AttributeValues>();
            _db.Execute(() =>
            {
                result = _db.GetList<P_AttributeValues>();
            });
            return result;
        }
        #endregion
    }
}
