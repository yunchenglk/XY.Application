using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class CheckAuthorize
    {
        private static CheckAuthorize _instance;
        public static CheckAuthorize instance()
        {
            if (_instance == null)
            {
                _instance = new CheckAuthorize();
            }
            return _instance;
        }
        public bool CheckedCompanyByUrl(string url,Guid comid)
        {
            Company company = CompanyService.instance().GetCompanyByUrl(url);
            if (company != null) {
                return company.ID.Equals(comid);
            }
            return false;

        }

    }
}
