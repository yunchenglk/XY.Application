using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class Product_AttService
    {
        public static IEnumerable<ProductAttr> GetAttsByPID(Guid pid)
        {
            List<ProductAttr> attrs = new List<ProductAttr>();
            foreach (var item in Product_Att_ValService.instance().GetEnumByProductID(pid))
            {
                ProductAttr pa = new ProductAttr();
                pa.key = Product_Att_KeyService.instance().Single(item.Att_Key_ID);
                pa.val = item;
                pa.price = Product_PriceService.instance().GetEnumByKVP(pid, item.Att_Key_ID, item.ID);
                attrs.Add(pa);
            }
            return attrs;

        }

    }
}
