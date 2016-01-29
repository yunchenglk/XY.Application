using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class _post_Product_Attr
    {
        public Guid ProductID { get; set; }
        public Guid Att_key { get; set; }
        public string Att_val { get; set; }
        public Decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
