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
    public class ProductController : ApiController
    {
        //private static readonly string url = System.Web.HttpContext.Current.Request.UrlReferrer.GetLeftPart(UriPartial.Authority);
        //Guid cid = WebSitesService.instance().GetCompanyIDByHTTP(url);
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="wheres">查询条件</param>
        /// <param name="sortorder">倒序/正序</param>
        /// <param name="sortname">排序字段</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int pageIndex, int pageSize, string wheres, string sortorder, string sortname)
        {
            int totalcount = 0;
            IEnumerable<Product> datalist = ProductService.instance().GetPageByDynamic(pageIndex, pageSize, out totalcount, sortname, sortorder, wheres);
            if (datalist.Count() > 0)
            {
                datalist.ToList().ForEach(m =>
                {
                    m.Attr = Product_AttService.GetAttsByPID(m.ID);
                });
            }
            ResultBase_Page page = new ResultBase_Page(pageIndex, pageSize, totalcount);
            page.content = datalist;
            return Util.Utils.ConvertToJson(page);
        }
        /// <summary>
        /// 根据ID获取指定信息
        /// </summary>
        /// <param name="id">信息表ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id)
        {
            Guid ID;
            if (Guid.TryParse(id, out ID))
                return Util.Utils.ConvertToJson(ProductService.instance().Single(ID));
            else
                return null;
        }
        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <param name="type">0:全部信息1:推荐信息/2:置顶信息</param>
        /// <param name="id">栏目ID</param>
        /// <param name="count">获取的总信息数</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int type, string id, int? count)
        {
            Guid CID;
            if (!Guid.TryParse(id, out CID))
                return null;
            IEnumerable<Product> result = new List<Product>();
            switch (type)
            {
                case 1:
                    result = ProductService.instance().GetProductByCid_IsRecommend(CID);
                    break;
                case 2:
                    result = ProductService.instance().GetProductByCid_IsTop(CID);
                    break;
                default:
                    result = ProductService.instance().GetProductByCid(CID);
                    break;
            }
            if (count.HasValue)
                return Util.Utils.ConvertToJson(result.Take(count.Value));
            return Util.Utils.ConvertToJson(result);

        }
    }
}
