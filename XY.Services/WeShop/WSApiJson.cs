using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;
using XY.Util;

namespace XY.Services
{
    public static class WSApiJson
    {
        /// <summary>
        /// 新增商品分类
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="classid"></param>
        /// <returns></returns>
        public static string vdian_shop_cate_add(string access_token, Class c)
        {

            List<cates> calist = new List<cates>();
            calist.Add(new cates()
            {
                cate_name = c.Title,
                sort_num = c.Sort
            });
            @public p = new @public("vdian.shop.cate.add", access_token);
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"cates\":" + JsonHelper.SerializeObject(calist));
            str.Append("}&public=");
            str.Append(JsonHelper.SerializeObject(p));
            return str.ToString();
        }


        /// <summary>
        /// 创建微店商品
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string vdian_item_add(product_item entity)
        {
            StringBuilder str = new StringBuilder();
            str.Append(JsonHelper.SerializeObject(entity));
            return str.ToString();
        }


    }
}
