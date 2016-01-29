using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class WX_MenuService : BaseService<WX_Menu>
    {
        private static WX_MenuService _instance;
        public static WX_MenuService instance()
        {
            if (_instance == null)
            {
                _instance = new WX_MenuService();
            }
            return _instance;
        }
        public int Insert(WX_Menu entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<WX_Menu>(entity);
            });
            return result;
        }
        public int Update(WX_Menu entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<WX_Menu>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<WX_Menu>(m => m.ID == id);
            });
            return result;
        }
        public WX_Menu Single(Guid id)
        {
            WX_Menu result = new WX_Menu();
            _db.Execute(() =>
            {
                result = _db.Single<WX_Menu>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<WX_Menu> GetEnum()
        {
            IEnumerable<WX_Menu> result = new List<WX_Menu>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Menu>();
            });
            return result;
        }
        public IEnumerable<WX_Menu> GetEnumByCompanyID(Guid Companyid)
        {
            IEnumerable<WX_Menu> result = new List<WX_Menu>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Menu>(m => m.CompanyID == Companyid && m.ParentID == Guid.Empty && m.DR == false);
            });
            foreach (var item in result)
            {
                item.Childs = GetChilds(item.ID);
                item.KeyWord = WX_KeyWordService.instance().GetEnumerableByID(item.KeyWordID).FirstOrDefault();
            }
            return result;
        }
        public IEnumerable<WX_Menu> GetChilds(Guid pid)
        {
            IEnumerable<WX_Menu> result = new List<WX_Menu>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Menu>(m => m.ParentID == pid && m.DR == false);
            });
            return result;
        }
        public IEnumerable<WX_Menu> GetTopEnumByCompanyID(Guid pid)
        {
            IEnumerable<WX_Menu> result = new List<WX_Menu>();
            _db.Execute(() =>
            {
                result = _db.GetList<WX_Menu>(m => m.CompanyID == pid && m.ParentID == Guid.Empty && m.DR == false);
            });
            return result;
        }


    }
}
