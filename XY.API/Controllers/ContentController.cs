using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XY.Entity;
using XY.Services;

namespace XY.API.Controllers
{
    [Filters.AuthorizeFilter]
    /// <summary>
    /// 获取内容
    /// </summary>
    public class ContentController : ApiController
    {
        /// <summary>
        /// 根据ID，获取内容
        /// </summary>
        /// <param name="id">classID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                return Util.Utils.ConvertToJson(ClassService.instance().Single(ID));
            }
            return null;
        }
    }
}
