using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class @public
    {
        public string method { get; set; }
        public string access_token { get; set; }
        public string version { get; set; }
        public string format { get; set; }
        public @public(string method, string access_token)
        {
            this.method = method;
            this.access_token = access_token;
            this.version = "1.0";
            this.format = "json";
        }
    }
}
