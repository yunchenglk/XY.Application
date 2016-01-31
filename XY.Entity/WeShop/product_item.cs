using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class product_item
    {
        public string item_name { get; set; }
        public List<string> imgs { get; set; }
        public string price { get; set; }
        public string stock { get; set; }
        public string merchant_code { get; set; }
        public List<string> cate_ids { get; set; }
        public List<skus> skus { get; set; }


    }
    public class skus {
        public string title { get; set; }
        public string price { get; set; }
        public string stock { get; set; }
        public string sku_merchant_code { get; set; }

    }
}
