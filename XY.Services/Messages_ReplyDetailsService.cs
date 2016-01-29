using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Messages_ReplyDetailsService : BaseService<Messages_ReplyDetails>
    {
        private static Messages_ReplyDetailsService _instance;
        public static Messages_ReplyDetailsService instance()
        {
            if (_instance == null)
            {
                _instance = new Messages_ReplyDetailsService();
            }
            return _instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        public IEnumerable<Messages_ReplyDetails> GetChilds(Guid pid, Guid mid)
        {
            IEnumerable<Messages_ReplyDetails> result = new List<Messages_ReplyDetails>();
            _db.Execute(() =>
            {
                result = _db.GetList<Messages_ReplyDetails>(m => m.ParentID == pid && m.MessageID == mid);
            });
            return result;
        }
    }
}
