using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class FilesService : BaseService<Files>
    {
        private static FilesService _instance;
        public static FilesService instance()
        {
            if (_instance == null)
                _instance = new FilesService();
            return _instance;
        }
        public int Insert(Files entity)
        {
            int result = 0;
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Files>(entity);
            });
            return result;
        }
        public int Update(Files entity)
        {
            int result = 0;
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Update<Files>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Files>(m => m.ID == id);
            });
            return result;
        }
        public int DeleteByRelationID(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Files>(m => m.RelationID == id);
            });
            return result;
        }
        public IEnumerable<Files> GetEnumByID(Guid id)
        {
            IEnumerable<Files> result = new List<Files>();
            _db.Execute(() =>
            {
                result = _db.GetList<Files>(m => m.ID == id);
            });
            return result;
        }
        public Files Single(Guid id)
        {
            Files result = new Files();
            _db.Execute(() =>
            {
                result = _db.Single<Files>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Files> GetFilesByRelationID(Guid id)
        {
            IEnumerable<Files> result = new List<Files>();
            _db.Execute(() =>
            {
                result = _db.GetList<Files>(m => m.RelationID == id).OrderBy(m => m.CreateTime);
            });
            return result;
        }
        public IEnumerable<Files> GetFilesByCompanyID(Guid cid)
        {
            IEnumerable<Files> result = new List<Files>();
            _db.Execute(() =>
            {
                result = _db.GetList<Files>(m => m.CompanyID == cid);
            });
            return result;
        }
    }
}
