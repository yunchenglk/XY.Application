using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class CompanyService : BaseService<Company>
    {
        public IEnumerable<string> URLAll;
        private static CompanyService _instance;
        public static CompanyService instance()
        {
            if (_instance == null)
            {
                _instance = new CompanyService();
            }
            return _instance;
        }
        public CompanyService()
        {
            _db.Execute(() =>
            {
                URLAll = _db.GetList<Company>(m => m.DR == false).Select(m => m.URL);
            });
        }
        public int Insert(Company entity)
        {
            int result = 0;
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            _db.Execute(() =>
            {
                result = _db.Insert<Company>(entity);
            });
            return result;
        }
        public int Update(Company entity)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Update<Company>(entity, m => m.ID == entity.ID);
            });
            return result;
        }
        public int Delete(Guid id)
        {
            int result = 0;
            _db.Execute(() =>
            {
                result = _db.Delete<Company>(m => m.ID == id);
            });
            return result;
        }
        public Company Single(Guid id)
        {
            Company result = new Company();
            _db.Execute(() =>
            {
                result = _db.Single<Company>(m => m.ID == id);
            });
            return result;
        }
        public IEnumerable<Company> GetEnum()
        {
            IEnumerable<Company> result = new List<Company>();
            _db.Execute(() =>
            {
                result = _db.GetList<Company>();
            });
            return result;
        }
        public IEnumerable<Company> GetEnumByID(Guid id)
        {
            IEnumerable<Company> result = new List<Company>();
            _db.Execute(() =>
            {
                result = _db.GetList<Company>(m => m.ID == id);
            });
            return result;
        }
        public string GetNameByID(Guid id)
        {
            string result = "";
            if (id.Equals(Guid.Empty))
                return "管理员";
            _db.Execute(() =>
            {
                result = _db.GetList<Company>(m => m.ID == id).FirstOrDefault().Name;
            });
            return result;
        }
        public Company GetCompanyByUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = url.ToLower().Replace("http://", "");
            }
            Company result = new Company();
            _db.Execute(() =>
            {
                result = _db.GetList<Company>(m => m.URL.Contains(url)).FirstOrDefault();
            });
            return result;
        }
    }
}
