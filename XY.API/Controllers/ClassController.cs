using System;
using System.Net.Http;
using System.Web.Http;
using XY.Entity;
using XY.Services;

namespace XY.API.Controllers
{
    [Filters.AuthorizeFilter]
    public class ClassController : ApiController
    {
        /// <summary>
        /// 获取子栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <param name="cid">公司ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id, string cid)
        {
            Guid ID;
            Guid CID;
            if (Guid.TryParse(id, out ID) && Guid.TryParse(cid, out CID))
            {

                return Util.Utils.ConvertToJson(ClassService.instance().GetChildByID(ID, CID));

            }
            return null;
        }
        /// <summary>
        /// 根据ID，获取内容
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id)
        {
            Guid ID = Guid.Empty;
            if (Guid.TryParse(id, out ID))
            {
                return Util.Utils.ConvertToJson(ClassService.instance().Single(ID));
            }
            return null;
        }
    }
}
