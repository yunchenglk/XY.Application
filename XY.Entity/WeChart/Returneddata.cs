using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class Returneddata<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
        public Returneddata(int draw, int totalcount)
        {
            this.draw = draw;
            this.recordsTotal = totalcount;
            this.recordsFiltered = totalcount;
        }
    }
}
