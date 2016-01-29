using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class button
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string key { get; set; }
        public List<sub_button> sub_button { get; set; }
    }
}
