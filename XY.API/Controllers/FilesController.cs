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
    /// <summary>
    /// 获取文件
    /// </summary>
    public class FilesController : ApiController
    {
        /// <summary>
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
            {
                return Util.Utils.ConvertToJson(FilesService.instance().Single(ID));
            }
            return null;
        }
        /// <summary>
        /// 获取RelationID的文件信息
        /// </summary>
        /// <param name="RelationID">关联ID</param>
        /// <returns></returns>
        public HttpResponseMessage GetRelationFiles(string RelationID)
        {
            Guid ID;
            if (Guid.TryParse(RelationID, out ID))
            {
                return Util.Utils.ConvertToJson(FilesService.instance().GetFilesByRelationID(ID));
            }
            return null;
        }
    }
}
