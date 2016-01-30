using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Product_Price
    {
        public string PriceStr
        {
            get
            {
                return Util.Utils.Rmoney(0, Convert.ToDouble(Price));
            }
        }

    }
}
