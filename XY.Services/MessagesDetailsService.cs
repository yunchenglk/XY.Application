using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class MessagesDetailsService : BaseService<MessagesDetails>
    {
        private static MessagesDetailsService _instance;
        public static MessagesDetailsService instance()
        {
            if (_instance == null)
            {
                _instance = new MessagesDetailsService();
            }
            return _instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid">CompanyID</param>
        /// <returns></returns>
        public IEnumerable<MessagesDetails> GetEnum(Guid cid)
        {
            IEnumerable<MessagesDetails> result = new List<MessagesDetails>();
            _db.Execute(() =>
            {
                result = _db.GetList<MessagesDetails>(m => m.CompanyID == cid);
            });
            return result;
        }
    }
}
