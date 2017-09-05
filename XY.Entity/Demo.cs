using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunChengLK.Framework.Data;

namespace XY.Entity
{
    public class Demo : IEntity
    {

        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
